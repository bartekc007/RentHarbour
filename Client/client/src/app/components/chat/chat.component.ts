import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Message } from 'src/app/_models/models';
import { AccountService } from 'src/app/_services/account.service';
import { ChatService } from 'src/app/_services/chat.service';
import { SignalRService } from 'src/app/_services/signal-r.service';


@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit, OnDestroy {
  chatId!: string; // Id czatu
  messages: Message[] = [];
  newMessage: string = '';
  userName!: string;

  constructor(
    private route: ActivatedRoute,
    private chatService: ChatService,
    private toastr: ToastrService,
    private signalRService: SignalRService,
    private accountService: AccountService
  ) { }

  ngOnInit(): void {
    this.accountService.getUserNameByToken().subscribe(response => {
      this.userName = response.userName;
    });
    this.chatId = this.route.snapshot.paramMap.get('chatId') || '';
    this.loadMessages();
    this.signalRService.startConnection(this.chatId);
    this.signalRService.addTransferMessageDataListener((chatId, senderId, recipientId, senderName, recipientName, message) => {
      if (chatId === this.chatId) {
        this.messages.push({ 
          id: '', 
          chatId, 
          senderId, 
          recipientId, 
          senderName, 
          recipientName, 
          content: message, 
          sentAt: new Date() 
        });
      }
    });
  }

  ngOnDestroy(): void {
    this.signalRService.stopConnection();
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

    this.signalRService.sendMessage(this.chatId, this.newMessage);
    this.newMessage = ''; // Wyczyść pole tekstowe
  }
}
