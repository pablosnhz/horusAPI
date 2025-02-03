import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IProducts } from 'src/app/interface/products';
import { AuthService } from 'src/app/services/auth.service';
import { ProductsService } from 'src/app/services/productos.service';


@Component({
  selector: 'app-card-main',
  templateUrl: './card-main.component.html',
  styleUrls: ['./card-main.component.css']
})
export class CardMainComponent implements OnInit {

    sliderUno: any;
    defaultTransform: any;
    listaMerch: IProducts[] = [];

    ngOnInit(): void {
      this.sliderUno = document.getElementById("sliderUno");
      this.defaultTransform=0

      this.merch();
    }

    constructor(private productosService: ProductsService, private route: Router, private auth: AuthService) {}

    detailCard(productId: number){
      this.route.navigate(['/details', productId])
    }


    merch(){
      this.productosService.merch().subscribe((data) => {
        // console.log(data);
        if(Array.isArray(data)) {
          // this.listaMerch = data;
          this.listaMerch = data.slice(17, 22)
        }
      })
    }

    addCart(product: IProducts){
      if(this.auth.$user()) {
        this.productosService.addProducts(product, 2)
      } else {
        this.route.navigate(['auth/login']);
      }
    }


    next() {
      this.defaultTransform = this.defaultTransform - 398;
      const maxTransform = this.sliderUno.scrollWidth - this.sliderUno.clientWidth;
      if (Math.abs(this.defaultTransform) >= maxTransform) {
        this.defaultTransform = -maxTransform;
      }
      this.sliderUno.style.transform = "translateX(" + this.defaultTransform + "px)";
    }

    prev() {
      this.defaultTransform = this.defaultTransform + 398;
      if (this.defaultTransform > 0) {
        this.defaultTransform = 0;
      }
      this.sliderUno.style.transform = "translateX(" + this.defaultTransform + "px)";
    }
  }
