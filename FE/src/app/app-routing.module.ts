import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/guards/authGuard';

const routes: Routes = [

  {
    path: '',
    loadChildren: () =>
      import('../app/auth/auth.module').then((m) => m.AuthModule)
  },

  {
    path: 'employees',
    loadChildren: () =>
      import('../app/components/components.module').then((m) => m.ComponentsModule),
      canActivate: [AuthGuard],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
