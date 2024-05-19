import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpResponseModel, PropertyDto } from '../_models/models';

@Injectable({
  providedIn: 'root'
})
export class CatalogService {
  baseUrl = "http://localhost:8000/api/";

  constructor(private http: HttpClient) { }

  getAll(): Observable<HttpResponseModel<PropertyDto[]>> {
    return this.http.get<HttpResponseModel<PropertyDto[]>>(this.baseUrl + 'Property/all');
  }

  getById(propertyId: string): Observable<PropertyDto>  {
    return this.http.get<PropertyDto>(this.baseUrl + 'Property/' + propertyId);
  }

}
