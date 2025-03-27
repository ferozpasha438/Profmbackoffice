import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { startWith, debounceTime, distinctUntilChanged, switchMap, map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { AuthorizeService } from 'src/app/api-authorization/AuthorizeService';
import { CustomSelectListItem } from 'src/app/models/MenuItemListDto';
import { MultiFileUploadDto } from 'src/app/models/sharedDto';
import { ApiService } from 'src/app/services/api.service';
import { NotificationService } from 'src/app/services/notification.service';
import { DBOperation } from 'src/app/services/utility.constants';
import { UtilityService } from 'src/app/services/utility.service';
import { ValidationService } from 'src/app/sharedcomponent/ValidationService';
import { FileUploadComponent } from 'src/app/sharedcomponent/fileupload.component';
import { ParentSalesMgtComponent } from 'src/app/sharedcomponent/parentsalesmgt.component';
import { ParentFomMgtComponent } from '../../../sharedcomponent/parentfommgt.component';

@Component({
  selector: 'app-addupdatecustomer',
  templateUrl: './addupdatecustomer.component.html',
  styles: [
  ]
})
export class AddupdatecustomerComponent extends ParentFomMgtComponent implements OnInit {
  modalTitle!: string;
  modalBtnTitle!: string;
  dbops!: DBOperation;
  form!: FormGroup;
  id: number = 0;
  imageUrl: string | ArrayBuffer | null | undefined = '';
  fileUploadone!: File;
  fileUploadtwo!: File;
  fileUploadthree!: File;
  fileUploadfour!: File;
  custCityCode1!: string;
  custCityCode2!: string;
  CustCategoryCodeList:Array<CustomSelectListItem> =[];
  cityList1: Array<CustomSelectListItem> = [];
  cityList2: Array<CustomSelectListItem> = [];
  userTypes: Array<CustomSelectListItem> = [];
  categoryCodeControl = new FormControl('', Validators.required);
  salesTermsCodeControl = new FormControl('', Validators.required);
  filteredCategoryCodes: Observable<Array<CustomSelectListItem>>;
  filteredSalesTermsCodes: Observable<Array<CustomSelectListItem>>;
  custARAcControl = new FormControl('', Validators.required);
  filteredcustARAc: Observable<Array<CustomSelectListItem>>;
  custArAcCodeControl = new FormControl('', Validators.required);
  filteredcustArAcCode: Observable<Array<CustomSelectListItem>>;
  custDefExpAcCodeControl = new FormControl('', Validators.required);
  filteredcustDefExpAcCode: Observable<Array<CustomSelectListItem>>;
  custARAdjAcCodeControl = new FormControl('', Validators.required);
  filteredcustARAdjAcCode: Observable<Array<CustomSelectListItem>>;
  custARDiscAcCodeControl = new FormControl('', Validators.required);
  filteredcustARDiscAcCode: Observable<Array<CustomSelectListItem>>;
  readonly: string = "";
  canAutoGenCustCode: boolean = false;
  isDataLoading: boolean = false;
  file1Url: string = '';
  file2Url: string = '';
  file3Url: string = '';
  selectedFile: File | null = null;
  constructor(private fb: FormBuilder, private http: HttpClient, private router: Router, private apiService: ApiService,
    private authService: AuthorizeService, private utilService: UtilityService, private translate: TranslateService, public dialog: MatDialog,
    private notifyService: NotificationService, private validationService: ValidationService, public dialogRef: MatDialogRef<AddupdatecustomerComponent>) {
    super(authService);

    this.filteredCategoryCodes = this.categoryCodeControl.valueChanges.pipe(
      startWith(''),
      debounceTime(utilService.autoDelay()),
      distinctUntilChanged(),
      switchMap((val: string) => {
        if (val.trim() !== '')
          this.isDataLoading = true;
        return this.filterCategoryCodes(val || '')
      })
    );

    this.filteredSalesTermsCodes = this.salesTermsCodeControl.valueChanges.pipe(
      startWith(''),
      debounceTime(utilService.autoDelay()),
      distinctUntilChanged(),
      switchMap((val: string) => {
        if (val.trim() !== '')
          this.isDataLoading = true;
        return this.filterSalesTermsCodes(val || '')
      })
    );

    this.filteredcustARAc = this.custARAcControl.valueChanges.pipe(
      startWith(''),
      debounceTime(utilService.autoDelay()),
      distinctUntilChanged(),
      switchMap((val: string) => {
        if (val.trim() !== '')
          this.isDataLoading = true;
        return this.filterAccountCodes(val || '')
      })
    );

    this.filteredcustArAcCode = this.custArAcCodeControl.valueChanges.pipe(
      startWith(''),
      debounceTime(utilService.autoDelay()),
      distinctUntilChanged(),
      switchMap((val: string) => {
        if (val.trim() !== '')
          this.isDataLoading = true;
        return this.filterAccountCodes(val || '')
      })
    );
    this.filteredcustDefExpAcCode = this.custDefExpAcCodeControl.valueChanges.pipe(
      startWith(''),
      debounceTime(utilService.autoDelay()),
      distinctUntilChanged(),
      switchMap((val: string) => {
        if (val.trim() !== '')
          this.isDataLoading = true;
        return this.filterAccountCodes(val || '')
      })
    );
    this.filteredcustARAdjAcCode = this.custARAdjAcCodeControl.valueChanges.pipe(
      startWith(''),
      debounceTime(utilService.autoDelay()),
      distinctUntilChanged(),
      switchMap((val: string) => {
        if (val.trim() !== '')
          this.isDataLoading = true;
        return this.filterAccountCodes(val || '')
      })
    );
    this.filteredcustARDiscAcCode = this.custARDiscAcCodeControl.valueChanges.pipe(
      startWith(''),
      debounceTime(utilService.autoDelay()),
      distinctUntilChanged(),
      switchMap((val: string) => {
        if (val.trim() !== '')
          this.isDataLoading = true;
        return this.filterAccountCodes(val || '')
      })
    );

  }

  ngOnInit(): void {

    this.setForm();
    this.canAutoGenerateCustCode();
    this.loadCities();
    if (this.id > 0) {
      this.readonly = "readonly";
      this.setEditForm();

    }
  }
  closeModel() {
    this.dialogRef.close();
  }
  setForm() {
    //let cDate: IMyDateModel = { isRange: false, singleDate: {}, dateRange: null };
    this.form = this.fb.group({
      //'': ['', Validators.required],
      //   'stateone': [''],
      //   'countryone': [''],
      //   'statetwo': [''],
      //   'countrytwo': [''],
      'custCode': ['', [Validators.required, Validators.maxLength(20)]],
      'custName': ['', Validators.required],
      'custArbName': ['', Validators.required],
      'custAlias': ['', Validators.required],
      //   'custType': ['',Validators.required],
      'vatNumber': ['', Validators.required],
      'custRating': ['', Validators.required],
      'custDiscount': ['', Validators.required],
      'custCrLimit': ['', Validators.required],
      'custSalesRep': ['', Validators.required],
      'custSalesArea': ['', Validators.required],
      'custARAc': [''],
      'custLastPaidDate': ['', Validators.required],
      'custLastSalesDate': ['', Validators.required],
      'custLastPayAmt': ['', Validators.required],
      'custAddress1': ['', Validators.required],
      //   'custCityId1': [''],

      'custMobile1': ['', [Validators.required]],
      'password': [''],
      'custPhone1': ['', [Validators.required]],
      'custEmail1': ['', [Validators.required, Validators.email]],
      'custContact1': ['', Validators.required],
      'custAddress2': ['', Validators.required],
      //   'custCityId2': [''],

      'custMobile2': ['', [Validators.required]],
      'custPhone2': ['', [Validators.required]],
      'custEmail2': ['', [Validators.required, Validators.email]],
      'custContact2': ['', Validators.required],
      //   'custUDF1': [''],
      //   'custUDF2': [''],
      //   'custUDF3': [''],
      //   'custAllowCrsale': [false],
      //   'custAlloCrOverride': [false],
      //   'custOnHold': [false],
      //   'custAlloChkPay': [false],
      //   'custSetPriceLevel': [false],
      //   'custIsVendor': [false],
      //   'custArAcBranch': [false],
      //   'custArAcCode': ['', Validators.required],
      //   'custDefExpAcCode': ['',Validators.required],
      //   'custARAdjAcCode': ['',Validators.required],
      //   'custARDiscAcCode': ['',Validators.required],
      //   'custCatCode': [''],
      //   'salesTermsCode': [''],
      //   'custCityCode1': [''],
      //   'custCityCode2': [''],
      'isActive': [true],
      //   'custOutStandBal': [''],
      //   'custAvailCrLimit': [''],

      'stateone': [''],
      'countryone': [''],
      'statetwo': [''],
      'countrytwo': [''],
      //   'custCode': [''],
      //  'custName': [''],
      //  'custArbName': [''],
      // 'custAlias': [''],
      // 'custRating': [''],
      'custType': [''],
      'custCatCode': [''],
      'salesTermsCode': [''],
      // 'custDiscount': [''],
      // 'custCrLimit': [''],
      // 'custSalesRep': [''],
      //'custSalesArea': [''],
      //'custARAc': [''],
      //'custLastPaidDate': [''],
      //'custLastSalesDate': [''],
      //'custLastPayAmt': [''],
      // 'custAddress1': [''],
      'custCityCode1': ['', Validators.required],

      //'custMobile1': [''],
      //'custPhone1': [''],
      //'custEmail1': [''],
      //'custContact1': [''],
      //'custAddress2': [''],
      'custCityCode2': ['', Validators.required],

      //'custMobile2': [''],
      //'custPhone2': [''],
      //'custEmail2': [''],
      //'custContact2': [''],
      'custUDF1': [''],
      'custUDF2': [''],
      'custUDF3': [''],
      'imageUrl': [''],
      //'isActive': [true],
      'custAllowCrsale': [false],
      'custAlloCrOverride': [false],
      'custOnHold': [false],
      'custAlloChkPay': [false],
      'custSetPriceLevel': [false],
      //'custPriceLevel': [100],
      'custIsVendor': [false],
      'custArAcBranch': [false],
      'custArAcCode': [''],
      'custDefExpAcCode': [''],
      'custARAdjAcCode': [''],
      'custARDiscAcCode': [''],
      'custOutStandBal': [0],
      'custAvailCrLimit': ['']
    });

  }
  //submit() {
  //  //let custArAcCode = this.custArAcCodeControl.value as string;
  //  console.log(this.custArAcCodeControl);
  //}

  onSelectFiles(fileInput: any) {
    if (fileInput.target.files.length > 3) {
      this.notifyService.showWarning("Select Maximum 3 Images");
    } else if (fileInput.target.files.length > 0) {
      this.fileUploadone = <File>fileInput.target.files[0];
      if (fileInput.target.files.length > 1) {
        this.fileUploadtwo = <File>fileInput.target.files[1];
      }
      if (fileInput.target.files.length > 2) {
        this.fileUploadthree = <File>fileInput.target.files[2];
      }
    }
  }


  //onFileSelected(event: Event) {
  //  const input = event.target as HTMLInputElement;
  //  if (input.files && input.files.length) {
  //    this.selectedFile = input.files[0];
  //    const reader = new FileReader();

  //    reader.onload = (e) => {
  //      this.imageUrl = e.target?.result; // Preview Image
  //    };

  //    reader.readAsDataURL(this.selectedFile);
  //  }
  //}

  //onFileSelected(fileInput: any) {
  //  if (fileInput.target.files.length > 1) {
  //    this.notifyService.showWarning("Select Only 1 Image");
  //  } else if (fileInput.target.files.length > 0) {
         
  //    this.fileUploadfour = <File>fileInput.target.files[0];
     
  //  }
  //}

  onFileSelected(fileInput: any) {
    if (fileInput.target.files.length > 1) {
      this.notifyService.showWarning("Select Only 1 Image");
      return;
    }

    if (fileInput.target.files.length > 0) {
      this.fileUploadfour = fileInput.target.files[0];

      // Preview Image
      const reader = new FileReader();
      reader.onload = (e) => {
        this.imageUrl = e.target?.result; // Store base64 URL for preview
      };
      reader.readAsDataURL(this.fileUploadfour);
    }
  }

  submit() {
    if (this.id > 0)
      this.form.value['id'] = this.id;

    let custCatCode = this.categoryCodeControl.value as string;
    let salesTermsCode = this.salesTermsCodeControl.value as string;
    let custARAc = this.custARAcControl.value as string;

    let custARDiscAcCode = this.custARDiscAcCodeControl.value as string;
    let custDefExpAcCode = this.custDefExpAcCodeControl.value as string;
    let custARAdjAcCode = this.custARAdjAcCodeControl.value as string;
    let custArAcCode = this.custArAcCodeControl.value as string;

    if (this.utilService.hasValue(custCatCode)) {
      this.form.value['custCatCode'] = this.utilService.removeSqueres(custCatCode);
      this.categoryCodeControl.setValue(custCatCode);
    }
    else {
      console.log("custCatCode-");
    }

    if (this.utilService.hasValue(salesTermsCode)) {

      this.form.value['salesTermsCode'] = this.utilService.removeSqueres(salesTermsCode);
      this.salesTermsCodeControl.setValue(salesTermsCode);
    }
    else {
      console.log("salesTermsCode-");
    }

    if (this.utilService.hasValue(custARAc)) {
      this.form.value['custARAc'] = this.utilService.removeSqueres(custARAc);
      this.custARAcControl.setValue(custARAc);
    }
    else {
      console.log("custARAc-");
    }

    if (this.utilService.hasValue(custArAcCode)) {
      this.form.value['custArAcCode'] = this.utilService.removeSqueres(custArAcCode);
      this.custArAcCodeControl.setValue(custArAcCode);
    }
    else {
      console.log("custArAcCode-");
    }

    if (this.utilService.hasValue(custDefExpAcCode)) {
      this.form.value['custDefExpAcCode'] = this.utilService.removeSqueres(custDefExpAcCode);
      this.custDefExpAcCodeControl.setValue(custDefExpAcCode);

    }
    else {
      console.log("custDefExpAcCode-");
    }
    if (this.utilService.hasValue(custARAdjAcCode)) {
      this.form.value['custARAdjAcCode'] = this.utilService.removeSqueres(custARAdjAcCode);
      this.custARAdjAcCodeControl.setValue(custARAdjAcCode);

    }
    else {
      console.log("custARAdjAcCode-");
    }
    if (this.utilService.hasValue(custARDiscAcCode)) {
      this.form.value['custARDiscAcCode'] = this.utilService.removeSqueres(custARDiscAcCode);
      this.custARDiscAcCodeControl.setValue(custARDiscAcCode);

    }
    else {
      console.log("custARDiscAcCode-");
    }

    if (this.form.valid) {
      this.apiService.post('FomCustomer', this.form.value)
        .subscribe(res => {
          if (res && this.fileUploadfour != null && this.fileUploadfour != undefined) {
            const custRes = res as any;
            const formData = new FormData();
            formData.append("id", custRes.id.toString());
            formData.append("WebRoot", this.authService.ApiEndPoint().replace("api", "") + 'Customerfiles/');
            formData.append("Image1IForm", this.fileUploadone);
            formData.append("Image2IForm", this.fileUploadtwo);
            formData.append("Image3IForm", this.fileUploadthree);
            formData.append('Image4IForm', this.fileUploadfour);
            this.apiService.post('FomCustomer/UploadCustomerFiles', formData)
              .subscribe(res => {
                this.utilService.OkMessage();
                this.dialogRef.close(true);
              },
                error => {
                  console.error(error);
                  this.utilService.ShowApiErrorMessage(error);
                });
          } else if (res) {
            this.utilService.OkMessage();
            // this.reset();
            this.dialogRef.close(true);
          } else {
            this.notifyService.showWarning("error");
          }
          
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
    this.apiService.get('FomCustomer', this.id).subscribe(res => {
      if (res) {
     

        this.form.patchValue(res);
        this.file1Url = res.custUDF1;
        this.file2Url = res.custUDF2;
        this.file3Url = res.custUDF3;
        this.imageUrl = res.imageUrl;


        this.apiService.getall('FomCustomer/getStateCountrybyCityCode/' + res['custCityCode1']).subscribe(res => {
          this.form.patchValue({ 'stateone': res['stateName'], 'countryone': res['countryName'] });
        });

        this.apiService.getall('FomCustomer/getStateCountrybyCityCode/' + res['custCityCode2']).subscribe(res => {
          this.form.patchValue({ 'statetwo': res['stateName'], 'countrytwo': res['countryName'] });
        });

        this.form.patchValue({ 'id': 0 });

      }
    });
  }
  loadCities() {
    this.apiService.getall('FomCustomer/getCitiesSelectList').subscribe(res => {
      this.cityList1 = res;
      this.cityList2 = res;
    });

    this.apiService.getPagination('fomCustomerCategory', this.utilService.getQueryString(0, 1000, '', '')).subscribe(res => {
      if (res) {
        this.CustCategoryCodeList = res['items'];
        if (this.id > 0)
          this.setEditForm();
      }

    });

    //this.apiService.getall('userType/getUserTypeSelectList').subscribe(res => {
    //  this.userTypes = res;
    //});
  }
  canAutoGenerateCustCode() {
    this.apiService.getall('customerMaster/canAutoGenerateCustCode').subscribe(res => {
      this.canAutoGenCustCode = res;
    });
  }
  getStateCountrybyCityCode1(event: any) {
    const id = event.target.value;
    this.apiService.getall('FomCustomer/getStateCountrybyCityCode/' + id).subscribe(res => {
      this.form.patchValue({ 'custCityCode1': res['cityCode'], 'stateone': res['stateName'], 'countryone': res['countryName'] });
    });
  }
  getStateCountrybyCityCode2(event: any) {
    const id = event.target.value;
    this.apiService.getall('FomCustomer/getStateCountrybyCityCode/' + id).subscribe(res => {
      this.form.patchValue({ 'custCityCode2': res['cityCode'], 'statetwo': res['stateName'], 'countrytwo': res['countryName'] });
    });
  }
  reset() {
    this.form.controls['custName'].setValue('');
  }
  filterCategoryCodes(val: string): Observable<Array<CustomSelectListItem>> {
    return this.apiService.getall(`customerCategory/getSelectCustomerCategoryCodeList?search=${val}`)
      .pipe(
        map(response => {
          const res = response as Array<CustomSelectListItem>;
          this.isDataLoading = false;
          /* if (res.length == 0) { this.categoryCodeControl.setValue('');}*/
          return res;
        })
      )
  }
  filterSalesTermsCodes(val: string): Observable<Array<CustomSelectListItem>> {
    return this.apiService.getall(`salesTermsCode/getSelectSalesTermsCodeList?search=${val}`)
      .pipe(
        map(response => {
          const res = response as Array<CustomSelectListItem>;
          this.isDataLoading = false;
          return res;
        })
      )
  }

  filterAccountCodes(val: string): Observable<Array<CustomSelectListItem>> {
    return this.apiService.getall(`mainAccounts/getSelectAccountCategoryList?search=${val}`)
      .pipe(
        map(response => {
          const res = response as Array<CustomSelectListItem>;
          this.isDataLoading = false;
          return res;
        })
      )
  }


  //uploadFile() {

  //  const formData = new FormData();
  //  formData.append("id", this.requestData.requestNumber);
  //  formData.append("requestType", this.requestData.requestType);
  //  formData.append("requestSubType", this.requestData.requestSubType);
  //  formData.append("fileIForm", this.requestData.fileIForm);
  //  formData.append("fileName", this.authService.ApiEndPoint().replace("api", "") + 'Uploads/Adendums/');

  //  if (this.requestData.fileIForm != null) {
  //    this.apiService.post('pvAllRequests/fileUpload', formData).subscribe(res => {
  //      if (res) {

  //        this.utilService.OkMessage();

  //        this.dialogRef.close(true);

  //      }
  //    },
  //      error => {
  //        console.error(error);
  //        this.utilService.ShowApiErrorMessage(error);
  //      });

  //  }
  //  else {
  //    this.utilService.ShowApiErrorMessage(this.translate.instant("Select_File"))
  //  }
  //}


  //optionSelected(event: MatAutocompleteSelectedEvent) {
  //  console.log(event.option.value);
  //}

  validate(event: MatAutocompleteSelectedEvent, control: string, action: string) {
    let value: string = '';
    if (action == "change") {
      value = this.utilService.removeSqueres(event.option.value);
    }



    switch (control) {
      case "categoryCodeControl":

        this.apiService.getall('CustomerCategory/getCustCatByCustCatCode/' + value).subscribe(res => {
          if (res != null)
            this.form.value['custCatCode'] = res['custCatCode'];
          else {
            this.form.value['custCatCode'] = '';
            this.categoryCodeControl.setValue('');
          }
        });
        break;
      case "salesTermsCodeControl":

        this.apiService.getall('SalesTermsCode/getSalesTermsByTermsCode/' + value).subscribe(res => {
          if (res != null) {
            this.form.value['salesTermsCode'] = res['salesTermsCode'];
          }
          else {
            this.form.value['salesTermsCode'] = '';
            this.salesTermsCodeControl.setValue('');
          }
        });
        break;


      case "custARAcControl":

        this.apiService.getall('MainAccounts/getAccountByAccountCode/' + value).subscribe(res => {
          if (res != null) {
            this.form.value['custARAc'] = res['finAcCode'];
          }
          else {
            this.form.value['custARAc'] = '';
            this.custARAcControl.setValue('');
          }
        });
        break;
      case "custDefExpAcCodeControl":

        this.apiService.getall('MainAccounts/getAccountByAccountCode/' + value).subscribe(res => {

          if (res != null) {
            this.form.value['custDefExpAcCode'] = res['finAcCode'];
          }
          else {
            this.form.value['custDefExpAcCode'] = '';
            this.custDefExpAcCodeControl.setValue('');
          }
        });
        break;
      case "custARAdjAcCodeControl":

        this.apiService.getall('MainAccounts/getAccountByAccountCode/' + value).subscribe(res => {
          if (res != null)
            this.form.value['custARAdjAcCode'] = res['finAcCode'];
          else {
            this.form.value['custARAdjAcCode'] = '';
            this.custARAdjAcCodeControl.setValue('');
          }
        });
        break;
      case "custArAcCodeControl":

        this.apiService.getall('MainAccounts/getAccountByAccountCode/' + value).subscribe(res => {
          if (res != null)
            this.form.value['custArAcCode'] = res['finAcCode'];
          else {
            this.form.value['custArAcCode'] = '';
            this.custArAcCodeControl.setValue('');
          }
        });
        break;
      case "custARDiscAcCodeControl":

        this.apiService.getall('MainAccounts/getAccountByAccountCode/' + value).subscribe(res => {
          if (res != null)
            this.form.value['custARDiscAcCode'] = res['finAcCode'];
          else {
            this.form.value['custARDiscAcCode'] = '';
            this.custARDiscAcCodeControl.setValue('');
          }
        });
        break;
      default: ;

    }

  }


  // private openDialogManage<T>(id: number = 0, dbops: DBOperation, modalTitle: string = '', modalBtnTitle: string = '', component: T, moduleFile: MultiFileUploadDto = { module: '00', action: '00act' }, width: number = 100) {
  //   let dialogRef = this.utilService.openDialogCongif(this.dialog, component, width);
  //   (dialogRef.componentInstance as any).dbops = dbops;
  //   (dialogRef.componentInstance as any).modalTitle = modalTitle;
  //   (dialogRef.componentInstance as any).id = id;
  //   (dialogRef.componentInstance as any).moduleFile = moduleFile;

  //   dialogRef.afterClosed().subscribe(res => {
  //     if (res && res === true)
  //       this.authService.SetApiEndPoint(data.salesapiurl);
  //     //this.initialLoading();
  //   });
  // }

//   uploadFile() {
//     this.openDialogManage(0, DBOperation.update, this.translate.instant('Create_New_Sales_Invoice'), '', FileUploadComponent, { module: 'SND', action: 'customer' });
//   }

// }
}
