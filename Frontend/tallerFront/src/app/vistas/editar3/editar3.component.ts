import { Component, OnInit } from '@angular/core';
import {Router, ActivatedRoute} from '@angular/router';
import { EmpleadoI } from '../../modelos/empleado.interface';
import { ApiService } from 'src/app/servicios/api/api.service';
import {FormGroup, FormControl, Validators} from '@angular/forms';
import { formatCurrency } from '@angular/common';
import { CitaI } from 'src/app/modelos/citas.interface';

@Component({
  selector: 'app-editar3',
  templateUrl: './editar3.component.html',
  styleUrls: ['./editar3.component.css']
})

// Componente Editar utilizado para la edición de datos de citas
export class EditarComponent3 implements OnInit {

  constructor(private activerouter:ActivatedRoute, private router:Router, private api:ApiService) { }

  datosCita:CitaI | undefined;

  editarForm3 = new FormGroup({
    id: new FormControl(''),
    date: new FormControl(''),
    time: new FormControl(''),
    attendedClient: new FormControl(''),
    licensePlate: new FormControl(''),
    branchID: new FormControl(''),
    requiredService: new FormControl(''),
    mechanicId: new FormControl(''),
    assistantId: new FormControl(''),
    necessaryParts: new FormControl('')
  });

  getToken(){
    return localStorage.getItem('token');
  }

  ngOnInit(): void {
    let citaid = this.activerouter.snapshot.paramMap.get('id')
    this.api.getSingleCita(citaid).subscribe(data =>{
      this.datosCita = data;
      
      this.editarForm3.setValue({
        'id': citaid,
        'date': this.datosCita.Date,
        'time': this.datosCita.Time,
        'attendedClient': this.datosCita.AttendedClientID as unknown as string,
        'licensePlate': this.datosCita.LicensePlate,
        'branchID': this.datosCita.BranchID as unknown as string,
        'requiredService': this.datosCita.RequiredService as unknown as string,
        'mechanicId': this.datosCita.MechanicID as unknown as string,
        'assistantId': this.datosCita.AssistantID as unknown as string,
        'necessaryParts': this.datosCita.NecessaryParts as unknown as string
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

