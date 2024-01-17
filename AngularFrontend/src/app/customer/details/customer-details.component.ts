import { BootstrapOptions, Component, Input } from '@angular/core';
import { Customer } from '../../models/customer.model';

@Component({
  selector: 'customer-details',
  templateUrl: './customer-details.component.html',
  styleUrls: ['./customer-details.component.scss'],
})
export class CustomerDetailsComponent {
    @Input() customer: Customer;
    @Input() display: boolean = false;

    constructor(
    ) {
    }
}
