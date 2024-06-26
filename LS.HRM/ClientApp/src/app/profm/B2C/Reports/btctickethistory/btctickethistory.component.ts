import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AuthorizeService } from 'src/app/api-authorization/AuthorizeService';
import { ApiService } from 'src/app/services/api.service';
import { NotificationService } from 'src/app/services/notification.service';
import { UtilityService } from 'src/app/services/utility.service';
import { ValidationService } from 'src/app/sharedcomponent/ValidationService';
import { ParentB2CComponent } from 'src/app/sharedcomponent/parentb2c.component';
import { CustomSelectListItem } from '../../../../models/MenuItemListDto';

@Component({
  selector: 'app-btctickethistory',
  templateUrl: './btctickethistory.component.html',
  styles: [
  ]
})
export class BtctickethistoryComponent extends ParentB2CComponent implements OnInit {
  serviceList: any;
  statusSelectionList: Array<any> = [];
  resources: Array<CustomSelectListItem> = [];
  CustomerContractList: Array<CustomSelectListItem> = [];
  list: Array<any> = [];
  form!: FormGroup;

  constructor(private fb: FormBuilder, private apiService: ApiService,
    private authService: AuthorizeService, private utilService: UtilityService,
    private notifyService: NotificationService, private validationService: ValidationService) {
    super(authService)
  };

  ngOnInit(): void {
    this.loadFormData();
    this.setForm();
  }
  setForm() {
    this.form = this.fb.group({
      'custCode': [''],
      'ticketStatus': [''],
      'serviceType': [''],
      'resourceCode': [''],
      'dateFrom': [null],
      'dateTo': [null],
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

  }

  refresh() {

  }

  openPrint() {
    const printContent = document.getElementById("printcontainer") as HTMLElement;
    this.utilService.printForLocale(printContent);
  }
}
