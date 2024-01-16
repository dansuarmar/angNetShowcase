import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CustomerRoutingModule } from './customer-routing.module';
import { CustomerPage } from './List/customer-list.page';
import { HttpBackend, HttpClientModule } from '@angular/common/http';
import { TableModule } from 'primeng/table';

@NgModule({
  imports: [
    CommonModule,
    HttpClientModule,
    CustomerRoutingModule,
    TableModule
  ],
  declarations: [CustomerPage],
})
export class CustomerModule { }
