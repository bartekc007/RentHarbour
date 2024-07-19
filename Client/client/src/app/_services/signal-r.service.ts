import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { AccountService } from './account.service';


@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private hubConnection: signalR.HubConnection | undefined;
  private baseUrl = 'https://localhost:9003';
  constructor(private accountService: AccountService) {}

  public startConnection(chatId: string) {
    let currentToken: string = "";
    this.accountService.getCurrentUserToken().subscribe(token => {
      if(token != null)
        currentToken = token;
    });
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${this.baseUrl}/chatHub?chatId=${chatId}`, {
        accessTokenFactory: () => currentToken,
        withCredentials: true
      })
      .withAutomaticReconnect()
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err));
  }

  public stopConnection() {
    if (this.hubConnection) {
      this.hubConnection.stop()
        .then(() => console.log('Connection stopped'))
        .catch(err => console.log('Error while stopping connection: ' + err));
    }
  }

  public addTransferMessageDataListener(callback: (chatId: string, senderId: string, recipientId: string, senderName: string, recipientName: string, message: string) => void) {
    this.hubConnection?.on('ReceiveMessage', (chatId: string, senderId: string, recipientId: string, senderName: string, recipientName: string, message: string) => {
      callback(chatId, senderId, recipientId, senderName, recipientName, message);
    });
  }


  public async sendMessage(chatId: string, message: string) {
    let currentToken: string = "";
    this.accountService.getCurrentUserToken().subscribe(token => {
      if(token != null)
        currentToken = token;
    });
    if (this.hubConnection?.state === signalR.HubConnectionState.Connected) {
      try {
        await this.hubConnection.invoke('SendMessage', chatId, message,currentToken);
      } catch (err) {
        console.error('Error sending message: ' + err);
      }
    } else {
      console.error('Cannot send data if the connection is not in the "Connected" State.');
    }
  }
}
