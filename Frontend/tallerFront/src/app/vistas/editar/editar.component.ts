import { Component, OnInit } from '@angular/core';
import {Router, ActivatedRoute} from '@angular/router';
import { EmpleadoI } from '../../modelos/empleado.interface';
import { ApiService } from 'src/app/servicios/api/api.service';
import {FormGroup, FormControl, Validators} from '@angular/forms';
import { ResponseI } from 'src/app/modelos/response.interface';

@Component({
  selector: 'app-editar',
  templateUrl: './editar.component.html',
  styleUrls: ['./editar.component.css']
})
export class EditarComponent implements OnInit {

  constructor(private activerouter:ActivatedRoute, private router:Router, private api:ApiService) { }

  datosEmpleado:EmpleadoI | undefined;
  empleadoid:any;
  infoStat: boolean = false;
  infoText: any = "";

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
    this.empleadoid = this.activerouter.snapshot.paramMap.get('id')
    //let token = this.getToken;
    this.api.getSingleEmpleado(this.empleadoid).subscribe(data =>{
      this.datosEmpleado = data;
      
      this.editarForm.setValue({
        'id': this.empleadoid,
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
    console.log(this.empleadoid)
  }

  patchForm(form:any){
    let idN = parseInt(form.id);
    let ageN = parseInt(form.age);
    let peticion = {ID:idN, Name:form.name, LastName:form.lastName, Email:form.email,
      BirthDate:form.birthDate, Age:ageN, Position:form.position, StartingDate:form.startingDate}
    
    if (idN == this.empleadoid){

    }
      this.api.patchEmpleado(peticion, form.id).subscribe(data=>{
        console.log(data);
        let dataResponse:ResponseI = data;
        if (dataResponse.status == "Ok"){
          this.infoStat = true;
          this.infoText = "Empleado editado con exito";
        }else{
          this.infoStat = true;
          this.infoText = "No se pudo crear";
        }

    });
  }

  eliminar(form:any){
    let idN = parseInt(form.id);
    if (idN == this.empleadoid){
      this.api.deleteEmpleado(idN).subscribe(data =>{
        console.log(data);
        let dataResponse:ResponseI = data;
        if (dataResponse.status == "Ok"){
          this.infoStat = true;
          this.infoText = "Empleado eliminado con exito";
        }else{
          this.infoStat = true;
          this.infoText = "No se pudo eliminar";
        }
      });
    }
  }

  salir(){
    this.router.navigate(['mainmenu']);
  }

}

