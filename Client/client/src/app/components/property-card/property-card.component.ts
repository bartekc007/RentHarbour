import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { HttpResponseModel, PropertyDto, UpdateFollowedPropertiesModel } from 'src/app/_models/models';
import { PropertyService } from 'src/app/_services/property.service';

@Component({
  selector: 'app-property-card',
  templateUrl: './property-card.component.html',
  styleUrls: ['./property-card.component.css']
})
export class PropertyCardComponent implements OnInit {
  @Input()
  property!: PropertyDto;
  isFollowed: boolean = false;

  constructor(private propertyService: PropertyService, private router: Router) { }
  ngOnInit(): void {
  }

  userDetals() {
    this.router.navigate(['properties', this.property.id]);
  }

  followProperty(){
    let model: UpdateFollowedPropertiesModel = {
      propertyId: this.property.id,
      action: this.isFollowed ? 1:0
    };

    this.propertyService.followProperty(model).subscribe((data: HttpResponseModel<boolean>) => {
      if(data.status)
        this.isFollowed = ! this.isFollowed
    });
  }
}
