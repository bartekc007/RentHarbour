import { Component, OnInit } from '@angular/core';
import { User } from './_models/models';
import { AccountService } from './_services/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'The Dating App';
  properties: any;

  constructor(private accountService: AccountService, private toastr: ToastrService) {}

  ngOnInit() {
    this.setCurrentUser();
  }

  setCurrentUser() {
    const userData = localStorage.getItem('user');
    const user: User = userData ? JSON.parse(userData) : null;
    this.accountService.setCurrentUser(user);
  }

  showSuccess(message: string) {
    this.toastr.success(message, 'Success');
  }

  showError(message: string) {
    this.toastr.error(message, 'Błąd');
  }

  showInfo(message: string) {
    this.toastr.info(message, 'Info');
  }

  showWarning(message: string) {
    this.toastr.warning(message, 'Ostrzeżenie');
  }

}
