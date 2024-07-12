import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AcceptRentalRequest, RentalDocument, RentalDocumentRequest, RentalOffer } from 'src/app/_models/models';
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
  documents!: RentalDocument[];

  constructor(private rentalService: RentalService,
     private documentService: DocumentService,
      private route: ActivatedRoute,
      private router: Router,
      private toastr: ToastrService) { }

  ngOnInit(): void {
    let id = String(this.route.snapshot.paramMap.get('id'));
    this.rentalService.getRentalOffer(id).subscribe(response => {
      this.offer = response.data;
      this.loadDocuments()
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

  rejectOffer() {
    let model: AcceptRentalRequest = {
      OfferId: this.offer.id,
      Status: 3,
    };
    this.rentalService.acceptOffer(model).subscribe(response => {
      if (response.data == true){
        this.toastr.success('offer rejected',"success");
        this.router.navigate(['offers']);
      }
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

  loadDocuments() {
    let id = String(this.route.snapshot.paramMap.get('id'));
    this.documentService.getDocumentsByOfferId(id).subscribe(
      response => {
        this.documents = response.data;
      },
      error => {
        console.error('Error loading documents:', error);
        this.toastr.error('Unexpected error occurred.', "Error");
      }
    );
  }

  downloadDocument(documentId: string, fileName: string) {
    this.documentService.downloadDocument(documentId).subscribe(
      response => {
        const blob = new Blob([response], { type: 'application/pdf' });
        const url = window.URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.href = url;
        a.download = fileName;
        a.click();
        window.URL.revokeObjectURL(url);
      },
      error => {
        console.error('Error downloading document:', error);
        this.toastr.error('Unexpected error occurred.', "Error");
      }
    );
  }
}
