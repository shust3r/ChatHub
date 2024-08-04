import { Component, OnInit } from '@angular/core';
import { DatabaseService } from '../Services/database.service';
import { IChat } from '../Interfaces/IChat';

@Component({
  selector: 'app-chat-list',
  templateUrl: './chat-list.component.html',
  styleUrl: './chat-list.component.css'
})
export class ChatListComponent implements OnInit {
  chats: IChat[];

  constructor(private dbSvc: DatabaseService) {}

  ngOnInit(): void {
    this.dbSvc.getChats().subscribe(r => {
      this.chats = r;
    });
  }

  openClick(chatId: string)
  {
    this.dbSvc.chatId = +chatId;
  }

  deleteChat(chatId: string)
  {
    this.dbSvc.deleteChat(+chatId);
  }
}
