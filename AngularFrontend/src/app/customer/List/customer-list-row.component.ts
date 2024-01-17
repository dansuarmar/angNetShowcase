import {
    BootstrapOptions,
    Component,
    EventEmitter,
    Input,
    Output,
    ViewChild,
} from '@angular/core';
import { Customer } from '../../models/customer.model';
import { MessageService } from 'primeng/api';

@Component({
    selector: '[customer-list-row]',
    templateUrl: './customer-list-row.component.html',
    styleUrls: ['./customer-list-row.component.scss'],
})
export class CustomerListRowComponent {
    editing: boolean = false;
    customerTemp: Customer = new Customer();
    @Input() customer: Customer;
    @Output() customerChanged = new EventEmitter<Customer>();
    @Output() changeCanceled = new EventEmitter<Customer>();
    @Output() deleteCustomer = new EventEmitter<Customer>();
    @ViewChild('firstName') firstNameField: any;
    @ViewChild('email') emailField: any;

    constructor() {}

    onRowEditInit(customer: Customer) {
        Object.assign(this.customerTemp, this.customer);
        this.editing = true;
    }

    onRowEditSave(customer: Customer) {
        if(!this.firstNameField.valid || !this.emailField.valid){
            return;
        }
        this.editing = false;
        this.customerChanged.emit(customer);    
    }

    onRowEditCancel(customer: Customer, index: number) {
        this.editing = false;
        this.changeCanceled.emit(this.customerTemp);
    }

    onRowDelete(customer: Customer){
        this.deleteCustomer.emit(customer);
    }
}
