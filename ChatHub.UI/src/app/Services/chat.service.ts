import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ChatService {

  public connection : signalR.HubConnection = new signalR.HubConnectionBuilder()
  .withUrl("https://simplechatapi-f8ffdyevfha5bwd3.polandcentral-01.azurewebsites.net/chat/")
  .configureLogging(signalR.LogLevel.Information)
  .build();

  public messages$ = new BehaviorSubject<any>([]);
  public connectedUsers$ = new BehaviorSubject<string[]>([]);
  public messages: any[] = [];
  public users: string[] = [];

  constructor() {
    this.start();
    this.connection.on("ReceiveMessage", (UserName: string, message: string, messageTime: string)=>{
      this.messages = [...this.messages, {UserName, message, messageTime}];
      this.messages$.next(this.messages);
    });

    this.connection.on("ConnectedUser", (users: any)=>{
      this.connectedUsers$.next(users);
    });
  }

  public async start(){
    try {
      await this.connection.start().catch(function (e){});
    } catch (error) {
      console.log(error);
    }
  }

  public async joinRoom(UserName: string, RoomName: string){
    return this.connection.invoke("JoinRoom", {UserName, RoomName});
  }

  public async sendMessage(message: string){
    this.connection.invoke("SendMessage", message);
  }

  public async leaveRoom(){
    return this.connection.stop().catch(function (e){});
  }
}
