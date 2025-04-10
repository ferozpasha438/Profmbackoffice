import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { TranslateService } from '@ngx-translate/core';
import moment from 'moment';
import { Chart, ChartOptions, ChartType, registerables } from 'chart.js';
import { AuthorizeService } from 'src/app/api-authorization/AuthorizeService';
import { ApiService } from 'src/app/services/api.service';
import { NotificationService } from 'src/app/services/notification.service';
import { DBOperation } from 'src/app/services/utility.constants';
import { UtilityService } from 'src/app/services/utility.service';
import { DeleteConfirmDialogComponent } from 'src/app/sharedcomponent/delete-confirm-dialog';
import { PaginationService } from 'src/app/sharedcomponent/pagination.service';
import { ParentFomMgtComponent } from 'src/app/sharedcomponent/parentfommgt.component';
import { CustomSelectListItem, LanCustomSelectListItem } from '../../models/MenuItemListDto';

@Component({
  selector: 'app-getticketsummaryreport',
  templateUrl: './getticketsummaryreport.component.html',
  styleUrls: ['./getticketsummaryreport.component.scss']
})
export class GetticketsummaryreportComponent extends ParentFomMgtComponent implements OnInit {

  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort!: MatSort;

  searchValue: string = '';
  sortingOrder: string = 'id desc';
  isLoading: boolean = false;
  totalItemsCount: number = 0;
  data!: MatTableDataSource<any>;
  displayedColumns: string[] = ['date', 'opening', 'received', 'totReceive', 'foreClosed', 'closed', 'completed', 'totalClosed', 'totJobs', 'closing','percentage'];
  isArab: boolean = false;

  filter: any = {
    page: 0,
    pageCount: 10,
    query: "",
    orderBy: "id desc",
    ticketNumber: "",
    customerCode: "",
    siteCode: "",
    deptCode:"",
    supervisor: "",
    statusStr: ''
  };

  totalItems = 0;
  search: string = '';
  CustomerCodeList: Array<CustomSelectListItem> = [];
  ContractCodeList: Array<CustomSelectListItem> = [];
  SiteCodeList: Array<CustomSelectListItem> = [];
  //statusOptions: Array<CustomSelectListItem> = [];
  DisciplineCodeList: Array<LanCustomSelectListItem> = [];
  //isLoading = false;
  //totalItemsCount: number = 0;
  //data: Array<any> = [];
  //customerCode: string = '';
  fromDateIp: any;
  toDateIp: any;
  statusStr: string = '';
  statusOptions = [
    { value: 'Corrective', text: 'Corrective' },
    { value: 'Preventive', text: 'Preventive' },
    { value: 'Noral', text: 'Normal' } // Fixed typo
  ];

  fromDateError: string | null = null;
  toDateError: string | null = null;
  hasErrors: boolean = false;
  jobStatusData: Array<any> = [];
  /*performanceStatistics: Array<any> = [];*/
  performanceStatistics: Array<any> = [];
  //performanceStatistics: any = {
  //  balance: 89,
  //  completed: 11
    
  //};
  balancePercentage: any = 0;

  completedPercentage: any = 0;
  staticsCompleted: any;
  staticsTotalReceived: any;
  staticsBalance: any;
  public pieChartOptions: ChartOptions = {
    responsive: true
  };

  public pieChartLabels: string[] = ['Balance', 'Completed'];
  public pieChartData: number[] = [];
  public pieChartType: ChartType = 'pie';
  public pieChartLegend = true;

  constructor(private apiService: ApiService, private authService: AuthorizeService, private translate: TranslateService,
    private utilService: UtilityService, private notifyService: NotificationService, public dialog: MatDialog,
    public pageService: PaginationService) {
    super(authService);
    Chart.register(...registerables);
  }

  ngOnInit(): void {
    this.isArab = this.utilService.isArabic();
    this.balancePercentage = 0;
    this.completedPercentage = 0;
   // this.updateJobData();
  //  this.loadJobTickets();
    this.loadDisciplineData();
   // this.renderChart();
   // this.loadPerformanceStatistics();
    this.statusOptions;
  
  }



  refresh() {
    this.searchValue = '';
 
  //  this.initialLoading();
  }

  initialLoading() {
    this.loadList(0, this.pageService.pageCount, "", this.sortingOrder);
  }

  onPageSwitch(event: PageEvent) {
    this.pageService.change(event);
    this.loadList(event.pageIndex, event.pageSize, "", this.sortingOrder);
  }

  private loadList(page: number | undefined, pageCount: number | undefined, query: string | null | undefined, orderBy: string | null | undefined) {
    this.isLoading = true;

    this.apiService.getPagination('FomCustomerContract/getSummaryJobTicketsReport', this.utilService.getQueryString(page, pageCount, query, orderBy)).subscribe(result => {
      this.totalItemsCount = 0;
      this.data = new MatTableDataSource(result.items);

      this.totalItemsCount = result.totalCount;
      setTimeout(() => {
        this.paginator.pageIndex = page as number;
        this.paginator.length = this.totalItemsCount;
      });
      this.data.sort = this.sort;

      console.log(this.data.sort)
      console.log(this.data.paginator)

      this.isLoading = false;
    }, error => this.utilService.ShowApiErrorMessage(error));
  }



  validateDates(): void {
    this.fromDateError = null;
    this.toDateError = null;
    this.hasErrors = false;

    if (this.fromDateIp && !this.toDateIp) {
      this.toDateError = 'To Date is required when From Date is selected.';
      this.hasErrors = true;
    }

    if (this.fromDateIp && this.toDateIp) {
      if (new Date(this.fromDateIp) > new Date(this.toDateIp)) {
        this.fromDateError = 'From Date cannot be greater than To Date.';
        this.hasErrors = true;
      }
    }
  }

  onCustSiteCode(selectedValue: string) {
    console.log('Selected Customer Code:', selectedValue);

    // Perform additional logic here, such as fetching dependent site codes
    if (selectedValue) {
      this.fetchSiteCodesForCustomer(selectedValue);
    }
  }


  fetchSiteCodesForCustomer(custCode: string) {
    this.apiService.getall(`fomCustomerContract/GetSelectCustomerSiteByCustCode?custCode=${custCode}`).subscribe(res => {
      if (res) {
        this.SiteCodeList = res;
      }
    })
  }

  
  loadData() {
    if (this.fromDateIp != null) {
      let picker = moment as any;
      this.filter.fromDate = picker(this.fromDateIp).format('YYYY-MM-DD') as Date;
    }
    if (this.toDateIp != null) {
      let picker = moment as any;
      this.filter.toDate = picker(this.toDateIp).format('YYYY-MM-DD') as Date;
    }

    this.isLoading = true;
   

    if (this.hasErrors) {
      alert('Please fix the validation errors before generating the report.');
      return;
    }

    this.apiService.postFomCpUrl('FomCustomerContract/getSummaryJobTicketsReport', this.filter)
      .subscribe(result => {
        this.isLoading = false;
        this.balancePercentage = 0;
        this.completedPercentage = 0;
        this.totalItems = result?.totalCount ?? 0;
        this.data = result;
        //// Assign result items to MatTableDataSource

        this.data = new MatTableDataSource(result.items.slice());

        this.jobStatusData = result.chartData;

        this.performanceStatistics = result.performanceStatistics
        this.staticsCompleted = result.performanceStatistics.completed;
        this.staticsTotalReceived = result.performanceStatistics.totalReceived;
        this.staticsBalance = result.performanceStatistics.balance;
        this.balancePercentage = result.performanceStatistics.balancePercentage;
        this.completedPercentage = result.performanceStatistics.completedPercentage;
      
        var res = this.data;
     
        

        this.data = new MatTableDataSource(result.items.slice());
        this.renderChart();
        this.renderPerformanceChart();
       // this.performanceStatistics = result.performanceStatistics;
      });

  }


  

  getTotalCount(): number {
    return this.jobStatusData.reduce((total, status) => total + status.count, 0);
  }
 
  //renderChart(): void {
  //  const labels = this.jobStatusData.map((status) => status.name);
  //  const data = this.jobStatusData.map((status) => status.count);
 

  //  new Chart('jobStatusChart', {
  //    type: 'pie',
  //    data: {
  //      labels: labels,
  //      datasets: [
  //        {
  //          data: data,
  //          backgroundColor: ['#4285F4', '#EA4335', '#FBBC05', '#34A853', '#A142F4'],
  //          borderWidth: 1
  //        }
  //      ]
  //    },
  //    options: {
  //      responsive: true,
  //      plugins: {
  //        legend: {
  //          position: 'bottom'
  //        }
  //      }
  //    }
  //  });
  //}


  chartInstance2: any; // Store chart instance globally

  renderChart(): void {
    const canvas = document.getElementById('jobStatusChart') as HTMLCanvasElement;

    if (!canvas) {
      console.error("Canvas element not found!");
      return;
    }

    const chartContext = canvas.getContext('2d');

    if (!chartContext) {
      console.error("Failed to get canvas context!");
      return;
    }

    // Destroy the existing chart instance before creating a new one
    if (this.chartInstance2) {
      this.chartInstance2.destroy();
    }

    // Check if data is available before rendering the chart
    if (!this.jobStatusData || this.jobStatusData.length === 0) {
      console.warn("No data available for rendering chart!");
      return; // Prevent rendering an empty chart
    }

    // Prepare new data for chart
    const labels = this.jobStatusData.map((status) => status.name);
    const data = this.jobStatusData.map((status) => status.count);

    this.chartInstance2 = new Chart(canvas, {
      type: 'pie',
      data: {
        labels: labels,
        datasets: [
          {
            data: data,
            backgroundColor: ['#4285F4', '#EA4335', '#FBBC05', '#34A853', '#A142F4'],
            borderWidth: 1
          }
        ]
      },
      options: {
        responsive: true,
        plugins: {
          legend: {
            position: 'bottom'
          }
        }
      }
    });

    console.log("Chart updated successfully with data:", this.jobStatusData);
  }


  //renderChart(): void {
  //  console.log("Updated jobStatusData:", this.jobStatusData); // Debugging log

  //  const canvas = document.getElementById('jobStatusChart') as HTMLCanvasElement;

  //  if (!canvas) {
  //    console.error("Canvas element not found!");
  //    return;
  //  }

  //  const chartContext = canvas.getContext('2d');

  //  if (!chartContext) {
  //    console.error("Failed to get canvas context!");
  //    return;
  //  }

  //  if (this.chartInstance2) {
  //    this.chartInstance2.destroy();
  //  }

  //  this.chartInstance2 = new Chart(canvas, { // Use canvas instead of chartContext
  //    type: 'pie',
  //    data: {
  //      labels: this.jobStatusData.map((status) => status.name),
  //      datasets: [
  //        {
  //          data: this.jobStatusData.map((status) => status.count),
  //          backgroundColor: ['#4285F4', '#EA4335', '#FBBC05', '#34A853', '#A142F4'],
  //          borderWidth: 1
  //        }
  //      ]
  //    },
  //    options: {
  //      responsive: true,
  //      plugins: {
  //        legend: {
  //          position: 'bottom'
  //        }
  //      }
  //    }
  //  });
  //}



  chartInstance: any;
 // chartInstance: any;
  renderPerformanceChart(): void {
     
    const ctx = document.getElementById('performanceChart') as HTMLCanvasElement;
    if (this.chartInstance) {
      this.chartInstance.destroy();
    }

   
    this.chartInstance = new Chart(ctx, {
      type: 'pie',
      data: {
        labels: ['Balance', 'Completed'],
        datasets: [{
          data: [this.balancePercentage, this.completedPercentage],
          backgroundColor: ['#FF6384', '#36A2EB']
        }]
      },
      options: {
        responsive: true,
        plugins: {
          legend: { position: 'top' }
        }
      }
    });

  }




  loadReport() {
  //  this.data = [];
    this.filter.page = 0;

    this.loadData();
    setTimeout(() => {

      for (this.filter.page = 1; this.filter.page < (Math.ceil(this.totalItems / this.filter.pageCount)); this.filter.page += 1) {
        this.loadData();

      }
    }, 1000);


  }

  loadDisciplineData() {
    this.apiService.getall('FomDiscipline/getDepartmentSelectList').subscribe(res => {
      if (res)
        this.DisciplineCodeList = res.slice();
    });


    this.apiService.getall('FomCustomer/getSelectCustomerList').subscribe(res => {
      console.log(res);
      this.CustomerCodeList = res.slice();
    });

  }

  onStatusChange(selectedValue: string) {
    if (selectedValue =='') {
     alert('No valid type selected');
    } else {
      console.log('Selected status:', this.filter.statusStr);
      this.statusStr = selectedValue;
    }
  }
  public delete(id: number) {
    const dialogRef = this.utilService.openDeleteConfirmDialog(this.dialog, DeleteConfirmDialogComponent);
    dialogRef.afterClosed().subscribe(canDelete => {
      if (canDelete && id > 0) {
        this.apiService.delete('FomDiscipline', id).subscribe(res => {
          this.refresh();
          this.utilService.OkMessage();
        },
        );
      }
    },
      error => this.utilService.ShowApiErrorMessage(error));
  }


  public printReport() {
    const tableContent = document.querySelector('.table-responsive')?.innerHTML;

    if (!tableContent) {
      alert('No data to print.');
      return;
    }

    const printWindow = window.open('', '', 'width=900,height=650');
    printWindow?.document.write(`
      <html>
        <head>
          <title>Resources Report</title>
          <style>
            body {
              font-family: Arial, sans-serif;
              margin: 20px;
            }
            h4 {
              text-align: center;
              margin-bottom: 20px;
            }
            table {
              width: 100%;
              border-collapse: collapse;
              margin-bottom: 20px;
            }
            th, td {
              border: 1px solid #000;
              padding: 8px;
              text-align: left;
            }
            th {
              background-color: #f2f2f2;
            }
            .table {
              margin: auto;
            }
          </style>
        </head>
        <body>
          <h4>Resources Report</h4>
          ${tableContent}
        </body>
      </html>
    `);

    printWindow?.document.close();
    printWindow?.print();
    //printWindow?.onafterprint = () => {
    //  printWindow?.close();
    //};
  }

}
