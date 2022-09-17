import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router'
import { ListaCitasI } from 'src/app/modelos/listaCitas.interface';
import { ApiService } from 'src/app/servicios/api/api.service';

import { ListaEmpleadosI } from '../../modelos/listaempleados.interface'

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard3.component.html',
  styleUrls: ['./dashboard3.component.css']
})


// Componente Dashboard utilizado pra la generación de la tabla para la muestra de citas 
export class DashboardComponent3 implements OnInit {

  citas: ListaCitasI[] = [];
  
  constructor(private api:ApiService, private router:Router) { 
  }

  //Funcion: Recibe información sobre citas 
  ngOnInit(): void {
    this.api.getAllCitas().subscribe(data =>{
      this.citas = data;
      console.log(data)
    })
  }

  //Funcion: Editar una cita segun su id
  //Entrada: id 
  editarCita(id: any){
    this.router.navigate(['editar3', id])
  }

  //Funcion: Genera nueva cita 
  nuevoCita(){
    this.router.navigate(['nuevo3']);

  }
}
