import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { TranslateService } from '@ngx-translate/core';
import moment from 'moment';
import { Chart } from 'chart.js/auto';
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
  selector: 'app-getprojectwisesummaryreport',
  templateUrl: './getprojectwisesummaryreport.component.html',
  styleUrls: ['./getprojectwisesummaryreport.component.scss']
})
export class GetprojectwisesummaryreportComponent extends ParentFomMgtComponent implements OnInit {

  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort!: MatSort;

  searchValue: string = '';
  sortingOrder: string = 'id desc';
  isLoading: boolean = false;
  totalItemsCount: number = 0;
  data!: MatTableDataSource<any>;
  displayedColumns: string[] = ['projectName', 'opening', 'received', 'totReceive', 'foreClosed', 'closed', 'completed', 'totalClosed', 'totJobs', 'closing', 'percentage'];
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
  performanceStatistics: Array<any> = [];
  balancePercentage: any=0;

  completedPercentage: any=0;
  staticsCompleted: any;
  staticsTotalReceived: any;
  staticsBalance: any;
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
  
  constructor(private apiService: ApiService, private authService: AuthorizeService, private translate: TranslateService,
    private utilService: UtilityService, private notifyService: NotificationService, public dialog: MatDialog,
    public pageService: PaginationService) {
    super(authService);
  }

  ngOnInit(): void {
    this.isArab = this.utilService.isArabic();
   // this.updateJobData();
  //  this.loadJobTickets();
      this.balancePercentage =0;
        this.completedPercentage = 0;
    this.loadDisciplineData();

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

    this.apiService.getPagination('FomCustomerContract/getProjectWiseSummaryJobTicketsReport', this.utilService.getQueryString(page, pageCount, query, orderBy)).subscribe(result => {
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

  chartInstance: any;
  renderPerformanceChart(): void {
   /* alert(`Balance Percentage: ${this.balancePercentage}, Completed Percentage: ${this.completedPercentage}`);*/
    const ctx = document.getElementById('performanceChart') as HTMLCanvasElement;
    if (this.chartInstance) {
      this.chartInstance.destroy();
    }
    //const balancePercentage = this.performanceStatistics.map((balance) => balance.balancePercentage);
    //const completedPercentage = this.performanceStatistics.map((completed) => completed.completedPercentage);
    //const data = this.performanceStatistics;

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
    this.apiService.postFomCpUrl('FomCustomerContract/getProjectWiseSummaryJobTicketsReport', this.filter)
      .subscribe(result => {
        this.balancePercentage =0;
        this.completedPercentage = 0;
        this.isLoading = false;
        this.totalItems = result?.totalCount ?? 0;
        this.data = result;
        //// Assign result items to MatTableDataSource
        this.data = new MatTableDataSource(result.items.slice());

        this.performanceStatistics = result.performanceStatistics
        this.staticsCompleted = result.performanceStatistics.completed;
        this.staticsTotalReceived = result.performanceStatistics.totalReceived;
        this.staticsBalance = result.performanceStatistics.balance;
        this.balancePercentage = result.performanceStatistics.balancePercentage;
        this.completedPercentage = result.performanceStatistics.completedPercentage;


        var res = this.data;

        //  this.createChart();
        this.renderPerformanceChart();

        this.data = new MatTableDataSource(result.items.slice());

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
