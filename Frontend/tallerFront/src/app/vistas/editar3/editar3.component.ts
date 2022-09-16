import { Component, OnInit } from '@angular/core';
import {Router, ActivatedRoute} from '@angular/router';
import { EmpleadoI } from '../../modelos/empleado.interface';
import { ApiService } from 'src/app/servicios/api/api.service';
import {FormGroup, FormControl, Validators} from '@angular/forms';
import { formatCurrency } from '@angular/common';

@Component({
  selector: 'app-editar3',
  templateUrl: './editar3.component.html',
  styleUrls: ['./editar3.component.css']
})
export class EditarComponent3 implements OnInit {

  constructor(private activerouter:ActivatedRoute, private router:Router, private api:ApiService) { }

  datosCita:EmpleadoI | undefined;

  editarForm3 = new FormGroup({
    id: new FormControl(''),
    name: new FormControl(''),
    lastName: new FormControl(''),
    email: new FormControl(''),
    birthDate: new FormControl(''),
    age: new FormControl(''),
    position: new FormControl(''),
    startingDate: new FormControl('')
  });

  getToken(){
    return localStorage.getItem('token');
  }

  ngOnInit(): void {
    let citaid = this.activerouter.snapshot.paramMap.get('id')

    this.api.getSingleEmpleado(citaid).subscribe(data =>{
      this.datosCita = data;
      
      this.editarForm3.setValue({
        'id': citaid,
        'name': this.datosCita.name,
        'lastName': this.datosCita.lastName,
        'email': this.datosCita.email,
        'birthDate': this.datosCita.birthDate,
        'age': this.datosCita.age as unknown as string,
        'position': this.datosCita.position,
        'startingDate': this.datosCita.startingDate
      });

      console.log(this.datosCita);
    })
    console.log(citaid)
  }

  salir(){
    this.router.navigate(['mainmenu']);
  }

  postForm(form:any){
    let idN  = form.id as number;
    let ageN = form.age as number;
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

