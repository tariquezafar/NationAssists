import { NgModule } from '@angular/core';
import { SharedModule } from './../shared.module';
import { CreateUserComponent } from './create-user.component';
import { RouterModule, Routes } from '@angular/router';

export const router: Routes = [
  {path: '', component: CreateUserComponent}
]

@NgModule({
  declarations: [ CreateUserComponent],
  entryComponents: [],
  imports: [
    SharedModule,
    RouterModule.forChild(router)
  ]
})
export class CreateUserModule { }