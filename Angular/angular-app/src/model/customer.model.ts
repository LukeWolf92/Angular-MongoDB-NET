import { Invoice } from "./invoice.model";

export interface Customer {
    id: string;
    companyName: string;
    address: string;
    state: string;
    country: string;
    subscriptionState: number;
    invoices: Invoice[];
  }
  