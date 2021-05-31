import { NgModule } from '@angular/core';
import { SharedModule } from './../shared.module';
import { UserMasterComponent } from './user-master.component';
import { RouterModule, Routes } from '@angular/router';

export const router: Routes = [
  {path: '', component: UserMasterComponent}
]

@NgModule({
  declarations: [ UserMasterComponent],
  entryComponents: [],
  imports: [
    SharedModule,
    RouterModule.forChild(router)
  ]
})
export class UserMasterModule { }