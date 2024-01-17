import { Component, OnInit } from '@angular/core';
import { Customer } from '../../models/customer.model';
import { CustomerService } from 'src/app/services/customers.service';
import {
    FormBuilder,
    FormControl,
    FormGroup,
    Validators,
} from '@angular/forms';
import { Router } from '@angular/router';

@Component({
    selector: 'customer-create',
    templateUrl: './customer-create.page.html',
    styleUrls: ['./customer-create.page.scss'],
})
export class CustomerCreatePage {
    customerForm: FormGroup;
    customer: Customer = new Customer();
    displayError: boolean = false;
    errorMessage: string = "";

    constructor(
        private customerService: CustomerService,
        private formBuilder: FormBuilder,
        private router: Router
    ) {
        this.customerForm = this.formBuilder.group({
            firstName: new FormControl('', {
                updateOn: 'change',
                validators: [Validators.required, Validators.maxLength(200), Validators.minLength(1)],
            }),
            lastName: new FormControl('', {
                updateOn: 'change',
                validators: [Validators.maxLength(200)],
            }),
            email: new FormControl('', {
                updateOn: 'change',
                validators: [Validators.required, Validators.email, Validators.maxLength(320)],
            }),
            phone: new FormControl('', {
                updateOn: 'change',
                validators: [Validators.maxLength(100)],
            }),
            description: new FormControl('', {
                updateOn: 'change',
                validators: [Validators.maxLength(1000)],
            }),
        });
    }

    onSubmit(){
        console.log(this.customerForm);
        if(!this.customerForm.valid){
            this.customerForm.markAllAsTouched();
            this.customerForm.markAsDirty();
            this.errorMessage = "Error while saving the Customer. Please check fields for mistakes.";
            this.displayError = true;
            return;
        }

        this.mapFormToCustomer();
        this.customerService.add(this.customer).subscribe({
            next: (result) => {
                this.customer.id = result.id;
                // this.router.navigate(['details', this.customer.id]);
                this.router.navigate(['']);
            },
            error: (err) => { 
                this.errorMessage = err;
                this.displayError = true;
            },
        });
    }

    private mapFormToCustomer(){
        Object.assign(this.customer, this.customerForm.value);
    }
}
