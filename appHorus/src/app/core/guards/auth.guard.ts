import { Injectable } from "@angular/core";
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { AuthService } from "src/app/services/auth.service";

@Injectable({
  providedIn: 'root'
})

export class authGuard implements CanActivate {

constructor(private authService: AuthService, private router: Router) {}

canActivate(route:ActivatedRouteSnapshot, state:RouterStateSnapshot){

    if(!this.authService.$user()) {
      return this.router.parseUrl('/auth');
    } else {
      return true;
    }
  }
}
