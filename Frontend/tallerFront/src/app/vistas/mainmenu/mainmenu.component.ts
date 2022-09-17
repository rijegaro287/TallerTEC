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

  //Función: Navega hacia el dashboard de empleados
  gestionEmpleado(){
    this.router.navigate(['dashboard']);
  }

  //Función: Navega hacia el dashboard de clientes
  gestionCliente(){
    this.router.navigate(['dashboard2']);
  }

  //Función: Navega hacia el dashboard de citas
    gestionCitas(){
    this.router.navigate(['dashboard3']);
  }
  

}
