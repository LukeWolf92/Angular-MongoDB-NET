import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Customer } from 'src/model/customer.model';
import { Invoice } from 'src/model/invoice.model';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private apiUrl = 'https://localhost:44355/api';

  constructor(private http: HttpClient) {}

  getAllCustomers(): Observable<Customer[]> {
    return this.http.get<Customer[]>(`${this.apiUrl}/CustomerService/GetAll`);
  }

  getCustomer(id: string): Observable<Customer> {
    return this.http.get<Customer>(`${this.apiUrl}/CustomerService/Get?ID=${id}`);
  }

  updateCustomer(customer: Customer) {
    return this.http.put<Customer>(`${this.apiUrl}/CustomerService/Update`,customer);
  }

  insertCustomer(customer: Customer) {
    return this.http.post<Customer>(`${this.apiUrl}/CustomerService/Insert`,customer);
  }


  getInvoicesByCustomer(iDcustomer: string): Observable<Invoice[]> {
    return this.http.get<Invoice[]>(`${this.apiUrl}/InvoiceService/GetAllByCustomer?IDcustomer=${iDcustomer}`);
  }
}