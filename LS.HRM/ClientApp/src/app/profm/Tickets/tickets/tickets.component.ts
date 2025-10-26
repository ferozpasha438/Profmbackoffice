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
import { ParentSystemSetupComponent } from 'src/app/sharedcomponent/parentsystemsetup.component';
import { FomSharedService } from '../../../services/fomShared.service';
import { ParentFomMgtComponent } from '../../../sharedcomponent/parentfommgt.component';
import { CustomSelectListItem } from '../../../models/MenuItemListDto';
import { TicketstatusactionComponent } from '../shared/ticketstatusaction/ticketstatusaction.component';


@Component({
  selector: 'app-tickets',
  templateUrl: './tickets.component.html',
  styleUrls: ['./tickets.component.scss']
})

export class TicketsComponent extends ParentFomMgtComponent implements OnInit {
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
    statusStr: ''
  };
  totalItems = 0;
  search: string = '';
  statusSelectionList: Array<any> = [];
  statusSelectionListActions: Array<CustomSelectListItem> = [];
  isLoading = false;
  totalItemsCount: number = 0;
  data: Array<any> = [];
  remarks: string = '';

  joStatusVoid: number = 3;
  joStatusApproved: number = 5;
  joStatusWorkInProgress: number = 7;
  joStatusCompleted: number = 11;
  joStatusClosed: number = 9;

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
    this.loadStatusSelectionList();
  }

  loadData() {
    this.isLoading = true;
    this.apiService.postFomCpUrl('FomCustomerContract/getTicketsListPaginationWithFilter', this.filter).subscribe(result => {
      this.isLoading = false;
      this.totalItems = result?.totalCount ?? 0;
      this.data = result.items.slice();
    });
  }
  loadStatusSelectionList() {

    this.apiService.getall('FomCustomerContract/getSelectJobStatusesEnum').subscribe(res => {
      this.statusSelectionList = res.slice();
    });
    this.apiService.getall('FomCustomerContract/getSelectJobStatusesEnumForTicketList').subscribe(res => {
      this.statusSelectionListActions = res.slice();
    });

  }

  resetFilter() {
    this.filter = {
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
    this.loadData();
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


  private openDialogManage(ticketNumber: string, dbops: DBOperation, modalTitle: string, modalBtnTitle: string, component: any) {
    let dialogRef = this.utilService.openCrudDialog(this.dialog, component);
    (dialogRef.componentInstance as any).dbops = dbops;
    (dialogRef.componentInstance as any).modalTitle = modalTitle;
    (dialogRef.componentInstance as any).modalBtnTitle = modalBtnTitle;
    (dialogRef.componentInstance as any).ticketNumber = ticketNumber;

    dialogRef.afterClosed().subscribe(res => {
      if (res && res === true)
        this.loadData();
    });
  }

  ticketdetail(ticketNumber: string) {
    this.sharedService.ticketNumberToEdit = ticketNumber;
    this.router.navigate(['/dashboard/profm/ticketdetail']);
  }

  changeStatus(evt: any, row: any) {
    const ticketStatus = +evt.target.value;
    if (ticketStatus > 0) {
      const dialogRef = this.utilService.openDeleteConfirmDialog(this.dialog, DeleteConfirmDialogComponent);
      dialogRef.afterClosed().subscribe(canDelete => {
        if (canDelete) {
          if (ticketStatus == this.joStatusVoid || ticketStatus == this.joStatusApproved || ticketStatus == this.joStatusClosed || ticketStatus == this.joStatusCompleted) { //For Void or Cancel or complete
            let statusRef = this.utilService.openCrudDialog(this.dialog, TicketstatusactionComponent, '40%');
            (statusRef.componentInstance as any).modalTitle = 'Enter_Remarks';
            statusRef.afterClosed().subscribe(res => {
              if (res && res.length > 0) {
                this.remarks = res;
                this.changeTicketStatus(ticketStatus, row.ticketNumber);
              }
            });
          }
          else {

            this.changeTicketStatus(ticketStatus, row.ticketNumber);
          }
        }
        else {
          //this.resetFilter();
          this.loadData();
        }
      });
    }
  }

  changeTicketStatus(ticketStatus: number, ticketNumber: string) {
    this.apiService.post(`FomCustomerContract/changeJobStatusForTicket`, { userName: this.authService.getUserName(), status: ticketStatus, ticketNumber: ticketNumber, remarks: this.remarks }).subscribe(res => {
      this.utilService.OkMessage();
      this.loadData();
    }, error => {
      this.utilService.ShowApiErrorMessage(error);
    });
  }

  statusSelectionListActionItems(joStatus: any): Array<CustomSelectListItem> {
    if (joStatus == this.joStatusApproved) {
      return this.getStatusSelectionListActionItems([this.joStatusVoid.toString(), this.joStatusApproved.toString(), this.joStatusWorkInProgress.toString()]);
    }
    else if (joStatus == this.joStatusWorkInProgress) {
      return this.getStatusSelectionListActionItems([this.joStatusClosed.toString(), this.joStatusWorkInProgress.toString(), this.joStatusCompleted.toString()]);
    }
    else if (joStatus == this.joStatusClosed) {
      return this.getStatusSelectionListActionItems([this.joStatusClosed.toString(), this.joStatusCompleted.toString()]);
    }
    else if (joStatus == this.joStatusCompleted) {
      return this.getStatusSelectionListActionItems([this.joStatusCompleted.toString()]);
    }
    return this.getStatusSelectionListActionItems([this.joStatusVoid.toString(), this.joStatusApproved.toString()]);
  }

  getStatusSelectionListActionItems(joStatusList: Array<string>): Array<CustomSelectListItem> {
    return this.statusSelectionListActions.filter(item => joStatusList.includes(item.value));
  }

}
