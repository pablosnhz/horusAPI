import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {

  form: FormGroup;

  constructor(fb: FormBuilder, private auth: AuthService, private route: Router) {
    this.form = fb.group({
      nombre: ['', Validators.required],
      email: ['', Validators.required],
      password: ['', Validators.required]
    })
  }

  ngOnInit(): void {

  }

  submitRegister() {
    if (this.form.valid) {
      console.log(this.form.value);
    }
    const { nombre, email, password } = this.form.value;
    this.auth.register(nombre, email, password).subscribe(response => {
      console.log('Register Response:', response);
      this.route.navigate(['auth/login']);
    })
  }
}
