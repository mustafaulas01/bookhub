import { Component, OnInit } from '@angular/core';
import { Product } from '../shared/models/product';
import { ShopService } from './shop.service';
import { Category } from '../shared/models/category';
import { publisher } from '../shared/models/publisher';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {

  products: Product[] = [];
  categories: Category[] = [];
  publishers:publisher[]=[];
  categoryIdSelected=0;
  publisherIdSelected=0;

  constructor(private shopServices: ShopService) { }

  ngOnInit(): void {

    this.getProducts();
    this.getCategories();
    this.getPublishers();

  }

  getProducts() {
    this.shopServices.getProducts(this.categoryIdSelected,this.publisherIdSelected).subscribe({
      next: response => this.products = response,
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
}
