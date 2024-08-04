import { Component, OnInit } from '@angular/core';
import { DatabaseService } from '../Services/database.service';
import { IMessage } from '../Interfaces/IMessage';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrl: './messages.component.css'
})
export class MessagesComponent implements OnInit{
  messages: IMessage[];

  constructor(private dbSvc: DatabaseService) {}

  ngOnInit(): void {
    this.dbSvc.getMessages().subscribe(r => {
      this.messages = r;
    });
  }
}
