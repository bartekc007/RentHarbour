import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AccountService } from './account.service';
import { RentalDocumentRequest } from '../_models/models';

@Injectable({
  providedIn: 'root'
})
export class DocumentService {

  baseUrl = "https://localhost:9005/api/";

  constructor(private http: HttpClient, private accountService: AccountService) { }

  uploadDocument(data: RentalDocumentRequest): Observable<any> {

    let currentToken: string = "";
    this.accountService.getCurrentUserToken().subscribe(token => {
      if(token != null)
        currentToken = token;
    });
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${currentToken}`
    });

    const url = `${this.baseUrl}/rentalDocument/UploadDocument`;
    return this.http.post<any>(url, data, {headers});
  }
}
