import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { HttpResponseModel, UpdateFollowedPropertiesModel } from '../_models/models';
import { Observable } from 'rxjs';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})
export class PropertyService {
  
  baseUrl = "http://localhost:8001/api/";

  constructor(private http: HttpClient, private accountService: AccountService) { }

  followProperty(model: UpdateFollowedPropertiesModel): Observable<HttpResponseModel<boolean>> {
    let currentToken: string = "";
    this.accountService.getCurrentUserToken().subscribe(token => {
      if(token != null)
        currentToken = token;
    });
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${currentToken}`
    });
    return this.http.post<HttpResponseModel<boolean>>(this.baseUrl + 'Followed/update', model, {headers});
   }
}
