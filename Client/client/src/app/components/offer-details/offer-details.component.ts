import { Component, OnInit } from '@angular/core';
import { RentalOffer } from 'src/app/_models/models';

@Component({
  selector: 'app-offer-details',
  templateUrl: './offer-details.component.html',
  styleUrls: ['./offer-details.component.css']
})
export class OfferDetailsComponent implements OnInit{
  
  offer!: RentalOffer;

  constructor() {  }

  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }
 
}
