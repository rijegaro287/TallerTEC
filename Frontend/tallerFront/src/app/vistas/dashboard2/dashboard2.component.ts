import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router'
import { ListaClientesI } from 'src/app/modelos/listaClientes.interface';
import { ApiService } from 'src/app/servicios/api/api.service';


@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard2.component.html',
  styleUrls: ['./dashboard2.component.css']
})

// Componente Dashboard utilizado pra la generación de la tabla para la muestra de clientes
export class DashboardComponent2 implements OnInit {

  clientes: ListaClientesI[] = [];
  
  constructor(private api:ApiService, private router:Router) { 
  }

  //Funcion: Recibe información sobre citas   
  ngOnInit(): void {
    this.api.getAllClientes().subscribe(data =>{
      this.clientes = data;
      console.log(data)
    })
  }

  //Funcion: Editar cliente segun su id
  //Entrada: id
  editarCliente(id: any){
    this.router.navigate(['editar2', id])
  }

  //Funcion: Genera nuevo cliente
  nuevoCliente(){
    this.router.navigate(['nuevo2']);

  }
}


