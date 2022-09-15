import { Injectable } from '@angular/core';
import { LoginI } from 'src/app/modelos/login.interface';
import { ResponseI } from 'src/app/modelos/response.interface';
import { ListaEmpleadosI } from 'src/app/modelos/listaempleados.interface';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  url:string = "https://localhost:3456/";

  constructor(private http:HttpClient) { }

  loginID(form:LoginI){

    let direccion = this.url + "login";
    return this.http.post(direccion, form);
  }

  getAllEmpleados():Observable<ListaEmpleadosI[]>{
    let direccion = this.url + "employee/get_all";

    return this.http.get<ListaEmpleadosI[]>(direccion);
  }

}
