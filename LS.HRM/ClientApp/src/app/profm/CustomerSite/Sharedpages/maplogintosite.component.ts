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
  cityList1: Array<CustomSelectListItem> = [];
  custCodeControl = new FormControl('', Validators.required);
  //filteredCustCodes: Observable<Array<CustomSelectListItem>>;
  isDataLoading: boolean = false;
  isChildCustomer: boolean = false;
  customerForm!: FormGroup;
  customerCodes: any[] = [];
  loginSites: any[] = [];

  constructor(private fb: FormBuilder, private http: HttpClient, private router: Router, private apiService: ApiService,
    private authService: AuthorizeService, private utilService: UtilityService, private translate: TranslateService,
    private notifyService: NotificationService, private validationService: ValidationService, public dialogRef: MatDialogRef<MaplogintositeComponent>) {
    super(authService);
   

  }

  ngOnInit(): void {
    this.customerForm = this.fb.group({
      customerCode: ['']
    });

   // this.getCustomerCodes();
  }


  getCustomerCodes() {
    //this.customerService.getCustomerCodes().subscribe((data) => {
    //  this.customerCodes = data;
    //});
  }

  onCustomerSelect(event: any) {
    const customerCode = event.target.value;
    //this.customerService.getLoginCodes(customerCode).subscribe((data) => {
    //  this.loginSites = data.map((site: any) => ({
    //    ...site,
    //    checked: !!site.siteCode // Check if site code is not null
    //  }));
   // });
  }


  saveMapping() {
    //const selectedSites = this.loginSites
    //  .filter((site) => site.checked)
    //  .map((site) => site.siteCode)
    //  .join(',');

    //const payload = {
    //  customerCode: this.customerForm.value.customerCode,
    //  siteCodes: selectedSites
    //};

    //this.customerService.updateSiteMapping(payload).subscribe((response) => {
    //  console.log('Mapping saved successfully:', response);
    //  alert('Mapping saved successfully!');
    //});
  }

  selectAll(checked: boolean) {
   // this.loginSites.forEach((site) => (site.checked = checked));
  }



  closeModel() {
    this.dialogRef.close();
  }
  //setForm() {
  //  //this.form = this.fb.group({

  //  //  'siteCode': ['SITEXXXXXX'],
  //  //  'siteName': ['', Validators.required],
  //  //  'siteArbName': ['', Validators.required],
  //  //  'customerCode': [''],
  //  //  'siteAddress': ['', Validators.required],
  //  //  'siteCityCode': ['', Validators.required],
  //  //  'siteGeoLatitude': ['', Validators.required],
  //  //  'siteGeoLongitude': ['', Validators.required],
  //  //  'siteGeoGain': ['', Validators.required],
  //  //  'isChildCustomer': [false],
  //  //  'vatNumber': [''],
  //  //  'isActive': [true]
  //  //});

  //}
  //submit() {

  //  if (this.id > 0)
  //    this.form.value['id'] = this.id;

  //  let custCode = this.customerCode;

  //  if (this.utilService.hasValue(custCode)) {
  //    this.form.value['customerCode'] = custCode;
  //    this.custCodeControl.setValue(custCode);

  //  }
  //  else {
  //    this.utilService.FillUpFields();
  //  }
  //  if (this.form.valid) {

    

  //    this.apiService.post('FomCustomerSite', this.form.value)
  //      .subscribe(res => {
  //        this.utilService.OkMessage();
  //        this.dialogRef.close(true);
  //      },
  //        error => {
  //          console.error(error);
  //          this.utilService.ShowApiErrorMessage(error);
  //        });

  //  }
  //  else
  //    this.utilService.FillUpFields();
  //  // this.ngOnInit();
  //}

  
  //setEditForm() {
  //  this.apiService.get('FomCustomerSite/getSiteById', this.id).subscribe(res => {

  //    if (res) {
       
  //      this.form.patchValue(res);
  //      this.custCodeControl.setValue(res['customerCode']);
       
  //      this.form.patchValue({ 'id': 0 });
  //    }


  //  });



  //}

  //loadCities() {
  //  this.apiService.getall('FomCustomer/getCitiesSelectList').subscribe(res => {
  //    this.cityList1 = res;
  //  });

  //  this.apiService.getPagination('FomCustomer', this.utilService.getQueryString(0, 1000, '', '')).subscribe(res => {
  //    if (res)
  //      this.CustomerCodeList = res['items'];
  //  });
  //}
  //reset() {
  //  this.form.controls['siteCode'].setValue('');
  //  this.form.controls['siteName'].setValue('');
  //  this.form.controls['SiteArbName'].setValue('');
  //  this.form.controls['customerCode'].setValue('');
  //  this.form.controls['siteAddress'].setValue('');
  //  this.form.controls['siteCityCode'].setValue('');
  //  this.form.controls['siteGeoLatitude'].setValue('');
  //  this.form.controls['siteGeoLongitude'].setValue('');
  //  this.form.controls['siteGeoGain'].setValue('');
  //  this.form.controls['isChildCustomer'].setValue('');
  //  this.form.controls['vatNumber'].setValue('');
  //  this.form.controls['isActive'].setValue(true);

  //}

  
  

 

 

}

