import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-create-user',
  templateUrl: './create-user.component.html',
  styleUrls: ['./create-user.component.scss']
})
export class CreateUserComponent implements OnInit {

 

  constructor() { }

  ngOnInit() {
  }

  groupColumns: string[] = ['id', 'name', 'phone', 'email', 'role', 'expiryDate', 'companyBranch'];
  groupTable = tableData;

}


export interface PeriodicElement {
  id: string;
  name: string;
  phone: string;
  email: string;
  role: string;
  expiryDate: string;
  companyBranch: string;
}

const tableData: PeriodicElement[] = [
  {id:'24848ere', name: 'Prabhakar', phone: '+91-8888888898',email: 'prabhakm@gmail.com', role: 'Manager', expiryDate: '12 May 2022', companyBranch: 'Delhi'},
  {id:'24848ere', name: 'Prabhakar', phone: '+91-8888888898',email: 'prabhakm@gmail.com', role: 'Manager', expiryDate: '12 May 2022', companyBranch: 'Delhi'},
  {id:'24848ere', name: 'Prabhakar', phone: '+91-8888888898',email: 'prabhakm@gmail.com', role: 'Manager', expiryDate: '12 May 2022', companyBranch: 'Delhi'},
  {id:'24848ere', name: 'Prabhakar', phone: '+91-8888888898',email: 'prabhakm@gmail.com', role: 'Manager', expiryDate: '12 May 2022', companyBranch: 'Delhi'},
  {id:'24848ere', name: 'Prabhakar', phone: '+91-8888888898',email: 'prabhakm@gmail.com', role: 'Manager', expiryDate: '12 May 2022', companyBranch: 'Delhi'},
  {id:'24848ere', name: 'Prabhakar', phone: '+91-8888888898',email: 'prabhakm@gmail.com', role: 'Manager', expiryDate: '12 May 2022', companyBranch: 'Delhi'},
  {id:'24848ere', name: 'Prabhakar', phone: '+91-8888888898',email: 'prabhakm@gmail.com', role: 'Manager', expiryDate: '12 May 2022', companyBranch: 'Delhi'},
];
