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
