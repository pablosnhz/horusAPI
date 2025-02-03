import { Component, OnInit, inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { takeUntil } from 'rxjs';
import { IProducts } from 'src/app/interface/products';
import { AuthService } from 'src/app/services/auth.service';
import { ProductsService } from 'src/app/services/productos.service';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit{


  productoId: number = 0;
  productDetails: IProducts | undefined;
  mensajeExito: boolean = false;
  detailProducts: IProducts | undefined;
  productosId: number = 0;

  constructor(private route: ActivatedRoute, private productService: ProductsService, private router: Router,
    private auth: AuthService
   ){}

  ngOnInit(): void {
    this.route.params
    .subscribe(params => {
      // this.productoId = +params['id'];
      this.productosId = +params['productoId'];

      // this.productsDetails(this.productoId);
      this.detailsProducts(this.productosId);

    // filtro busqueda IProducts
    this.productService.searchResults$
    .subscribe(searchResults => {
      if(searchResults.length > 0) {
        this.detailProducts = searchResults[0]
        }
      })
    })

    // this.productService.searchResultsMerch$
    // .subscribe(searchMerchResults => {
    //   if(searchMerchResults.length > 0){
    //     this.productDetails = searchMerchResults[0];
    //   }
    // })

    this.productService.refreshResults();
  }

  // detalles service, paso informacion
  // productsDetails(productId: number){
  //   console.log('producto', this.productDetails);
  //   this.productService.merchDetails(productId)
  //   .subscribe(
  //     (product) => {
  //       console.log('detalle', this.productDetails);
  //       this.productDetails = product;
  //     })
  // }

  // detalles service
  detailsProducts(productsId: number){
    console.log('producto', this.productDetails?.title);
    this.productService.apiProductDetail(productsId)
    .subscribe(
      (product) => {
        console.log('detalle', this.detailProducts);
        this.detailProducts = product;

      })
  }

  addCart(product: IProducts){
    if(this.auth.$user()) {
      this.productService.addProducts(product, 0);
      this.mensajeExito = true;
    } else {
      this.router.navigate(['auth/login']);
    }


  setTimeout(() => {
    this.mensajeExito = false;
  }, 1000);
  }
}



