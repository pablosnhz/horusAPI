import { Injectable } from "@angular/core";
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { AuthService } from "src/app/services/auth.service";

@Injectable({
  providedIn: 'root'
})

export class nouserGuard implements CanActivate {

constructor(private auth: AuthService, private router: Router) {}

canActivate(route:ActivatedRouteSnapshot, state:RouterStateSnapshot){

    if(this.auth.$user()) {
      return this.router.parseUrl('/');
    } else {
      return true;
    }
  }
}
