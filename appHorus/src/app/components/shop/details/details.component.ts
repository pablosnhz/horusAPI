import { Component, OnInit, inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { takeUntil } from 'rxjs';
import { IMerch } from 'src/app/interface/merch';
import { IProducts } from 'src/app/interface/products';
import { ProductsService } from 'src/app/services/productos.service';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit{


  productoId: number = 0;
  productDetails: IMerch | undefined;
  mensajeExito: boolean = false;
  detailProducts: IProducts | undefined;
  productosId: number = 0;

  constructor(private route: ActivatedRoute, private productService: ProductsService ){}

  ngOnInit(): void {
    this.route.params
    .subscribe(params => {
      this.productoId = +params['id'];
      this.productosId = +params['id']

      this.productsDetails(this.productoId);
      this.detailsProducts(this.productosId);


    // filtro busqueda IProducts
    this.productService.searchResults$
    .subscribe(searchResults => {
      if(searchResults.length > 0) {
        this.detailProducts = searchResults[0]
        }
      })
    })

    this.productService.searchResultsMerch$
    .subscribe(searchMerchResults => {
      if(searchMerchResults.length > 0){
        this.productDetails = searchMerchResults[0];
      }
    })

    this.productService.refreshResults();
  }

  // detalles service, paso informacion
  productsDetails(productId: number){
    this.productService.merchDetails(productId)
    .subscribe(
      (product) => {
        this.productDetails = product;
      })
  }

  // detalles service
  detailsProducts(productsId: number){
    this.productService.apiProductDetail(productsId)
    .subscribe(
      (product) => {
        this.detailProducts = product;
      })
  }

  addCart(product: IMerch){
    this.productService.addProducts(product, 0);
    this.mensajeExito = true;

  setTimeout(() => {
    this.mensajeExito = false;
  }, 1000);
  }
}



