import { Component, Inject } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-custom-modal',
  templateUrl: './photo-modal.component.html'
})

export class PhotoModalComponent {
  photos!: string[];
  index!: number;
  closeBtnName!: string;

  constructor(public bsModalRef: BsModalRef) {}

  get currentPhoto(): string {
    return this.photos[this.index];
  }
}
