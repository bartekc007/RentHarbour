import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {}
  loggedIn = false;

  constructor(public accountService: AccountService) {}

  ngOnInit(): void {
    this.accountService.getCurrentUserToken().subscribe(data=>{
      if(data!=null)
        this.loggedIn = true
    })
  }

  login() {
    this.accountService.login(this.model).subscribe({
      next: response => {
        console.log(response);
        this.loggedIn = true;
      },
      error: error => console.log(error)
    })
  }

  logout(){
    this.accountService.logout();
    this.loggedIn = false;
  }

}
