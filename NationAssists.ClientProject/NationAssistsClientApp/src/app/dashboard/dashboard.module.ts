import { NgModule } from '@angular/core';
import { SharedModule } from './../shared.module';
import { DashboardComponent } from './dashboard.component';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { ChartsModule } from 'ng2-charts';

export const router: Routes = [
  {path: '', component: DashboardComponent}
]

@NgModule({
  declarations: [ DashboardComponent],
  entryComponents: [],
  imports: [
    SharedModule,
    FormsModule,
    ChartsModule,
    RouterModule.forChild(router)
  ]
})
export class DashboardModule { }