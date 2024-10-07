import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { AuthorizeService } from 'src/app/api-authorization/AuthorizeService';
import { ApiService } from 'src/app/services/api.service';
import { NotificationService } from 'src/app/services/notification.service';
import { UtilityService } from 'src/app/services/utility.service';
import { PaginationService } from 'src/app/sharedcomponent/pagination.service';
import { ParentFomMgtComponent } from 'src/app/sharedcomponent/parentfommgt.component';
import { CustomSelectListItem } from '../../models/MenuItemListDto';
import { DBOperation } from '../../services/utility.constants';
import { ParentB2CComponent } from '../../sharedcomponent/parentb2c.component';
import { AllschedulecalendarComponent } from '../GetScheduling/SharedPages/allschedulecalendar.component';
import { BtcschedulecalendarComponent } from './btcschedulecalendar.component';
import { ParentB2CFrontComponent } from '../../sharedcomponent/parentb2cfront.component';


@Component({
  selector: 'app-btcschedulelist',
  templateUrl: './btcschedulelist.component.html',
  styles: [
  ]
})
export class BtcschedulelistComponent extends ParentB2CFrontComponent implements OnInit {

  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort!: MatSort;

  form!: FormGroup;
  searchValue: string = '';
  sortingOrder: string = 'id desc';
  CustomerContractList: Array<CustomSelectListItem> = [];
  isLoading: boolean = false;
  totalItemsCount: number = 0;
  data: MatTableDataSource<any> = new MatTableDataSource();
  displayedColumns: string[] = ['contractCode', 'siteName', 'customerName', 'schDate', 'time', 'department', 'remarks', 'tranNumber', 'serType', 'frequency', 'serviceItem'];
  isArab: boolean = false;

  constructor(private apiService: ApiService, private authService: AuthorizeService, private translate: TranslateService,
    private utilService: UtilityService, private notifyService: NotificationService, public dialog: MatDialog,
    public pageService: PaginationService, private router: Router, private fb: FormBuilder) {
    super(authService);
    this.form = this.fb.group({
      startDate: [''], // Start Date FormControl
      endDate: [''], // End Date FormControl
      contractId: [''] // Contract ID FormControl
    });
  }

  ngOnInit(): void {
    this.isArab = this.utilService.isArabic();
    this.loadCustomerContract();
    this.initialLoading();
  }

  // navigateToRoute(): void {
  //   this.router.navigate(['/dashboard/profm/getschedulecalendar']); // Replace '/destination-route' with the actual route you want to navigate to
  // }

  refresh() {
    this.form.controls['startDate'].setValue(null);
    this.form.controls['endDate'].setValue(null);
    this.form.controls['contractId'].setValue(null);

    this.searchValue = '';
    this.initialLoading();
  }

  initialLoading() {
    this.loadListMain(0, this.pageService.pageCount, "", this.sortingOrder);
  }


  onPageSwitch(event: PageEvent) {
    this.pageService.change(event);
    this.loadListMain(event.pageIndex, event.pageSize, "", this.sortingOrder);
  }

  //formatTime(timeObj: any): string {
  //  const hours = timeObj.hours < 10 ? '0' + timeObj.hours : timeObj.hours;
  //  const minutes = timeObj.minutes < 10 ? '0' + timeObj.minutes : timeObj.minutes;
  //  // const seconds = timeObj.seconds < 10 ? '0' + timeObj.seconds : timeObj.seconds;
  //  return hours + ':' + minutes;
  //}

  private loadListMain(page: number | undefined, pageCount: number | undefined, query: string | null | undefined, orderBy: string | null | undefined) {
    this.isLoading = true;

    this.apiService.getPagination('FomCustomerContract/getB2CFrontOfficeSchedulingList', this.utilService.getQueryString(page, pageCount, query, orderBy)).subscribe(result => {
      this.totalItemsCount = 0;
      this.data = new MatTableDataSource(result.items);

      this.totalItemsCount = result.totalCount;

      setTimeout(() => {
        this.paginator.pageIndex = page as number;
        this.paginator.length = this.totalItemsCount;
      });
      this.data.sort = this.sort;

      //console.log(this.data.sort)
      //console.log(this.data.paginator)

      this.isLoading = false;
    }, error => this.utilService.ShowApiErrorMessage(error));
  }
  private loadList(page: number | undefined, pageCount: number | undefined, query: string | null | undefined, orderBy: string | null | undefined, contractId: number = 0, startDate: string | null | undefined, endDate: string | null | undefined) {
    this.isLoading = true;

    this.apiService.getPagination('FomCustomerContract/getB2CFrontOfficeSchedulingList', this.utilService.getQueryStringWithContractData(page, pageCount, query, orderBy, contractId, startDate, endDate)).subscribe(result => {
      this.totalItemsCount = 0;
      this.data = new MatTableDataSource(result.items);

      this.totalItemsCount = result.totalCount;

      setTimeout(() => {
        this.paginator.pageIndex = page as number;
        this.paginator.length = this.totalItemsCount;
      });
      this.data.sort = this.sort;

      //console.log(this.data.sort)
      //console.log(this.data.paginator)

      this.isLoading = false;
    }, error => this.utilService.ShowApiErrorMessage(error));
  }


  //loadCustomerContract(){
  //  this.apiService.getPagination('FomCustomerContract', this.utilService.getQueryString(0, 1000, '', '')).subscribe(res => {
  //    if (res)
  //      this.CustomerContractList = res['items'];
  //  });
  //}

  loadCustomerContract() {
    this.apiService.getall('fomCustomerContract/getCustomerContractSelectList').subscribe(res => {
      if (res) {
        this.CustomerContractList = res;
      }
    })
  }

  searchFilter() {
    var contractId = 0;
    var startDate = "";
    var endDate = "";
    //let sd = new Date(this.form.controls['startDate'].value);
    //let ed = new Date(this.form.controls['endDate'].value);
    //this.form.value['startDate'] = new Date(sd.getFullYear(), sd.getMonth(), sd.getDate() + 1);
    //this.form.value['endDate'] = new Date(ed.getFullYear(), ed.getMonth(), ed.getDate() + 1);

    if (this.form.controls['contractId'].value != undefined && this.form.controls['contractId'].value != null && this.form.controls['contractId'].value != '') {
      contractId = parseInt(this.form.controls['contractId'].value);
    }
    if (this.form.controls['startDate'].value != undefined && this.form.controls['startDate'].value != null && this.form.controls['startDate'].value != '') {
      let sd = new Date(this.form.controls['startDate'].value);
      startDate = sd.getFullYear().toString() + "-" + (sd.getMonth() + 1).toString() + "-" + sd.getDate().toString();
    }
    if (this.form.controls['endDate'].value != undefined && this.form.controls['endDate'].value != null && this.form.controls['endDate'].value != '') {
      let ed = new Date(this.form.controls['endDate'].value);
      endDate = ed.getFullYear().toString() + "-" + (ed.getMonth() + 1).toString() + "-" + ed.getDate().toString();
    }
    this.loadList(0, this.pageService.pageCount, "", this.sortingOrder, contractId, startDate, endDate);

    //this.apiService.get('FomCustomerContract/GetFomCalenderScheduleList/${contractId}/${this.utilService.selectedDate(startDate)}/${this.utilService.selectedDate(endDate)}`).subscribe(result => {
    //this.apiService.postFomUrl('FomCustomerContract/GetFomCalenderScheduleList', this.form.value).subscribe(result => {
    //  this.totalItemsCount = 0;
    //  this.data = result;//new MatTableDataSource(result);

    //  this.totalItemsCount = result.detailRows.length;

    //  setTimeout(() => {
    //    this.paginator.pageIndex = 10;//page as number
    //    this.paginator.length = this.totalItemsCount;
    //  });
    //  this.data.sort = this.sort;

    //  console.log(this.data.sort)
    //  console.log(this.data.paginator)

    //  this.isLoading = false;
    //}, error => this.utilService.ShowApiErrorMessage(error));

    // Now you have access to startDate, endDate, and contractId, you can perform further operations


  }


  private openGenSchDetailsRow(row: any, dbops: DBOperation, modalTitle: string, modalBtnTitle: string) {
    let dialogRef = this.utilService.openCrudDialog(this.dialog, BtcschedulecalendarComponent);
    (dialogRef.componentInstance as any).dbops = dbops;
    (dialogRef.componentInstance as any).modalTitle = modalTitle;
    (dialogRef.componentInstance as any).modalBtnTitle = modalBtnTitle;
    (dialogRef.componentInstance as any).row = row;
    dialogRef.afterClosed().subscribe(res => {
      if (res && res === true)
        this.initialLoading();
    });
  }

  showCalander() {
    this.openGenSchDetailsRow('', DBOperation.update, this.translate.instant('All_Schedule_Contract_Details'), 'All_Schedule_Details');
  }

  // applyFilter(searchValue: any) {
  //   const search = searchValue;//.target.value as string;
  //   //if (search && search.length >= 3) {
  //   if (search) {
  //     this.searchValue = search;
  //     this.loadList(0, this.pageService.pageCount, this.searchValue, this.sortingOrder);
  //   }
  // }

  // private openDialogManage(id: number = 0, dbops: DBOperation, modalTitle: string, modalBtnTitle: string) {
  //   let dialogRef = this.utilService.openCrudDialog(this.dialog, GetschedulelistComponent);
  //   (dialogRef.componentInstance as any).dbops = dbops;
  //   (dialogRef.componentInstance as any).modalTitle = modalTitle;
  //   (dialogRef.componentInstance as any).modalBtnTitle = modalBtnTitle;
  //   (dialogRef.componentInstance as any).id = id;

  //   dialogRef.afterClosed().subscribe(res => {
  //     if (res && res === true)
  //       this.initialLoading();
  //   });
  // }

  // public create() {
  //   this.openDialogManage(0, DBOperation.create, this.translate.instant('Add Sub Contractor'), 'Add');
  // }
  // public edit(id: number) {
  //   this.openDialogManage(id, DBOperation.update, this.translate.instant('Update Sub Contractor'), 'Update');
  // }

  // public delete(id: number) {
  //   const dialogRef = this.utilService.openDeleteConfirmDialog(this.dialog, DeleteConfirmDialogComponent);
  //   dialogRef.afterClosed().subscribe(canDelete => {
  //     if (canDelete && id > 0) {
  //       this.apiService.delete('', id).subscribe(res => {
  //         this.refresh();
  //         this.utilService.OkMessage();
  //       },
  //       );
  //     }
  //   },
  //     error => this.utilService.ShowApiErrorMessage(error));
  // }

}
