import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Customer } from 'src/model/customer.model';
import { ApiService } from '../api.service';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.css']
})
export class CustomerComponent implements OnInit {
  data: Customer[] = [];

  displayedColumns: string[] = [
    'id',
    'companyName',
    'address',
    'state',
    'country',
    'subscriptionState',
    'invoices',
  ];
  constructor(private apiService: ApiService, private router: Router) {}

  clickedRows = new Set<Customer>();

  ngOnInit(): void {
    this.apiService.getAllCustomers().subscribe(
      (response: Customer[]) => {
        this.data = response;
      },
      (error) => {
        console.error('Error fetching data from API:', error);
      }
    );
  }

  redirectToAnotherPage(customer: Customer): void {
    this.router.navigate(['/customer-details', customer.id]);
  }

  AddCustomer(){
    this.router.navigate(['/customer-details', 0]);
  }
}
