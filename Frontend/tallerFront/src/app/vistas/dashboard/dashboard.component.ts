import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router'
import { ApiService } from 'src/app/servicios/api/api.service';

import { ListaEmpleadosI } from '../../modelos/listaempleados.interface'

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})


export class DashboardComponent implements OnInit {

  empleados: ListaEmpleadosI[] = [];
  
  constructor(private api:ApiService, private router:Router) { 
  }

  ngOnInit(): void {
    this.api.getAllEmpleados().subscribe(data =>{

      console.log(data)
    })
  }

  editarEmpleado(id: any){
    this.router.navigate(['editar', id])
  }

  nuevoEmpleado(){
    this.router.navigate(['nuevo']);

  }
}
