import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthResponse } from '../auth.model';
import { AuthService } from '../auth.service';
import { Login } from '../login.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.page.component.html',
  styleUrls: ['./login.page.component.css']
})
export class LoginPageComponent implements OnInit {

  loginData = new Login();

  constructor(private http: HttpClient, @Inject('API_URL') private apiUrl: string,
    private router: Router,
    private authSvc: AuthService ) { }

  ngOnInit() {
  }

  logIn(userName, password) {
    this.loginData = {
      email: userName.value,
      password: password.value
    }
    this.http.post(this.apiUrl + 'authentication/login', this.loginData)
      .subscribe((response: AuthResponse) => {
        this.authSvc.saveToken(response.token);
        this.router.navigateByUrl('matches');
      }, error => console.error(error));
  }

}
