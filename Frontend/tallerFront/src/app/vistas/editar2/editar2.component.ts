import { Component, OnInit } from '@angular/core';
import {Router, ActivatedRoute} from '@angular/router';
import { ClienteI } from '../../modelos/cliente.interface';
import { ApiService } from 'src/app/servicios/api/api.service';
import {FormGroup, FormControl, Validators} from '@angular/forms';
import { ResponseI } from 'src/app/modelos/response.interface';
import { formatCurrency } from '@angular/common';

@Component({
  selector: 'app-editar2',
  templateUrl: './editar2.component.html',
  styleUrls: ['./editar2.component.css']
})

// Componente Editar utilizado para la edición de datos de clientes
export class EditarComponent2 implements OnInit {

  constructor(private activerouter:ActivatedRoute, private router:Router, private api:ApiService) { }

  datosCliente:ClienteI | undefined;
  clienteid:any;
  infoStat: boolean = false;
  infoText: any = "";

  editarForm2 = new FormGroup({
    id: new FormControl(''),
    name: new FormControl(''),
    lastName: new FormControl(''),
    email: new FormControl(''),
    phoneNumber: new FormControl(''),
    address: new FormControl('')
  });


  ngOnInit(): void {
    this.clienteid = this.activerouter.snapshot.paramMap.get('id')
    this.api.getSingleCliente(this.clienteid).subscribe(data =>{
      this.datosCliente = data;
      
      this.editarForm2.setValue({
        'id': this.clienteid,
        'name': this.datosCliente.name,
        'lastName': this.datosCliente.lastName,
        'email': this.datosCliente.email,
        'phoneNumber': this.datosCliente.phoneNumber,
        'address': this.datosCliente.address,
      });

      console.log(this.datosCliente);
    })
    console.log(this.clienteid)
  }

  salir(){
    this.router.navigate(['mainmenu']);
  }

  patchForm(form:any){
    let idN  = form.id as number;
    let phoneN = form.phoneNumber as number;

    let peticion = {ID:idN, Name:form.name, LastName:form.lastName, Email:form.email,
    PhoneNumber:phoneN, Address:form.address};
    
    if (idN == this.clienteid){

    }
      this.api.patchCliente(peticion, form.id).subscribe(data=>{
        console.log(data);
        let dataResponse:ResponseI = data;
        if (dataResponse.status == "Ok"){
          this.infoStat = true;
          this.infoText = "Cliente editado con exito";
        }else{
          this.infoStat = true;
          this.infoText = "No se pudo crear";
        }

    });


  }

  eliminar(form:any){
    
      let idN = parseInt(form.id);
      if (idN == this.clienteid){
        this.api.deleteEmpleado(idN).subscribe(data =>{
          console.log(data);
          let dataResponse:ResponseI = data;
          if (dataResponse.status == "Ok"){
            this.infoStat = true;
            this.infoText = "Cliente eliminado con exito";
          }else{
            this.infoStat = true;
            this.infoText = "No se pudo eliminar";
          }
        });
      }
    
  }

}

