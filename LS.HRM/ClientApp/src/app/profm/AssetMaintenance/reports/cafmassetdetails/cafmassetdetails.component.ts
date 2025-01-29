import { Component, OnInit } from '@angular/core';
import { ParentB2CFrontComponent } from '../../../../sharedcomponent/parentb2cfront.component';
import { ApiService } from '../../../../services/api.service';
import { AuthorizeService } from '../../../../api-authorization/AuthorizeService';
import { NotificationService } from '../../../../services/notification.service';
import { UtilityService } from '../../../../services/utility.service';
import { CustomSelectListItem, LanCustomSelectListItem } from '../../../../models/MenuItemListDto';
import { AddupdateassetmasterComponent } from '../../assetmaster/addupdateassetmaster/addupdateassetmaster.component';

@Component({
  selector: 'app-cafmassetdetails',
  templateUrl: './cafmassetdetails.component.html',
  styles: [
  ]
})
export class CafmassetdetailsComponent extends ParentB2CFrontComponent implements OnInit {

  jobList: Array<any> = [];
  customerList: Array<LanCustomSelectListItem> = [];
  projectList: Array<LanCustomSelectListItem> = [];

  totalDrAmount: number = 0;
  totalCrAmount: number = 0;
  totalBalance: number = 0;
  totalProfitLossAmount: number = 0;
  isArab: boolean = false;

  isLoading: boolean = false;
  company: any;
  customerCode: string = '';
  projCode: string = '';

  constructor(private apiService: ApiService,
    private authService: AuthorizeService, private utilService: UtilityService,
    private notifyService: NotificationService) {
    super(authService);
  }

  ngOnInit(): void {
    this.isArab = this.utilService.isArabic();
    this.loadData();
  }

  loadData() {
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
  resetProjectCode() { this.projCode = '' }
  resetCustomerCode() { this.customerCode = '' }
  search() {

    this.isLoading = true;
    this.apiService.getall(`assetMaintenanceReport/cafmassetdetails?customerCode=${this.customerCode}&projCode=${this.projCode}`).subscribe(res => {
      this.isLoading = false;
      if (res) {
        this.jobList = res['list'];
        this.company = res['company'];
        const asstscales = this.utilService.getAssetScales();

        this.jobList.forEach(item => {
          item.assetScale = asstscales[item.assetScale];
        })
      }
    });
  }



  openPrint() {
    const printContent = document.getElementById("printcontainer") as HTMLElement;
    this.utilService.printForLocale(printContent);
  }



}

