import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Product } from '../shared/models/product';
import { Category } from '../shared/models/category';
import { publisher } from '../shared/models/publisher';

@Injectable({
  providedIn: 'root'
})
export class ShopService {

  constructor(private http:HttpClient) { }

  baseUrl="https://localhost:5001/api/"

  getProducts(categoryId?:number,publisherId?:number)
  {
    let myparams=new HttpParams();
    if(categoryId) myparams=myparams.append('categoryId',categoryId);
    if(publisherId) myparams=myparams.append('publisherId',publisherId);

    return this.http.get<Product[]>(this.baseUrl+'products',{params:myparams});
  }
  getCategories()
  {
    return this.http.get<Category[]>(this.baseUrl+'categories');
  }

  getPublishers()
  {
    return this.http.get<publisher[]>(this.baseUrl+'publishers');
  }
}
