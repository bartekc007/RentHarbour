import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpResponseModel, Payment } from '../_models/models';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {

  baseUrl = "https://localhost:9005/api/";

  constructor(private http: HttpClient, private accountService: AccountService) { }

  getRentalOffers(propertyId: string): Observable<HttpResponseModel<Payment[]>>{
    let currentToken: string = "";
    this.accountService.getCurrentUserToken().subscribe(token => {
      if(token != null)
        currentToken = token;
    });
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${currentToken}`
    });
    return this.http.get<HttpResponseModel<Payment[]>>(this.baseUrl + `Payment/${propertyId}`,{headers});
  }
}
