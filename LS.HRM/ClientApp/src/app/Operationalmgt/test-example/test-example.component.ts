import { DatePipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatCalendar, MatCalendarCellCssClasses } from '@angular/material/datepicker';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { DateTime } from 'luxon';
import { Moment } from 'moment';

import { ChartComponent } from "ng-apexcharts";

import {
  ApexNonAxisChartSeries,
  ApexResponsive,
  ApexChart
} from "ng-apexcharts";
import { AuthorizeService } from '../../api-authorization/AuthorizeService';
import { ApiService } from '../../services/api.service';
import { NotificationService } from '../../services/notification.service';
import { UtilityService } from '../../services/utility.service';
import { PaginationService } from '../../sharedcomponent/pagination.service';
import { ParentOptMgtComponent } from '../../sharedcomponent/parentoptmgt.component';
import { ValidationService } from '../../sharedcomponent/ValidationService';
import { OprServicesService } from '../opr-services.service';

export type ChartOptions = {
  series: ApexNonAxisChartSeries;
  chart: ApexChart;
  responsive: ApexResponsive[];
  labels: any;
};

@Component({
  selector: 'app-test-example',
  templateUrl: './test-example.component.html',
  providers: [DatePipe]
})

export class TestExampleComponent extends ParentOptMgtComponent implements OnInit {
  @ViewChild("chart") chart!: ChartComponent;
  public chartOptions1!: Partial<ChartOptions>;
  public chartOptions2!: Partial<ChartOptions>;
  public chartOptions3!: Partial<ChartOptions>;
  isArab: boolean = false;
  isLoadSchoolDashBoard: boolean = false;
  isLoading: boolean = false;
  attendanceData: MatTableDataSource<any>;
  totalItemsCount: number;
  sortingOrder: string = 'id desc';
  interval: any;
  @ViewChild('calendar') calendar!: MatCalendar<Moment>;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  displayedColumns: string[] = ['projectCode', 'projectName', 'siteCode', 'siteName', 'employeeNumber', 'employeeName','shiftCode', 'inTime', 'outTime', 'late', 'isGeofenseOut', 'geofenseOutCount', 'overtime', 'isOnBreak', 'Actions'];

  oprDashboard: any = { notReportedEmpCount: 1, totalEmpCount: 1, reportedEmpCount: 1 };
  //todayAttData: number[] = [];

  pageNumber = 0;
  pageSize = 10;

  isShowDonut: boolean = false;
  date: any = new Date('2022-11-14');
  filterOptions: Array<any> = [{ key: "late", isSelected: false }];
  input: any = {
   // date: this.date,
    pageNumber: 0,
    pageSize: 10,
    branchCode: '',
    siteCode: '',
    projectCode: '',
    employeeNumber: '',
    filterOptions: this.filterOptions.slice(),
  };

  citySelectionList: Array<any> = [];
  projectsSelectionList: Array<any> = [];
  sitesSelectionList: Array<any> = [];
  employeesSelectionList: Array<any> = [];
  constructor(private datePipe: DatePipe, private fb: FormBuilder, private http: HttpClient, private router: Router, private apiService: ApiService, private authService: AuthorizeService,
    private notifyService: NotificationService, private utilService: UtilityService, private validationService: ValidationService, public dialog: MatDialog, private oprService: OprServicesService,
    public pageService: PaginationService, private translate: TranslateService) {
    super(authService);
  }

  ngOnInit(): void {
    //this.date=this.datePipe.transform(this.date, 'yyyy-MM-dd');
  
    this.isArab = this.utilService.isArabic();
    this.loadFilterOptions();
    this.loadCitiesList();
    this.loadInitialData();
  }

  loadInitialData() {
    this.totalItemsCount = 0;
    this.loadList(0);

  }

  resetFilter() {
    this.totalItemsCount = 0;
    this.oprDashboard = null;
    this.input = {
     // date: this.date,
      pageNumber: 0,
      pageSize: 10,
    };
    this.loadList(0);

  }

  private loadList(page: number) {
    this.isLoading = true;
    this.pageService.change({ pageIndex: page, pageSize: this.pageSize, previousPageIndex: page - 1, length: this.totalItemsCount });
    this.input.pageNumber = page;
    this.input.filterOptions = this.filterOptions;
    this.apiService.post('OperationsDashboard/getOpeartionsDashboardByFilter', this.input).subscribe((db: any) => {
      if (db) {
        this.oprDashboard = db as any;
        this.oprDashboard.todayAttData = [] as Array<number>;
        this.oprDashboard.todayAttData.push(db.totalEmpCount);
        this.oprDashboard.todayAttData.push(db.reportedEmpCount);
        this.oprDashboard.todayAttData.push(db.lateArrivalsCount);
        this.oprDashboard.todayAttData.push(db.notReportedEmpCount);
        this.oprDashboard.todayAttData.push(db.shiftsNotAssignedCount);
        this.oprDashboard.todayAttData.push(db.leavesCount);
        this.attendanceData = new MatTableDataSource(db.employeeAttendance);

        this.totalItemsCount = db.totalItemsCount;
        this.projectsSelectionList = db.projectsSelectionList;
        this.sitesSelectionList = db.sitesSelectionList;
        this.employeesSelectionList = db.employeesSelectionList;
        setTimeout(() => {
          this.paginator.pageIndex = page as number;
          this.paginator.length = this.totalItemsCount;
        });




        this.chartOptions1 = {
          series: this.oprDashboard.todayAttData,
          chart: {
            type: "donut",
            height: 150,
            width: 280
          },
          labels: ["Total Employees", "Reported Employees", "Late Arrivals Count", "Not Reported Employees", "Shifts Not Assigned","Employees On Leave"],
          responsive: [
            {
              breakpoint: 480,
              options: {
                chart: {
                  width: 500,
                },
                legend: {
                  position: "bottom"
                }
              }
            }
          ]
        };
        this.isShowDonut = true;
      }

    });

  }


  onSortOrder(sort: any) {
    this.totalItemsCount = 0;
    //this.attendanceData.data = [];
    this.pageService.change({ pageSize: this.pageSize, pageIndex: 0, previousPageIndex: -1, length: 0 });
    this.sortingOrder = sort.active + ' ' + sort.direction;
    this.input.sortingOrder = this.sortingOrder;
    this.loadList(0);

  }
  onPageSwitch(event: PageEvent) {
    this.pageService.change(event);
    this.pageSize = event.pageSize;
    this.input.pageSize = event.pageSize;
    this.loadList(event.pageIndex);
  }
  applyFilter() {
    this.input.filterOptions = this.filterOptions;
    this.loadList(0);
  }
  translateToolTip(msg: string) {
    return `${this.translate.instant(msg)}`;

  }

  loadCitiesList() {
    //  this.apiService.getall('City/getCitiesSelectList').subscribe((res: any) => {
    this.apiService.getall('Branch/getBranchSelectListForUser').subscribe((res: any) => {
      this.citySelectionList = res as Array<any>;

      this.citySelectionList.forEach(e => {
        e.lable = e.value + "-" + e.text;
      });
    });

  }
  updateFilterOptions(index: number) {
    this.filterOptions[index].isSelected = !this.filterOptions[index].isSelected;
    this.pageService.change({ pageSize: this.pageSize, pageIndex: 0, previousPageIndex: -1, length: 0 });
    this.loadList(0);
  }
  loadFilterOptions() {
    this.apiService.getall('OperationsDashboard/getFilterOptions').subscribe((res: any) => {
      this.filterOptions = res as Array<any>;
    });

  }
  enterAutoAttendance() {
    this.isLoading = true;
    let notReportedEmployees = this.oprDashboard.employeeAttendance as Array<any>;
    notReportedEmployees.filter((e: any) => e.isReported == false);
    if (notReportedEmployees.length > 0) {
      this.apiService.post('EmployeesAttendance/EnterAutoAttendanceForAllProjectSites', notReportedEmployees)
        .subscribe(res => {
          if (res) {

            this.utilService.OkMessage();
            this.loadInitialData();
          }
        },
          error => {
            console.error(error);
            this.utilService.ShowApiErrorMessage(error);
          });

    }
    else {
      this.notifyService.showWarning("No_Updates_Found");
    }
  }


}
