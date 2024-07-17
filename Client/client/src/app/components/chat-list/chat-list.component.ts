import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Chat } from 'src/app/_models/models';
import { ChatService } from 'src/app/_services/chat.service';

@Component({
  selector: 'app-chat-list',
  templateUrl: './chat-list.component.html',
  styleUrls: ['./chat-list.component.css']
})
export class ChatListComponent implements OnInit {
  chats: Chat[] = [];

  constructor(private chatService: ChatService, private router: Router) { }

  ngOnInit(): void {
    this.loadChats();
  }

  loadChats() {
    this.chatService.getChatsForUser().subscribe(
      chats => {
        this.chats = chats;
      },
      error => {
        console.error('Error loading chats:', error);
        // Handle error appropriately (e.g., show error message)
      }
    );
  }

  openChat(chat: Chat) {
    this.router.navigate(['/chat', chat.id]); // Navigate to chat component with chat ID
  }
}
