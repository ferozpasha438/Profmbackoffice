import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { Observable } from 'rxjs';
//import {  startWith, debounceTime, distinctUntilChanged, switchMap, map } from 'rxjs/operators';
import { HttpClientModule } from '@angular/common/http';
import { AuthorizeService } from 'src/app/api-authorization/AuthorizeService';
import { CustomSelectListItem } from 'src/app/models/MenuItemListDto';
import { ApiService } from 'src/app/services/api.service';
import { NotificationService } from 'src/app/services/notification.service';
import { DBOperation } from 'src/app/services/utility.constants';
import { UtilityService } from 'src/app/services/utility.service';
import { ValidationService } from 'src/app/sharedcomponent/ValidationService';
import { ParentOptMgtComponent } from 'src/app/sharedcomponent/parentoptmgt.component';
import { ParentFomMgtComponent } from '../../../sharedcomponent/parentfommgt.component';

interface CustomerLogin {
  userClientLoginCode: string;
  custCode: string;
  custName: string;
  password: string;
  isActive: boolean;
}

interface Customer {
  custCode: string;
  // Add other properties if needed
}


@Component({
  selector: 'app-addupdatemultilogin',
  templateUrl: './addupdatemultilogin.component.html',
  styleUrls: ['./addupdatemultilogin.component.css']
 
})
export class AddupdatemultiloginComponent extends ParentFomMgtComponent implements OnInit {
  readonly: string = "";
  modalTitle!: string;
  modalBtnTitle!: string;
  dbops!: DBOperation;
  form!: FormGroup;
  id: number = 0;
  //customerCode!: string;
  custCityCode!: string;
  CustomerCodeList: Array<CustomSelectListItem> = [];
  cityList1: Array<CustomSelectListItem> = [];
  custCodeControl = new FormControl('', Validators.required);
  //filteredCustCodes: Observable<Array<CustomSelectListItem>>;
  isDataLoading: boolean = false;
  isChildCustomer: boolean = false;
  loginCode!: string;

  custCode!: string;
  customerCode!: string;
  custName!: string;
  password!: string;
  isActive!: boolean;
  existingLogins: CustomerLogin[] = [];

  isEditMode: boolean = false;



  newLogin: CustomerLogin = {
    userClientLoginCode: '',
    custCode: this.custCode,
    custName: '',
    password: '',
    isActive: true
  };




  constructor(private fb: FormBuilder, private http: HttpClient, private router: Router, private apiService: ApiService,
    private authService: AuthorizeService, private utilService: UtilityService, private translate: TranslateService,
    private notifyService: NotificationService, private validationService: ValidationService, public dialogRef: MatDialogRef<AddupdatemultiloginComponent>) {
    super(authService);
 

  }

  ngOnInit(): void {
    
    //this.existingLogins = [
    //  {
    //    loginCode: 'SHAMSHIR',
    //    customerCode: 'CUST005',
    //    name: 'Shamshir Ali',
    //    password: '1234',
    //    isActive: true
    //  },
    //  {
    //    loginCode: 'CUST005',
    //    customerCode: 'CUST005',
    //    name: 'Customer Cust005',
    //    password: '1234',
    //    isActive: true
    //  }
    //];

    this.initializeData();

   // this.getLoginData('CUST0005');
  }



  initializeData() {
    console.log('Initializing data...');
    // Load data or set up initial state
    const custCode = (this.customerCode as any)?.custCode;
    this.getLoginData(custCode);

  }



  getLoginData(customerCode: string) {
    if (customerCode) {
      this.custCode = customerCode;

      // Pass customerCode as a route parameter, not a query parameter
      this.apiService.getall(`fomCustomer/getClientsByCustomerCode/${this.custCode}`)
        .subscribe({
          next: (res) => {
            if (res) {
              this.existingLogins = res;
              this.resetNewLogin();
              console.log('Data fetched:', res);
            }
          },
          error: (err) => {
            console.error('Error fetching data:', err);
          }
        });
    }
  }



  //getLoginData(custCode: string): void {
  //  this.apiService.getall(`fomCustomer/GetClientsByCustomerCode?custCode=${custCode}`).subscribe({
  //    next: (data) => {
  //      this.existingLogins = data;
  //      console.log('Data fetched:', data);
  //    },
  //    error: (err) => {
  //      console.error('Error fetching data:', err);
  //    }
  //  });
  //}

  //addLogin() {
  //  if (
  //    this.newLogin.userClientLoginCode &&
  //    this.newLogin.custCode &&
  //    this.newLogin.custName &&
  //    this.newLogin.password
  //  ) {
  //    this.existingLogins.push({ ...this.newLogin });
  //    this.resetNewLogin();
  //  } else {
  //    alert('Please fill all fields!');
  //  }
  //}

  //addLogin() {
  //  if (this.isEditMode == false) {
  //    if (
  //      this.newLogin.userClientLoginCode &&
  //      this.newLogin.custCode &&
  //      this.newLogin.custName &&
  //      this.newLogin.password
  //    ) {
  //      // Check for duplicate userClientLoginCode
  //      const isDuplicate = this.existingLogins.some(
  //        (login) => login.userClientLoginCode === this.newLogin.userClientLoginCode
  //      );

  //      if (isDuplicate) {
  //        alert('User Client Login Code already exists!');
  //        return;
  //      }

  //      this.existingLogins.push({ ...this.newLogin });
  //      this.resetNewLogin();
  //    } else {
  //      alert('Please fill all fields!');
  //    }
  //  } else {
  //      if (
  //        this.newLogin.userClientLoginCode &&
  //        this.newLogin.custCode &&
  //        this.newLogin.custName &&
  //        this.newLogin.password
  //        ) {
  //          this.existingLogins.push({ ...this.newLogin });
  //          this.resetNewLogin();
  //        } else {
  //        alert('Please fill all fields!');
  //  }

  //  }
  //}
  addLogin() {
    if (!this.newLogin.userClientLoginCode || !this.newLogin.custCode || !this.newLogin.custName || !this.newLogin.password) {
      alert('Please fill all fields!');
      return;
    }

    if (this.isEditMode === false) {
      // Add new record (check for duplicates)
      const isDuplicate = this.existingLogins.some(
        (login) => login.userClientLoginCode === this.newLogin.userClientLoginCode
      );

      if (isDuplicate) {
        alert('User Client Login Code already exists!');
        return;
      }

      this.existingLogins.push({ ...this.newLogin });
      alert('Record added successfully!');
    } else {
      // Update existing record (based on userClientLoginCode)
      const existingIndex = this.existingLogins.findIndex(
        (login) => login.userClientLoginCode === this.newLogin.userClientLoginCode
      );

      if (existingIndex !== -1) {
        this.existingLogins[existingIndex].custCode = this.newLogin.custCode;
        this.existingLogins[existingIndex].custName = this.newLogin.custName;
        this.existingLogins[existingIndex].password = this.newLogin.password;
        alert('Record updated successfully!');
      }
    }

    this.resetNewLogin(); // Reset the form after add or update
  }



  resetNewLogin() {
    this.newLogin = {
      userClientLoginCode: '',
      custCode: this.custCode,
      custName: '',
      password: '',
      isActive: true
    };
    this.isEditMode = false;
  }


  //deleteLogin(loginCode: string) {
  //  this.existingLogins = this.existingLogins.filter(
  //    (login) => login.userClientLoginCode !== loginCode
  //  );
  //}


  deleteLogin(id: number) {
    if (confirm('Are you sure you want to delete this customer login?')) {
      this.apiService.delete('fomCustomer/DeleteMultiLoginCustomer', id).subscribe({
        next: () => {
          alert('Record deleted successfully!');
          this.resetNewLogin();
          this.initializeData();
        },
        error: (err) => {
          console.error('Delete failed:', err);
          alert('Failed to delete record.');
        }
      });
    }
  }


  editLogin(login: CustomerLogin) {
    this.newLogin = { ...login };
    this.isEditMode = true; // Set to true when editing
   // this.deleteLogin(login.userClientLoginCode);
  }



//  if(this.id > 0)
//this.form.value['id'] = this.id;

//this.apiService.post('fomCustomerCategory', this.form.value)
//  .subscribe(res => {
//    this.utilService.OkMessage();
//    this.reset();
//    this.dialogRef.close(true);
//  },
//    error => {
//      console.error(error);
//      this.utilService.ShowApiErrorMessage(error);
//    });
//    }
//    else
//this.utilService.FillUpFields();



   //Save login using API
  saveLogin() {
    const dataToSave = this.existingLogins;
    this.apiService.post('FomCustomer/CreateUpdateMultiLoginCustomer', dataToSave).subscribe(
      (res) => {
        alert('Login saved successfully!');
        this.resetNewLogin();
        this.initializeData();
      },
      (err) => {
        console.error('Error saving login:', err);
        alert('Failed to save login.');
      }
    );
  }




  // Cancel (Reset Form)
  cancelLogin() {
    this.resetNewLogin();
  }

  
  cancel() {
    this.dialogRef.close();
  }

  closeModel() {
    this.dialogRef.close();
  }
 


  
  


 

  
  

  

  

  

}

