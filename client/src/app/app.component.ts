import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Product } from './models/product';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'Bookhub';
  products:Product[]=[];

  constructor(private http: HttpClient) { }
  ngOnInit(): void {
    this.http.get<Product[]>('https://localhost:5001/api/products').subscribe({
      next: response => {this.products=response},//what to do next
      error: error => console.log("eror:" + error),//what to do if there is error
      complete: () => {
        console.log("Requested completed")
      }
    })
    console.log("porducts:"+this.products)
  }

}
