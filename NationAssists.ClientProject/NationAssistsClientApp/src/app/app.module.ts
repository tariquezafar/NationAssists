import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { SharedModule } from './shared.module';
import { AppRoutingModule } from './app-routing.module';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DashboardHeaderModule } from './dashboard-header/dashboard-header.module';
import { DashboardMenuModule } from './dashboard-menu/dashboard-menu.module';
import { ManageservicesComponent } from './manageservices/manageservices.component';

@NgModule({
  declarations: [
    AppComponent
   
  ],
  imports: [
    SharedModule,
    AppRoutingModule,
    BrowserModule,
    BrowserAnimationsModule,
    DashboardHeaderModule,
    DashboardMenuModule
  ],
  providers: [],
  entryComponents: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

