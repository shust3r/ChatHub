import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IChat } from '../Interfaces/IChat';
import { IMessage } from '../Interfaces/IMessage';

@Injectable({
  providedIn: 'root'
})
export class DatabaseService {
  private readonly api: string = "http://localhost:5087/chats/";

  public chatId: number;

  constructor(private http: HttpClient) { }

  getChats(): Observable<IChat[]> {
    return this.http.get<IChat[]>(`${this.api}`);
  }

  getMessages(): Observable<IMessage[]> {
    return this.http.get<IMessage[]>(`${this.api}${this.chatId}`);
  }

  deleteChat(chatId: number) {
    this.http.delete(`${this.api}${chatId}`).subscribe();
    window.location.reload();
  }
}
