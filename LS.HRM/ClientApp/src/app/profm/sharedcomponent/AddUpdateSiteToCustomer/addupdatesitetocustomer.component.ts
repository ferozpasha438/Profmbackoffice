import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { Observable } from 'rxjs';
import { debounceTime, distinctUntilChanged, map, mergeMap, startWith, switchMap } from 'rxjs/operators';
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
  selector: 'app-addupdatesitetocustomer',
  templateUrl: './addupdatesitetocustomer.component.html',
  styles: [
  ]
})
export class AddupdatesitetocustomerComponent extends ParentFomMgtComponent implements OnInit {
  modalTitle!: string;
  modalBtnTitle!: string;
  dbops!: DBOperation;
  form!: FormGroup;
  id: number = 0;
  customerCode!: string;
  custCityCode!: string;
  CustomerCodeList: Array<CustomSelectListItem> = [];
  cityList: Array<CustomSelectListItem> = [];
  custCodeControl = new FormControl();
  filteredCustCodes: Observable<Array<CustomSelectListItem>>;
  isDataLoading: boolean = false;
  constructor(private fb: FormBuilder, private http: HttpClient, private router: Router, private apiService: ApiService,
    private authService: AuthorizeService, private utilService: UtilityService, private translate: TranslateService,
    private notifyService: NotificationService, private validationService: ValidationService, public dialogRef: MatDialogRef<AddupdatesitetocustomerComponent>) {
    super(authService);

    this.filteredCustCodes = this.custCodeControl.valueChanges.pipe(
      startWith(''),
      debounceTime(utilService.autoDelay()),
      distinctUntilChanged(),
      switchMap((val: string) => {
        if (val.trim() !== '')
          this.isDataLoading = true;
        return this.filterCustCodes(val || '')
      })
    );

  }

  ngOnInit(): void {
    this.loadCities();

    this.setForm();
    if (this.id > 0) {
      this.setEditForm();
    }
  }
  closeModel() {
    this.dialogRef.close();
  }
  setForm() {
    this.form = this.fb.group({

      'siteCode': ['SITEXXXXXX'],
      'siteName': ['', Validators.required],
      'siteArbName': ['', Validators.required],
      'customerCode': ['', Validators.required],
      'siteAddress': ['', Validators.required],
      'siteCityCode': [''],
      'siteGeoLatitude': ['', Validators.required],
      'siteGeoLongitude': ['', Validators.required],
      'siteGeoGain': ['', Validators.required],
      'isActive': [true]
    });
  }

  filterCustCodes(val: string): Observable<Array<CustomSelectListItem>> {
    return this.apiService.getall(`FomCustomerSite/getSelectCustomerList?search=${val}`)
      .pipe(
        map(response => {
          const res = response as Array<CustomSelectListItem>;
          this.isDataLoading = false;
          return res;
        })
      )
  }


  submit() {
    if (this.form.valid) {
      if (this.id > 0)
        this.form.value['id'] = 0;
   //   let custCode = this.custCodeControl.value as string;
      this.apiService.post('FomCustomerSite', this.form.value)
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
      this.utilService.FillUpFields();
  }
  setEditForm() {
    this.apiService.get('FomCustomerSite/getSiteById', this.id).subscribe(res => {
      this.form.value['id'] = 0;
      if (res) {
        this.form.controls['customerCode'].setValue(res.custCode);
        this.custCodeControl.setValue(res['customerCode']);
      }
    });
  }


  //loadData() {
  //  this.apiService.getPagination('fomItemCategory', this.utilService.getQueryString(0, 1000, '', '')).subscribe(res => {
  //    if (res)
  //      this.CategoryCodeList = res['items'];
  //  });

  //}




  loadCities() {

    this.apiService.getall('FomCustomer/getCitiesSelectList').subscribe(res => {
      this.cityList = res;
    });

    this.apiService.getPagination('FomCustomer', this.utilService.getQueryString(0, 1000, '', '')).subscribe(res => {
      if (res)
        this.CustomerCodeList = res['items'];
    });
  }
}
