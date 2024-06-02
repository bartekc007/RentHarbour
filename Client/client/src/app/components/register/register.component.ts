import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { HttpResponseModel, User, UserRegisterRequest } from 'src/app/_models/models';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  registerForm!: FormGroup;
  user!: UserRegisterRequest;

  constructor(private formBuilder: FormBuilder, private accountService: AccountService, private router: Router) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm(): void {
    this.registerForm = this.formBuilder.group({
      username: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      phoneNumber: ['', [Validators.pattern('[0-9]{9,14}')]],
      dateOfBirth: ['', Validators.required],
      address: ['', Validators.required],
      city: ['', Validators.required],
      country: ['', Validators.required]
    });
  }

  register(): void {
    if (this.registerForm.valid) {
      this.user = this.registerForm.value;
      this.accountService.register(this.user).subscribe(
        (response: HttpResponseModel<User>) => {
          if (response.data.username) {
            this.router.navigate(['']);
          }
        },
        (error) => {
          console.error('Registration failed:', error);
        }
      );
    }
  }

  cancel(): void {
    // obsłuż anulowanie
  }
}