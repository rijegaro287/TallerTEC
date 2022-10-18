import { Injectable } from '@angular/core';
import { LoginI } from 'src/app/modelos/login.interface';
import { ResponseI } from 'src/app/modelos/response.interface';
import { ListaEmpleadosI } from 'src/app/modelos/listaempleados.interface';
import { EmpleadoI } from "../../modelos//empleado.interface";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ListaClientesI } from 'src/app/modelos/listaClientes.interface';
import { ListaCitasI } from 'src/app/modelos/listaCitas.interface';
import { NewEmpleadoI } from 'src/app/modelos/newEmpleado.interface';
import { ClienteI } from 'src/app/modelos/cliente.interface';
import { CitaI } from 'src/app/modelos/citas.interface';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  url: string = "https://localhost:7279/";

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

  getAllClientes() {
    let direccion = this.url + "client/get_all";
    return this.http.get<ListaClientesI[]>(direccion);
  }

  getSingleEmpleado(id: any) {
    let direccion = this.url + "employee/get/" + id;
    return this.http.get<EmpleadoI>(direccion);
  }    
  
  getSingleCliente(id: any) {
    let direccion = this.url + "client/get/" + id;
    return this.http.get<ClienteI>(direccion);
  }    
  
  getSingleCita(id: any) {
    let direccion = this.url + "appointment/get/" + id;
    return this.http.get<ListaCitasI>(direccion);
  }    
  

  getAllCitas() {
    let direccion = this.url + "appointment/get_all";

    return this.http.get<ListaCitasI[]>(direccion);
  }


  postEmpleado(form:any){

    let direccion = this.url + "employee/add";

    return this.http.post(direccion, form);
  }

  postCliente(form:any){

    let direccion = this.url + "client/add";

    return this.http.post(direccion, form);
  }

  postCita(form:any){

    let direccion = this.url + "appointment/add";

    return this.http.post(direccion, form);
  }

  patchEmpleado(form:any, id:string){
    let direccion = this.url + 'employee/update/'+id;
    return this.http.patch<ResponseI>(direccion, form)
  }

  patchCliente(form:any, id:string){
    let direccion = this.url + 'client/update/'+id;
    return this.http.patch<ResponseI>(direccion, form)
  }

  patchCita(form:any, id:string){
    let direccion = this.url + 'appointment/update/'+id;
    return this.http.patch<ResponseI>(direccion, form)
  }

  deleteEmpleado(id:any):Observable<ResponseI>{
    let direccion = this.url + 'employee/delete/'+id;
    return this.http.delete<ResponseI>(direccion);
  }

  deleteCliente(id:any):Observable<ResponseI>{
    let direccion = this.url + 'client/delete/'+id;
    return this.http.delete<ResponseI>(direccion);
  }

  deleteCita(id:any):Observable<ResponseI>{
    let direccion = this.url + 'appointment/delete/'+id;
    return this.http.delete<ResponseI>(direccion);
  }

  getFactura(id:any){
    let direccion = this.url + 'appointment/generate_bill/'+id;
    return this.http.get<ResponseI>(direccion);
  }

  getReporteVehiculos(){
    let direccion = this.url + 'report/get_top_clients';
    return this.http.get<ResponseI>(direccion);
  }

  getReporteClientes(){
    let direccion = this.url + 'report/get_top_vehicles';
    return this.http.get<ResponseI>(direccion);
  }

}


