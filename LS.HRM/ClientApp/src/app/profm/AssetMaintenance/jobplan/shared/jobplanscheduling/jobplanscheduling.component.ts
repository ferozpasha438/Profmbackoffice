import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatCalendar, MatCalendarCell, MatCalendarCellCssClasses } from '@angular/material/datepicker';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { Moment } from 'moment';
import { AuthorizeService } from 'src/app/api-authorization/AuthorizeService';
import { ApiService } from 'src/app/services/api.service';
import { NotificationService } from 'src/app/services/notification.service';
import { DBOperation } from 'src/app/services/utility.constants';
import { UtilityService } from 'src/app/services/utility.service';
import { DeleteConfirmDialogComponent } from 'src/app/sharedcomponent/delete-confirm-dialog';
import { PaginationService } from 'src/app/sharedcomponent/pagination.service';
import { ParentB2CFrontComponent } from '../../../../../sharedcomponent/parentb2cfront.component';
import { ValidationService } from '../../../../../sharedcomponent/ValidationService';

@Component({
  selector: 'app-jobplanscheduling',
  templateUrl: './jobplanscheduling.component.html',
  styles: [
  ]
})
export class JobplanschedulingComponent extends ParentB2CFrontComponent implements OnInit {

  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort!: MatSort;
  @ViewChild('calendar') calendar!: MatCalendar<Moment>;

  @Input() jobPlanCode: string = '';

  searchValue: string = '';
  contractCode: string = '';
  custCode: string = '';
  sortingOrder: string = 'id desc';
  isLoading: boolean = false;
  totalItemsCount: number = 0;
  form!: FormGroup;
  // myForm: FormGroup;
  row: any = { id: 0 };
  isReadOnly: boolean = false;
  id: number = 0;
  deptCode: string = '';
  shId: number = 0;
  data: MatTableDataSource<any> = new MatTableDataSource();
  isArab: boolean = false;
  //DepartmentCodeList: Array<CustomSelectListItem> = [];
  DepartmentCodeList: Array<any> = [];
  isShowCalander: boolean = false;
  isShowList: boolean = true;

  selectedDate: Date = new Date();
  currentMonth: number | undefined;
  scheduleData: Array<any> = [];
  scheduleDateData: Array<any> = [];
  scheduleDays: Array<any> = [];
  generalDays: Array<any> = [];

  //constructor(private fb: FormBuilder,private apiService: ApiService, private authService: AuthorizeService, private translate: TranslateService,
  //  private utilService: UtilityService, private notifyService: NotificationService, public dialog: MatDialog,
  //  public pageService: PaginationService, private router: Router) {
  //  super(authService);
  //}


  constructor(private fb: FormBuilder, private apiService: ApiService, private translate: TranslateService,
    private authService: AuthorizeService, private utilService: UtilityService, //public dialogRef: MatDialogRef<JobplanschedulingComponent>,
    private notifyService: NotificationService, public dialog: MatDialog, public pageService: PaginationService, private validationService: ValidationService, private router: Router) {
    super(authService)
  }
  ngOnInit(): void {
    this.id = this.row.id;
    this.isArab = this.utilService.isArabic();
    this.loadData();
  }

  refresh() {
    this.searchValue = '';
    this.initialLoading();
  }

  setForm() {
    this.form = this.fb.group(
      {
        'contractCode': [''],
        'custCode': [''],
        'contStartDate': [''],
        'contEndDate': [''],
        'deptCode': [''],
        'contDeptCode': [''],

      }
    );
    //   this.isReadOnly = false;
  }

  loadData() {
    this.getCurrentAndLastDateOfMonth();
  }


  setEditForm() {

  }

  reset() {
    this.form.controls['custSiteCode'].setValue('');
    this.form.controls['contStartDate'].setValue('');
    this.form.controls['contEndDate'].setValue('');
    this.form.controls['contDeptCode'].setValue('');
    this.form.controls['contProjManager'].setValue('');
    this.form.controls['contProjSupervisor'].setValue('');
    this.form.controls['remarks'].setValue('');
    this.form.controls['contApprAuthorities'].setValue('');
    this.form.controls['IsAppreoved'].setValue('');
    this.form.controls['IsSheduleRequired'].setValue('');
    this.form.controls['approvedDate'].setValue('');
    this.form.controls['isActive'].setValue('');
  }

  initialLoading() {
  }

  getShedule() {
    this.initialLoading();
  }


  applyFilter(searchValue: any) {
    const search = searchValue;//.target.value as string;
    //if (search && search.length >= 3) {
    if (search) {
      this.searchValue = search;
    }
  }



  //public create() {
  //  this.openDialogManage(0, DBOperation.create, this.translate.instant('Add Sub Contractor'), 'Add');
  //}
  public showCalander() {
    this.isShowList = true;
    //const calendarElement = this.calendar.nativeElement;
    //const nextButton = calendarElement.querySelector('.mat-calendar-next-button');
    //if (nextButton) {
    //  nextButton.addEventListener('click', () => {
    //    console.log('Next button clicked');
    //    alert('Month changed');
    //  });
    //}
  }
  public showList() {
    this.isShowList = true;
  }
  dateSelected(date: Date) {
    this.scheduleDateData = [];
    this.selectedDate = date;
    const newMonth = date.getMonth();
    if (newMonth !== this.currentMonth) {
      this.currentMonth = newMonth;
    }
    this.scheduleDateData = this.scheduleData
      .filter(d => new Date(d.schDate).getDate() === date.getDate()
        && new Date(d.schDate).getMonth() === date.getMonth()
        && new Date(d.schDate).getFullYear() === date.getFullYear());
  }
  monthSelected(date: Date) {
    // alert(date);
    setTimeout(() => {
      this.checkRowData(date);
    }, 500);
    //alert(date);
    //this.isShowList = true;
    //this.selectedDate = date;
    //const newMonth = date.getMonth();
    //if (newMonth !== this.currentMonth) {
    //  this.currentMonth = newMonth;
    //}
    //this.getCurrentAndLastDateOfMonth();
  }
  onActiveDateChange(date: Date) {
    //this.selectedDate = date;
    //const newMonth = date.getMonth();
    //if (newMonth !== this.currentMonth) {
    //  this.currentMonth = newMonth;
    //}
    //this.getCurrentAndLastDateOfMonth();
  }
  //highlight(days: string[]) {
  //  const day = document.querySelectorAll(
  //    'mat-calendar .mat-calendar-table .mat-calendar-body-cell'
  //  );
  //  Array.from(day).forEach((element) => {
  //    const matchDay = days.find((d) => d === element.getAttribute('aria-label')) !== undefined;
  //    if (matchDay) {
  //      this.renderer.addClass(element, 'available');
  //      this.renderer.setAttribute(element, 'title', 'Event 1');
  //    } else {
  //      this.renderer.removeClass(element, 'available');
  //      this.renderer.removeAttribute(element, 'title');
  //    }
  //  });
  //}



  //clickMonth() {
  //  const startDate = new Date(this.selectedDate.getMonth() + 1, 0);
  ////  const month = .getMonth();
  //  alert('hi click month' + startDate);
  // // this.selectedDate.setDate = startDate;
  //}

  handleMonthChange() {
    // Do whatever you want when the month changes
  }

  handleMonthSelected(event: any): void {
    // Function to handle month selection
    // You can access event value to get the selected month information
  }
  dateClass() {
    return (date: Date): MatCalendarCellCssClasses => {


      var highlightDate = this.scheduleDays
        .map(strDate => new Date(strDate))
        .some(d => d.getDate() === date.getDate()
          && d.getMonth() === date.getMonth()
          && d.getFullYear() === date.getFullYear());
      console.log('highlightDate', this.scheduleDays);
      if (highlightDate) {
        return highlightDate ? 'special-event-date-ppm date' + date.getDate() + date.getMonth() + date.getFullYear() : '';
      }
      else {
        highlightDate = this.generalDays
          .map(strDate => new Date(strDate))
          .some(d => d.getDate() === date.getDate()
            && d.getMonth() === date.getMonth()
            && d.getFullYear() === date.getFullYear());
        if (highlightDate) {
          return highlightDate ? 'special-holiday-date' : '';
        }
        else {
          highlightDate = this.generalDays
            .map(strDate => new Date(strDate))
            .some(d => d.getDate() === date.getDate()
              && d.getMonth() === date.getMonth()
              && d.getFullYear() === date.getFullYear());
          return highlightDate ? 'special-date' : '';
        }
      }
    };
  }
  //customDate() {
  //  return (date: Date): CustomDateCellComponent => {

  //    if (highlightDate) {
  //      return highlightDate.length > 0 ? "<div class='dynamicNumber'>" + highlightDate.length + "</div>" : "";
  //    }
  //    else {
  //      highlightDate = this.generalDays
  //        .map(strDate => new Date(strDate))
  //        .filter(d => d.getDate() === date.getDate()
  //          && d.getMonth() === date.getMonth()
  //          && d.getFullYear() === date.getFullYear());
  //      if (highlightDate) {
  //        return highlightDate.length > 0 ? "<div class='dynamicNumber'>" + highlightDate.length + "</div>" : "";
  //      }
  //      else {
  //        highlightDate = this.generalDays
  //          .map(strDate => new Date(strDate))
  //          .filter(d => d.getDate() === date.getDate()
  //            && d.getMonth() === date.getMonth()
  //            && d.getFullYear() === date.getFullYear());
  //        return highlightDate.length > 0 ? "<div class='dynamicNumber'>" + highlightDate.length + "</div>" : "";
  //      }
  //    }
  //  };
  //}
  incrementDate(date: Date, days: number) {
    const newDate = new Date(date);
    newDate.setDate(newDate.getDate() + days);
    return newDate;
  }
  checkRowData(stDate: Date) {
    var sDate = new Date(stDate.getFullYear(), stDate.getMonth(), 1);
    var eDate = new Date(stDate.getFullYear(), stDate.getMonth() + 1, 0);
    while (sDate <= eDate) {
      var highlightDateData = this.scheduleDays
        .map(strDate => new Date(strDate))
        .filter(d => d.getDate() === sDate.getDate()
          && d.getMonth() === sDate.getMonth()
          && d.getFullYear() === sDate.getFullYear());
      if (highlightDateData && highlightDateData.length > 0) {
        const elements = document.getElementsByClassName('dynamic' + sDate.getDate() + sDate.getMonth() + sDate.getFullYear());
        if (elements.length == 0) {
          const parent = document.getElementsByClassName('date' + sDate.getDate() + sDate.getMonth() + sDate.getFullYear())[0];
          const newDiv = document.createElement('div');
          newDiv.textContent = highlightDateData.length.toString();
          newDiv.classList.add('dynamicNumber'); // Optional: Add a class name
          newDiv.classList.add('dynamic' + sDate.getDate() + sDate.getMonth() + sDate.getFullYear());
          if (parent) {
            parent.appendChild(newDiv);
          }
        }
      }
      sDate = this.incrementDate(sDate, 1);
    }
  }
  getCurrentAndLastDateOfMonth() {
    this.selectedDate = new Date();
    if (this.selectedDate) {
      this.scheduleData = [];
      this.scheduleDays = [];
      this.apiService.getall(`assetMaintenance/allJobPlanFomCalenderScheduleList?jobPlanCode=${this.jobPlanCode}`).subscribe(result => {
        this.totalItemsCount = 0;
        if (result) {
          this.scheduleData = result;
          for (var i = 0; i < result.length; i++) {
            this.scheduleDays.push(result[i].schDate);
          }
        }
        this.isShowList = false;
        setTimeout(() => {
          this.checkRowData(this.selectedDate);
          this.scheduleDateData = this.scheduleData
            .filter(d => new Date(d.schDate).getDate() === this.selectedDate.getDate()
              && new Date(d.schDate).getMonth() === this.selectedDate.getMonth()
              && new Date(d.schDate).getFullYear() === this.selectedDate.getFullYear());
        }, 500);
      }, error => this.isShowList = false);



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


  //public delete(id: number) {
  //  const dialogRef = this.utilService.openDeleteConfirmDialog(this.dialog, DeleteConfirmDialogComponent);
  //  dialogRef.afterClosed().subscribe(canDelete => {
  //    if (canDelete && id > 0) {
  //      this.apiService.delete('', id).subscribe(res => {
  //        this.refresh();
  //        this.utilService.OkMessage();
  //      },
  //      );
  //    }
  //  },
  //    error => this.utilService.ShowApiErrorMessage(error));
  //}

  closeModel() {
    //this.dialogRef.close();
  }

}
