import { AfterViewChecked, Component, ElementRef, inject, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { ChatService } from '../Services/chat.service';
import { IMessage } from '../Interfaces/IMessage';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.css'
})
export class ChatComponent implements OnInit, AfterViewChecked {
  chatSvc = inject(ChatService);
  inputMessage = "";
  messages: IMessage[] = [];
  router = inject(Router);
  loggedInUserName = sessionStorage.getItem("user");
  roomName = sessionStorage.getItem("room");

  @ViewChild('scrollMe') private scrollContainer!: ElementRef;

  ngOnInit(): void {
    this.chatSvc.messages$.subscribe(response =>{
      this.messages = response;
      console.log(this.messages);
    });
  }

  ngAfterViewChecked(): void {
    this.scrollContainer.nativeElement.scrollTop = this.scrollContainer.nativeElement.scrollHeight;
  }

  sendMessage(){
    this.chatSvc.sendMessage(this.inputMessage)
    .then(()=>{
      this.inputMessage='';
    }).catch((ex)=>{
      console.log(ex);
    })
  }

  leaveRoom(){
    this.chatSvc.leaveRoom()
    .then(()=>{
      this.router.navigate(['welcome']);
      setTimeout(()=>{
        location.reload();
      }, 0);
    }).catch((ex)=>{
      console.log(ex);
    })
  }
}
