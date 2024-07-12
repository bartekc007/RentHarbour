import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AccountService } from './account.service';
import { HttpResponseModel, RentalDocument, RentalDocumentRequest } from '../_models/models';

@Injectable({
  providedIn: 'root'
})
export class DocumentService {
  baseUrl = "https://localhost:9004/api/";

  constructor(private http: HttpClient, private accountService: AccountService) { }

  uploadDocument(data: RentalDocumentRequest): Observable<any> {
    let currentToken: string = "";
    this.accountService.getCurrentUserToken().subscribe(token => {
      if (token != null) {
        currentToken = token;
      }
    });
    
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${currentToken}`
    });

    const formData: FormData = new FormData();
    formData.append('OfferId', data.offerId);
    formData.append('File', data.file);

    const url = `${this.baseUrl}OfferDocuments/UploadDocument`;
    return this.http.post<any>(url, formData, { headers });
  }

  getDocumentsByOfferId(offerId: string): Observable<HttpResponseModel<RentalDocument[]>> {
    let currentToken: string = "";
    this.accountService.getCurrentUserToken().subscribe(token => {
      if (token != null) {
        currentToken = token;
      }
    });

    const headers = new HttpHeaders({
      'Authorization': `Bearer ${currentToken}`
    });

    const url = `${this.baseUrl}OfferDocuments/${offerId}/documents`;
    return this.http.get<HttpResponseModel<RentalDocument[]>>(url, { headers });
  }

  downloadDocument(documentId: string): Observable<Blob> {
    let currentToken: string = "";
    this.accountService.getCurrentUserToken().subscribe(token => {
      if (token != null) {
        currentToken = token;
      }
    });

    const headers = new HttpHeaders({
      'Authorization': `Bearer ${currentToken}`
    });

    const url = `${this.baseUrl}OfferDocuments/DownloadDocument/${documentId}`;
    return this.http.get(url, { headers, responseType: 'blob' });
  }
}
