import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {
  constructor() { }

  onProceedcheckout(): any {
    return true;
  }
}
