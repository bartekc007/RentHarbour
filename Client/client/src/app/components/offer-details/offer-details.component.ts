import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AcceptRentalRequest, RentalDocumentRequest, RentalOffer } from 'src/app/_models/models';
import { DocumentService } from 'src/app/_services/document.service';
import { RentalService } from 'src/app/_services/rental.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-offer-details',
  templateUrl: './offer-details.component.html',
  styleUrls: ['./offer-details.component.css']
})
export class OfferDetailsComponent implements OnInit {

  offer!: RentalOffer;
  pdfFile: File | null = null;

  constructor(private rentalService: RentalService,
     private documentService: DocumentService,
      private route: ActivatedRoute,
       private toastr: ToastrService) { }

  ngOnInit(): void {
    let id = String(this.route.snapshot.paramMap.get('id'));
    this.rentalService.getRentalOffer(id).subscribe(response => {
      this.offer = response.data;
    });
  }

  handleFileInput(files: FileList | null) {
    if (files && files.length > 0) {
      this.pdfFile = files[0];
    }
  }

  removeFile() {
    this.pdfFile = null;
  }

  onFileChange(event: Event) {
    const inputElement = event.target as HTMLInputElement;
    if (inputElement.files) {
      this.handleFileInput(inputElement.files);
    }
  }

  acceptOffer() {
    let model: AcceptRentalRequest = {
      OfferId: this.offer.id,
      Status: this.pdfFile != null ? 2 : 1,
    };
    this.rentalService.acceptOffer(model).subscribe(response => {
      if (response.data == true)
        this.toastr.success('offer accepted',"success");
      else 
        this.toastr.error('unexpected error occured.',"error");
    });
  }

  uploadDocument() {
    if (!this.pdfFile) {
      this.toastr.error('Please select a PDF file to upload.',"error");
      return;
    }

    let id = String(this.route.snapshot.paramMap.get('id'));
    var model : RentalDocumentRequest = {
      offerId: id,
      file: this.pdfFile
    }
    this.documentService.uploadDocument(model).subscribe(
      response => {
        console.log('Document uploaded successfully:', response);
        this.toastr.success('file uploaded successfully',"error");

        this.pdfFile = null; 
      },
      error => {
        console.error('Error uploading document:', error);
        this.toastr.error('unexpected error occured.',"error");
      }
    );
  }
}
