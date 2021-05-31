import { NgModule } from '@angular/core';
import { SharedModule } from './../shared.module';
import { DashboardHeaderComponent } from './dashboard-header.component';

@NgModule({
  declarations: [DashboardHeaderComponent],
  imports: [
    SharedModule,
  ],
  exports: [DashboardHeaderComponent]
})
export class DashboardHeaderModule { }

