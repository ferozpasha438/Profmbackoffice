import { Component, OnInit, ViewChild, ElementRef, } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
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
import { DBOperation } from '../../../services/utility.constants';
import { GetschedulelistComponent } from './getschedulelist.component';

@Component({
  selector: 'app-getschedulecalendar',
  templateUrl: './getschedulecalendar.component.html',
})
export class GetschedulecalendarComponent extends ParentFomMgtComponent implements OnInit {

  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort!: MatSort;
//  @ViewChild('calendar') calendar: ElementRef | undefined;
  searchValue: string = '';
  sortingOrder: string = 'id desc';
  isLoading: boolean = false;
  totalItemsCount: number = 0;
  id: number = 0;
  selectedDate: Date = new Date();
  currentMonth: number | undefined;
  data: MatTableDataSource<any> = new MatTableDataSource();
  displayedColumns: string[] = ['contractId', 'schDate', 'department', 'serType', 'frequency', 'tranNumber', 'serviceItem','remarks'];
  isArab: boolean = false;

  @ViewChild('calendar') calendar!: ElementRef ;
  constructor(private apiService: ApiService, private authService: AuthorizeService, private translate: TranslateService,
    private utilService: UtilityService, private notifyService: NotificationService, public dialog: MatDialog, public dialogRef: MatDialogRef<GetschedulelistComponent>,
    public pageService: PaginationService, private router: Router) {
    super(authService);
  }

  


  ngOnInit(): void {
    this.isArab = this.utilService.isArabic();
    this.getCurrentAndLastDateOfMonth();
    //this.initialLoading();
  }


 
  //navigateToRoute(): void {
  //  this.router.navigate(['/dashboard/profm/getschedulelist']); // Replace '/destination-route' with the actual route you want to navigate to
  //}

  refresh() {
    this.searchValue = '';
   // this.initialLoading();
  }

  
  // events: any[] = [
  //   { title: 'Meeting', date: new Date(2024, 1, 29) },
  //   { title: 'Presentation', date: new Date(2024, 1, 29) },
  //   { title: 'Training', date: new Date(2024, 1, 29) }
  // ];

  //selectedDate: Date;

  //dateSelected(date: Date) {
  //  this.selectedDate = date;
  //  alert('dateSelected event');
  //}


  dateSelected(date: Date) {
    this.selectedDate = date;
    const newMonth = date.getMonth();
    if (newMonth !== this.currentMonth) {
      this.currentMonth = newMonth;
      //this.handleMonthChange();
    }
  }


  ngAfterViewInit() {
    const calendarElement = this.calendar.nativeElement;
    const nextButton = calendarElement.querySelector('.mat-calendar-next-button');
    if (nextButton) {
      nextButton.addEventListener('click', () => {
        console.log('Next button clicked');
        // Handle next button click event
      });
    }
  }



  //clickMonth() {
  //  const startDate = new Date(this.selectedDate.getMonth() + 1, 0);
  ////  const month = .getMonth();
  //  alert('hi click month' + startDate);
  // // this.selectedDate.setDate = startDate;
  //}

  handleMonthChange() {
    console.log('Month changed');
    alert('Month changed');
    // Do whatever you want when the month changes
  }

  handleMonthSelected(event: any): void {
    // Function to handle month selection
    console.log('Selected month:', event);
    alert('hi');
    // You can access event value to get the selected month information
  }

  getCurrentAndLastDateOfMonth() {
    if (this.selectedDate) {
      const startDate = this.selectedDate;

      const endDate = new Date(startDate.getFullYear(), startDate.getMonth() + 1, 0);
      var contractId = this.id;
      this.apiService.getall(`FomCustomerContract/GetFomCalenderScheduleList/${contractId}/${this.utilService.selectedDate(startDate)}/${this.utilService.selectedDate(endDate)}`).subscribe(result => {
        this.totalItemsCount = 0;
        if (result) {
          alert('Hi');
        }
      });



      //this.apiService.getall(`FomCustomerContract/GetFomCalenderScheduleList/${contractId}/${startDate}/${endDate}`).subscribe(res => {
      //  console.log(res);
      //  alert('hi');
      //  if (res) {
      //    //this.isSchGenerated = res['isSchGenerated'];
      //    // this.resetShedule();
      //    alert('res');
      //  }
      //});

      //this.apiService.getall(`FomCustomerContract/GetFomCalenderScheduleList/${contractId}/${currentDate}/${lastDateOfMonth}`).subscribe(res => {
      //  console.log(res);
      //  alert('hi');
      //  if (res) {
      //    //this.isSchGenerated = res['isSchGenerated'];
      //    // this.resetShedule();
      //    alert('res');
      //  }
      //});

      //this.apiService.getall(`FomCustomerContract/GetFomCalenderScheduleList/${contractId}/${startDate}/${endDate}`).subscribe(result => {
      //  this.totalItemsCount = 0;
      //  if (result) {
      //    alert('Hi');
      //  }
      

      //  this.isLoading = false;
      //}, error => this.utilService.ShowApiErrorMessage(error));
    }
  }


  initialLoading() {
    this.loadList(0, this.pageService.pageCount, "", this.sortingOrder);
  }

  private loadList(page: number | undefined, pageCount: number | undefined, query: string | null | undefined, orderBy: string | null | undefined) {
    this.isLoading = true;

    this.apiService.getPagination('', this.utilService.getQueryString(page, pageCount, query, orderBy)).subscribe(result => {
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

  // applyFilter(searchValue: any) {
  //   const search = searchValue;//.target.value as string;
  //   //if (search && search.length >= 3) {
  //   if (search) {
  //     this.searchValue = search;
  //     this.loadList(0, this.pageService.pageCount, this.searchValue, this.sortingOrder);
  //   }
  // }

   private openDialogManage(id: number = 0, dbops: DBOperation, modalTitle: string, modalBtnTitle: string) {
     let dialogRef = this.utilService.openCrudDialog(this.dialog, GetschedulelistComponent);
     (dialogRef.componentInstance as any).dbops = dbops;
     (dialogRef.componentInstance as any).modalTitle = modalTitle;
     (dialogRef.componentInstance as any).modalBtnTitle = modalBtnTitle;
     (dialogRef.componentInstance as any).id = id;

     dialogRef.afterClosed().subscribe(res => {
       if (res && res === true)
         this.initialLoading();
     });
   }

  // public create() {
  //   this.openDialogManage(0, DBOperation.create, this.translate.instant('Add Sub Contractor'), 'Add');
  // }
  public navigateToRoute() {
     this.openDialogManage(1, DBOperation.update, this.translate.instant('Update Sub Contractor'), 'Update');
   }

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


  closeModel() {
    this.dialogRef.close();
  }
}
