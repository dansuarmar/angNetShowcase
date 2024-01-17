import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CustomerRoutingModule } from './customer-routing.module';
import { CustomerListPage } from './List/customer-list.page';
import { HttpClientModule } from '@angular/common/http';
import { TableModule } from 'primeng/table';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CustomerCreatePage as CustomerCreateComponent } from './create/customer-create.page';
import { InputGroupModule } from 'primeng/inputgroup';
import { InputGroupAddonModule } from 'primeng/inputgroupaddon';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { CalendarModule } from 'primeng/calendar';
import { ChipsModule } from 'primeng/chips';
import { DropdownModule } from 'primeng/dropdown';
import { InputMaskModule } from 'primeng/inputmask';
import { InputNumberModule } from 'primeng/inputnumber';
import { CascadeSelectModule } from 'primeng/cascadeselect';
import { MultiSelectModule } from 'primeng/multiselect';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { RippleModule } from 'primeng/ripple';
import { DialogModule } from 'primeng/dialog';
import { CustomerDetailsComponent } from './details/customer-details.component';
import { ConfirmationService, MessageService } from 'primeng/api';
import { CustomerListRowComponent } from './List/customer-list-row.component';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ToastModule } from 'primeng/toast';

@NgModule({
  imports: [
    CommonModule,
    HttpClientModule,
    TableModule,
    FormsModule,
    ReactiveFormsModule,
    CustomerRoutingModule,
    AutoCompleteModule,
		CalendarModule,
		ChipsModule,
		DropdownModule,
		InputMaskModule,
		InputNumberModule,
		CascadeSelectModule,
		MultiSelectModule,
    ToastModule,
		InputTextareaModule,
		InputTextModule,
		InputGroupModule,
		InputGroupAddonModule,
    ButtonModule,
		RippleModule,
    DialogModule,
    ConfirmDialogModule
  ],
  declarations: [
    CustomerListPage, 
    CustomerCreateComponent,
    CustomerDetailsComponent,
    CustomerListRowComponent
  ],
  providers:[
    MessageService,
    ConfirmationService
  ]
})
export class CustomerModule { }
