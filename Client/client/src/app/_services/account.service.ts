import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { HttpResponseModel, User } from '../_models/models';
import { ReplaySubject } from 'rxjs/internal/ReplaySubject';
import { Observable, map, take } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = "http://localhost:8002/api/";
  private currentUserSource = new ReplaySubject<User | null>(1);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) {}

   login(model: any) {
    return this.http.post(this.baseUrl + 'User/login', model).pipe(
      map((response: any) => {
        const user = response.data;
        if(user) {
          this.setCurrentUser(user);
        }
      })
    )
   }

   setCurrentUser(user: User) {
    localStorage.setItem('user',JSON.stringify(user));
    this.currentUserSource.next(user);
  }

  getCurrentUserToken(): Observable<string | null> {
    return this.currentUser$.pipe(
      take(1),
      map(user => user?.accessToken || null)
    );
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }
}
