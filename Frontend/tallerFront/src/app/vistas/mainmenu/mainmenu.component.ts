import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-mainmenu',
  templateUrl: './mainmenu.component.html',
  styleUrls: ['./mainmenu.component.css']
})
export class MainmenuComponent implements OnInit {

  constructor(private router:Router) { }

  ngOnInit(): void {
  }

  gestionEmpleado(){
    this.router.navigate(['dashboard']);
  }

  gestionCliente(){
    this.router.navigate(['dashboard2']);
  }

  gestionCitas(){
    this.router.navigate(['dashboard']);
  }
  
  gestionFacturacion(){
    this.router.navigate(['dashboard']);
  }

}
