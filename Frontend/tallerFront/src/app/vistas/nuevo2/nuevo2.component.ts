import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ApiService } from 'src/app/servicios/api/api.service';
import { FormGroup, FormControl, Validators, Form } from '@angular/forms';
import { ResponseI } from 'src/app/modelos/response.interface';
import { fromEvent } from 'rxjs';

@Component({
  selector: 'app-nuevo',
  templateUrl: './nuevo2.component.html',
  styleUrls: ['./nuevo2.component.css']
})
export class NuevoComponent2 implements OnInit {
  infoStat: boolean = false;
  infoText: any = "";
  constructor(private activerouter:ActivatedRoute, private router:Router, private api:ApiService) { }

    nuevoForm = new FormGroup({
    id: new FormControl(''),
    name: new FormControl(''),
    lastName: new FormControl(''),
    email: new FormControl(''),
    phoneNumber: new FormControl(''),
    address: new FormControl('')
  });

  ngOnInit(): void {

  }

  postForm(form:any){
    let idN  = form.id as number;
    let phoneN = form.phoneNumber as number;

    let peticion = {ID:idN, Name:form.name, LastName:form.lastName, Email:form.email,
    PhoneNumber:phoneN, Address:form.address};

    this.api.postCliente(peticion).subscribe(data=>{
      console.log(data);
      let dataResponse:ResponseI = data as ResponseI;
      console.log(dataResponse.status);
      if (dataResponse.status == "Ok"){
        this.infoStat = true;
        this.infoText = "Cliente creado";
      }else{
        this.infoStat = true;
        this.infoText = "No se pudo crear";
      }
    })
  }

  salir(){
    this.router.navigate(['dashboard']);
  }

}
