import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PropertyListComponent } from './components/property-list/property-list.component';
import { PropertyDetailsComponent } from './components/property-details/property-details.component';
import { RegisterComponent } from './components/register/register.component';

const routes: Routes = [
  { path: '', redirectTo: '/properties', pathMatch: 'full' },
  { path: 'properties', component: PropertyListComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'properties/:type', component: PropertyListComponent },
  { path: 'properties/:id', component: PropertyDetailsComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
