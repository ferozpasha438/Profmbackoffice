import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { Observable } from 'rxjs';
//import {  startWith, debounceTime, distinctUntilChanged, switchMap, map } from 'rxjs/operators';

import { AuthorizeService } from 'src/app/api-authorization/AuthorizeService';
import { CustomSelectListItem } from 'src/app/models/MenuItemListDto';
import { ApiService } from 'src/app/services/api.service';
import { NotificationService } from 'src/app/services/notification.service';
import { DBOperation } from 'src/app/services/utility.constants';
import { UtilityService } from 'src/app/services/utility.service';
import { ValidationService } from 'src/app/sharedcomponent/ValidationService';
import { ParentOptMgtComponent } from 'src/app/sharedcomponent/parentoptmgt.component';
import { ParentFomMgtComponent } from '../../../sharedcomponent/parentfommgt.component';
import { SelectionModel } from '@angular/cdk/collections';

@Component({
  selector: 'app-maplogintosite',
  templateUrl: './maplogintosite.component.html',
  styles: [
  ]
})
export class MaplogintositeComponent extends ParentFomMgtComponent implements OnInit {
  readonly: string = "";
  modalTitle!: string;
  modalBtnTitle!: string;
  dbops!: DBOperation;
  form!: FormGroup;
  id: number = 0;
  customerCode!: string;
  custCityCode!: string;
  CustomerCodeList: Array<CustomSelectListItem> = [];
  CustomerLoginList: Array<CustomSelectListItem> = [];
  sitesList: Array<CustomSelectListItem> = [];
  custCodeControl = new FormControl('', Validators.required);
  //filteredCustCodes: Observable<Array<CustomSelectListItem>>;
  isDataLoading: boolean = false;
  isChildCustomer: boolean = false;
  customerForm!: FormGroup;
  customerCodes: any[] = [];
  loginSites: any[] = [];
  selection = new SelectionModel<any>(true, []);

  constructor(private fb: FormBuilder, private http: HttpClient, private router: Router, private apiService: ApiService,
    private authService: AuthorizeService, private utilService: UtilityService, private translate: TranslateService,
    private notifyService: NotificationService, private validationService: ValidationService, public dialogRef: MatDialogRef<MaplogintositeComponent>) {
    super(authService);


  }

  ngOnInit(): void {
    this.customerForm = this.fb.group({
      custCode: ['', Validators.required],
      userClientLoginCode: ['', Validators.required],
    });

    this.getCustomerCodes();
  }


  getCustomerCodes() {
    this.apiService.getall('FomCustomer/getSelectCustomerList').subscribe(res => {
      if (res)
        this.CustomerCodeList = res;
    });  
  }

  getSelectFomCustomerContractByCustCode(custCode: string) {
    this.apiService.getall(`FomCustomerSite/getSelectFomCustomerContractByCustCode/${custCode}`).subscribe(res => {
      if (res)
        this.sitesList = res;
    });
  }

  onCustomerSelect(event: any) {
    const customerCode = event.target.value;
    this.getSelectFomCustomerContractByCustCode(customerCode);
    this.apiService.getall(`FomCustomer/getSelectCustomerLoginList?custCode=${customerCode}`).subscribe(res => {
      if (res)
        this.CustomerLoginList = res;
    });
  }

  onCustomerLoginCodeSelect(event: any) {
    const loginCode = event.target.value;
    this.apiService.getall(`FomCustomer/getSelectSitesByCustomerLoginCode?custCode=${this.customerForm.get('custCode')?.value}&loginCode=${loginCode}`).subscribe(res => {
      this.selection = new SelectionModel<any>(true, []);
      if (res && res.length > 0) {
        const selectedSites: Array<any> = (res as Array<any>[]).map((item: any) => item);        
        this.selection.select(...this.sitesList.filter(item => selectedSites.some(d => d.value === item.value)));
      }      
    });
  }

  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.sitesList.length;
    return numSelected === numRows;
  }

  toggleAllRows() {
    if (this.isAllSelected()) {
      this.selection.clear();
      return;
    }

    this.selection.select(...this.sitesList);
  }

  saveMapping() {
    if (this.customerForm.valid) {
      const siteCodes = this.selection.selected.map(item => item.value).join(',');
      this.customerForm.value['siteCode'] = siteCodes;
      this.apiService.post('FomCustomer/CreateMappingLoginCodesToSites', this.customerForm.value)
        .subscribe(res => {
          this.utilService.OkMessage();
          this.dialogRef.close(true);
        },
          error => {
            console.error(error);
            this.utilService.ShowApiErrorMessage(error);
          });

    }
    else
      this.notifyService.showError('Please fill all fields!');
  }

  closeModel() {
    this.dialogRef.close();
  }
}

