import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './vistas/login/login.component';
import { DashboardComponent } from './vistas/dashboard/dashboard.component';
import { DashboardComponent2 } from './vistas/dashboard2/dashboard2.component';
import { DashboardComponent3 } from './vistas/dashboard3/dashboard3.component';
import { NuevoComponent } from './vistas/nuevo/nuevo.component';
import { EditarComponent } from './vistas/editar/editar.component';
import { MainmenuComponent } from './vistas/mainmenu/mainmenu.component';
import { NuevoComponent2 } from './vistas/nuevo2/nuevo2.component';

const routes: Routes = [
  {path:'', redirectTo:'login', pathMatch:'full'},
  {path:'login', component:LoginComponent},
  {path:'dashboard', component:DashboardComponent},
  {path:'dashboard2', component:DashboardComponent2},
  {path:'dashboard3', component:DashboardComponent3},
  {path:'nuevo', component:NuevoComponent},
  {path:'nuevo2', component:NuevoComponent2},
  //{path:'nuevo3', component:NuevoComponent},
  {path:'editar/:id', component:EditarComponent},
  {path:'mainmenu', component:MainmenuComponent}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
export const routingComponents = [LoginComponent, DashboardComponent, DashboardComponent2, DashboardComponent3, 
  NuevoComponent, NuevoComponent2,EditarComponent, MainmenuComponent]
