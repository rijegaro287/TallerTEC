import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './vistas/login/login.component';
import { DashboardComponent } from './vistas/dashboard/dashboard.component';
import { DashboardComponent2 } from './vistas/dashboard2/dashboard2.component';
import { NuevoComponent } from './vistas/nuevo/nuevo.component';
import { EditarComponent } from './vistas/editar/editar.component';
import { MainmenuComponent } from './vistas/mainmenu/mainmenu.component';

const routes: Routes = [
  {path:'', redirectTo:'login', pathMatch:'full'},
  {path:'login', component:LoginComponent},
  {path:'dashboard', component:DashboardComponent},
  {path:'dashboard2', component:DashboardComponent2},
  {path:'nuevo', component:NuevoComponent},
  {path:'editar', component:EditarComponent},
  {path:'mainmenu', component:MainmenuComponent}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
export const routingComponents = [LoginComponent, DashboardComponent, DashboardComponent2,NuevoComponent, EditarComponent, MainmenuComponent]
