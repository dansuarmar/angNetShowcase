import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PagedList } from '../../models/paged-list.model';
import { Customer } from '../../models/customer.model';
import { CustomerService } from 'src/app/services/customers.service';

@Component({
  selector: 'customer-list',
  templateUrl: './customer-list.page.html',
  styleUrls: ['./customer-list.page.scss'],
})
export class CustomerPage implements OnInit {
    pagedList!: PagedList;
    customers: Customer[] = [];

    constructor(
        private customerService: CustomerService
    ) {
    }

  ngOnInit() {
    this.customerService.getAll().subscribe({
        next: (result) => {
            this.customers = result.items;
        }
    });
  }
}
