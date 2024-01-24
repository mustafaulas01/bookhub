import { Component, OnInit } from '@angular/core';
import { Product } from '../shared/models/product';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  
  products:Product[]=[];

  constructor(private shopServices:ShopService){}

  ngOnInit(): void {
   
    this.shopServices.getProducts().subscribe ({
    next:response=>this.products=response,
    error:error=>console.log("hata :"+error)
    })

  }
}
