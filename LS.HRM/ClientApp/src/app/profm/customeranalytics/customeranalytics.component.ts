import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AuthorizeService } from 'src/app/api-authorization/AuthorizeService';
import { ApiService } from 'src/app/services/api.service';
import { NotificationService } from 'src/app/services/notification.service';
import { UtilityService } from 'src/app/services/utility.service';
import { PaginationService } from 'src/app/sharedcomponent/pagination.service';
import { ParentFomMgtComponent } from 'src/app/sharedcomponent/parentfommgt.component';
import { ChartComponent } from "ng-apexcharts";
import {
  ApexAxisChartSeries,
  ApexChart,
  ApexDataLabels,
  ApexXAxis,
  ApexPlotOptions,
  ApexStroke
} from "ng-apexcharts";

export type ChartOptions = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  dataLabels: ApexDataLabels;
  plotOptions: ApexPlotOptions;
  xaxis: ApexXAxis;
  stroke: ApexStroke;
};

@Component({
  selector: 'app-customeranalytics',
  templateUrl: './customeranalytics.component.html',
  styleUrls: [

  ]
})
export class CustomeranalyticsComponent extends ParentFomMgtComponent implements OnInit {
  @ViewChild("chart") chart!: ChartComponent;
  public chartOptions: Partial<ChartOptions>;
  
  form!: FormGroup;
  searchValue: string = '';
  displayedColumns: string[] = ['opening', 'received', 'total', 'closed', 'pending', 'completion', 'isActive',];
  displayedColumns1: string[] = ['opening', 'received', 'total', 'closed', 'pending', 'completion', 'isActive',];
  data!: MatTableDataSource<any>;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  totalItemsCount!: number;
  isLoading!: boolean;
  sortingOrder: string = '';
  canAddCustCateg: boolean = true;

  constructor(
    private apiService: ApiService, 
    private authService: AuthorizeService,
    private utilService: UtilityService,
    private fb: FormBuilder, 
    private notifyService: NotificationService, 
    public dialog: MatDialog,
    public pageService: PaginationService
  ) { 
    super(authService);
    
    this.chartOptions = {
      series: [
        { name: "Pending", data: [44, 55, 41, 64, 22, 43, 21] },
        { name: "Closed", data: [53, 32, 33, 52, 13, 44, 32] },
        { name: "Total", data: [53, 32, 33, 52, 13, 44, 32] }
      ],
      chart: { type: "bar", height: 300 },
      plotOptions: {
        bar: { horizontal: true, dataLabels: { position: "top" } }
      },
      dataLabels: {
        enabled: true,
        offsetX: -6,
        style: { fontSize: "12px", colors: ["#fff"] }
      },
      stroke: { show: true, width: 1, colors: ["#fff"] },
      xaxis: { categories: [7, 6, 5, 4, 3, 2, 1] }
    };
  }
  

  customerList = [
    { id: 1, name: 'CUST00001' },
    { id: 2, name: 'CUST00002' },
    { id: 3, name: 'CUST00003' },
    { id: 4, name: 'CUST00004' }
  ];

  ngOnInit(): void {
   // this.initialLoading();
    this.form = this.fb.group({ customer: [] });
  }

  initialLoading() {
   // this.loadList(0, this.pageService.pageCount, "", this.sortingOrder);
  }

  private loadList(page: number, pageCount: number, query: string | null, orderBy: string | null) {
    this.isLoading = true;
    this.apiService.getPagination('', this.utilService.getQueryString(page, pageCount, query, orderBy)).subscribe({
      next: (result) => {
        this.totalItemsCount = 0;
        this.data = new MatTableDataSource(result.items);
        this.data.paginator = this.paginator;
        this.canAddCustCateg = result.length <= 0;
        this.isLoading = false;
      },
      error: (error) => {
        this.utilService.ShowApiErrorMessage(error);
        this.isLoading = false;
      }
    });
  }

  searchFilter(value: string) {
    this.data.filter = value.trim().toLowerCase();
  }


  refresh() {
    this.searchValue = '';
    this.initialLoading();
  }

  
  applyFilter(searchValue: any) {
    const search = searchValue;//.target.value as string;
    //if (search && search.length >= 3) {
    if (search) {
      this.searchValue = search;
      this.loadList(0, this.pageService.pageCount, this.searchValue, this.sortingOrder);
    }
  }
}
