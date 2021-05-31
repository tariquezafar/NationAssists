import { NgModule } from '@angular/core';
import { SharedModule } from './../shared.module';
import { ManageservicesComponent } from './manageservices.component';
import { RouterModule, Routes } from '@angular/router';

export const router: Routes = [
  { path: '', component: ManageservicesComponent }
]

@NgModule({
  declarations: [ManageservicesComponent],
  entryComponents: [],
  imports: [
    SharedModule,
    RouterModule.forChild(router)
  ],
  exports: [ManageservicesComponent]
  
})
export class manageServicesModule { }
