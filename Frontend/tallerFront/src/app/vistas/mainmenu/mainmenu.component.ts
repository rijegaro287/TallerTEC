import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from 'src/app/servicios/api/api.service';

@Component({
  selector: 'app-mainmenu',
  templateUrl: './mainmenu.component.html',
  styleUrls: ['./mainmenu.component.css']
})
export class MainmenuComponent implements OnInit {

  constructor(private router:Router, private api: ApiService) { }
  infoStat: boolean = false;
  infoText: any = "";

  ngOnInit(): void {
  }

  //Función: Navega hacia el dashboard de empleados
  gestionEmpleado(){
    this.router.navigate(['dashboard']);
  }

  //Función: Navega hacia el dashboard de clientes
  gestionCliente(){
    this.router.navigate(['dashboard2']);
  }

  //Función: Navega hacia el dashboard de citas
    gestionCitas(){
    this.router.navigate(['dashboard3']);
  }
  reporteClientes(){
    console.log("Reporte top clientes generado");
    this.api.getReporteClientes();
    this.infoStat = true;
    this.infoText = "Reporte de top de clientes creado, revise en su carpeta de reportes";

  }

  reporteVehiculo(){
    console.log("Reporte top vehiculos generado");
    this.api.getReporteVehiculos();
    this.infoStat = true;
    this.infoText = "Reporte de top de vehiculos creado, revise en su carpeta de reportes";
  }

}
