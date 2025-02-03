import { HttpClient } from '@angular/common/http';
import { Injectable, signal, WritableSignal } from '@angular/core';
import { tap } from 'rxjs/internal/operators/tap';
import { environment } from 'src/environments/environment.development';
import { register } from 'swiper/element/bundle';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) {
    this.restoreUser();
  }

  apiUserUlr: string = environment.apiEndpoints.myApi;
  $user: WritableSignal<any> = signal(null);
  $userName: WritableSignal<any> = signal(null);
  $userRol: WritableSignal<any> = signal(null);

  login(email: string, password: string) {
    return this.http.post(this.apiUserUlr + '/login', { email, password }).pipe(
      tap((response: any)=> {

        // console.log(`response login: ${JSON.stringify(response)}`);
        const user = { email, token: response.token, userName: response.userName };
        localStorage.setItem('user', JSON.stringify(user));
        this.$user.set(user);
        localStorage.setItem('token', response.token);
      })
    )
  }

  register(nombre: string, email: string, password: string) {
    return this.http.post(this.apiUserUlr + '/registrarse', { nombre, email, password });
  }

  logout() {
    this.$user.set(null);
    localStorage.removeItem('user');
    localStorage.removeItem('token');
    localStorage.removeItem('userName');
  }

  getUser(){
    const token = this.getToken();
    this.http.get(this.apiUserUlr + '/users', {
      headers: { Authorization: `Bearer ${token}` }
    })
    .subscribe(
      (response: any) => {
        if(response){
          // console.log(`response: ${JSON.stringify(response)}`);
          const user = { userName: response.userName, role: response.role };
          // console.log(user);
          this.$userName.set(user);
        } else {
          console.error('Error al obtener el usuario');
        }
    })
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  restoreUser() {
    const token = localStorage.getItem('token');
    if(token) {
      const user = { email: '', token };
      this.$user.set(user);
    }
  }
}
