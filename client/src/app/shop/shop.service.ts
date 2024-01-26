import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Category } from '../shared/models/category';
import { publisher } from '../shared/models/publisher';
import { ProductListResponse } from '../shared/models/productListResponse';

@Injectable({
  providedIn: 'root'
})
export class ShopService {

  constructor(private http:HttpClient) { }

  baseUrl="https://localhost:5001/api/"

  getProducts(categoryId?:number,publisherId?:number,sort?:string,pageNumber:number=1,pageSize:number=4)
  {
    let myparams=new HttpParams();
    if(categoryId) myparams=myparams.append('categoryId',categoryId);
    if(publisherId) myparams=myparams.append('publisherId',publisherId);
    if(sort) myparams=myparams.append('sort',sort);
    myparams=myparams.append("pageNumber",pageNumber);
    myparams=myparams.append("pageSize",pageSize);

    return this.http.get<ProductListResponse>(this.baseUrl+'products',{params:myparams});
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
