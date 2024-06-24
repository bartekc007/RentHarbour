import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {}
  loggedIn = false;

  constructor(public accountService: AccountService, private router: Router) {}

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
        this.accountService.setCurrentUser(response.data);
      },
      error: error => console.log(error)
    })
  }

  logout(){
    this.accountService.logout();
    this.loggedIn = false;
  }

  register() {
    this.router.navigate(['register']);
  }
}
