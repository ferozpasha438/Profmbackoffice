import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import moment from 'moment';
import { AuthorizeService } from 'src/app/api-authorization/AuthorizeService';
import { ApiService } from 'src/app/services/api.service';
import { NotificationService } from 'src/app/services/notification.service';
import { DBOperation } from 'src/app/services/utility.constants';
import { UtilityService } from 'src/app/services/utility.service';
import { DeleteConfirmDialogComponent } from 'src/app/sharedcomponent/delete-confirm-dialog';
import { PaginationService } from 'src/app/sharedcomponent/pagination.service';
import { ParentSystemSetupComponent } from 'src/app/sharedcomponent/parentsystemsetup.component';
import { CustomSelectListItem } from '../../../models/MenuItemListDto';
import { FomSharedService } from '../../../services/fomShared.service';
import { ParentFomMgtComponent } from '../../../sharedcomponent/parentfommgt.component';
import * as XLSX from 'xlsx';

@Component({
  selector: 'app-getreport',
  templateUrl: './getreport.component.html',
  styleUrls: ['./getreport.component.scss']
})
export class GetreportComponent extends ParentFomMgtComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  //@ViewChild(MatPaginator) paginator: MatPaginator;

  @ViewChild(MatSort, { static: true }) sort!: MatSort;
  filter: any = {
    page: 0,
    pageCount: 10,
    query: "",
    orderBy: "id desc",
    ticketNumber: "",
    customerCode: "",
    siteCode: "",
    supervisor: "",
    statusStr: ''
  };
  totalItems = 0;
  search: string = '';
  CustomerCodeList: Array<CustomSelectListItem> = [];
  ContractCodeList: Array<CustomSelectListItem> = [];
  SiteCodeList: Array<CustomSelectListItem> = [];
  statusSelectionList: Array<any> = [];
  isLoading = false;
  totalItemsCount: number = 0;

  data: Array<any> = [];
  data1!: MatTableDataSource<any> | null;
  customerCode: string = '';
  fromDateIp: any;
  toDateIp: any;
  constructor(private fb: FormBuilder, private apiService: ApiService, private authService: AuthorizeService,
    private utilService: UtilityService, private notifyService: NotificationService, public dialog: MatDialog,
    public pageService: PaginationService, private sharedService: FomSharedService, private router: Router) {
    super(authService);
  }

  ngOnInit(): void {

    this.loadStatusSelectionList();
  }

  loadStatusSelectionList() {

    this.apiService.getall('FomCustomerContract/getSelectJobStatusesEnum').subscribe(res => {
      console.log(res);
      this.statusSelectionList = res.slice();
    });

    //this.apiService.getPagination('FomCustomer', this.utilService.getQueryString(0, 1000, '', '')).subscribe(res => {
    //  if (res)
    //    this.CustomerCodeList = res['items'];
    //});

    this.apiService.getall('FomCustomer/getSelectCustomerList').subscribe(res => {
      console.log(res);
      this.CustomerCodeList = res.slice();
    });


    this.apiService.getall('FomCustomerContract/GetCustomerContractSelectList').subscribe(res => {
      console.log(res);
      this.ContractCodeList = res.slice();
    });

    
  }


  private loadList(page: number | undefined, pageCount: number | undefined, query: string | null | undefined, orderBy: string | null | undefined) {
    this.isLoading = true;

    this.apiService.getPagination('FomCustomerContract/getJobTicketReport', this.utilService.getQueryString(page, pageCount, query, orderBy)).subscribe(result => {
      this.totalItemsCount = 0;
      this.data1 = new MatTableDataSource(result.items);

      this.totalItemsCount = result.totalCount;

      this.data1.paginator = this.paginator;
      this.data1.sort = this.sort;

      this.isLoading = false;
    }, error => this.utilService.ShowApiErrorMessage(error));
  }





  onCustSiteCode(selectedValue: string) {
    console.log('Selected Customer Code:', selectedValue);

    // Perform additional logic here, such as fetching dependent site codes
    if (selectedValue) {
      this.fetchSiteCodesForCustomer(selectedValue);
    }
  }


  fetchSiteCodesForCustomer(custCode: string) {
    this.apiService.getall(`fomCustomerContract/GetSelectCustomerSiteByCustCode?custCode=${custCode}`).subscribe(res => {
      if (res) {
        this.SiteCodeList = res;
      }
    })
  }

  loadData() {
    if (this.fromDateIp != null) {
      let picker = moment as any;
      this.filter.fromDate = picker(this.fromDateIp).format('YYYY-MM-DD') as Date;
    }
    if (this.toDateIp != null) {
      let picker = moment as any;
      this.filter.toDate = picker(this.toDateIp).format('YYYY-MM-DD') as Date;
    }

   // this.isLoading = true;
    this.apiService.postFomCpUrl('FomCustomerContract/getJobTicketReport', this.filter).subscribe(result => {
      this.isLoading = false;
      console.log('API Result:', result); // Debugging line
      this.totalItems = result?.totalCount ?? 0;
      console.log('Total Items:', this.totalItems); // Debugging line
      this.data = result.items.slice();
      this.totalItemsCount = result.totalCount;
    });
  }

  //change(event: PageEvent) {
  //  this.filter.page = event.pageIndex;
  //  this.filter.pageCount= event.pageSize;
  //}
  onPageSwitch(event: PageEvent) {
    console.log("Page switched:", event);
    this.filter.page = event.pageIndex;
    this.filter.pageCount = event.pageSize;
    this.loadData();
  }
  loadReport() {
    this.data = [];
    this.filter.page = 0;

    this.loadData();
    setTimeout(() => {

      for (this.filter.page = 1; this.filter.page < (Math.ceil(this.totalItems / this.filter.pageCount)); this.filter.page += 1) {
        this.loadData();

      }
    }, 1000);


  }




  resetFilter() {
    this.filter = {
      status: null,
      page: 0,
      pageCount: 10,
      query: "",
      orderBy: "id desc",
      ticketNumber: "",
      customerCode: "",
      siteCode: "",
      statusStr: "",
      fromDate: null,
      toDate: null,
    };
    this.data = [];
    this.fromDateIp = null;
    this.toDateIp = null;
    //this.loadData();
  }

  //onPageSwitch(event: PageEvent) {
  //  this.pageService.change(event);
  //  this.loadList(event.pageIndex, event.pageSize, "", this.sortingOrder);
  //}

  clickPaginationButton(buttonId: string) {
    if (buttonId == 'rightBtn' && (this.filter.page < ((this.totalItems / this.filter.pageCount) - 1))) {
      this.filter.page += 1;
    }
    else if (buttonId == 'leftBtn' && (this.filter.page > 0)) {
      this.filter.page -= 1;
    }
    else if (buttonId == 'upBtn') {

      this.filter.page = 0;
      this.filter.orderBy = "id desc";
    }
    else if (buttonId == 'downBtn') {
      this.filter.page = 0;
      this.filter.orderBy = "id asc";
    }
    this.loadReport();
  }


  exportToExcel() {
    let tableData: Array<any> = [];
    this.data.forEach((e: any) => {
      tableData.push({ TicketNumber: e.ticketNumber, ProjectName: e.projectNameEng, CustomerName: e.customerNameEng, JobMaintenanceType: e.jobMaintenanceType, DepartmentName: e.depNameEng, JoiningDate: e.joDate, Status: e.statusStr });

    });
    const fileName = "JobTicketReport.xlsx";

    const ws: XLSX.WorkSheet = XLSX.utils.json_to_sheet(tableData);
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, "JobTicketReport");

    XLSX.writeFile(wb, fileName);
  }
}
