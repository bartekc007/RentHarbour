import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AcceptRentalRequest, HttpResponseModel, RentalOffer, RentalRequest } from '../_models/models';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})
export class RentalService {

  baseUrl = "https://localhost:9005/api/";

  constructor(private http: HttpClient, private accountService: AccountService) { }

  createRentalRequest(model: RentalRequest): Observable<HttpResponseModel<boolean>> {
    let currentToken: string = "";
    this.accountService.getCurrentUserToken().subscribe(token => {
      if(token != null)
        currentToken = token;
    });
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${currentToken}`
    });
    return this.http.post<HttpResponseModel<boolean>>(this.baseUrl + 'RentalRequests/create',model, {headers})
  }

  getRentalOffers(): Observable<HttpResponseModel<RentalOffer[]>>{
    let currentToken: string = "";
    this.accountService.getCurrentUserToken().subscribe(token => {
      if(token != null)
        currentToken = token;
    });
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${currentToken}`
    });
    return this.http.get<HttpResponseModel<RentalOffer[]>>(this.baseUrl + 'RentalRequests',{headers});
  }

  getRentalOffer(offerId: string): Observable<HttpResponseModel<RentalOffer>> {
    let currentToken: string = "";
    this.accountService.getCurrentUserToken().subscribe(token => {
      if(token != null)
        currentToken = token;
    });
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${currentToken}`
    });
    return this.http.get<HttpResponseModel<RentalOffer>>(this.baseUrl + `RentalRequests/${offerId}`, {headers});
  }

  acceptOffer(model: AcceptRentalRequest): Observable<HttpResponseModel<boolean>> {
    let currentToken: string = "";
    this.accountService.getCurrentUserToken().subscribe(token => {
      if(token != null)
        currentToken = token;
    });

    const headers = new HttpHeaders({
      'Authorization': `Bearer ${currentToken}`,
      'Content-Type': 'application/json'
    });

    return this.http.post<HttpResponseModel<boolean>>(`${this.baseUrl}RentalRequests/AcceptRentalRequest`, model, { headers });
  }

  getRentalRequestsByUserId(): Observable<HttpResponseModel<RentalRequest[]>> {
    let currentToken: string = "";
    this.accountService.getCurrentUserToken().subscribe(token => {
      if(token != null)
        currentToken = token;
    });

    const headers = new HttpHeaders({
      'Authorization': `Bearer ${currentToken}`,
      'Content-Type': 'application/json'
    });

    return this.http.get<HttpResponseModel<RentalRequest[]>>(this.baseUrl + 'RentalRequests/GetRentalRequestsByUserId', { headers })   
  }

}
