import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Payment } from 'src/app/_models/models';
import { PaymentService } from 'src/app/_services/payment.service';

@Component({
  selector: 'app-payment-list',
  templateUrl: './payment-list.component.html',
  styleUrls: ['./payment-list.component.css']
})
export class PaymentListComponent implements OnInit {

  payments: Payment[] = [];

  constructor(private paymentService: PaymentService,
    private route: ActivatedRoute,
    private router: Router,) { }

  ngOnInit(): void {
    let id = String(this.route.snapshot.paramMap.get('id'));
    this.paymentService.getRentalOffers(id).subscribe(response =>{
      this.payments = response.data;
    });
  }

  payRent(payment: Payment) {
    throw new Error('Method not implemented.');
    }
}
