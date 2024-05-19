import { Component, Input, OnInit } from '@angular/core';
import { PropertyDto } from 'src/app/_models/models';

@Component({
  selector: 'app-property-card',
  templateUrl: './property-card.component.html',
  styleUrls: ['./property-card.component.css']
})
export class PropertyCardComponent implements OnInit {
  @Input()
  property!: PropertyDto;

  constructor() { }
  ngOnInit(): void {
  }
}
