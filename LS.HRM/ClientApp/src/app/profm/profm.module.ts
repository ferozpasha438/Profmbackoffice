import { NgModule } from '@angular/core';
import { CommonModule,DatePipe } from '@angular/common';
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
import { BtctickethistoryComponent } from './B2C/Reports/btctickethistory/btctickethistory.component';
import { BtcticketsummarybycustComponent } from './B2C/Reports/btcticketsummarybycust/btcticketsummarybycust.component';
import { BtcticketsummarybyservicetypeComponent } from './B2C/Reports/btcticketsummarybyservicetype/btcticketsummarybyservicetype.component';
import { AssetmasterComponent } from './AssetMaintenance/assetmaster/assetmaster.component';
import { SectionlistComponent } from './Section/sectionlist.component';
import { AddupdatesectionComponent } from './Section/addupdatesection.component';
import { AddupdateassetmasterComponent } from './AssetMaintenance/assetmaster/addupdateassetmaster/addupdateassetmaster.component';
import { AddupdateassettaskComponent } from './AssetMaintenance/assetmaster/addupdateassettask/addupdateassettask.component';
import { JobplanlistComponent } from './AssetMaintenance/jobplan/jobplanlist.component';
import { AddupdatejobplanComponent } from './AssetMaintenance/jobplan/addupdatejobplan/addupdatejobplan.component';
import { AddupdatejobplanscheduleComponent } from './AssetMaintenance/jobplan/addupdatejobplanschedule/addupdatejobplanschedule.component';
import { GeneratedatescheduleComponent } from './AssetMaintenance/jobplan/generatedateschedule/generatedateschedule.component';
import { JobplannotesComponent } from './AssetMaintenance/jobplan/jobplannotes/jobplannotes.component';
import { SchedulejobplanlistComponent } from './AssetMaintenance/jobplan/schedulejobplanlist/schedulejobplanlist.component';
import { AssetjoborderlistComponent } from './AssetMaintenance/jobplan/assetjoborderlist/assetjoborderlist.component';
import { AssetjoborderchilditemsComponent } from './AssetMaintenance/jobplan/assetjoborderchilditems/assetjoborderchilditems.component';
import { AssetclosinginfoComponent } from './AssetMaintenance/jobplan/assetclosinginfo/assetclosinginfo.component';
import { JobplanschedulingComponent } from './AssetMaintenance/jobplan/shared/jobplanscheduling/jobplanscheduling.component';
import { JobplanschedulingpopupComponent } from './AssetMaintenance/jobplan/jobplanschedulingpopup/jobplanschedulingpopup.component';
import { AssettasklistpopupComponent } from './AssetMaintenance/jobplan/assettasklistpopup/assettasklistpopup.component';
import { JobplanscheduleprintComponent } from './AssetMaintenance/jobplan/jobplanscheduleprint/jobplanscheduleprint.component';



/*import { ActivityDialogComponent } from './Customercontract/Sharedpages/activitydialog.component';*/
import { MatDialogModule } from '@angular/material/dialog';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { FormsModule } from '@angular/forms';
import { GetreportresourceslistComponent } from './Resourcereports/getreportresourceslist.component';
import { GetcustomersreportComponent } from './Customersreport/getcustomersreport.component';
import { GetreportComponent } from './JobTicket/getjobticket/getreport.component';
//import { JobticketreportsComponent } from './JobTicketReports/jobticketreports.component';
import { GetticketsummaryreportComponent } from './GetTicketSummaryReport/getticketsummaryreport.component';



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
    BtcresourceallocateComponent,
    BtctickethistoryComponent,
    BtcticketsummarybycustComponent,
    BtcticketsummarybyservicetypeComponent,
    AssetmasterComponent,
    SectionlistComponent,
    AddupdatesectionComponent,
    AddupdateassetmasterComponent,
    AddupdateassettaskComponent,
    JobplanlistComponent,
    AddupdatejobplanComponent,
    AddupdatejobplanscheduleComponent,
    GeneratedatescheduleComponent,
    JobplannotesComponent,
    SchedulejobplanlistComponent,
    AssetjoborderlistComponent,
    AssetjoborderchilditemsComponent,
    AssetclosinginfoComponent,
    JobplanschedulingComponent,
    JobplanschedulingpopupComponent,
    AssettasklistpopupComponent,
    JobplanscheduleprintComponent,
    GetreportresourceslistComponent,
    GetcustomersreportComponent,
    /*ActivityDialogComponent,*/
    GetreportComponent,
  /*  JobticketreportsComponent,*/
    GetticketsummaryreportComponent
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
    MatDialogModule,
    MatCheckboxModule,
    FormsModule
  ],
  providers: [DatePipe],
  exports: [CommonModule],
})
export class PROfmModule { }
