import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit{
  constructor(private auth: AuthService, private route: Router) { }

  ngOnInit(): void {
    this.auth.getUser();
  }

  userRol$ = this.auth.$userRol;
  userNames$ = this.auth.$userName;

  logout() {
    this.auth.logout();
    this.route.navigate(['/']);
  }
}
