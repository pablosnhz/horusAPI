import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ShopComponent } from './components/shop/shop.component';
import { nouserGuard } from './core/guards/nouser.guard';

const routes: Routes = [
  {
    path: '', component: ShopComponent,
    children: [
      {
        path: '',
        loadChildren: () => import('./components/shop/shop.module').then((m) => m.ShopsRoutingModule)
      },
    ]
  },
  {
    path: 'auth',
    children: [
      {
        path: '',
        loadChildren: () => import('./components/auth/auth.module').then((m) => m.AuthModule)
      },
    ]
  },
  {
    path: '**', redirectTo: '', pathMatch: 'full'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
