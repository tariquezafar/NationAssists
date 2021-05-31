import { Component, OnInit, EventEmitter, Output} from '@angular/core';

@Component({
  selector: 'app-dashboard-menu',
  templateUrl: './dashboard-menu.component.html',
  styleUrls: ['./dashboard-menu.component.scss']
})

export class DashboardMenuComponent implements OnInit {

  constructor(){
  }
  ngOnInit(){
  }

  @Output() valueChangestep1 = new EventEmitter();

  Nextvaluestep1 ='';
  Next() {
    this.valueChangestep1.emit( this.Nextvaluestep1);
  }

}