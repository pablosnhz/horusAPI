import { Component } from '@angular/core';
import { IProducts } from 'src/app/interface/products';
import { ProductsService } from 'src/app/services/productos.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.css']
})
export class ShopComponent {

  products: { product: IProducts; quantity: number; }[] = [];
  totalQuantity: number = 0;

  constructor(private productsService: ProductsService ) {}

  ngOnInit() {
    this.productsService.products
    .subscribe(products => {
      this.products = products;

      // console.log('Productos actualizados', this.products)
      this.quantityProduct();
    });
  }

  quantityProduct(){
    this.totalQuantity = this.products.reduce((total, item) =>
      total + item.quantity, 0)
    // console.log('total agregado', this.totalQuantity )
  }


}
