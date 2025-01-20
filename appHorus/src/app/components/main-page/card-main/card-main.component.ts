import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IMerch } from 'src/app/interface/merch';
import { ProductsService } from 'src/app/services/productos.service';


@Component({
  selector: 'app-card-main',
  templateUrl: './card-main.component.html',
  styleUrls: ['./card-main.component.css']
})
export class CardMainComponent implements OnInit {

    sliderUno: any;
    defaultTransform: any;
    listaMerch: IMerch[] = [];

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

    ngOnInit(): void {
      this.sliderUno = document.getElementById("sliderUno");
      this.defaultTransform=0

      this.merch();
    }

    constructor(private productosService: ProductsService, private route: Router) {

     }


    detailCard(productId: number){
      this.route.navigate(['/details', productId])
    }


    merch(){
      this.productosService.merch().subscribe((data) => {
        if(Array.isArray(data)) {
          this.listaMerch = data.slice(0, 5);
        }
      })
    }

    addCart(product: IMerch){
      this.productosService.addProducts(product, 2)
    }


  }







