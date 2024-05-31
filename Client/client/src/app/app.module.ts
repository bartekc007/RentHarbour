import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http'

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavComponent } from './components/nav/nav.component';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { PropertyListComponent } from './components/property-list/property-list.component';
import { PropertyCardComponent } from './components/property-card/property-card.component';
import { PropertyDetailsComponent } from './components/property-details/property-details.component';
import { PhotoModalComponent } from './components/photo-modal/photo-modal.component';
import { BsModalService } from 'ngx-bootstrap/modal';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    PropertyListComponent,
    PropertyCardComponent,
    PropertyDetailsComponent,
    PhotoModalComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    FormsModule,
    BsDropdownModule.forRoot()
  ],
  providers: [BsModalService ],
  bootstrap: [AppComponent]
})
export class AppModule { }
