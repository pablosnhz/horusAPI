import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MainPageComponent } from './components/main-page/main-page.component';
import { NavbarComponent } from './components/shared/navbar/navbar.component';
import { FooterComponent } from './components/shared/footer/footer.component';
import { ShopComponent } from './components/shop/shop.component';
import { FormsModule } from '@angular/forms';
import { CardMainComponent } from './components/main-page/card-main/card-main.component';
import { CardSecondaryComponent } from './components/main-page/card-secondary/card-secondary.component';
import { ProductsComponent } from './components/shop/products/products.component';

import { HttpClientModule } from '@angular/common/http';
import { ShoppingCartComponent } from './components/shop/shopping-cart/shopping-cart.component';
import { DetailsComponent } from './components/shop/details/details.component';
import { RouterModule } from '@angular/router';
import { ShopRoutingModule } from './components/shop/shop-routing-routing.module';
import { AutoDestroyService } from './services/utils/auto-destroy.service';

@NgModule({
  declarations: [
    AppComponent,
    MainPageComponent,
    NavbarComponent,
    FooterComponent,
    ShopComponent,
    CardMainComponent,
    CardSecondaryComponent,
    ProductsComponent,
    ShoppingCartComponent,
    DetailsComponent,


  ],
  imports: [
    BrowserModule,
    CommonModule,
    AppRoutingModule,
    FormsModule,
    RouterModule,

    HttpClientModule
  ],
  providers: [ AutoDestroyService ],
  bootstrap: [AppComponent],
  schemas: [ CUSTOM_ELEMENTS_SCHEMA ]
})
export class AppModule { }
