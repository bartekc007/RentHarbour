import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { GetUserNameResponse, HttpResponseModel, User } from '../_models/models';
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

  login(model: any): Observable<HttpResponseModel<User>> {
    return this.http.post<HttpResponseModel<User>>(this.baseUrl + 'User/login', model);
  }

   register(model: any): Observable<HttpResponseModel<User>> {
    return this.http.post<HttpResponseModel<User>>(this.baseUrl + 'User/register', model);
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

  getUserNameByToken(): Observable<GetUserNameResponse>{
    let currentToken: string = "";
    this.getCurrentUserToken().subscribe(token => {
      if(token != null)
        currentToken = token;
    });
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${currentToken}`
    });
    return this.http.get<GetUserNameResponse>(this.baseUrl + 'User/userNameByToken',{headers});
  }
}
