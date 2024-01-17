import { Component, OnInit } from '@angular/core';
import { PagedList } from '../../models/paged-list.model';
import { Customer } from '../../models/customer.model';
import { CustomerService } from 'src/app/services/customers.service';
import { ConfirmationService, MessageService, ConfirmEventType } from 'primeng/api';

@Component({
    selector: 'customer-list',
    templateUrl: './customer-list.page.html',
    styleUrls: ['./customer-list.page.scss'],
})
export class CustomerListPage implements OnInit {
    pagedList!: PagedList;
    customers: Customer[] = [];
    selectedCustomer!: Customer;

    constructor(
        private customerService: CustomerService,
        private confirmationService: ConfirmationService,
        private messageService: MessageService
    ) {}

    onCustomerChanged(customer: Customer) {
      console.log('OnCustomerChanged');  
      console.log(customer);
        this.customerService.update(customer).subscribe({
            next: (result) => {
                const index = this.customers.findIndex(
                    (item) => item.id === customer.id
                );
                this.customers[index] = result;
                this.messageService.add({ severity: 'info', summary: 'Confirmed', detail: 'Customer Updated' });
            },
        });
    }

    onCustomerDelete(customer: Customer){
        console.log(customer);
        this.confirmationService.confirm({
            message: 'Do you want to delete this Customer?',
            header: 'Delete Confirmation',
            icon: 'pi pi-info-circle',
            acceptButtonStyleClass:"p-button-danger p-button-text",
            rejectButtonStyleClass:"p-button-text p-button-text",
            acceptIcon:"none",
            rejectIcon:"none",

            accept: () => {
                this.customerService.delete(customer).subscribe({
                    next: (result) => {
                        const index = this.customers.findIndex(
                            (item) => item.id === customer.id
                        );
                        this.customers.splice(index, 1);
                        this.messageService.add({ severity: 'info', summary: 'Confirmed', detail: 'Customer deleted' });
                    }
                });
            },
            reject: () => {
                this.messageService.add({ severity: 'error', summary: 'Canceled', detail: 'Customer deletion cancelled' });
            }
        });
    }

    onChangedCanceled(customer: Customer) {
        console.log(customer);
        const index = this.customers.findIndex(
            (item) => item.id === customer.id
        );
        this.customers[index] = customer;
    }

    onRowSelect(event: any){
      console.log(event.data);
      const selectedCustomer = event.data;
      sessionStorage.setItem('lastSelection', selectedCustomer.id);
    }

    ngOnInit() {
        this.customerService.getAll().subscribe({
            next: (result) => {
                this.pagedList = result;
                this.customers = result.items;
                var lastSelectedId = sessionStorage.getItem('lastSelection');
                if(lastSelectedId){
                  this.selectedCustomer = this.customers.find(m => m.id === lastSelectedId);
                }
            },
        });
    }
}
