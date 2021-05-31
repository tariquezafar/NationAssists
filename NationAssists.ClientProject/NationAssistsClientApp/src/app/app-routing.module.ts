/// <reference path="manageservices/manageservicesmodule.ts" />
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('./dashboard/dashboard.module').then(m => m.DashboardModule)
  },
  {
    path: 'user-master',
    loadChildren: () => import('./user-master/user-master.module').then(m => m.UserMasterModule)
  },
  {
    path: 'create-user',
    loadChildren: () => import('./create-user/create-user.module').then(m => m.CreateUserModule)
  },
  {
    path: 'admin/Services/SubCategroy',
    loadChildren: () => import('./manageservices/manageServicesModule').then(m => m.manageServicesModule)
  },
]

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot(routes, {
      scrollPositionRestoration: 'enabled'
    })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
