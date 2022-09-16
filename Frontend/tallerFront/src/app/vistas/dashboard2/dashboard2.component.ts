import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router'
import { ListaClientesI } from 'src/app/modelos/listaClientes.interface';
import { ApiService } from 'src/app/servicios/api/api.service';

import { ListaEmpleadosI } from '../../modelos/listaempleados.interface'

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard2.component.html',
  styleUrls: ['./dashboard2.component.css']
})


export class DashboardComponent2 implements OnInit {

  clientes: ListaClientesI[] = [];
  
  constructor(private api:ApiService, private router:Router) { 
  }

  ngOnInit(): void {
    this.api.getAllEmpleados().subscribe(data =>{

      console.log(data)
    })
  }

  editarCliente(id: any){
    this.router.navigate(['editar', id])
  }

  nuevoCliente(){
    this.router.navigate(['nuevo']);

  }
}


