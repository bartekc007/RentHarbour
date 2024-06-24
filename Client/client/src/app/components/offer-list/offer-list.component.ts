import { Component, OnInit } from '@angular/core';
import { RentalOffer } from 'src/app/_models/models';
import { RentalService } from 'src/app/_services/rental.service';

@Component({
  selector: 'app-offer-list',
  templateUrl: './offer-list.component.html',
  styleUrls: ['./offer-list.component.css']
})
export class OfferListComponent implements OnInit {

rentalOffers!: RentalOffer[]; 
selectedOffer!: RentalOffer ;

constructor(private rentalService: RentalService) {}

  ngOnInit(): void {
    this.rentalService.getRentalOffers().subscribe(response => {
      this.rentalOffers = response.data;
    })
  }

  onSelectOffer(offer: RentalOffer): void {
    this.selectedOffer = offer;
  }
}
