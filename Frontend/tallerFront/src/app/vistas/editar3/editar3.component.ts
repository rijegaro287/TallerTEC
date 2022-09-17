import { Component, OnInit } from '@angular/core';
import {Router, ActivatedRoute} from '@angular/router';
import { EmpleadoI } from '../../modelos/empleado.interface';
import { ApiService } from 'src/app/servicios/api/api.service';
import {FormGroup, FormControl, Validators} from '@angular/forms';
import { formatCurrency } from '@angular/common';
import { ListaCitasI } from 'src/app/modelos/listaCitas.interface';
import { ResponseI } from 'src/app/modelos/response.interface';
@Component({
  selector: 'app-editar3',
  templateUrl: './editar3.component.html',
  styleUrls: ['./editar3.component.css']
})

// Componente Editar utilizado para la edición de datos de citas
export class EditarComponent3 implements OnInit {

  constructor(private activerouter:ActivatedRoute, private router:Router, private api:ApiService) { }

  datosCita:ListaCitasI | undefined;
  citaid:any;
  infoStat: boolean = false;
  infoText: any = "";

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
    this.citaid = this.activerouter.snapshot.paramMap.get('id')
    this.api.getSingleCita(this.citaid).subscribe(data =>{
      this.datosCita = data;
      console.log(this.datosCita);
      this.editarForm3.setValue({
        'id': this.citaid,
        'date': this.datosCita.date,
        'time': this.datosCita.time,
        'attendedClient': this.datosCita.attendedClientID as unknown as string,
        'licensePlate': this.datosCita.licensePlate,
        'branchID': this.datosCita.branchID as unknown as string,
        'requiredService': this.datosCita.requiredService as unknown as string,
        'mechanicId': this.datosCita.mechanicID as unknown as string,
        'assistantId': this.datosCita.assistantID as unknown as string,
        'necessaryParts': this.datosCita.necessaryParts as unknown as string
      });

      console.log(this.datosCita);
    })
    console.log(this.citaid)
  }

  salir(){
    this.router.navigate(['mainmenu']);
  }

  patchForm(form:any){
    let idN  = parseInt(form.id);
    let attID = parseInt(form.attendedClient);
    let branchN = parseInt(form.branchID);
    let requiredN = parseInt(form.requiredService);
    let mechN = parseInt(form.mechanicId);
    let assistN = parseInt(form.assistantId);
    console.log(form.necessaryParts);

    let peticion = {ID:idN, Date:form.date, Time:form.time, AttendedClientID:attID, LicensePlate:form.licensePlate,
      BranchID:branchN, RequiredService:requiredN, MechanicID:mechN, AssistantID:assistN, NecessaryParts:form.necessaryParts};
    
      if (idN == this.citaid){

      }
        this.api.patchCita(peticion, form.id).subscribe(data=>{
          console.log(data);
          let dataResponse:ResponseI = data;
          if (dataResponse.status == "Ok"){
            this.infoStat = true;
            this.infoText = "Cita editada con exito";
          }else{
            this.infoStat = true;
            this.infoText = "No se pudo crear";
          }
  
      });
    
    console.log(peticion);
    console.log(form);
  }

  eliminar(form:any){
    let idN = parseInt(form.id);
    if (idN == this.citaid){
      this.api.deleteCita(idN).subscribe(data =>{
        console.log(data);
        let dataResponse:ResponseI = data;
        if (dataResponse.status == "Ok"){
          this.infoStat = true;
          this.infoText = "Cita eliminada con exito";
        }else{
          this.infoStat = true;
          this.infoText = "No se pudo eliminar";
        }
      });
    }
  }

  factura(form:any){
    console.log("factura realizada");
    this.api.getFactura(this.citaid).subscribe(data=>{
      this.infoStat = true;
      this.infoText = "factura creada, revise en su carpeta de reportes";
      console.log(data);
    });
  }

}

