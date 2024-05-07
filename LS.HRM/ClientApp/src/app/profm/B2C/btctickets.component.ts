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


@Component({
  selector: 'app-btctickets',
  templateUrl: './btctickets.component.html'
})

export class BtcTicketsComponent extends ParentB2CComponent implements OnInit {
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

  }
  setForm() {
    this.form = this.fb.group({});
    // this.loadStatusSelectionList();
  }

  loadData() {
    this.isLoading = true;
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

  resetFilter(serviceType: string = 'All') {
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
    };
    this.loadData();
  }

  setServiceType(serviceType: string) {
    //this.filter.serviceType = serviceType;   
    this.resetFilter(serviceType);
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

  ticketAction(item: any, actionType: string) {
    let reqObj = { ticketNumber: item.ticketNumber, actionType: actionType, reasonCode: '' }
    if (actionType == 'cancel' || actionType == 'foreclose') {
      let dialogRef = this.utilService.openCrudDialog(this.dialog, CommonRemarkComponent);
      (dialogRef.componentInstance as any).title = 'Reason Text';
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
