import { RouterModule, Routes } from "@angular/router";
import { NgModule } from "@angular/core";
import { LoginComponent } from "./login/login.component";
import { RegisterComponent } from "./register/register.component";
import { ProfileComponent } from "./profile/profile.component";
import { authGuard } from "src/app/core/guards/auth.guard";
import { nouserGuard } from "src/app/core/guards/nouser.guard";
import { ProductscrudComponent } from "./profile/contain/productscrud/productscrud.component";

const routes_auth:Routes = [
  {
    path: 'login', component: LoginComponent, canActivate: [nouserGuard]
  },
  {
    path: 'register', component: RegisterComponent, canActivate: [nouserGuard]
  },
  {
    path: 'profile', component: ProfileComponent,
    canActivate: [authGuard],
    children: [
      {
        path: '', component: ProductscrudComponent
      }
    ]
  },
  {
    path: '**',
    redirectTo: 'login'
  }
]

@NgModule({
  imports: [RouterModule.forChild(routes_auth)],
  exports: [RouterModule],
})
export class AuthRoutingModule { }
