import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CustomerListPage } from './List/customer-list.page';
import { CustomerCreatePage } from './create/customer-create.page';
import { CustomerDetailsComponent } from './details/customer-details.component';

const routes: Routes = [
  {
    path: '',
    component: CustomerListPage
  },
  {
    path: 'create',
    component: CustomerCreatePage
  },
  // {
  //   path:'details/:id',
  //   component: CustomerDetailsComponent
  // }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CustomerRoutingModule {}
