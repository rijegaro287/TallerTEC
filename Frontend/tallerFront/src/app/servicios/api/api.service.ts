import { Injectable } from '@angular/core';
import { LoginI } from 'src/app/modelos/login.interface';
import { ResponseI } from 'src/app/modelos/response.interface';
import { ListaEmpleadosI } from 'src/app/modelos/listaempleados.interface';
import { EmpleadoI } from "../../modelos//empleado.interface";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ListaClientesI } from 'src/app/modelos/listaClientes.interface';
import { ListaCitasI } from 'src/app/modelos/listaCitas.interface';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  url: string = "https://localhost:3456/";

  constructor(private http: HttpClient) { }

  loginID(form: LoginI){
    console.log(form);
    let direccion = this.url + "login";
    return this.http.post(direccion, form);
  }

  getAllEmpleados(): Observable<ListaEmpleadosI[]> {
    let direccion = this.url + "employee/get_all";

    return this.http.get<ListaEmpleadosI[]>(direccion);
  }

  addEmpleado() {
    let direccion = this.url + "employee/add";
    return this.http.post(direccion, {
      "newEmployee": {
        "ID": 26,
        "Name": "Sashiia",
        "LastName": "Varelan",
        "Email": "shashia2@gmail.com",
        "BirthDate": "01/01/1980",
        "Age": 19,
        "Position": "Bottom",
        "StartingDate": "01/01/2020"
      },
      "password": "1234567"
    });
  }


  getAllClientes() {
    let direccion = this.url + "client/get_all";
    return this.http.get<ListaClientesI[]>(direccion);
  }

<<<<<<< HEAD
  getSingleEmpleado(id: any) {
    let direccion = this.url + "empleados?id=" + id;
    return this.http.get<EmpleadoI>(direccion);
  }    
  
=======
  getAllCitas() {
    let direccion = this.url + "appointment/get_all";

    return this.http.get<ListaCitasI[]>(direccion);
  }
>>>>>>> c8f71b87f768fd5afcb5ec6676b05db73f69c446

}


