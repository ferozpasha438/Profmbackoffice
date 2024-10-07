import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { AuthorizeService } from 'src/app/api-authorization/AuthorizeService';
import { ApiService } from 'src/app/services/api.service';
import { NotificationService } from 'src/app/services/notification.service';
import { DBOperation } from 'src/app/services/utility.constants';
import { UtilityService } from 'src/app/services/utility.service';
import { DeleteConfirmDialogComponent } from 'src/app/sharedcomponent/delete-confirm-dialog';
import { PaginationService } from 'src/app/sharedcomponent/pagination.service';
import { ParentB2CComponent } from '../../sharedcomponent/parentb2c.component';
import { FomSharedService } from '../../services/fomShared.service';
import { CommonRemarkComponent } from './commonremark.component';
import { BtcresourceallocateComponent } from './btcresourceallocate.component';
import { ParentB2CFrontComponent } from '../../sharedcomponent/parentb2cfront.component';


@Component({
  selector: 'app-btctickets',
  templateUrl: './btctickets.component.html'
})

export class BtcTicketsComponent extends ParentB2CFrontComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
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
    statusStr: '',
    serviceType: 'All',
    status: 0,
    fromDate: null,
  };
  totalItems = 0;
  search: string = '';
  statusSelectionList: Array<any> = [];
  isLoading = false;
  totalItemsCount: number = 0;
  data: Array<any> = [];
  constructor(private fb: FormBuilder, private apiService: ApiService, private authService: AuthorizeService,
    private utilService: UtilityService, private notifyService: NotificationService, public dialog: MatDialog,
    public pageService: PaginationService, private sharedService: FomSharedService, private router: Router) {
    super(authService);
  }
  form!: FormGroup;
  ngOnInit(): void {
    this.setForm();
    this.loadData();
    this.statusSelectionList = [
      { text: 'All', id: 0 },
      { text: 'Approved', id: 5 },
      { text: 'Closed', id: 9 },
      { text: 'Completed', id: 11 },
      { text: 'Void', id: 3 },
    ];
  }
  setForm() {
    this.form = this.fb.group({});
    this.loadStatusSelectionList();
  }

  loadData() {
    this.isLoading = true;
    if (this.filter.fromDate != null)
      this.filter.fromDate = this.utilService.selectedDate(this.filter.fromDate);

    this.apiService.postFomCpUrl('fomMobJobTicketHead/getFrontOfficeB2CTicketsListPaginationWithFilter', this.filter).subscribe(result => {

      this.isLoading = false;
      this.totalItems = result?.totalCount ?? 0;
      this.data = result.items.slice();
    });
  }
  loadStatusSelectionList() {

    this.apiService.getall('FomCustomerContract/getSelectJobStatusesEnum').subscribe(res => {
      console.log(res);
      this.statusSelectionList = res.slice();
    });
  }

  resetFilter(serviceType: string = 'All', setstatus: number = 0, fromDate: any = null) {
    this.filter = {
      page: 0,
      pageCount: 10,
      query: "",
      orderBy: "id desc",
      ticketNumber: "",
      customerCode: "",
      siteCode: "",
      supervisor: "",
      serviceType: serviceType,
      statusStr: '',
      status: setstatus,
      fromDate: fromDate,
    };
    this.loadData();
  }

  setServiceType(serviceType: string) {
    //this.filter.serviceType = serviceType;   
    this.resetFilter(serviceType);
  }
  setstatus(setstatus: any) {
    this.resetFilter('All', setstatus);
  }
  changeDate(fromDate: any) {
    this.resetFilter('All', 0, fromDate);
  }

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
    this.loadData();
  }

  private openResourceAllocation(row: any, modalTitle: string, modalBtnTitle: string) {
    let dialogRef = this.utilService.openCrudDialog(this.dialog, BtcresourceallocateComponent,'50');
    (dialogRef.componentInstance as any).modalTitle = modalTitle;
    (dialogRef.componentInstance as any).modalBtnTitle = modalBtnTitle;
    (dialogRef.componentInstance as any).row = row;
    dialogRef.afterClosed().subscribe(res => {
      if (res && res === true)
        this.loadData();
    });
  }

  assignAndApprove(item: any, actionType: string) {
    this.openResourceAllocation(item, '', 'Assign & Approve');

  }

  ticketAction(item: any, actionType: string) {
   
    let reqObj = { ticketNumber: item.ticketNumber, actionType: actionType, reasonCode: '' }
    if (actionType == 'cancel' || actionType == 'complete' || actionType == 'close' || actionType == 'note') {
      let dialogRef = this.utilService.openCrudDialog(this.dialog, CommonRemarkComponent,'40');
      (dialogRef.componentInstance as any).title = actionType + " remarks" ;// (actionType == 'cancel' ? 'Reason Text' : 'Feed Back');
      dialogRef.afterClosed().subscribe(remark => {
        if (remark) {
          reqObj.reasonCode = remark;
          this.ticketActionRequest(reqObj);
        }
      });
    }
    else {
      this.ticketActionRequest(reqObj);
    }
  }

  ticketActionRequest(reqObj: any) {
    //console.log(reqObj);    
    this.apiService.post('fomMobJobTicketHead/frontOfficeB2CTicketAction', reqObj)
      .subscribe(res => {
        this.utilService.OkMessage();
        this.loadData();
      },
        error => {
          this.utilService.ShowApiErrorMessage(error);
        });

  }

}
