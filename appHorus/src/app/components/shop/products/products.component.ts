import { Component, OnInit, inject } from '@angular/core';
import { Router } from '@angular/router';
import { takeUntil } from 'rxjs';
import { IProducts } from 'src/app/interface/products';
import { AuthService } from 'src/app/services/auth.service';
import { ProductsService } from 'src/app/services/productos.service';
import { AutoDestroyService } from 'src/app/services/utils/auto-destroy.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit{


  ngOnInit(): void {
    this.products();
  }

  constructor(private productsService: ProductsService, private route: Router, private auth: AuthService ) { }

  isLoading = true;
  listaProductos: IProducts[] = [];

  products(){
    this.productsService.productos()
    .subscribe((data) => {
      if(Array.isArray(data)) {
        this.listaProductos = data;
        this.isLoading = false;
      }
    })
  }

  addCart(productos: IProducts) {
    if(this.auth.$user()) {
      this.productsService.addProducts(productos, 0);
    } else {
      this.route.navigate(['auth/login']);
    }
  }

  detailProduct(productId: number){
    this.route.navigate(['/details', productId]);
  }

}

