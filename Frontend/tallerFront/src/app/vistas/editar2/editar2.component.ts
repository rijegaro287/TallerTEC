import { Component, OnInit } from '@angular/core';
import {Router, ActivatedRoute} from '@angular/router';
import { ClienteI } from '../../modelos/cliente.interface';
import { ApiService } from 'src/app/servicios/api/api.service';
import {FormGroup, FormControl, Validators} from '@angular/forms';
import { formatCurrency } from '@angular/common';

@Component({
  selector: 'app-editar2',
  templateUrl: './editar2.component.html',
  styleUrls: ['./editar2.component.css']
})
export class EditarComponent2 implements OnInit {

  constructor(private activerouter:ActivatedRoute, private router:Router, private api:ApiService) { }

  datosCliente:ClienteI | undefined;

  editarForm2 = new FormGroup({
    id: new FormControl(''),
    name: new FormControl(''),
    lastName: new FormControl(''),
    email: new FormControl(''),
    phoneNumber: new FormControl(''),
    address: new FormControl('')
  });


  ngOnInit(): void {
    let clienteid = this.activerouter.snapshot.paramMap.get('id')
    this.api.getSingleCliente(clienteid).subscribe(data =>{
      this.datosCliente = data;
      
      this.editarForm2.setValue({
        'id': clienteid,
        'name': this.datosCliente.name,
        'lastName': this.datosCliente.lastName,
        'email': this.datosCliente.email,
        'phoneNumber': this.datosCliente.phoneNumber,
        'address': this.datosCliente.address,
      });

      console.log(this.datosCliente);
    })
    console.log(clienteid)
  }

  salir(){
    this.router.navigate(['mainmenu']);
  }

  postForm(form:any){
    let idN  = form.id as number;
    let ageN = form.age as number;
    this.api.putEmpleado(form).subscribe(data =>{
      console.log(data)
    })
    console.log(form);
  }

  eliminar(){
    //this.api.deleteEmpleado(this.editarForm)
    //let datos:EmpleadoI = this.editarForm.value;
    //this.api.deleteEmpleado(datos).subscribe(data =>{
     // console.log(data);
    //})
    //console.log("eliminar");
  }

}

