import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PropertyDto } from 'src/app/_models/models';
import { CatalogService } from 'src/app/_services/catalog.service';
import { PhotoModalComponent } from '../photo-modal/photo-modal.component';
import { MatDialog } from '@angular/material/dialog';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-property-details',
  templateUrl: './property-details.component.html',
  styleUrls: ['./property-details.component.css']
})
export class PropertyDetailsComponent implements OnInit{
  property!: PropertyDto;
  bsModalRef!: BsModalRef;

  constructor(
    private catalogservice: CatalogService, 
    private route: ActivatedRoute, 
    private modalService: BsModalService) { 
  }

  ngOnInit(): void {
    this.loadProperty();
  }

  loadProperty() {
    let id = String(this.route.snapshot.paramMap.get('id'));
    this.catalogservice.getById(id).subscribe(data => {
      this.property = data.data;
      this.property.photos = ["./assets/images/placeholder.png"]
    })
  }

  openModal(photos: string[], index: number) {
    this.bsModalRef = this.modalService.show(PhotoModalComponent, {
      initialState: {
        photos: photos,
        index: index,
        closeBtnName: 'Close'
      }
    });
  }

}
