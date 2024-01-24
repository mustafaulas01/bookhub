import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Product } from '../shared/models/product';

@Injectable({
  providedIn: 'root'
})
export class ShopService {

  constructor(private http:HttpClient) { }

  baseUrl="https://localhost:5001/api/"

  getProducts()
  {
    return this.http.get<Product[]>(this.baseUrl+'products?pageNumber=1&pageSize=8');
  }
}
