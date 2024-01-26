import { Component, OnInit } from '@angular/core';
import { Product } from '../shared/models/product';
import { ShopService } from './shop.service';
import { Category } from '../shared/models/category';
import { publisher } from '../shared/models/publisher';
import { ProductListResponse } from '../shared/models/productListResponse';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {

  products: Product[] = [];
  productResponse!: ProductListResponse;
  categories: Category[] = [];
  publishers:publisher[]=[];
  categoryIdSelected=0;
  publisherIdSelected=0;
  sortSelected='name';
  sortOptions= [
    {name:'Alphabetical',value:'name'},
    {name:'Price:Low to high',value:'priceAsc'},
    {name:'Price:High to low',value:'priceDesc'}
  ]
  totalCount=0;
  pageNumber=1;
  pageSize=4;

  constructor(private shopServices: ShopService) { }

  ngOnInit(): void {

    this.getProducts();
    this.getCategories();
    this.getPublishers();

  }

  getProducts() {
    this.shopServices.getProducts(this.categoryIdSelected,this.publisherIdSelected,this.sortSelected,this.pageNumber,this.pageSize).subscribe({
      next: response => { this.products = response.data;
      this.totalCount=response.totalCount;
      this.pageSize=response.pageSize;
      },
      error: error => console.log("hata :" + error)
    })
  }

  getCategories() {
    this.shopServices.getCategories().subscribe({
      next: response => this.categories = [{id:0,name:'All'},...response],
      error: error => console.log("hata :" + error)
    })
  }

  getPublishers() {
    this.shopServices.getPublishers().subscribe({
      next: response => this.publishers = [{id:0,name:'All'},...response],
      error: error => console.log("hata :" + error)
    })
  }

  onCategorySelected(categoryId:number){
    this.categoryIdSelected=categoryId;
    this.getProducts();
  }

  onPublisherSelected(publisherId:number){
    this.publisherIdSelected=publisherId;
    this.getProducts();
  }

  onSortSelected(event:any){
   this.sortSelected=event.target.value;
   this.getProducts();

  }
}
