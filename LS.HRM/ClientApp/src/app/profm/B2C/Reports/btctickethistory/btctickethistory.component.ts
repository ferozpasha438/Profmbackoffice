import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AuthorizeService } from 'src/app/api-authorization/AuthorizeService';
import { ApiService } from 'src/app/services/api.service';
import { NotificationService } from 'src/app/services/notification.service';
import { UtilityService } from 'src/app/services/utility.service';
import { ValidationService } from 'src/app/sharedcomponent/ValidationService';
import { ParentB2CComponent } from 'src/app/sharedcomponent/parentb2c.component';
import { CustomSelectListItem } from '../../../../models/MenuItemListDto';
import { ParentB2CFrontComponent } from '../../../../sharedcomponent/parentb2cfront.component';

@Component({
  selector: 'app-btctickethistory',
  templateUrl: './btctickethistory.component.html',
  styles: [
  ]
})
export class BtctickethistoryComponent extends ParentB2CFrontComponent implements OnInit {
  serviceList: any;
  statusSelectionList: Array<any> = [];
  resources: Array<CustomSelectListItem> = [];
  CustomerContractList: Array<CustomSelectListItem> = [];
  list: Array<any> = [];
  isLoading: boolean = false;
  form!: FormGroup;

  constructor(private fb: FormBuilder, private apiService: ApiService,
    private authService: AuthorizeService, private utilService: UtilityService,
    private notifyService: NotificationService, private validationService: ValidationService) {
    super(authService)
  };

  ngOnInit(): void {
    this.loadFormData();
    this.setForm();
    this.filter();
  }
  setForm() {
    this.form = this.fb.group({
      'custCode': [''],
      'ticketStatus': [''],
      'serviceType': [''],
      'resourceCode': [''],
      'dateFrom': [''],
      'dateTo': [''],
    });
  }

  loadFormData() {

    this.apiService.getall('fomMobB2CService/getAssignResourceSelectList').subscribe(res => {
      this.resources = res;
    });
    this.apiService.getall('fomCustomerContract/getCustomerContractSelectList').subscribe(res => {
      if (res) {
        this.CustomerContractList = res;
      }
    });

    this.statusSelectionList = [
      { text: 'All', id: 0 },
      { text: 'Approved', id: 5 },
      { text: 'Closed', id: 9 },
      { text: 'Completed', id: 11 },
      { text: 'Void', id: 3 },
    ];

    this.serviceList = [
      { id: "D", text: 'Daily Service', },
      { id: "M", text: 'Monthly Service' },
      { id: "Y", text: 'Yearly Service' },
    ];

  }

  filter() {
    this.isLoading = true;
    // const qsLong = '?' + Object.keys(this.form.value).map(key => `${key}=${encodeURIComponent(this.form.value[key])}`).join('&')

    this.form.controls['dateFrom'].setValue(this.utilService.getCommonDate(this.form.controls['dateFrom'].value));
    this.form.controls['dateTo'].setValue(this.utilService.getCommonDate(this.form.controls['dateTo'].value));

    const qs = new URLSearchParams(this.form.value).toString();
    this.apiService.getPagination(`fomMobB2CReport/getB2CTickethistory`, qs).subscribe(res => {
      this.isLoading = false;
      if (res) {
        let listItems = res['listItems'];
        this.list = listItems['list'];

      }
    });
  }

  refresh() {
    this.form.patchValue({
      'custCode': '',
      'ticketStatus': '',
      'serviceType': '',
      'resourceCode': '',
      'dateFrom': '',
      'dateTo': '',
    });
    this.filter();
  }

  b2cFrequency(freq: any): string {

    return '';
  }
  openPrint() {
    const printContent = document.getElementById("printcontainer") as HTMLElement;
    this.utilService.printForLocale(printContent);
  }
}
