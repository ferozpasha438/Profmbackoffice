import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GetactivitieslistComponent } from './Activities/Getactivities/getactivitieslist.component';
import { GetcustomerlistsComponent } from './Customer/GetCustomer/getcustomerlists.component';
import { GetcustomercategorylistComponent } from './Customercategory/GetCustomercategory/getcustomercategorylist.component';
import { GetcustomercontractComponent } from './Customercontract/GetCustomercontract/getcustomercontract.component';
import { GetcustomersitesComponent } from './CustomerSite/GetCustomerSite/getcustomersites.component';
import { GetdisciplineslistComponent } from './Disciplines/GetDisciplines/getdisciplineslist.component';
import { GetscheduleslistdatesComponent } from './GetScheduling/getscheduleslistdates.component';
import { GetitemlistsComponent } from './Item/GetItem/getitemlists.component';
import { GetitemcategoriesComponent } from './ItemCategories/GetItemCategories/getitemcategories.component';
import { GetitemsubcategoriesComponent } from './ItemSubCategories/GetItemSubCategories/getitemsubcategories.component';
import { GetresourceslistComponent } from './Resources/GetResources/getresourceslist.component';
import { GetresourcetypeslistComponent } from './ResourceTypes/GetResourceTypes/getresourcetypeslist.component';
import { GetsubcontractorslistComponent } from './SubContractors/GetSubContractors/getsubcontractorslist.component';
import { TicketdetailComponent } from './Tickets/ticketdetail/ticketdetail.component';
import { TicketsComponent } from './Tickets/tickets/tickets.component';
import { WorkOrderDetailComponent } from './WorkOrders/workOrderDetail/workorderdetail.component';
import { WorkOrdersComponent } from './WorkOrders/workOrders/workOrders.component';
import { BtcTicketsComponent } from './B2C/btctickets.component';
import { BtcschedulelistComponent } from './B2C/btcschedulelist.component';

const routes: Routes = [
  { path: 'getdisciplines', component: GetdisciplineslistComponent },
  { path: 'getactivities', component: GetactivitieslistComponent },
  { path: 'customer', component: GetcustomerlistsComponent },
  { path: 'getcustomercategory', component: GetcustomercategorylistComponent },
  { path: 'getcustomercontract', component: GetcustomercontractComponent },
  { path: 'getitem', component: GetitemlistsComponent },
  { path: 'getitemcategories', component: GetitemcategoriesComponent },
  { path: 'getitemsubcategories', component: GetitemsubcategoriesComponent },
  { path: 'getresources', component: GetresourceslistComponent },
  { path: 'getsubcontractors', component: GetsubcontractorslistComponent },
  { path: 'getcustomers', component: GetcustomerlistsComponent },
  { path: 'getresourcetypes', component: GetresourcetypeslistComponent },
  { path: 'getcustomersites', component: GetcustomersitesComponent },
  { path: 'getscheduleslist', component: GetscheduleslistdatesComponent },
  { path: 'tickets', component: TicketsComponent },
  { path: 'ticketdetail', component: TicketdetailComponent },
  { path: 'getworkorders', component: WorkOrdersComponent },
  { path: 'workorderdetail', component: WorkOrderDetailComponent },

  { path: 'btcscheduleslist', component: BtcschedulelistComponent },
  { path: 'btctickets', component: BtcTicketsComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PROfmRoutingModule { }
