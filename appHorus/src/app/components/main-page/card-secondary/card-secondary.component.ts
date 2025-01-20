import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IMerch } from 'src/app/interface/merch';
import { ProductsService } from 'src/app/services/productos.service';

import { register } from 'swiper/element/bundle';
register();

@Component({
  selector: 'app-card-secondary',
  templateUrl: './card-secondary.component.html',
  styleUrls: ['./card-secondary.component.css']
})
export class CardSecondaryComponent implements OnInit {

slider: any;
defaultTransform: any;
merchList: IMerch[] = [];

constructor(private route: Router, private productsService: ProductsService){}

ngOnInit(): void {
  this.slider = document.getElementById("slider");
  this.defaultTransform=0;

  this.merchCard();
  }



merchCard(){
  this.productsService.merch()
  .subscribe((data) => {
    if(Array.isArray(data))
    this.merchList = data.slice(5, 15)
  })
}

detailMerch(productId: number){
    this.route.navigate(['/details', productId]);
  }

addCart(product: IMerch) {
    this.productsService.addProducts(product, 0);
  }


goNext() {
  this.defaultTransform = this.defaultTransform - 398;
  if (Math.abs(this.defaultTransform) >= this.slider.scrollWidth / 1.7) this.defaultTransform = 0;
  this.slider.style.transform = "translateX(" + this.defaultTransform + "px)";
}

goPrev() {
  if (Math.abs(this.defaultTransform) === 0) this.defaultTransform = 0;
  else this.defaultTransform = this.defaultTransform + 398;
  this.slider.style.transform = "translateX(" + this.defaultTransform + "px)";
}

}
