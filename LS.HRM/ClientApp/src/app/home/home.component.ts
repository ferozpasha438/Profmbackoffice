import { HttpClient } from '@angular/common/http';
import { Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatCalendar, MatCalendarCellCssClasses } from '@angular/material/datepicker';
import { MatTableDataSource } from '@angular/material/table';
import { Moment } from 'moment';
import { default as data } from "../../assets/i18n/siteConfig.json";
import { AuthorizeService } from '../api-authorization/AuthorizeService';
import { ApiService } from '../services/api.service';
import { UtilityService } from '../services/utility.service';
import { ApexAxisChartSeries, ApexDataLabels, ApexFill, ApexLegend, ApexPlotOptions, ApexStroke, ApexTooltip, ApexXAxis, ApexYAxis, ChartComponent } from "ng-apexcharts";
import { NotificationService } from '../services/notification.service';
import { PaginationService } from '../sharedcomponent/pagination.service';
import { TranslateService } from '@ngx-translate/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';


import {
  ApexNonAxisChartSeries,
  ApexResponsive,
  ApexChart
} from "ng-apexcharts";
import { CustomSelectListItem } from '../models/MenuItemListDto';
export type ChartOptions = {
  series: ApexNonAxisChartSeries;
  chart: ApexChart;
  responsive: ApexResponsive[];
  labels: any;
  plotOptions: ApexPlotOptions;
};
export type RadialChartOptions = {
  series: ApexNonAxisChartSeries;
  chart: ApexChart;
  labels: string[];
  plotOptions: ApexPlotOptions;
  colors: string[];
  fill: ApexFill;
  stroke: ApexStroke;
};

export type SemiRadialChartOptions = {
  series: ApexNonAxisChartSeries;
  chart: ApexChart;
  labels: string[];
  plotOptions: ApexPlotOptions;
  fill: ApexFill;
};

export type MonthlyChartOptions = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  dataLabels: ApexDataLabels;
  plotOptions: ApexPlotOptions;
  yaxis: ApexYAxis;
  xaxis: ApexXAxis;
  fill: ApexFill;
  tooltip: ApexTooltip;
  stroke: ApexStroke;
  legend: ApexLegend;
};


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  @ViewChild("chart") chart!: ChartComponent;
  //public chartOptions1!: Partial<ChartOptions>;
  //public chartOptions2!: Partial<ChartOptions>;
  //public chartOptions3!: Partial<ChartOptions>;
  //public chartOptions4!: Partial<ChartOptions>;
  //public chartOptions5!: Partial<ChartOptions>;
  //public chartOptions6!: Partial<ChartOptions>;
  public radialChartOptions1!: Partial<RadialChartOptions>;
  public radialChartOptions2!: Partial<RadialChartOptions>;
  public radialChartOptions3!: Partial<RadialChartOptions>;
  public radialChartOptions4!: Partial<RadialChartOptions>;
  public radialChartOptions5!: Partial<RadialChartOptions>;
  public radialChartOptions6!: Partial<RadialChartOptions>;
  profmDashboard: any;
  public semiRadialChartOptions1!: Partial<SemiRadialChartOptions>;
  public semiRadialChartOptions2!: Partial<SemiRadialChartOptions>;
  public monthlyChartOptions!: Partial<MonthlyChartOptions>;
  isArab: boolean = false;

  @ViewChild('calendar') calendar!: MatCalendar<Moment>;

  selectedDate!: Moment;
  displayedColumns: string[] = [];
  //data!: MatTableDataSource<any>;
  totalItemsCount!: number;
  searchValue: string = '';
  sortingOrder: string = 'id desc';
  isLoading: boolean = false;
  id: number = 0;
  form!: FormGroup;
  branchCodeList: Array<any> = [];
  isShowDiv: boolean = false;
  examHeaderID: number = 0;
  branchCode: string = '';
  minDate!: any;
  maxDate!: any;
  datesWeekends: Array<any> = [];
  datesHolidays: Array<any> = [];
  datesEvents: Array<any> = [];
  allDatesData: Array<any> = [];
  dashboardEvents: Array<any> = [];
  totalStudents: number = 0;
  studentsOnLeave: number = 0;
  feeDueStudents: number = 0;
  totalTeachers: number = 0;
  newRegistrations: number = 0;
  feeDueTotal: number = 0;
  todayAttData: number[] = [];
  monthAttData: number[] = [];
  yearAttData: number[] = [];
  dashboardStudents: Array<any> = [];
  isShowDonut: boolean = false;
  isShowCalandar: boolean = false;
  //#startregion Opr
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  isLoadSchoolDashBoard: boolean = false;
  attendanceData!: MatTableDataSource<any>;
  interval: any;
  oprDashboard: any = { notReportedEmpCount: 1, totalEmpCount: 1, reportedEmpCount: 1, lateArrivalsCount: 0 };
  pageNumber = 0;
  pageSize = 10;
  filterOptions: Array<any> = [];// [{ key: "late", isSelected: false }];
  input: any = {
    date: new Date('2022-11-01'),
    pageNumber: 0,
    pageSize: 10,
    branchCode: null,
    siteCode: null,
    projectCode: null,
    employeeNumber: null,
    filterOptions: this.filterOptions.slice(),
    dashBoardSubType: "management",//operations,management
  };
  citySelectionList: Array<any> = [];
  projectsSelectionList: Array<any> = [];
  sitesSelectionList: Array<any> = [];
  employeesSelectionList: Array<any> = [];
  //#endregion Opr

  CustomerContractList: Array<CustomSelectListItem> = [];
  CustomerCodeList: Array<CustomSelectListItem> = [];

  @Output() monthYearSelected = new EventEmitter<Date>();
  userSelectedDate: Date = new Date();

  chosenYearHandler(normalizedYear: Date) {
    //const ctrlValue = new Date();
    //ctrlValue.setFullYear(normalizedYear.getFullYear());
    //this.selectedDate = ctrlValue;
    //this.monthYearSelected.emit(this.selectedDate);
  }

  
  constructor(private authService: AuthorizeService, private http: HttpClient,
    private apiService: ApiService, private utilService: UtilityService
    //#start region Opr
    , private notifyService: NotificationService, public pageService: PaginationService, private translate: TranslateService,
    private fb: FormBuilder

    //#end region Opr

    //#startregion profm

    //end region profm

  ) {
    this.form = this.fb.group({
      contractId: [''],
      customerCode: [''],
      userDate: [''],
    });
    this.radialChartOptions1 = {
      series: [100],
      chart: {
        height: 150,
        width: 100,
        type: "radialBar"
      },
      plotOptions: {
        radialBar: {
          track: {
            background: '#356a7e'
          },
          hollow: {
            size: "50%",
          },
          dataLabels: {

            name: {
              fontSize: '0px',
            },
            value: {
              //  fontSize: '10px',
              fontWeight: 'bold',
              offsetY: -10,
            },
          }
        }
      },
      fill: {
        type: "gradient",
        gradient: {
          shade: "dark",
          type: "horizontal",
          shadeIntensity: 0.5,
          gradientToColors: ["#ABE5A1"],
          inverseColors: true,
          opacityFrom: 1,
          opacityTo: 1,
          stops: [0, 100]
        }
      },
      stroke: {
        lineCap: "round"
      },
      labels: ['']
    };
    this.radialChartOptions2 = {
      series: [90],
      chart: {
        height: 150,
        width: 100,
        type: "radialBar"
      },
      plotOptions: {
        radialBar: {
          track: {
            background: '#57AAA9'
          },
          hollow: {
            size: "50%",
          },
          dataLabels: {

            name: {
              fontSize: '0px',
            },
            value: {
              //  fontSize: '10px',
              fontWeight: 'bold',
              offsetY: -10,
            },
          }
        }
      },
      labels: ['']
    };
    this.radialChartOptions3 = {
      series: [100],
      chart: {
        height: 150,
        width: 90,
        type: "radialBar"
      },
      plotOptions: {
        radialBar: {
          track: {
            background: '#95C099'
          },
          hollow: {
            size: "50%",
          },
          dataLabels: {

            name: {
              fontSize: '0px',
            },
            value: {
              //  fontSize: '10px',
              fontWeight: 'bold',
              offsetY: -10,
            },
          }
        }
      },
      labels: ['']
    };
    this.radialChartOptions4 = {
      series: [100],
      chart: {
        height: 120,
        width: 100,
        type: "radialBar"
      },
      plotOptions: {
        radialBar: {
          track: {
            background: '#356a7e'
          },
          hollow: {
            size: "50%",
          },
          dataLabels: {

            name: {
              fontSize: '0px',
            },
            value: {
              //  fontSize: '10px',
              fontWeight: 'bold',
              offsetY: -10,
            },
          }
        }
      },
      labels: ['']
    };
    this.radialChartOptions5 = {
      series: [100],
      chart: {
        height: 120,
        width: 100,
        type: "radialBar"
      },
      plotOptions: {
        radialBar: {
          track: {
            background: '#57AAA9'
          },
          hollow: {
            size: "50%",
          },
          dataLabels: {

            name: {
              fontSize: '0px',
            },
            value: {
              //  fontSize: '10px',
              fontWeight: 'bold',
              offsetY: -10,
            },
          }
        }
      },
      colors: ["#e60023"],
    };
    this.radialChartOptions6 = {
      series: [100],
      chart: {
        height: 120,
        width: 100,
        type: "radialBar"
      },
      plotOptions: {
        radialBar: {
          track: {
            background: '#95C099'
          },
          hollow: {
            size: "50%",
          },
          dataLabels: {

            name: {
              fontSize: '0px',
            },
            value: {
              //  fontSize: '10px',
              fontWeight: 'bold',
              offsetY: -10,
            },
          }
        }
      },
      labels: ['']
    };


    this.semiRadialChartOptions1 = {
      series: [76],
      chart: {
        type: "radialBar",
        offsetY: -20,
        height: 255
      },
      plotOptions: {
        radialBar: {
          startAngle: -90,
          endAngle: 90,
          track: {
            background: "#e7e7e7",
            strokeWidth: "97%",
            margin: 5, // margin is in pixels
            dropShadow: {
              enabled: true,
              top: 2,
              left: 0,
              opacity: 0.31,
              blur: 2
            }
          },
          dataLabels: {
            name: {
              show: false
            },
            value: {
              offsetY: -2,
              fontSize: "22px"
            }
          }
        }
      },
      fill: {
        type: "gradient",
        gradient: {
          shade: "light",
          shadeIntensity: 0.4,
          inverseColors: false,
          opacityFrom: 1,
          opacityTo: 1,
          stops: [0, 50, 53, 91]
        }
      },
      labels: ["Average Results"]
    };
    this.semiRadialChartOptions2 = {
      series: [76],
      chart: {
        type: "radialBar",
        offsetY: -20,
        height: 255
      },
      plotOptions: {
        radialBar: {
          startAngle: -90,
          endAngle: 90,
          track: {
            background: "#e7e7e7",
            strokeWidth: "97%",
            margin: 5, // margin is in pixels
            dropShadow: {
              enabled: true,
              top: 2,
              left: 0,
              opacity: 0.31,
              blur: 2
            }
          },
          dataLabels: {
            name: {
              show: false
            },
            value: {
              offsetY: -2,
              fontSize: "22px"
            }
          }
        }
      },
      fill: {
        type: "gradient",
        gradient: {
          shade: "light",
          shadeIntensity: 0.4,
          inverseColors: false,
          opacityFrom: 1,
          opacityTo: 1,
          stops: [0, 50, 53, 91]
        }
      },
      labels: ["Average Results"]
    };


    this.monthlyChartOptions = {
      series: [
        {
          name: "Net Profit",
          data: [44, 55, 57, 56, 61, 58]
        }
      ],
      chart: {
        type: "bar",
        height: 350
      },
      plotOptions: {
        bar: {
          horizontal: false,
          columnWidth: "55%",
          // endingShape: "rounded"
        }

      },
      dataLabels: {
        enabled: false
      },
      stroke: {
        show: true,
        width: 2,
        colors: ["transparent"]
      },
      xaxis: {
        categories: [
          "Feb",
          "Mar",
          "Apr",
          "May",
          "Jun",
          "Jul"

        ]
      },
      yaxis: {
        title: {
          text: "tickets"
        }
      },
      fill: {
        opacity: 1,

      },

      tooltip: {
        y: {
          formatter: function (val) {
            return val + " tickets";
          }
        }
      }
    };
  }
  ngOnInit(): void {
    this.loadProfmDashBoardData();
    this.loadCustomerContract();
  }
  formatDate(date: Date) {
    const monthNames = ["January", "February", "March", "April", "May", "June",
      "July", "August", "September", "October", "November", "December"];
    const monthIndex = date.getMonth();
    const year = date.getFullYear();
    return monthNames[monthIndex] + ' ' + year;
  }
  chosenMonthHandler(monthDate: Date, picker: any) {
    this.userSelectedDate = monthDate;
    picker.close();
    this.form.patchValue({
      'userDate': monthDate
    });
    this.changeSelection();
  }
  changeSelection() {
    if (this.form.controls['contractId'].value === '') {
      this.form.value['contractId'] = 0;
    }
    if (this.form.controls['userDate'].value != '') {
      let sd = new Date(this.form.controls['userDate'].value);
      this.form.value['userDate']=new Date(sd.getFullYear(), sd.getMonth(), sd.getDate() + 1);
    }
    this.apiService.postFomUrl('FomWebDashboard/GetWebDashboardDataWithFilters', this.form.value).subscribe((res: any) => {
      this.profmDashboard = res;
      this.profmDashboard.totalData = [] as Array<number>;
      this.profmDashboard.totalData.push(res.totalTickets);
      this.profmDashboard.totalData.push(res.closedTickets);
      this.profmDashboard.totalData.push(res.pendingTickets);
      this.monthlyChartOptions = {
        series: [
          {
            name: "Tickets",
            data: res.monthlyTotalTickets
            //[44, 55, 57, 56, 61, 58]
          }
        ],
        chart: {
          type: "bar",
          height: 135
        },
        plotOptions: {
          bar: {
            horizontal: false,
            columnWidth: "55%",
            // endingShape: "rounded"
          }
        },
        dataLabels: {
          enabled: false
        },
        stroke: {
          show: true,
          width: 2,
          colors: ["transparent"]
        },
        xaxis: {
          categories: res.monthsNames
        },
        yaxis: {
          title: {
            text: "tickets"
          }
        },
        fill: {
          opacity: 1
        },
        tooltip: {
          y: {
            formatter: function (val) {
              return val + " tickets";
            }
          }
        }
      };



      this.semiRadialChartOptions1.series = [Math.round((res.closedTickets / res.totalTickets) * 100)];
      this.semiRadialChartOptions2.series = [Math.round((res.last30DaysData?.closedTickets / res.last30DaysData?.totalTickets) * 100)];

      this.radialChartOptions2.series = [Math.round((res.closedTickets / res?.totalTickets) * 100)];
      this.radialChartOptions3.series = [Math.round((res.pendingTickets / res?.totalTickets) * 100)];
      this.radialChartOptions5.series = [Math.round((res?.last30DaysData?.closedTickets / res?.last30DaysData?.totalTickets) * 100)];
      this.radialChartOptions6.series = [Math.round((res?.last30DaysData?.pendingTickets / res?.last30DaysData?.totalTickets) * 100)];
    });
  }
  loadCustomerContract() {
    this.apiService.getFomUrl('FomCustomerContract/GetCustomerContractSelectList').subscribe(res => {
      if (res) {
        this.CustomerContractList = res;
      }
    });
    this.apiService.getFomUrlPagination('FomCustomer', this.utilService.getQueryString(0, 1000, '', '')).subscribe(res => {
      if (res)
        this.CustomerCodeList = res['items'];
    });
  }
  loadProfmDashBoardData() {
    /*this.http.get('http://localhost:60186/api/FomWebDashboard/getWebDashboardData').subscribe((res: any) => {*/
    this.apiService.getFomUrl('FomWebDashboard/getWebDashboardData').subscribe((res: any) => {
   // this.http.get('https://hvserp.com/FomMob/api/FomWebDashboard/getWebDashboardData').subscribe((res: any) => {
      this.profmDashboard = res;
      this.profmDashboard.totalData = [] as Array<number>;
      this.profmDashboard.totalData.push(res.totalTickets);
      this.profmDashboard.totalData.push(res.closedTickets);
      this.profmDashboard.totalData.push(res.pendingTickets);
      this.monthlyChartOptions = {
        series: [
          {
            name: "Tickets",
            data: res.monthlyTotalTickets
            //[44, 55, 57, 56, 61, 58]
          }
        ],
        chart: {
          type: "bar",
          height: 135
        },
        plotOptions: {
          bar: {
            horizontal: false,
            columnWidth: "55%",
            // endingShape: "rounded"
          }
        },
        dataLabels: {
          enabled: false
        },
        stroke: {
          show: true,
          width: 2,
          colors: ["transparent"]
        },
        xaxis: {
          categories: res.monthsNames
          //  [
          //  "Feb",
          //  "Mar",
          //  "Apr",
          //  "May",
          //  "Jun",
          //  "xxxx"

          //]
        },
        yaxis: {
          title: {
            text: "tickets"
          }
        },
        fill: {
          opacity: 1
        },
        tooltip: {
          y: {
            formatter: function (val) {
              return val + " tickets";
            }
          }
        }
      };



      this.semiRadialChartOptions1.series = [Math.round((res.closedTickets / res.totalTickets) * 100)];
      this.semiRadialChartOptions2.series = [Math.round((res.last30DaysData?.closedTickets / res.last30DaysData?.totalTickets) * 100)];

      this.radialChartOptions2.series = [Math.round((res.closedTickets / res?.totalTickets) * 100)];
      this.radialChartOptions3.series = [Math.round((res.pendingTickets / res?.totalTickets) * 100)];
      this.radialChartOptions5.series = [Math.round((res?.last30DaysData?.closedTickets / res?.last30DaysData?.totalTickets) * 100)];
      this.radialChartOptions6.series = [Math.round((res?.last30DaysData?.pendingTickets / res?.last30DaysData?.totalTickets) * 100)];
    });

  }
}
