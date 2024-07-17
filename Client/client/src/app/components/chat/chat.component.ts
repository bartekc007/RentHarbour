import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Message, Chat } from 'src/app/_models/models';
import { ChatService } from 'src/app/_services/chat.service';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {
  userId!: string; // Id bieżącego użytkownika
  chatId!: string; // Id czatu
  messages: Message[] = [];
  newMessage: string = '';

  constructor(
    private route: ActivatedRoute,
    private chatService: ChatService,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.userId = ''; // Pobierz Id bieżącego użytkownika z sesji/logowania
    this.chatId = this.route.snapshot.paramMap.get('chatId') || '';
    this.loadMessages();
  }

  loadMessages() {
    this.chatService.getMessages(this.chatId).subscribe(
      messages => {
        this.messages = messages;
      },
      error => {
        console.error('Error loading messages:', error);
        this.toastr.error('Failed to load messages.', 'Error');
      }
    );
  }

  sendMessage() {
    if (!this.newMessage.trim()) {
      return;
    }

    const message: Message = {
      id: '',
      chatId: this.chatId,
      senderId: '', // Zostanie uzupełnione po stronie serwera
      recipientId: '', // Zostanie uzupełnione po stronie serwera
      content: this.newMessage,
      sentAt: new Date()
    };

    this.chatService.sendMessage(message).subscribe(
      () => {
        this.toastr.success('Message sent successfully.');
        this.loadMessages(); // Odśwież wiadomości po wysłaniu
        this.newMessage = ''; // Wyczyść pole tekstowe
      },
      error => {
        console.error('Error sending message:', error);
        this.toastr.error('Failed to send message.', 'Error');
      }
    );
  }
}
