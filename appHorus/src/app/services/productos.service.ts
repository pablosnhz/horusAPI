import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IProducts } from '../interface/products';
import { BehaviorSubject, Observable, catchError, map, takeUntil, throwError } from 'rxjs';
import { IMerch } from '../interface/merch';
import { Router } from '@angular/router';
import { AutoDestroyService } from './utils/auto-destroy.service';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {

  private readonly destroy$: AutoDestroyService = inject(AutoDestroyService);

  constructor(private http:HttpClient, private route: Router) {
    const storedCart = localStorage.getItem('cart')
    if(storedCart){
      this.cartProducts = JSON.parse(storedCart)

      this.updateCart();
    }
  }

  productos(){
      return this.http.get('https://fakestoreapi.com/products/')
    }

  merch(){
    return this.http.get('../../assets/merch.json')
  }

  merchDetails(productId: number): Observable<IMerch> {
    return this.http.get<IMerch[]>('../../assets/merch.json')
    .pipe(takeUntil(this.destroy$),
    map(products => {
        const product = products.find(item => item.id === productId);
        if (product) {
          return product;
        } else {
          throw new Error('Búsqueda no válida');
        }
      }),
      catchError(error => {
        return throwError('Búsqueda no válida');
      })
    );
  }

  apiProductDetail(productsId: number): Observable<IProducts>{
    return this.http.get<IProducts[]>('https://fakestoreapi.com/products/')
    .pipe(takeUntil(this.destroy$),
      map(products => {
        const product = products.find(item => item.id === productsId)
        if(product){
          return product
        } else {
          throw new Error (`no se encontro el elemento de tipo ID ${productsId}`)
        }
      }),
      catchError(error => {
        return throwError('Búsqueda no válida');
      })
    )
  }

  _productsBsubject: BehaviorSubject<{ product: IProducts, quantity: number }[]> = new BehaviorSubject<{ product: IProducts, quantity: number }[]>([]);

  get products(){
    return this._productsBsubject.asObservable();
  }

  cartProducts: { product: IProducts, quantity: number }[] = [];
  totalProductsInCart: number = 0;


  deleteProduct(index: number){
    this.cartProducts.splice(index, 1);
    this.updateCart();
  }

  updateCart(){
    this._productsBsubject.next([...this.cartProducts]);
    this.totalProductsInCart = this.cartProducts.reduce((total, item) => total + item.quantity, 0)
    localStorage.setItem('cart', JSON.stringify(this.cartProducts));
  }


  addProducts(product: IProducts , quantity: number) {
    const agregado = this.cartProducts.findIndex(item => item.product.title === product.title  )

    if(agregado !== -1){
      this.cartProducts[agregado].quantity++;
    } else {
      this.cartProducts.push({ product: product, quantity:1 })
    }
    this.updateCart();
  }



  // busquedaFiltrada
  private searchResultsSubject = new BehaviorSubject<IProducts[]>([]);
  public searchResults$ = this.searchResultsSubject.asObservable();


  searchProductss(title: string): void {
    if (title.trim() === '') {
      return;
    }
    this.http.get<IProducts[]>('https://fakestoreapi.com/products')
    .pipe(takeUntil(this.destroy$),
      map(products => products.filter(product => product.title.toLowerCase().includes(title.toLowerCase())))
    ).subscribe(
      searchResults => {
        this.searchResultsSubject.next(searchResults);
          if(searchResults.length > 0){
            this.navigateDetails(searchResults[0].title)
          }
      },
      error => {
        console.error('Error para encontrar los resultados', error);
      }
    );
  }

  navigateDetails(title: string): void {
    this.route.navigate(['/details', title]);
  }

  private searchResultsMerchSubject = new BehaviorSubject<IMerch[]>([]);
  public searchResultsMerch$ = this.searchResultsMerchSubject.asObservable();

  searchMerchProducts(title: string): void {
    if (title.trim() === '') {
      return;
    }
    this.http.get<IMerch[]>('../../assets/merch.json')
    .pipe(takeUntil(this.destroy$),
      map( merch => merch.filter(merchs => merchs.title.toLowerCase().includes(title.toLowerCase())) )
    ).subscribe(
      searchMerchResults => {
        this.searchResultsMerchSubject.next(searchMerchResults);
          if(searchMerchResults.length > 0){
            this.navigateMerchDetails(searchMerchResults[0].title)
          }
      },
      error => {
        console.error('Error para encontrar los resultados', error)
      }
    )
  }

  navigateMerchDetails(title: string): void {
    this.route.navigate(['/details', title]);
  }

  refreshResults(): void {
    this.searchResultsMerchSubject.next([]);
    this.searchResultsSubject.next([]);

  }

  // fin busqueda filtrada

}


