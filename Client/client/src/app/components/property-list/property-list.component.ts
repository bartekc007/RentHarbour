import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { HttpResponseModel, PropertiesGetAllRequest, PropertyDto, RentedProperty } from 'src/app/_models/models';
import { CatalogService } from 'src/app/_services/catalog.service';

@Component({
  selector: 'app-property-list',
  templateUrl: './property-list.component.html',
  styleUrls: ['./property-list.component.css']
})
export class PropertyListComponent implements OnInit {
  type!: string;
  properties: PropertyDto[] = [];
  filters: PropertiesGetAllRequest = {}
  searchForm!: FormGroup;
  rentedProperties : RentedProperty[] = [];

  constructor(private catalogService: CatalogService, private route: ActivatedRoute, private fb: FormBuilder) {}

  ngOnInit() {
    this.searchForm = this.fb.group({
      priceMin: [null, [Validators.min(0)]],
      priceMax: [null, [Validators.min(0)]],
      bedroomsMin: [null, [Validators.min(0)]],
      bedroomsMax: [null, [Validators.min(0)]],
      bathroomsMin: [null, [Validators.min(0)]],
      bathroomsMax: [null, [Validators.min(0)]],
      areaSquareMetersMin: [null, [Validators.min(0)]],
      areaSquareMetersMax: [null, [Validators.min(0)]],
      city: ['']
    }, { validator: this.rangeValidator });

    this.route.params.subscribe(params => {
      this.type = params['type'];

      if (this.type === 'followed') {
        
      } else if (this.type === 'owned') {
        
      } else if (this.type === 'rented') {
        this.catalogService.GetRented().subscribe((data: HttpResponseModel<RentedProperty[]>) => {
          this.rentedProperties = data.data;
          console.log('Rented properties data:', this.rentedProperties);
        });
      } else {
        this.catalogService.getAll({}).subscribe((data: HttpResponseModel<PropertyDto[]>) => {
          this.properties = data.data;
        });
      }
    });
  }

  rangeValidator(group: FormGroup): { [key: string]: boolean } | null {
    const minMaxPairs = [
      { min: 'priceMin', max: 'priceMax' },
      { min: 'bedroomsMin', max: 'bedroomsMax' },
      { min: 'bathroomsMin', max: 'bathroomsMax' },
      { min: 'areaSquareMetersMin', max: 'areaSquareMetersMax' }
    ];

    for (const pair of minMaxPairs) {
      const minControl = group.get(pair.min);
      const maxControl = group.get(pair.max);
      if (minControl && maxControl) {
        const min = minControl.value;
        const max = maxControl.value;
        if (min != null && max != null && min > max) {
          return { rangeInvalid: true };
        }
      }
    }
    return null;
  }

  onSearch() {
    this.filters = {
      priceMin: this.searchForm.get('priceMin')?.value,
      priceMax: this.searchForm.get('priceMax')?.value,
      bedroomsMin: this.searchForm.get('bedroomsMin')?.value,
      bedroomsMax: this.searchForm.get('bedroomsMax')?.value,
      bathroomsMin: this.searchForm.get('bathroomsMin')?.value,
      bathroomsMax: this.searchForm.get('bathroomsMax')?.value,
      areaSquareMetersMin: this.searchForm.get('areaSquareMetersMin')?.value,
      areaSquareMetersMax: this.searchForm.get('areaSquareMetersMax')?.value,
      city: this.searchForm.get('city')?.value
    };

    this.catalogService.getAll(this.filters).subscribe((data: HttpResponseModel<PropertyDto[]>) => {
      if(data.status == 204)
        this.properties = [];
      if(data.status == 200)
        this.properties = data.data;
    });
  }

  payRent(property: RentedProperty) {
    
  }
}
