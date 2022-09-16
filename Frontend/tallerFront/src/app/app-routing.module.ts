import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './vistas/login/login.component';
import { DashboardComponent } from './vistas/dashboard/dashboard.component';
import { DashboardComponent2 } from './vistas/dashboard2/dashboard2.component';
import { DashboardComponent3 } from './vistas/dashboard3/dashboard3.component';
import { NuevoComponent } from './vistas/nuevo/nuevo.component';
import { EditarComponent } from './vistas/editar/editar.component';
import { EditarComponent2 } from './vistas/editar2/editar2.component';
import { EditarComponent3 } from './vistas/editar3/editar3.component';
import { MainmenuComponent } from './vistas/mainmenu/mainmenu.component';
import { NuevoComponent2 } from './vistas/nuevo2/nuevo2.component';
import { NuevoComponent3 } from './vistas/nuevo3/nuevo3.component';

const routes: Routes = [
  {path:'', redirectTo:'login', pathMatch:'full'},
  {path:'login', component:LoginComponent},
  {path:'dashboard', component:DashboardComponent},
  {path:'dashboard2', component:DashboardComponent2},
  {path:'dashboard3', component:DashboardComponent3},
  {path:'nuevo', component:NuevoComponent},
  {path:'nuevo2', component:NuevoComponent2},
  {path:'nuevo3', component:NuevoComponent3},
  {path:'editar/:id', component:EditarComponent},
  {path:'editar2/:id', component:EditarComponent2},
  {path:'editar3/:id', component:EditarComponent3},
  {path:'mainmenu', component:MainmenuComponent}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
export const routingComponents = [LoginComponent, DashboardComponent, DashboardComponent2, DashboardComponent3, 
<<<<<<< HEAD
  NuevoComponent, NuevoComponent2, NuevoComponent3,EditarComponent, EditarComponent2, EditarComponent3, MainmenuComponent]
=======
  NuevoComponent, NuevoComponent2, NuevoComponent3, EditarComponent, MainmenuComponent]
>>>>>>> a554c39dbd1b394cd9517b073082cb165afaf683
