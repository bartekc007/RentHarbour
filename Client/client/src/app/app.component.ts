import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Property, User } from './_models/models';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'The Dating App';
  properties: any;

  constructor(private accountService: AccountService) {}

  ngOnInit() {
    this.setCurrentUser();
  }

  setCurrentUser() {
    const userData = localStorage.getItem('user');
    const user: User = userData ? JSON.parse(userData) : null;
    this.accountService.setCurrentUser(user);
  }

}
