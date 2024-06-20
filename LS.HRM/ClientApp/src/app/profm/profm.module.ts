import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PROfmRoutingModule } from './profm-routing.module';
import { NgxPrintModule } from 'ngx-print';
import { SharedModule } from '../sharedcomponent/shared.module';
import { NgApexchartsModule } from 'ng-apexcharts';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { GetactivitieslistComponent } from './Activities/Getactivities/getactivitieslist.component';
import { AddupdatedisciplinesComponent } from './Disciplines/Sharedpages/addupdatedisciplines.component';
import { AddupdateactivitesComponent } from './Activities/Sharedpages/addupdateactivites.component';
import { AddupdatecustomerComponent } from './Customer/Sharedpages/addupdatecustomer.component';
import { GetcustomercategorylistComponent } from './Customercategory/GetCustomercategory/getcustomercategorylist.component';
import { AddupdatecustomercategoryComponent } from './Customercategory/Sharedpages/addupdatecustomercategory.component';
import { GetcustomercontractComponent } from './Customercontract/GetCustomercontract/getcustomercontract.component';
import { GetdisciplineslistComponent } from './Disciplines/GetDisciplines/getdisciplineslist.component';
import { GetcustomersitesComponent } from './CustomerSite/GetCustomerSite/getcustomersites.component';
import { AddupdatesiteComponent } from './CustomerSite/Sharedpages/addupdatesite.component';
import { ListofdepartmentComponent } from './ListOfDepartment/listofdepartment.component';
import { AddupdatecustomercontractComponent } from './Customercontract/Sharedpages/addupdatecustomercontract.component';
import { GetcustomerlistsComponent } from './Customer/GetCustomer/getcustomerlists.component';
import { GetitemlistsComponent } from './Item/GetItem/getitemlists.component';
import { AddupdateitemsComponent } from './Item/Sharedpages/addupdateitems.component';
import { AddupdateitemcategoriesComponent } from './ItemCategories/Sharedpages/addupdateitemcategories.component';
import { GetitemcategoriesComponent } from './ItemCategories/GetItemCategories/getitemcategories.component';
import { GetitemsubcategoriesComponent } from './ItemSubCategories/GetItemSubCategories/getitemsubcategories.component';
import { AddupdateitemsubcategoriesComponent } from './ItemSubCategories/Sharedpages/addupdateitemsubcategories.component';
import { AddupdateresourcesComponent } from './Resources/Sharedpages/addupdateresources.component';
import { GetresourceslistComponent } from './Resources/GetResources/getresourceslist.component';
import { GetsubcontractorslistComponent } from './SubContractors/GetSubContractors/getsubcontractorslist.component';
import { AddupdatesubcontractorsComponent } from './SubContractors/Sharedpages/addupdatesubcontractors.component';
import { AddupdatesitetocustomerComponent } from './sharedcomponent/AddUpdateSiteToCustomer/addupdatesitetocustomer.component';
import { CustomerinvoicestatementComponent } from './sharedcomponent/CustomerInvoiceStatement/customerinvoicestatement.component';
import { CustomerstatementComponent } from './sharedcomponent/CustomerStatement/customerstatement.component';
import { GetresourcetypeslistComponent } from './ResourceTypes/GetResourceTypes/getresourcetypeslist.component';
import { AddupdateresourcetypesComponent } from './ResourceTypes/Sharedpages/addupdateresourcetypes.component';
import { NgSelectModule } from '@ng-select/ng-select';
import { GetschedulesComponent } from './Customercontract/Sharedpages/getschedules.component';
import { GetschedulelistComponent } from './Customercontract/Sharedpages/getschedulelist.component';
import { GetschedulecalendarComponent } from './Customercontract/Sharedpages/getschedulecalendar.component';
import { GetscheduleslistdatesComponent } from './GetScheduling/getscheduleslistdates.component';
import { TicketsComponent } from './Tickets/tickets/tickets.component';
import { WorkOrdersComponent } from './WorkOrders/workOrders/workOrders.component';
import { AllschedulecalendarComponent } from './GetScheduling/SharedPages/allschedulecalendar.component';
import { TicketdetailComponent } from './Tickets/ticketdetail/ticketdetail.component';
import { WorkOrderDetailComponent } from './WorkOrders/workOrderDetail/workorderdetail.component';
import { GetserviceitemComponent } from './ServiceItem/GetServiceItem/getserviceitem/getserviceitem.component';
import { AddupdateserviceitemComponent } from './ServiceItem/Sharedpages/addupdateserviceitem/addupdateserviceitem.component';
import { BtcTicketsComponent } from './B2C/btctickets.component';
import { CommonRemarkComponent } from './B2C/commonremark.component';
import { BtcschedulelistComponent } from './B2C/btcschedulelist.component';
import { BtcschedulecalendarComponent } from './B2C/btcschedulecalendar.component';
import { BtcresourceallocateComponent } from './B2C/btcresourceallocate.component';



@NgModule({
  declarations: [
    GetactivitieslistComponent,
    AddupdatedisciplinesComponent,
    AddupdateactivitesComponent,
    AddupdatecustomerComponent,
    GetcustomercategorylistComponent,
    AddupdatecustomercategoryComponent,
    GetcustomercontractComponent,
    GetdisciplineslistComponent,
    GetcustomersitesComponent,
    AddupdatesiteComponent,
    ListofdepartmentComponent,
    AddupdatecustomercontractComponent,
    GetschedulesComponent,
    GetschedulecalendarComponent,
    GetschedulelistComponent,
    GetcustomerlistsComponent,
    GetitemlistsComponent,
    AddupdateitemsComponent,
    AddupdateitemcategoriesComponent,
    GetitemcategoriesComponent,
    GetitemsubcategoriesComponent,
    AddupdateitemsubcategoriesComponent,
    AddupdateresourcesComponent,
    GetresourceslistComponent,
    AddupdatesubcontractorsComponent,
    GetsubcontractorslistComponent,
    AddupdatesitetocustomerComponent,
    CustomerinvoicestatementComponent,
    CustomerstatementComponent,
    GetresourcetypeslistComponent,
    AddupdateresourcetypesComponent,
    GetscheduleslistdatesComponent,
    TicketsComponent,
    WorkOrdersComponent,
    AllschedulecalendarComponent,
    TicketdetailComponent,
    WorkOrderDetailComponent,
    GetserviceitemComponent,
    AddupdateserviceitemComponent,
    WorkOrderDetailComponent,
    BtcTicketsComponent,
    CommonRemarkComponent,
    BtcschedulelistComponent,
    BtcschedulecalendarComponent,
    BtcresourceallocateComponent
  ],

  imports: [
    PROfmRoutingModule,
    NgxPrintModule,
    SharedModule,
    NgApexchartsModule,
    MatButtonModule,
    MatSelectModule,
    MatTooltipModule,
    MatDatepickerModule,
    MatFormFieldModule,
    NgSelectModule,
  ],

  exports: [CommonModule],
})
export class PROfmModule { }
