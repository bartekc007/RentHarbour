import { HttpStatusCode } from "@angular/common/http";

export interface Property {
 id: string;
 name: string;
 description: string;
 address: {
    street: string;
    city: string;
    state: string;
    postalCode: string;
    country: string;
    price: number;
    bedrooms: number;
    areaSquareMeters: number;
    isAvailable: boolean;
    photos: Array<any>;
 }
}

export interface PropertiesGetAllRequest {
    priceMax?: number;
    priceMin?: number;
    bedroomsMax?: number;
    bedroomsMin?: number;
    bathroomsMax?: number;
    bathroomsMin?: number;
    areaSquareMetersMax?: number;
    areaSquareMetersMin?: number;
    city?: string;
}

export interface User{
    username: string;
    accessToken: string;
    refreshToken: string
}

export interface UserRegisterRequest {
    userName: string;
    email: string;
    password: string;
    phoneNumber: string;
    dateOfBirth: Date;
    address: string;
    city: string;
    country: string;
  }

export interface AddressDto {
    street: string;
    city: string;
    state: string;
    country: string;
    zipCode: string;
}
  
export interface PropertyDto {
    id: string;
    name: string;
    description: string;
    address: AddressDto;
    price: number;
    bedrooms: number;
    bathrooms: number;
    areaSquareMeters: number;
    isAvailable: boolean;
    photos: string[];
}

export enum UserPropertyAction{
    Add,
    Remove
}

export interface UpdateFollowedPropertiesModel{
    propertyId: string;
    action: UserPropertyAction
}

export interface BsonDocument {
    [key: string]: any;
}

export interface HttpResponseModel<T> {
    status: HttpStatusCode
    data: T;
    errorMessage: string;
}

export interface PhotoDto {
    id: number;
    url: string;
    isMain: boolean;
}

export interface RentalRequest {
    propertyId: string;
    startDate: Date;
    endDate: Date;
    numberOfPeople: number;
    pets: boolean;
    messageToOwner: string;
}

export interface RentalOffer {
    id: number;
    propertyId: string;  // MongoDB Property ID
    startDate: Date;
    endDate: Date;
    numberOfPeople: number;
    pets: boolean;
    messageToOwner: string;
    status: string;  // e.g., Pending, Approved, Rejected
    propertyName: string;
    propertyStreet: string;
    propertyCity: string;
    propertyState: string;
    propertyPostalCode: string;
    propertyCountry: string;
    isAvailable: boolean;
    isActive: boolean;
    accepted: boolean;
}

export interface AcceptRentalRequest {
    OfferId: number;
    Status: number; //1 - accept, 2-modify, 3-decline
}

export interface RentalDocumentRequest {
    offerId: string; 
    file: File;
}

export interface RentalDocument {
    documentId: string;
    offerId: string; 
    fileName: string;
    isLatest: boolean;
    file: File;
}
