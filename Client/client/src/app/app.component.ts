import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Property } from './_models/models';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'client';
  properties: Array<Property> | any;

  constructor(private http: HttpClient){}

  ngOnInit(): void {
    this.http.get('http://localhost:8000/api/Property/all').subscribe({
      next: response  => this.properties = response,
      error: error => console.log(error),
      complete: () => console.log('Request has completed')
    })
  }

}
