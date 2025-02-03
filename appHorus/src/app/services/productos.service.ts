import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IProducts } from '../interface/products';
import { BehaviorSubject, Observable, catchError, map, takeUntil, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { AutoDestroyService } from './utils/auto-destroy.service';
import { environment } from 'src/environments/environment.development';

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
    return this.http.get<{ value: IProducts[] }>(environment.apiEndpoints.apiMerch + '/productos').pipe(
      map(response => response.value),
      catchError(error => {
        console.error(error);
        return throwError(() => error);
      }),
      takeUntil(this.destroy$)
    );
  }

  merch(): Observable<IProducts[]> {
    return this.http.get<{ value: IProducts[] }>(environment.apiEndpoints.apiMerch + '/productos').pipe(
      map(response => response.value),
      catchError(error => {
        return throwError(() => error);
      }),
      takeUntil(this.destroy$)
    );
  }

  merchDetails(productId: number): Observable<IProducts> {
    return this.http.get<IProducts[]>(environment.apiEndpoints.apiMerch + '/productos')
    .pipe(takeUntil(this.destroy$),
    map(products => {
        const product = products.find(item => item.productoId === productId);
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

  apiProductDetail(productsId: number): Observable<IProducts> {
    return this.http.get<{ value: IProducts[] }>(environment.apiEndpoints.apiMerch + '/productos')
      .pipe(
        map(response => {
          const products = response.value;
          console.log('datos de la api:', products);
          if (!products || products.length === 0) {
            throw new Error('array vacio');
          }
          const product = products.find(item => {
            return +item.productoId === +productsId;
          });
          console.log('producto encontrado:', product);
          if (product) {
            return product;
          } else {
            throw new Error(`producto no encontrado ${productsId}`);
          }
        }),
        catchError(error => {
          return throwError(() => 'busqueda no encontrada');
        }),
        takeUntil(this.destroy$)
      );
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
      this.searchResultsSubject.next([]);
      return;
    }
    this.productos().pipe(
      takeUntil(this.destroy$),
      map(products => {
        console.log('Productos recibidos:', products);
        const filteredProducts = products.filter(product =>
          product.title.toLowerCase().normalize("NFD").replace(/\s+/g, ' ').trim()
            .includes(title.toLowerCase().normalize("NFD").replace(/\s+/g, ' ').trim())
        );
        console.log('Productos filtrados:', filteredProducts);
        return filteredProducts;
      })
    ).subscribe(
      searchResults => {
        this.searchResultsSubject.next(searchResults);
        if(searchResults.length > 0){
          this.navigateDetails(searchResults[0].title)
        }
      },
      error => {
        console.error('Error para encontrar los resultados', error)
      }
    )
  }

  navigateDetails(title: string): void {
    this.route.navigate(['/details', title]);
  }

  private searchResultsMerchSubject = new BehaviorSubject<IProducts[]>([]);
  public searchResultsMerch$ = this.searchResultsMerchSubject.asObservable();

  searchMerchProducts(title: string): void {
    if (title.trim() === '') {
      return;
    }
    this.productos()
    .pipe(takeUntil(this.destroy$),
      map( merch => merch.filter(merchs => merchs.title.toLowerCase().includes(title.toLowerCase())) ),
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


  // ! peticiones crud, ya tenemos el get

  postProducts(product: IProducts): Observable<IProducts> {
    return this.http.post<IProducts>(environment.apiEndpoints.apiMerch + '/agregarproducto', product).pipe(
      catchError(error => {
        console.error(error);
        return throwError(() => error);
      }),
      takeUntil(this.destroy$)
    );
  }

  putProducts(id: number, producto: IProducts): Observable<any> {
    console.log(`producto a actualizar: ${JSON.stringify(producto)}`);
    return this.http.put(`${environment.apiEndpoints.apiMerch}/actualizarproducto/${id}`, producto);
  }

  deleteProducts(id: number): Observable<IProducts> {
    return this.http.delete<IProducts>(`${environment.apiEndpoints.apiMerch}/eliminarproducto/${id}`).pipe(
      catchError(error => {
        console.error(error);
        return throwError(() => error);
      }),
      takeUntil(this.destroy$)
    );
  }
}


