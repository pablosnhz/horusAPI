import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  form: FormGroup;

  constructor(private authService: AuthService, fb: FormBuilder, private route: Router) {
    this.form = fb.group({
      email: ['', Validators.required],
      password: ['', Validators.required]
    })
  }

  ngOnInit(): void {

  }

  submitLogin() {
    if (this.form.valid) {
      const { email, password } = this.form.value;
      console.log('accedio el usuario:', { email, password });

      this.authService.login(email, password).subscribe(response => {
        if(response){
          sessionStorage.setItem('token', (response as any).token);
          this.route.navigate(['/']);
        }
        // console.log('Login Response:', response);
        console.log('token response:', response.token);
      });
    } else {
      console.log('error');
    }
  }


}
