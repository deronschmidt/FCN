import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticateService } from '../_services/authenticate.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  model: any = {};

  errorMessage: string;
  constructor(private router: Router, private authService: AuthenticateService) { }

  ngOnInit() {
    sessionStorage.removeItem('UserName');
    sessionStorage.clear();
  }
  login() {
    this.authService.Login(this.model).subscribe(
      data => {
        if (data.active == true) {
          this.router.navigate(['/dashboard']);
        }
        else {
          this.authService.logout();
          this.errorMessage = data.Message;
        }
      },
      error => {
        this.authService.logout();
        this.errorMessage = error.message;
      });
  };
}     