import { NgModule } from '@angular/core';
import { SharedModule } from './../shared.module';
import { DashboardMenuComponent } from './dashboard-menu.component';
@NgModule({
  declarations: [DashboardMenuComponent],
  imports: [
    SharedModule,
  ],
  exports: [DashboardMenuComponent]
})
export class DashboardMenuModule { }
