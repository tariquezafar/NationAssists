import { Component, OnInit } from '@angular/core';
import { ChartOptions, ChartType, ChartDataSets } from 'chart.js';

import { SingleDataSet, Label, monkeyPatchChartJsLegend, monkeyPatchChartJsTooltip } from 'ng2-charts';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  public barChartOptions: ChartOptions = {
    responsive: true,
  };
  public barChartLabels: Label[] = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
  public barChartType: ChartType = 'bar';
  public barChartLegend = false;
  public barChartPlugins = [];

  public barChartData: ChartDataSets[] = [
    { data: [50, 33, 25, 35, 15, 22, 44, 18, 20, 3, 42, 22]}
  ];

  public chartColors: any[] = [
    { 
      backgroundColor:["#ce1127", "#ce1127", "#ce1127", "#ce1127", "#ce1127", "#ce1127", "#ce1127", "#ce1127", "#ce1127", "#ce1127", "#ce1127", "#ce1127"] 
    }];


    public pieChartOptions: ChartOptions = {
      responsive: true,
    };
    public pieChartLabels: Label[] = [['Complete'], ['Pending']];
    public pieChartData: SingleDataSet = [70, 30];
    public pieChartType: ChartType = 'pie';
    public pieChartOption: any = {
      legend: {
        position: 'bottom',
        labels: {
          fontSize: 12,
          usePointStyle: true
        }
      }
    }
    
    public pieChartLegend = true;
    public pieChartPlugins = [];

    public chartColors2: any[] = [
      { 
        backgroundColor:["#00a1fe", "#26c281"] 
    }];
    public chartColors3: any[] = [
      { 
        backgroundColor:["#0069fe", "#1c2a3c"] 
    }];
    public chartColors4: any[] = [
      { 
        backgroundColor:["#ce1127", "#357ebd"] 
    }];
      
  
    constructor() {
      monkeyPatchChartJsTooltip();
      monkeyPatchChartJsLegend();
    }
  

  ngOnInit() {
  }

}
