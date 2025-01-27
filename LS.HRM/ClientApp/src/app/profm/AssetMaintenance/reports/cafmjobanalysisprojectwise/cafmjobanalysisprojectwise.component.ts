import { Component, OnInit } from '@angular/core';
import { ParentB2CFrontComponent } from '../../../../sharedcomponent/parentb2cfront.component';
import { ApiService } from '../../../../services/api.service';
import { AuthorizeService } from '../../../../api-authorization/AuthorizeService';
import { NotificationService } from '../../../../services/notification.service';
import { UtilityService } from '../../../../services/utility.service';
import { CustomSelectListItem, LanCustomSelectListItem } from '../../../../models/MenuItemListDto';

@Component({
  selector: 'app-cafmjobanalysisprojectwise',
  templateUrl: './cafmjobanalysisprojectwise.component.html',
  styles: [
  ]
})
export class CafmjobanalysisprojectwiseComponent extends ParentB2CFrontComponent implements OnInit {

  jobList: Array<any> = [];
  customerList: Array<LanCustomSelectListItem> = [];
  projectList: Array<LanCustomSelectListItem> = [];
  statusList: Array<CustomSelectListItem> = [];
  departmentSelectList: Array<LanCustomSelectListItem> = [];

  totalDrAmount: number = 0;
  totalCrAmount: number = 0;
  totalBalance: number = 0;
  totalProfitLossAmount: number = 0;
  isArab: boolean = false;

  isLoading: boolean = false;
  company: any;
  dateFrom: string = '';
  dateTo: string = '';
 
  constructor(private apiService: ApiService,
    private authService: AuthorizeService, private utilService: UtilityService,
    private notifyService: NotificationService) {
    super(authService);
  }

  ngOnInit(): void {
    this.dateFrom = this.utilService.getStrtingYearDate();
    this.dateTo = this.utilService.getCurrentDate();
    this.isArab = this.utilService.isArabic();
    this.loadData();
  }

  loadData() {
    this.statusList = [{ text: 'All', value: 'All' }, { text: 'Closed', value: 'Closed' }, { text: 'Open', value: 'Open' }];
    this.apiService.getall(`fomDiscipline/getDepartmentSelectList`).subscribe(res => {
      if (res) {
        this.departmentSelectList = res;
      }
    });
    this.apiService.getall("assetMaintenanceReport/getSelectCustomerList").subscribe(res => {
      if (res) {
        this.customerList = res;
        const newList = this.customerList.map((i: any) => {
          i.text = !this.isArab ? i.text : i.textTwo;
          return i;
        });
      }
    });
  }

  customerChange(event: any) {
    let custId = event.value, custName = event.text;
    this.apiService.getall(`assetMaintenanceReport/getProjectCodesByCustomerCode?customerId=${custId}`).subscribe(res => {
      if (res) {
        this.projectList = res;
      }
    });
  } 
  search() {

    if (this.dateFrom && this.dateTo) {
      this.isLoading = true;
      this.apiService.getall(`assetMaintenanceReport/cafmDayWiseSummary?projectwise=yes&from=${this.utilService.getCommonDate(this.dateFrom)}&to=${this.utilService.getCommonDate(this.dateTo)}`).subscribe(res => {
        this.isLoading = false;
        if (res) {
          this.jobList = res['list'];
          this.company = res['company'];
        }
      });
    }
    else
      this.notifyService.showError("Select Dates");
  }



  openPrint() {
    const printContent = document.getElementById("printcontainer") as HTMLElement;
    this.utilService.printForLocale(printContent);
  }



}


