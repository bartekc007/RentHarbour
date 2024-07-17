import { Component, OnInit } from '@angular/core';
import { RentalOffer } from 'src/app/_models/models';
import { RentalService } from 'src/app/_services/rental.service';

@Component({
  selector: 'app-my-offer-list',
  templateUrl: './my-offer-list.component.html',
  styleUrls: ['./my-offer-list.component.css']
})
export class MyOfferListComponent  implements OnInit {

  rentalOffers!: RentalOffer[]; 
  selectedOffer!: RentalOffer ;
  
  constructor(private rentalService: RentalService) {}
  
    ngOnInit(): void {
      this.rentalService.getMyRentalOffers().subscribe(response => {
        this.rentalOffers = response.data;
      })
    }
  
    onSelectOffer(offer: RentalOffer): void {
      this.selectedOffer = offer;
    }
  }
