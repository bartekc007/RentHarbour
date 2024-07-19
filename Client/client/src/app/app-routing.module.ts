import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PropertyListComponent } from './components/property-list/property-list.component';
import { PropertyDetailsComponent } from './components/property-details/property-details.component';
import { RegisterComponent } from './components/register/register.component';
import { OfferListComponent } from './components/offer-list/offer-list.component';
import { OfferDetailsComponent } from './components/offer-details/offer-details.component';
import { MyOfferListComponent } from './components/my-offer-list/my-offer-list.component';
import { PaymentListComponent } from './components/payment-list/payment-list.component';
import { ChatComponent } from './components/chat/chat.component';
import { ChatListComponent } from './components/chat-list/chat-list.component';

const routes: Routes = [
  { path: '', redirectTo: '/properties', pathMatch: 'full' },
  { path: 'properties', component: PropertyListComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'offers', component: OfferListComponent },
  { path: 'my-offers', component: MyOfferListComponent },
  { path: 'offer/:id', component: OfferDetailsComponent },
  { path: 'payments/:id', component: PaymentListComponent },
  { path: 'properties/:id', component: PropertyDetailsComponent },
  { path: 'properties/:type', component: PropertyListComponent },
  { path: 'chat/:chatId', component: ChatComponent },
  { path: 'chat-list', component: ChatListComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
