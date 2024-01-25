import { Component, OnInit } from '@angular/core';
import { Product } from '../shared/models/product';
import { ShopService } from './shop.service';
import { Category } from '../shared/models/category';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {

  products: Product[] = [];
  categories: Category[] = [];

  constructor(private shopServices: ShopService) { }

  ngOnInit(): void {

    this.getProducts();
    this.getCategories();

  }

  getProducts() {
    this.shopServices.getProducts().subscribe({
      next: response => this.products = response,
      error: error => console.log("hata :" + error)
    })
  }

  getCategories() {
    this.shopServices.getCategories().subscribe({
      next: response => this.categories = response,
      error: error => console.log("hata :" + error)
    })
  }
}
