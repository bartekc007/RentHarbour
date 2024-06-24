import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PropertyDto, RentalRequest } from 'src/app/_models/models';
import { CatalogService } from 'src/app/_services/catalog.service';
import { PhotoModalComponent } from '../photo-modal/photo-modal.component';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RentalService } from 'src/app/_services/rental.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-property-details',
  templateUrl: './property-details.component.html',
  styleUrls: ['./property-details.component.css']
})
export class PropertyDetailsComponent implements OnInit {
  property!: PropertyDto;
  bsModalRef!: BsModalRef;
  rentalForm!: FormGroup;
  showRentalForm: boolean = false;
  startDateError: boolean = false;
  startDatePastError: boolean = false;
  endDateError: boolean = false;
  endDatePastError: boolean = false;
  endDateLessError: boolean = false;
  numberOfPeopleError: boolean = false;
  isRentalFormValid: boolean = false;

  constructor(
    private catalogService: CatalogService,
    private route: ActivatedRoute,
    private modalService: BsModalService,
    private formBuilder: FormBuilder,
    private rentalService: RentalService,
    private toastr: ToastrService
  ) {
    this.rentalForm = this.formBuilder.group({
      startDate: ['', Validators.required],
      endDate: ['', Validators.required],
      numberOfPeople: ['', [Validators.required, Validators.min(0)]],
      hasPets: [false],
      messageToOwner: ['', Validators.maxLength(400)]
    });
  }

  ngOnInit(): void {
    this.loadProperty();
  }

  loadProperty() {
    let id = String(this.route.snapshot.paramMap.get('id'));
    this.catalogService.getById(id).subscribe(data => {
      this.property = data.data;
      this.property.photos = ["./assets/images/placeholder.png"];
    });
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

  toggleRentalForm() {
    this.showRentalForm = !this.showRentalForm;
  }

  sendRentalRequest() {
    this.validateForm();
    if (this.isRentalFormValid) {
      var model: RentalRequest = {
        propertyId: this.property.id,
        startDate: new Date(this.rentalForm.get('startDate')?.value),
        endDate: new Date(this.rentalForm.get('endDate')?.value),
        numberOfPeople: this.rentalForm.get('numberOfPeople')?.value,
        pets: this.rentalForm.get('hasPets')?.value,
        messageToOwner: this.rentalForm.get('messageToOwner')?.value,
      }
      this.rentalService.createRentalRequest(model).subscribe(data => {
        var reponse = data.data;
        this.rentalForm.reset();
        this.showRentalForm = false;
        this.toastr.success('Request sent!',"success");
      });
      
    } else {
      console.error('Form is invalid. Cannot send rental request.');
    }
  }

  validateForm() {
    const startDate = new Date(this.rentalForm.get('startDate')?.value);
    const endDate = new Date(this.rentalForm.get('endDate')?.value);
    const numberOfPeople = this.rentalForm.get('numberOfPeople')?.value;
    const today = new Date();

    if (!this.rentalForm.get('startDate')?.value) {
      this.startDateError = true;
    } else {
      this.startDateError = false;
    } 
    if (startDate < today) {
      this.startDatePastError = true;
    } else {
      this.startDatePastError = false;
    }

    if (!this.rentalForm.get('endDate')?.value) {
      this.endDateError = true;
    } else {
      this.endDateError = false;
    } 
    if (endDate < today) {
      this.endDatePastError = true;
    } else {
      this.endDatePastError = false;
    }
    if (startDate >= endDate) {
      this.endDateLessError = true;
    } else {
      this.endDateLessError = false;
    }

    if (numberOfPeople < 0) {
      this.numberOfPeopleError = true;
    } else {
      this.numberOfPeopleError = false;
    }
    if (!this.rentalForm.get('numberOfPeople')?.value) {
      this.numberOfPeopleError = true;
    } else {
      this.numberOfPeopleError = false;
    }

    if(this.startDateError == true || 
      this.startDatePastError == true || 
      this.endDateError == true ||
      this.endDatePastError == true ||
      this.endDateLessError == true ||
      this.numberOfPeopleError == true
    ){
      this.isRentalFormValid = false;
    } else {
      this.isRentalFormValid = true;
    }
  }
}
