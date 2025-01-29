import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatCalendar, MatCalendarCellCssClasses } from '@angular/material/datepicker';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
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
import { ParentFomMgtComponent } from 'src/app/sharedcomponent/parentfommgt.component';
import { CustomSelectListItem } from '../../../models/MenuItemListDto';
import { ValidationService } from '../../../sharedcomponent/ValidationService';
import { GetschedulecalendarComponent } from './getschedulecalendar.component';

@Component({
  selector: 'app-getschedulelist',
  templateUrl: './getschedulelist.component.html',
})
export class GetschedulelistComponent extends ParentFomMgtComponent implements OnInit {

  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort!: MatSort;
  @ViewChild('calendar') calendar!: MatCalendar<Moment>;

  searchValue: string = '';
  contractCode: string = '';
  custCode: string = '';
  sortingOrder: string = 'id desc';
  isLoading: boolean = false;
  totalItemsCount: number = 0;
  form!: FormGroup;
  // myForm: FormGroup;
  row: any;
  isReadOnly: boolean = false;
  id: number = 0;
  deptCode: string = '';
  shId: number = 0;
  data: MatTableDataSource<any> = new MatTableDataSource();
  displayedColumns: string[] = ['contractCode', 'customerName','siteName', 'schDate', 'time', 'department', 'remarks', 'tranNumber', 'serType', 'frequency', 'serviceItem'];
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
    private authService: AuthorizeService, private utilService: UtilityService, public dialogRef: MatDialogRef<GetschedulelistComponent>,
    private notifyService: NotificationService, public dialog: MatDialog, public pageService: PaginationService, private validationService: ValidationService, private router: Router) {
    super(authService)
  }
  ngOnInit(): void {
    this.id = this.row.id;
    this.isArab = this.utilService.isArabic();
   //  this.initialLoading();
   
    this.loadData();
    this.setForm();
    if (this.id > 0)
      this.setEditForm();
  }

  //navigateToRoute(): void {
  //  this.router.navigate(['/dashboard/profm/getschedulecalendar']); // Replace '/destination-route' with the actual route you want to navigate to
  //}

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
        'startDate': [''],
        'endDate': [''],
        'deptCode': [''],
        'contDeptCode': [''],

      }
    );
    //   this.isReadOnly = false;
  }

  loadData() {
    this.apiService.getPagination('fomDiscipline', this.utilService.getQueryString(0, 1000, '', '')).subscribe(res => {
      if (res) {
        this.DepartmentCodeList = res['items'];
        this.DepartmentCodeList = this.DepartmentCodeList.filter(x => this.row.contDeptCode.split(",").includes(x.deptCode));
        if (this.id > 0)
          this.setEditForm();
      }

    });
  }


  setEditForm() {

    this.apiService.get('FomCustomerContract', this.id).subscribe(res => {
      // this.myForm.value['id'] = this.id;
      const deptCode: string = this.form.value['deptCode'] as string;
      console.log(res);
      if (res) {
        this.form.value['contractCode'] = res['contractCode'];
        this.form.patchValue(res);
      }
    });
  }

  reset() {
    this.form.controls['custSiteCode'].setValue('');
    this.form.controls['contStartDate'].setValue('');
    this.form.controls['contEndDate'].setValue('');
    this.form.controls['startDate'].setValue('');
    this.form.controls['endDate'].setValue('');
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
    const defaultPageIndex = 0; // Start with the first page
    const defaultPageSize = 10; // Default items per page
    const defaultQuery = ""; // No query initially
    const defaultOrderBy = "asc"; // Default sorting order

    // Call the method
    this.loadListMain(defaultPageIndex, defaultPageSize, defaultQuery, defaultOrderBy);
  //  this.loadList(0, this.pageService.pageCount, "", this.sortingOrder, this.contractCode, this.startDate, this.endDate, this.deptCode);
    
  }

  //old
  //private loadList(page: number | undefined, pageCount: number | undefined, query: string | null | undefined, orderBy: string | null | undefined) {
  //  this.isLoading = true;
  //  //const deptCode: string = 'AirConditioning';
  //  //const contractCode: string = 'ContractCode01';
  //  const deptCode: string = this.form.value['deptCode'] as string;
  //  const contractCode: string = this.form.value['contractCode'] as string;
  //  if (deptCode != undefined && deptCode != null && deptCode != '') {
  //    this.deptCode = deptCode;
  //    this.isShowCalander = true;
  //  } else {
  //    this.isShowCalander = false;
  //    this.deptCode = '';
  //  }
  //  this.apiService.getall(`FomCustomerContract/GetGeneratedSchedule/${deptCode}/${contractCode}`).subscribe(result => {
  //    this.totalItemsCount = 0;
  //    this.data = new MatTableDataSource(result.detailRows);
  //    this.scheduleData = result.detailRows;
  //    this.totalItemsCount = result.detailRows.length;
  //    setTimeout(() => {
  //      this.paginator.pageIndex = page as number;
  //      this.paginator.length = this.totalItemsCount;
  //    });
  //    this.data.sort = this.sort;
  //    this.isLoading = false;
  //  }, error => { this.utilService.ShowApiErrorMessage(error); this.isLoading = false; this.data = new MatTableDataSource(); this.totalItemsCount = 0; });
  //}


  private loadListMain(page: number | undefined, pageCount: number | undefined, query: string | null | undefined, orderBy: string | null | undefined) {
    this.isLoading = true;
    var startDate = "";
    var endDate = "";
    const deptCode: string = this.form.value['deptCode'] as string;
    const contractCode: string = this.form.get('contractCode')?.value;

    if (this.form.controls['startDate'].value != undefined && this.form.controls['startDate'].value != null && this.form.controls['startDate'].value != '') {
      let sd = new Date(this.form.controls['startDate'].value);
      startDate = sd.getFullYear().toString() + "-" + (sd.getMonth() + 1).toString() + "-" + sd.getDate().toString();
    }
    if (this.form.controls['endDate'].value != undefined && this.form.controls['endDate'].value != null && this.form.controls['endDate'].value != '') {
      let ed = new Date(this.form.controls['endDate'].value);
      endDate = ed.getFullYear().toString() + "-" + (ed.getMonth() + 1).toString() + "-" + ed.getDate().toString();
    }


    this.apiService.getPagination('FomCustomerContract/GetGeneratedScheduleFilter', this.utilService.getQueryFilterContractData(page, pageCount, query, orderBy, contractCode, startDate, endDate, deptCode)).subscribe(result => {
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




  //new
  private loadList(page: number | undefined, pageCount: number | undefined, query: string | null | undefined, orderBy: string | null | undefined, contractCode: string | null, startDate: string | null | undefined, endDate: string | null | undefined, deptCode: string | null) {
    this.isLoading = true;

    this.apiService.getPagination('FomCustomerContract/GetGeneratedScheduleFilter', this.utilService.getQueryFilterContractData(page, pageCount, query, orderBy, contractCode, startDate, endDate, deptCode)).subscribe(result => {
      this.totalItemsCount = 0;
      this.data = new MatTableDataSource(result.items);
      this.scheduleData = result.items;
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


  //onPageSwitch(event: PageEvent) {
  //  this.pageService.change(event);
  //  this.loadListMain(event.pageIndex, event.pageSize, "", this.sortingOrder);
  //}


  onPageSwitch(event: PageEvent) {

    this.pageService.change(event);
    var contractId = 0;
    var startDate = "";
    var endDate = "";
    //  const deptCode: string = 'AirConditioning';
    //const contractCode: string = 'ContractCode01';
    const deptCode: string = this.form.value['deptCode'] as string;
    const contractCode: string = this.form.get('contractCode')?.value;

    if (this.form.controls['startDate'].value != undefined && this.form.controls['startDate'].value != null && this.form.controls['startDate'].value != '') {
      let sd = new Date(this.form.controls['startDate'].value);
      startDate = sd.getFullYear().toString() + "-" + (sd.getMonth() + 1).toString() + "-" + sd.getDate().toString();
    }
    if (this.form.controls['endDate'].value != undefined && this.form.controls['endDate'].value != null && this.form.controls['endDate'].value != '') {
      let ed = new Date(this.form.controls['endDate'].value);
      endDate = ed.getFullYear().toString() + "-" + (ed.getMonth() + 1).toString() + "-" + ed.getDate().toString();
    }

    if (deptCode != undefined && deptCode != null && deptCode != '') {
      this.deptCode = deptCode;
      this.isShowCalander = true;
    } else {
      this.isShowCalander = false;
      this.deptCode = '';
    }

    if (endDate == null || endDate == "") {
      let ed = new Date(this.form.controls['contEndDate'].value);
      var EndDate = ed.getFullYear().toString() + "-" + (ed.getMonth() + 1).toString() + "-" + ed.getDate().toString();
      // const EndDate: string = this.form.value['contEndDate'] as string;
      endDate = EndDate;
    }

    if (startDate == null || startDate == "") {

      let sd = new Date(this.form.controls['contStartDate'].value);
      var StartDate = sd.getFullYear().toString() + "-" + (sd.getMonth() + 1).toString() + "-" + sd.getDate().toString();
      // const StartDate: string = this.form.value['contStartDate'] as string;
      startDate = StartDate;
    }
   // this.loadListMain(event.pageIndex, event.pageSize, "", this.sortingOrder);

    this.loadList(event.pageIndex, event.pageSize, "", this.sortingOrder, contractCode, startDate, endDate, this.deptCode);
  }


  getShedule() {

    var contractId = 0;
    var startDate = "";
    var endDate = "";
  //  const deptCode: string = 'AirConditioning';
    //const contractCode: string = 'ContractCode01';
    const deptCode: string = this.form.value['deptCode'] as string;
    const contractCode: string = this.form.get('contractCode')?.value;

    if (this.form.controls['startDate'].value != undefined && this.form.controls['startDate'].value != null && this.form.controls['startDate'].value != '') {
      let sd = new Date(this.form.controls['startDate'].value);
      startDate = sd.getFullYear().toString() + "-" + (sd.getMonth() + 1).toString() + "-" + sd.getDate().toString();
    }
    if (this.form.controls['endDate'].value != undefined && this.form.controls['endDate'].value != null && this.form.controls['endDate'].value != '') {
      let ed = new Date(this.form.controls['endDate'].value);
      endDate = ed.getFullYear().toString() + "-" + (ed.getMonth() + 1).toString() + "-" + ed.getDate().toString();
    }

    if (deptCode != undefined && deptCode != null && deptCode != '') {
      this.deptCode = deptCode;
      this.isShowCalander = true;
    } else {
      this.isShowCalander = false;
      this.deptCode = '';
    }

    if (endDate == null || endDate == "" ) {
      let ed = new Date(this.form.controls['contEndDate'].value);
    var  EndDate = ed.getFullYear().toString() + "-" + (ed.getMonth() + 1).toString() + "-" + ed.getDate().toString();
     // const EndDate: string = this.form.value['contEndDate'] as string;
      endDate = EndDate;
    }

    if (startDate == null || startDate == "" ) {

      let sd = new Date(this.form.controls['contStartDate'].value);
     var StartDate = sd.getFullYear().toString() + "-" + (sd.getMonth() + 1).toString() + "-" + sd.getDate().toString();
     // const StartDate: string = this.form.value['contStartDate'] as string;
      startDate = StartDate;
    }
    this.loadList(0, this.pageService.pageCount, "", this.sortingOrder, contractCode, startDate, endDate, this.deptCode);


 
  }


  applyFilter(searchValue: any) {
    const search = searchValue;//.target.value as string;
    //if (search && search.length >= 3) {
    if (search) {
      this.searchValue = search;
     // this.loadList(0, this.pageService.pageCount, this.searchValue, this.sortingOrder);
    }
  }

  private openDialogManage(id: number = 0, dbops: DBOperation, modalTitle: string, modalBtnTitle: string) {
    let dialogRef = this.utilService.openCrudDialog(this.dialog, GetschedulecalendarComponent);
    (dialogRef.componentInstance as any).dbops = dbops;
    (dialogRef.componentInstance as any).modalTitle = modalTitle;
    (dialogRef.componentInstance as any).modalBtnTitle = modalBtnTitle;
    (dialogRef.componentInstance as any).id = id;

    dialogRef.afterClosed().subscribe(res => {
      if (res && res === true)
        this.initialLoading();
    });
  }

  //public create() {
  //  this.openDialogManage(0, DBOperation.create, this.translate.instant('Add Sub Contractor'), 'Add');
  //}
  public showCalander() {
    this.isShowList = true;
    this.getCurrentAndLastDateOfMonth();
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
    this.scheduleDateData=[];
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
      if (highlightDate) {
        return highlightDate ? 'special-event-date' : '';
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
  getCurrentAndLastDateOfMonth() {
    
    this.selectedDate = new Date();
    if (this.selectedDate) {
      var contractId = this.id;
      this.scheduleDays = [];
      for (var i = 0; i < this.scheduleData.length; i++) {
        this.scheduleDays.push(this.scheduleData[i].schDate);
      }
      this.isShowList = false;
      setTimeout(() => {
        this.scheduleDateData = this.scheduleData
          .filter(d => new Date(d.schDate).getDate() === this.selectedDate.getDate()
            && new Date(d.schDate).getMonth() === this.selectedDate.getMonth()
            && new Date(d.schDate).getFullYear() === this.selectedDate.getFullYear());
      }, 500);
      
      


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

  //searchFilter
  
  searchFilter() {
    this.getShedule();
  }



    


 

  closeModel() {
    this.dialogRef.close();
  }

}
