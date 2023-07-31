import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiService } from '../api.service';
import { Customer } from 'src/model/customer.model';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Invoice } from 'src/model/invoice.model';

@Component({
  selector: 'app-customer-details',
  templateUrl: './customer-details.component.html',
  styleUrls: ['./customer-details.component.css']
})

export class CustomerDetailsComponent implements OnInit {
  private apiSubcription: Subscription | undefined;
  customerForm: FormGroup | undefined;
  customer: Customer | undefined;
  Id: string = "";
  submitAttempted: boolean = false;

  subscriptionStates = [
    { name: 'New', value: 0 },
    { name: 'Active', value: 1 },
    { name: 'Suspended', value: 2 },
  ];

  invoices: Invoice[] = [];

  displayedColumns: string[] = [
    'id',
    'iDcustomer',
    'invoiceNumber',
    'date',
    'total',
  ];

  constructor(private route: ActivatedRoute, private apiService: ApiService, private formBuilder: FormBuilder, private router: Router) { }

  ngOnDestroy(): void {
    this.apiSubcription?.unsubscribe();
  }

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.Id = params['id'];
    });

    this.customerForm = this.formBuilder.group({
      id: this.formBuilder.control(''),
      iDcustomer: this.formBuilder.control(''),
      companyName: this.formBuilder.control(''),
      address: this.formBuilder.control(''),
      state: this.formBuilder.control(''),
      country: this.formBuilder.control(''),
      subscriptionState: [this.getSubscriptionStateName(0), [Validators.pattern(/^[012]$/)]],
      numberOfInvoices: [this.formBuilder.control(0), [Validators.pattern(/^[0-9]+$/)]]
    });

    if (this.Id === '0') {
      this.customerForm.get('subscriptionState')?.setValue(0);
      this.customerForm.get('subscriptionState')?.disable();
      this.customerForm.get('numberOfInvoices')?.setValue(0);
    } else {

      this.apiSubcription = this.apiService.getCustomer(this.Id).subscribe({
        next: (response: Customer) => {
          this.customer = response;
          this.customerForm?.patchValue(response);

          this.apiService.getInvoicesByCustomer(response.iDcustomer).subscribe(
            (response2: Invoice[]) => {
              this.invoices = response2;
              this.customerForm?.get('numberOfInvoices')?.setValue(this.invoices.length);
            },
            (error) => {
              console.error('Error fetching invoices from API:', error);
            }
          );
        },
        error: (error) => {
          console.error('Error fetching customer details from API:', error);
        }
      });
    }

  }

  getSubscriptionStateValue(name: string): number {
    const state = this.subscriptionStates.find((s) => s.name === name);
    return state ? state.value : 0;
  }

  goBack(): void {
    this.router.navigate(['/customer']);
  }

  saveCustomer(): void {
    this.submitAttempted = true;
    if (this.customerForm!.valid) {
      const formData = this.customerForm!.value as Customer;

      if (this.Id === '0') {
        this.apiService.insertCustomer(formData).subscribe(
          (response: Customer) => {
            this.router.navigate(['/customer']);
          },
          (error) => {
            console.error('Error saving customer:', error);
          }
        );
      } else {
        this.apiService.updateCustomer(formData).subscribe(
          (response: Customer) => {
            this.router.navigate(['/customer']);
          },
          (error) => {
            console.error('Error updating customer:', error);
          }
        );
      }
    } else {
      console.warn('Form is invalid. Cannot save the customer.');
    }
  }

  getSubscriptionStateName(value: number): string {
    const state = this.subscriptionStates.find((s) => s.value === value);
    return state ? state.name : 'New';
  }
}