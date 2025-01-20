import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, forkJoin, map } from 'rxjs';
import { IMerch } from 'src/app/interface/merch';
import { IProducts } from 'src/app/interface/products';
import { ProductsService } from 'src/app/services/productos.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit{

  @Input() itemCount: number = 0;

  open: boolean = false;

  toggleMenu() {
    this.open = !this.open;
  }

  constructor( private route: Router, private productService: ProductsService ){}

  searchResults$!: Observable<IProducts[]>;
  filteredProducts: string = '';
  searchMerchResults$!: Observable<IMerch[]>;

  ngOnInit(): void {
    this.searchResults$ = this.productService.searchResults$;
    this.searchMerchResults$ = this.productService.searchResultsMerch$;

  }

  searchProducts():void {

    this.productService.searchProductss(this.filteredProducts);
    this.productService.searchMerchProducts(this.filteredProducts);
    this.filteredProducts = '';

    this.refreshPage();
  }

  searchResults(): Observable<( IProducts[] | IMerch[] )> {
    return forkJoin([
      this.searchResults$,
      this.searchMerchResults$,

    ]).pipe(
      map(([products, merch]) => [...products, ...merch])
      )
    }


    // por cada busqueda recargo pagina
    refreshPage(): void {
      this.route.navigateByUrl('/', {skipLocationChange: true}).then(() => {
        this.route.navigate([this.route.url])
    })
  }
}


