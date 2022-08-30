import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddEmployeeComponent } from './add-employee/add-employee.component';
import { EditEmployeeComponent } from './edit-employee/edit-employee.component';
import { EmployeeListComponent } from './employee-list/employee-list.component';
import { MainComponent } from './main/main.component';

const routes: Routes = [

  {
    path: '',
    component: MainComponent,
    children: [
      {
        path: '',
        component: EmployeeListComponent,
        pathMatch: 'full',
      },
      {
        path: 'addEmployee',
        component: AddEmployeeComponent,
      },
      {
        path: 'editEmployee/:id',
        component: EditEmployeeComponent,
      },
    ],
  },

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ComponentsRoutingModule { }
