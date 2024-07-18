import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AccountService } from './account.service';
import { Chat, ChatRequest, Message } from '../_models/models';
import { SignalRService } from './signal-r.service';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  private baseUrl = 'https://localhost:9003/api/';

  constructor(private http: HttpClient, private accountService: AccountService, private signalRService: SignalRService) { }

  sendMessage(message: Message): Observable<any> {
    let currentToken: string = "";
    this.accountService.getCurrentUserToken().subscribe(token => {
      if(token != null)
        currentToken = token;
    });
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${currentToken}`
    });

    const url = `${this.baseUrl}chat/send`;
    return this.http.post<any>(url, message, { headers });
  }

  getMessages(chatId: string): Observable<Message[]> {
    let currentToken: string = "";
    this.accountService.getCurrentUserToken().subscribe(token => {
      if(token != null)
        currentToken = token;
    });
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${currentToken}`
    });

    const url = `${this.baseUrl}chat/messages?chatId=${chatId}`;
    return this.http.get<Message[]>(url, { headers });
  }

  getChatsForUser(): Observable<Chat[]> {
    let currentToken: string = "";
    this.accountService.getCurrentUserToken().subscribe(token => {
      if(token != null)
        currentToken = token;
    });
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${currentToken}`
    });

    const url = `${this.baseUrl}chat/GetChatsForUser`;
    return this.http.get<Chat[]>(url, { headers });
  }

  getChat(chatId: string): Observable<Chat> {
    let currentToken: string = "";
    this.accountService.getCurrentUserToken().subscribe(token => {
      if(token != null)
        currentToken = token;
    });
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${currentToken}`
    });

    const url = `${this.baseUrl}chat/${chatId}`;
    return this.http.get<Chat>(url, { headers });
  }

  createChat(offerId: number): Observable<any> {
    let currentToken: string = "";
    this.accountService.getCurrentUserToken().subscribe(token => {
      if(token != null)
        currentToken = token;
    });
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${currentToken}`
    });
    let model: ChatRequest = {
      offerId: offerId
    }
    const url = `${this.baseUrl}chat/CreateChat`;
    return this.http.post<any>(url,model, { headers });
  }

  notifyServer(chatId: string, senderId: string, message: string) {
    this.signalRService.sendMessage(chatId, message);
  }
}
