import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';
import { RegisterResponse } from '../login.model';

@Component({
  selector: 'app-register.page',
  templateUrl: './register.page.component.html',
  styleUrls: ['./register.page.component.css']
})
export class RegisterPageComponent implements OnInit {

  errors: string;
  constructor(private http: HttpClient, @Inject('API_URL') private apiUrl: string,
    private router: Router,
    private authSvc: AuthService) { }

  ngOnInit() {
  }

  register(userName, passWord, passWord2) {
    console.log(userName.value);
    console.log(passWord.value);
    console.log(passWord2.value);
    //if (passWord.value != passWord2.value) {
    //  this.errors = "The password and confirmation password do not match.";
    //}
    //else {
      this.http.post(this.apiUrl + 'authentication/register', { email: userName.value, password:passWord.value, confirmPassword: passWord2.value})
        .subscribe((response: RegisterResponse) => {
          this.http.post(this.apiUrl + 'authentication/confirm', { email: userName.value, confirmationToken: response.confirmationToken })
            .subscribe(() => {
              this.router.navigateByUrl('');
            }, error => this.errors = error);
        }, error => this.errors = error);
    //}
  }
}
