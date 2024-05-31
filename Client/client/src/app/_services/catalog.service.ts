import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpResponseModel, PropertiesGetAllRequest, PropertyDto } from '../_models/models';

@Injectable({
  providedIn: 'root'
})
export class CatalogService {
  baseUrl = "http://localhost:8000/api/";

  constructor(private http: HttpClient) { }

  getAll(request: PropertiesGetAllRequest): Observable<HttpResponseModel<PropertyDto[]>> {
    let params = new HttpParams();

    if (request.priceMax != null) {
      params = params.set('PriceMax', request.priceMax.toString());
    }
    if (request.priceMin != null) {
      params = params.set('PriceMin', request.priceMin.toString());
    }
    if (request.bedroomsMax != null) {
      params = params.set('BedroomsMax', request.bedroomsMax.toString());
    }
    if (request.bedroomsMin != null) {
      params = params.set('BedroomsMin', request.bedroomsMin.toString());
    }
    if (request.bathroomsMax != null) {
      params = params.set('BathroomsMax', request.bathroomsMax.toString());
    }
    if (request.bathroomsMin != null) {
      params = params.set('BathroomsMin', request.bathroomsMin.toString());
    }
    if (request.areaSquareMetersMax != null) {
      params = params.set('AreaSquareMetersMax', request.areaSquareMetersMax.toString());
    }
    if (request.areaSquareMetersMin != null) {
      params = params.set('AreaSquareMetersMin', request.areaSquareMetersMin.toString());
    }
    if (request.city) {
      params = params.set('City', request.city);
    }

    return this.http.get<HttpResponseModel<PropertyDto[]>>(this.baseUrl + 'Property/all', { params: params });
  }

  getById(propertyId: string): Observable<HttpResponseModel<PropertyDto>>  {
    return this.http.get<HttpResponseModel<PropertyDto>>(this.baseUrl + 'Property/' + propertyId);
  }

}
