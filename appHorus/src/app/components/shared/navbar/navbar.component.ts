import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, forkJoin, map } from 'rxjs';
import { IProducts } from 'src/app/interface/products';
import { AuthService } from 'src/app/services/auth.service';
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

  constructor( private route: Router, private productService: ProductsService, private auth: AuthService ){}

  searchResults$!: Observable<IProducts[]>;
  filteredProducts: string = '';
  searchMerchResults$!: Observable<IProducts[]>;

  ngOnInit(): void {
    this.searchResults$ = this.productService.searchResults$;

    if(this.auth.$user()) {
      this.auth.getUser();
    }
  }

  userNames$ = this.auth.$userName;


  searchProducts(): void {
    this.productService.searchProductss(this.filteredProducts);
    this.filteredProducts = '';
  }

  searchResults(): Observable<( IProducts[] )> {
    return forkJoin([
      this.searchResults$,
    ]).pipe(
      map(([searchResults]) => searchResults)
      )
    }

    // por cada busqueda recargo pagina
    refreshPage(): void {
      this.route.navigateByUrl('/', {skipLocationChange: true}).then(() => {
        this.route.navigate([this.route.url])
      })
    }

    logout(){
      this.auth.logout();
      this.route.navigate(['/']);
    }

    isAuthenticated(): boolean {
      return this.auth.$user();
    }

}
