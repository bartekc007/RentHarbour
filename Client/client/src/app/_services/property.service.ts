import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PropertyService {
  baseUrl = "http://localhost:8002/api/";

  constructor(private http: HttpClient) { }
}
