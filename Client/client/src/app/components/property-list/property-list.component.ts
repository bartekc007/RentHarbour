import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpResponseModel, PropertyDto } from 'src/app/_models/models';
import { CatalogService } from 'src/app/_services/catalog.service';

@Component({
  selector: 'app-property-list',
  templateUrl: './property-list.component.html',
  styleUrls: ['./property-list.component.css']
})
export class PropertyListComponent implements OnInit {

  properties: PropertyDto[] = [];

  constructor(private catalogService: CatalogService) {}

  ngOnInit(): void {
    this.catalogService.getAll().subscribe((data: HttpResponseModel<PropertyDto[]>) => {
      this.properties = data.data;
    });
  }
}
