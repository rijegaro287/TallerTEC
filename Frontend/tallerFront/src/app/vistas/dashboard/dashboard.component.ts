import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router'
import { ApiService } from 'src/app/servicios/api/api.service';

import { ListaEmpleadosI } from '../../modelos/listaempleados.interface'

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})

// Componente Dashboard utilizado para la generación de la tabla para la muestra de empleados
export class DashboardComponent implements OnInit {

  empleados: ListaEmpleadosI[] = [];
  
  constructor(private api:ApiService, private router:Router) { 
  }

  //Función: Recibe información sobre empleados
  ngOnInit(): void {
    this.api.getAllEmpleados().subscribe(data =>{
      this.empleados = data;
      console.log(data)
    })
  }

  //Función: Editar empleado según su id
  //Entrada: id 
  editarEmpleado(id: any){
    this.router.navigate(['editar', id])
  }

  //Función: Crear nuevo empleado 
  nuevoEmpleado(){
    this.router.navigate(['nuevo']);

  }
}
