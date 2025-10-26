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
import { AllschedulecalendarComponent } from './SharedPages/allschedulecalendar.component';
import { DeleteConfirmDialogComponent } from '../../sharedcomponent/delete-confirm-dialog';
import { TicketstatusactionComponent } from '../Tickets/shared/ticketstatusaction/ticketstatusaction.component';
import { FomSharedService } from '../../services/fomShared.service';
import { ScheduleDetailComponent } from './scheduledetail/scheduledetail.component';
import { formatDate } from '@angular/common';

 @Component({
  selector: 'app-getscheduleslistdates',
  templateUrl: './getscheduleslistdates.component.html',
 })
export class GetscheduleslistdatesComponent extends ParentFomMgtComponent implements OnInit {

  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort!: MatSort;

  form!: FormGroup;
  searchValue: string = '';
  sortingOrder: string = 'id desc';
  CustomerContractList: Array<CustomSelectListItem> = [];
  isLoading: boolean = false;
  totalItemsCount: number = 0;
  data: MatTableDataSource<any> = new MatTableDataSource();
   displayedColumns: string[] = ['contractCode', 'siteName', 'customerName', 'schDate', 'time', 'department', 'remarks', 'tranNumber', 'statusStr', 'action'];//'serType', 'frequency',  'serviceItem'
   isArab: boolean = false;
   statusSelectionList: Array<any> = [];
   statusSelectionListActions: Array<CustomSelectListItem> = [];

   joStatusOpen: number = 0;
   joStatusCompleted: number = 1;
   joStatusVoid: number = 2;
   remarks: string = '';
   formData!: FormData;
  constructor(private apiService: ApiService, private authService: AuthorizeService, private translate: TranslateService,
    private utilService: UtilityService, private notifyService: NotificationService, public dialog: MatDialog, private sharedService: FomSharedService,
    public pageService: PaginationService, private router: Router, private fb: FormBuilder) {
    super(authService);
    this.form = this.fb.group({
      startDate: [''], // Start Date FormControl
      endDate: [''], // End Date FormControl
      contractId: [''], // Contract ID FormControl
      status: [''], // Contract ID FormControl

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

  formatTime(timeObj: any): string {
    const hours = timeObj.hours < 10 ? '0' + timeObj.hours : timeObj.hours;
    const minutes = timeObj.minutes < 10 ? '0' + timeObj.minutes : timeObj.minutes;
   // const seconds = timeObj.seconds < 10 ? '0' + timeObj.seconds : timeObj.seconds;
    return hours + ':' + minutes ;
   }

   private loadListMain(page: number | undefined, pageCount: number | undefined, query: string | null | undefined, orderBy: string | null | undefined) {
     this.isLoading = true;

     this.apiService.getPagination('FomCustomerContract/GetAllSchedulingList', this.utilService.getQueryString(page, pageCount, query, orderBy)).subscribe(result => {
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
   private loadList(page: number | undefined, pageCount: number | undefined, query: string | null | undefined, orderBy: string | null | undefined, contractId: number = 0, startDate: string | null | undefined, endDate: string | null | undefined, status: string | undefined) {
    this.isLoading = true;

     this.apiService.getPagination('FomCustomerContract/GetAllSchedulingList', this.utilService.getQueryStringWithContractData(page, pageCount, query, orderBy, contractId, startDate, endDate, status)).subscribe(result => {
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

   ticketdetail(row: any) {
     row.timeFormat = this.formatTime(row.time);
    let scheDetailRef = this.utilService.openCrudDialog(this.dialog, ScheduleDetailComponent, '100%');
     (scheDetailRef.componentInstance as any).scheduleId = row.id;
     (scheDetailRef.componentInstance as any).data = row;
     scheDetailRef.afterClosed().subscribe(res => {

     });
   }

   changeStatus(evt: any, row: any) {
     const ticketStatus = +evt.target.value;
     if (ticketStatus > 0) {
       const dialogRef = this.utilService.openDeleteConfirmDialog(this.dialog, DeleteConfirmDialogComponent);
       dialogRef.afterClosed().subscribe(canDelete => {
         if (canDelete) {
           if (ticketStatus == this.joStatusVoid || ticketStatus == this.joStatusCompleted) { //For Void or Complete        
             let statusRef = this.utilService.openCrudDialog(this.dialog, TicketstatusactionComponent, '40%');
             (statusRef.componentInstance as any).modalTitle = 'Enter_Remarks';
             (statusRef.componentInstance as any).hasFile = true;
             statusRef.afterClosed().subscribe(res => {
               if (res && res.remarks.length > 0) {
                 this.formData = new FormData();
                 this.remarks = res.remarks;
                 if (res.uploadfile) {
                   this.formData.append("fileone", res.uploadfile, res.uploadfile.name);                   
                 }
                 if (res.uploadfileTwo) {
                   this.formData.append("filetwo", res.uploadfileTwo, res.uploadfileTwo.name);                   
                 }                 
                 this.changeTicketStatus(ticketStatus, row.id);
               }
             });
           }
           else {
             //this.changeTicketStatus(ticketStatus, row.id);
           }
         }
         else {
           //this.resetFilter();
           this.initialLoading();
         }
       });
     }
   }

   changeTicketStatus(ticketStatus: number, id: number) {
     this.formData.append("input", JSON.stringify({ userName: this.authService.getUserName(), status: ticketStatus, id: id, remarks: this.remarks }));
     this.apiService.post(`FomCustomerContract/changePptMgmtStatusForPpt`, this.formData).subscribe(res => {
       this.utilService.OkMessage();
       this.initialLoading();
     }, error => {
       this.utilService.ShowApiErrorMessage(error);
     });
   }


   statusSelectionListActionItems(joStatus: any): Array<CustomSelectListItem> {
     if (joStatus == this.joStatusVoid) {
       return this.getStatusSelectionListActionItems([this.joStatusVoid.toString()]);
     }
     else if (joStatus == this.joStatusCompleted) {
       return this.getStatusSelectionListActionItems([this.joStatusCompleted.toString()]);
     }    
     return this.getStatusSelectionListActionItems([this.joStatusVoid.toString(), this.joStatusOpen.toString(), this.joStatusCompleted.toString()]);
   }

   getStatusSelectionListActionItems(joStatusList: Array<string>): Array<CustomSelectListItem> {
     return this.statusSelectionListActions.filter(item => joStatusList.includes(item.value));
   }


   //loadCustomerContract(){
   //  this.apiService.getPagination('FomCustomerContract', this.utilService.getQueryString(0, 1000, '', '')).subscribe(res => {
   //    if (res)
   //      this.CustomerContractList = res['items'];
   //  });
   //}

   loadCustomerContract() {
     this.apiService.getall('FomCustomerContract/GetCustomerContractSelectList').subscribe(res => {
       if (res) {
         this.CustomerContractList = res;
       }
     });
     this.apiService.getall('FomCustomerContract/getSelectPPTMgmtStatusEnumForPPTList').subscribe(res => {
       this.statusSelectionListActions = res;
       this.statusSelectionList = res;
     });

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
       startDate = sd.getFullYear().toString()+"-"+ (sd.getMonth()+1).toString()+"-"+ sd.getDate().toString();
     }
     if (this.form.controls['endDate'].value != undefined && this.form.controls['endDate'].value != null && this.form.controls['endDate'].value != '') {
       let ed = new Date(this.form.controls['endDate'].value);
       endDate = ed.getFullYear().toString() + "-" + (ed.getMonth()+1).toString() + "-" + ed.getDate().toString();
     }
     this.loadList(0, this.pageService.pageCount, "", this.sortingOrder, contractId, startDate, endDate, this.form.controls['status'].value ?? '');

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
     let dialogRef = this.utilService.openCrudDialog(this.dialog, AllschedulecalendarComponent);
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
