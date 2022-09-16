import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ApiService } from 'src/app/servicios/api/api.service';
import { FormGroup, FormControl, Validators, Form } from '@angular/forms';
import { fromEvent } from 'rxjs';

@Component({
  selector: 'app-nuevo',
  templateUrl: './nuevo.component.html',
  styleUrls: ['./nuevo.component.css']
})
export class NuevoComponent implements OnInit {

  constructor(private activerouter:ActivatedRoute, private router:Router, private api:ApiService) { }

    nuevoForm = new FormGroup({
    id: new FormControl(''),
    name: new FormControl(''),
    lastName: new FormControl(''),
    email: new FormControl(''),
    birthDate: new FormControl(''),
    age: new FormControl(''),
    position: new FormControl(''),
    startingDate: new FormControl(''),
    password: new FormControl('')
  });

  ngOnInit(): void {

  }

  postForm(form:any){
    let idN  = form.id as number;
    let ageN = form.age as number;
    let peticion = {newEmployee:{ID:idN, Name:form.name, LastName:form.lastName, Email:form.email,
    BirthDate:form.birthDate, Age:ageN, Position:form.position, StartingDate:form.startingDate}, Password:form.password}
    this.api.postEmpleado(peticion).subscribe(data=>{
      console.log(data);
    })
  }

  salir(){
    this.router.navigate(['dashboard']);
  }

}
