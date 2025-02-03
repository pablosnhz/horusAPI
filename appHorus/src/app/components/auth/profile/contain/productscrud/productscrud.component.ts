import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable, takeUntil, map, catchError, throwError } from 'rxjs';
import { IProducts } from 'src/app/interface/products';
import { AuthService } from 'src/app/services/auth.service';
import { ProductsService } from 'src/app/services/productos.service';
import { AutoDestroyService } from 'src/app/services/utils/auto-destroy.service';

@Component({
  selector: 'app-productscrud',
  templateUrl: './productscrud.component.html',
  styleUrls: ['./productscrud.component.css']
})
export class ProductscrudComponent implements OnInit{

  products: IProducts[] = [];
  searchProduct: string = '';

  allProducts: IProducts[] = [];
  createProductForm: FormGroup;
  updateProductForm: FormGroup;

  // paginado
  page: number = 1;
  pageSize: number = 6;
  totalProducts: number = 0;

  constructor(private productsService: ProductsService, private destroy$: AutoDestroyService, private fb: FormBuilder, private auth: AuthService) {
    this.createProductForm = this.fb.group({
      title: ['', [Validators.required]],
      description: ['', [Validators.required]],
      price: ['', [Validators.required]],
      priceAnterior: [''],
      imageUrl: ['', [Validators.required]],
      categoria: ['', [Validators.required]]
    }),
    this.updateProductForm = this.fb.group({
      productoId: [null, [Validators.required]],
      title: ['', [Validators.required]],
      description: ['', [Validators.required]],
      price: ['', [Validators.required]],
      priceAnterior: [''],
      imageUrl: ['', [Validators.required]],
      categoria: ['', [Validators.required]]
    })
  }


  ngOnInit(): void {
    // this.productsService.productos().subscribe((data) => {
    //   // console.log(data);
    //   this.products = data.slice(0,6);
    // });
    this.auth.getUser();
    this.loadProducts();
  }

  // constatamos el rol del usuario para las rutas
  userNames$ = this.auth.$userName;

  deleteProduct(id: number): void {
    // console.log('producto eliminado');
      this.productsService.deleteProducts(id).subscribe(() => {
      this.products = this.products.filter(product => product.productoId !== id);
    });
  }

  addProduct(): void {
    if (this.createProductForm.invalid) {
      return;
    }
    const newProduct: IProducts = this.createProductForm.value;

    this.productsService.postProducts(newProduct).subscribe((response) => {
      this.products.push(response);
      // console.log('producto agregado');
      this.createProductForm.reset();
      this.closeModal();
      this.loadProducts();
    });
  }

  updateProduct(event: Event): void {
    event.preventDefault();
    if (this.updateProductForm.invalid) {
      return;
    }

    const updatedProduct: IProducts = {
      ...this.updateProductForm.value
    };

    console.log(`id producto: ${updatedProduct.productoId}`);

    if (!updatedProduct.productoId) {
      console.error('no pasa el id del producto, es invalido');
      return;
    }

    this.productsService.putProducts(updatedProduct.productoId, updatedProduct).subscribe((response) => {
      this.products = this.products.map(item =>
        item.productoId === updatedProduct.productoId ? response : item
      );
      // console.log('actualizado con exito');
      this.updateProductForm.reset();
      this.closeCreateModal();
      this.loadProducts();
      // this.productsService.productos().subscribe((data) => {
      //   this.products = data.slice(0,6);
      // });
    });
  }


// paginado
loadProducts(): void {
  this.productsService.productos().subscribe((data) => {
    this.allProducts = data;
    this.totalProducts = data.length;
    this.searchFilterProducts();
  });
}

updatePaginatedProducts(filteredProducts: IProducts[]): void {
  const startIndex = (this.page - 1) * this.pageSize;
  const endIndex = startIndex + this.pageSize;
  this.products = filteredProducts.slice(startIndex, endIndex);
}

changePage(newPage: number): void {
  if (newPage < 1 || newPage > Math.ceil(this.totalProducts / this.pageSize)) return;
  this.page = newPage;
  this.searchFilterProducts();
}

math(): Math {
  return Math;
}

// busqueda filtrada
searchFilterProducts(): void {
  const filtered = this.allProducts.filter(product =>
    product.title.toLowerCase().includes(this.searchProduct.toLowerCase())
  );

  this.totalProducts = filtered.length;
  this.updatePaginatedProducts(filtered);
}

// modales

  isModalOpen = false;

  openModal() {
    this.isModalOpen = true;
  }

  closeModal() {
    this.isModalOpen = false;
  }

  isOpenCreateModal = false;

  openCreateModal(product: any) {
    console.log(`abriendo modal para actualizar el producto ${product.productoId}`);

    this.isOpenCreateModal = true;
    this.updateProductForm.patchValue(product);
  }

  closeCreateModal() {
    this.isOpenCreateModal = false;
  }
}









// updateProduct(producto: IMerch): void {
//   if (this.formGroup.invalid) {
//     return;
//   }
//   const updatedProduct: IMerch = {
//     ...producto,
//     ...this.formGroup.value
//   };

//   this.productsService.patchProducts(updatedProduct).subscribe((response) => {
//     this.products = this.products.map(item =>
//       item.productoId === producto.productoId ? response : item
//     );
//     console.log('actualizado con exito');
//     this.formGroup.reset();
//     this.closeModal();
//   }, error => {
//     console.error('error', error);
//   });
// }
