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

export interface User{
    username: string;
    token: string;
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
    photos: BsonDocument[];
}
  

export interface BsonDocument {
    [key: string]: any;
}

export interface HttpResponseModel<T> {
    data: T;
    errorMessage: string;
}
