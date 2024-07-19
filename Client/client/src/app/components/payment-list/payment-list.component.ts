import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Payment, PropertyDto } from 'src/app/_models/models';
import { CatalogService } from 'src/app/_services/catalog.service';
import { PaymentService } from 'src/app/_services/payment.service';

@Component({
  selector: 'app-payment-list',
  templateUrl: './payment-list.component.html',
  styleUrls: ['./payment-list.component.css']
})
export class PaymentListComponent implements OnInit {

  payments: Payment[] = [];
  property!: PropertyDto;

  constructor(private paymentService: PaymentService,
    private route: ActivatedRoute,
    private router: Router,
    private catalogService: CatalogService) { }

  ngOnInit(): void {
    let id = String(this.route.snapshot.paramMap.get('id'));
    this.paymentService.getRentalOffers(id).subscribe(response =>{
      this.payments = response.data;
    });
    this.catalogService.getById(id).subscribe(data => {
      this.property = data.data;
    });
  }

  payRent(payment: Payment) {
    throw new Error('Method not implemented.');
    }
}
