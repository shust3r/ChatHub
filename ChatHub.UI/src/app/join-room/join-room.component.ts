import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ChatService } from '../chat.service';

@Component({
  selector: 'app-join-room',
  templateUrl: './join-room.component.html',
  styleUrl: './join-room.component.css'
})
export class JoinRoomComponent implements OnInit {
  joinRoomForm!: FormGroup;
  fb = inject(FormBuilder);
  router = inject(Router);
  chatSvc = inject(ChatService);

  ngOnInit(): void {
    this.joinRoomForm = this.fb.group({
      user: ['', Validators.required],
      room: ['', Validators.required]
    })
  }

  joinRoom(){
    const {user, room} = this.joinRoomForm.value;
    sessionStorage.setItem("user", user);
    sessionStorage.setItem("room", room);
    this.chatSvc.joinRoom(user, room)
    .then(()=>{
      this.router.navigate(['chat']);
    }).catch((ex)=>{
      console.log(ex);
    })
  }
}