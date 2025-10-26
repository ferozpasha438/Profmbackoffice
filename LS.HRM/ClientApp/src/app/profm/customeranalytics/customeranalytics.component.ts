import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
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
import { CustomSelectListItem } from '../../models/MenuItemListDto';

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
  CustomerCodeList: Array<any> = [];

  noOfClicks: number = 0;
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


  }

  ngOnInit(): void {
    // this.initialLoading();
    this.form = this.fb.group({
      customerCode: '',
      statusStr: '',
      fromDate: ['', Validators.required],
    });

    this.apiService.getall('FomCustomer/getSelectCustomerList').subscribe(res => {
      if (res)
        this.CustomerCodeList = res;
    });
  }

  initialLoading() {
    // this.loadList(0, this.pageService.pageCount, "", this.sortingOrder);
  }

  private loadList(page: number, pageCount: number, query: string | null, orderBy: string | null) {
    this.isLoading = true;
    this.apiService.post('fomCustomerContract/getCustomerAnalyticsInScope', this.form.value).subscribe({
      next: (result: any) => {
        this.totalItemsCount = 0;
        this.data = new MatTableDataSource(result.items);

        this.data.paginator = this.paginator;
        this.canAddCustCateg = result.length <= 0;
        this.isLoading = false;

        this.setChatPopData();
      },
      error: (error) => {
        this.utilService.ShowApiErrorMessage(error);
        this.isLoading = false;
      }
    });
  }

  getChatPopData(key: string): any {
    return this.data.data.map((item: any) => item[key])
    //return this.data.data.map((item: any) => item['opening'])
    //let received = result.items.map((item: any) => item['received'])
    //let totJobs = result.items.map((item: any) => item['totJobs'])
  }

  setChatPopData() {
    //this.chartOptions.series?.entries

    this.chartOptions = {
      series: [
        { name: "Opening", data: this.getChatPopData('opening') },
        { name: "Received", data: this.getChatPopData('received') },
        { name: "Total", data: this.getChatPopData('totJobs') },
        { name: "Closed", data: this.getChatPopData('completed'), color: '#000fff' },
        { name: "Pending", data: this.getChatPopData('balance'), color: '#ff000f' },
      ],
      chart: { type: "bar", height: '550' },
      plotOptions: {
        bar: { horizontal: true, dataLabels: { position: "top" } }
      },
      dataLabels: {
        //enabled: true,  
        offsetX: -6,
        style: { fontSize: "11px", colors: ["#fff"] }
      },
      stroke: { show: true, width: 1, colors: ["#fff"] },
      xaxis: { categories: this.getChatPopData('custCode') }
    };

  }

  searchFilter(value: string) {
    this.data.filter = value.trim().toLowerCase();
  }


  refresh() {
    this.searchValue = '';
    this.initialLoading();
  }


  applyFilter(searchValue: any) {
    if (this.form.valid) {
      //this.form.valueChanges;
      this.noOfClicks++;
      this.form.controls['fromDate'].setValue(this.utilService.selectedDate(this.form.controls['fromDate'].value));
      this.loadList(0, this.pageService.pageCount, this.searchValue, this.sortingOrder);
    }
    else
      this.notifyService.showError('select a Date')
  }
}
