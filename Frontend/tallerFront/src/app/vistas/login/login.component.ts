import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ApiService } from 'src/app/servicios/api/api.service';
import { LoginI } from 'src/app/modelos/login.interface';
import { ResponseI } from 'src/app/modelos/response.interface';
import { Router } from '@angular/router';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  public loginForm = new FormGroup({
    email: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required)
  })

  constructor(private api: ApiService, private router: Router) { }

  errorStat: boolean = false;
  errormsj: any = "";

  ngOnInit(): void {

  }

  checkLocalStorage() {
    if (localStorage.getItem('token')) {
      this.router.navigate(['mainmenu'])
    }
  }

  onLogin(form: any) {
    this.api.loginID(form).subscribe(data => {
      console.log(form);
      console.log(data);

    });
  }
}

