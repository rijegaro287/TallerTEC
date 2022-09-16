import { Component, OnInit } from '@angular/core';
import {Router, ActivatedRoute} from '@angular/router';
import { EmpleadoI } from '../../modelos/empleado.interface';
import { ApiService } from 'src/app/servicios/api/api.service';
import {FormGroup, FormControl, Validators} from '@angular/forms';

@Component({
  selector: 'app-editar',
  templateUrl: './editar.component.html',
  styleUrls: ['./editar.component.css']
})
export class EditarComponent implements OnInit {

  constructor(private activerouter:ActivatedRoute, private router:Router, private api:ApiService) { }

  datosEmpleado:EmpleadoI | undefined;

  editarForm = new FormGroup({
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
    let empleadoid = this.activerouter.snapshot.paramMap.get('id')
    let token = this.getToken;
    this.api.getSingleEmpleado(empleadoid).subscribe(data =>{
      this.datosEmpleado = data;
      
      this.editarForm.setValue({
        'id': empleadoid,
        'name': this.datosEmpleado.name,
        'lastName': this.datosEmpleado.lastName,
        'email': this.datosEmpleado.email,
        'birthDate': this.datosEmpleado.birthDate,
        'age': this.datosEmpleado.age as unknown as string,
        'position': this.datosEmpleado.position,
        'startingDate': this.datosEmpleado.startingDate
      });

      console.log(this.datosEmpleado);
    })
    console.log(empleadoid)
  }

  salir(){
    this.router.navigate(['mainmenu']);
  }

}

