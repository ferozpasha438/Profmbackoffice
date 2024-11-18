import { Component, OnInit, ViewChild, Inject } from '@angular/core';
//import { FormGroup, FormBuilder, Validators, FormControl, ValidatorFn, AbstractControl } from '@angular/forms';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { AuthorizeService } from 'src/app/api-authorization/AuthorizeService';
import { ApiService } from 'src/app/services/api.service';
import { startWith, debounceTime, distinctUntilChanged, switchMap, map, filter } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { NotificationService } from 'src/app/services/notification.service';
import { DBOperation } from 'src/app/services/utility.constants';
import { UtilityService } from 'src/app/services/utility.service';
import { ParentHrmAdminComponent } from 'src/app/sharedcomponent/Parenthrmadmin.component';
import { ValidationService } from 'src/app/sharedcomponent/ValidationService';
import { CustomSelectListItem } from '../../../models/MenuItemListDto';
import { ParentFomMgtComponent } from '../../../sharedcomponent/parentfommgt.component';
import { ItemsList } from '@ng-select/ng-select/lib/items-list';
/*import { ActivityDialogComponent } from '../Sharedpages/activitydialog.component';*/

@Component({
  selector: 'app-addupdatecustomercontract',
  templateUrl: './addupdatecustomercontract.component.html',
  styles: [
  ]
})
export class AddupdatecustomercontractComponent extends ParentFomMgtComponent implements OnInit {
  modalTitle!: string;
  modalBtnTitle!: string;
  dbops!: DBOperation;
  form!: FormGroup;
  id: number = 0;
  isReadOnly: boolean = false;
  isDataLoading: boolean = false;
  filteredContApprovAuthCodes: Observable<Array<CustomSelectListItem>>;
  filteredContProjectManagerCodes: Observable<Array<CustomSelectListItem>>;
  filteredContSuperVisorCodes: Observable<Array<CustomSelectListItem>>;
  categoryCodeControl = new FormControl('', Validators.required);
  catProjectManagerControl = new FormControl('', Validators.required);
  catSuperVisorCodeControl = new FormControl('', Validators.required);
  catApproveAuthControl = new FormControl('', Validators.required);
  DepartmentCodeList: Array<CustomSelectListItem> = [];
  DepartmentSoftCodeList: Array<CustomSelectListItem> = [];
  DepartmentSpecializedCodeList: Array<CustomSelectListItem> = [];
  CustomerCodeList: Array<CustomSelectListItem> = [];
  SiteCodeList: Array<CustomSelectListItem> = [];
  selectedCars = [2];
  productList: Array<CustomSelectListItem> = [];
  productId: number = 0;
  isModalOpen = false;

  disciplines: Array<any> = [];
  deptActCodes: string = '';
  cars = [
        { id: 1, name: 'Electrical Department'},
        { id: 2, name: 'Janotorial Department' },
        { id: 3, name: 'Plumbing Department' },
        { id: 4, name: 'A/C Department' },
        { id: 5, name: 'Refrigator Service Department' },

  ];
  //disciplines = [
  //  {
  //    name: 'Carpentry',
  //    activities: [
  //      { name: 'Door Maintenance', selected: true },
  //      { name: 'Partition Maintenance', selected: false },
  //      { name: 'Desk Maintenance', selected: true }
  //    ]
  //  },
  //  {
  //    name: 'Hospitality',
  //    activities: [
  //      { name: 'Welcome Guest', selected: true },
  //      { name: 'Pantry Management', selected: true },
  //      { name: 'Bank Visits', selected: false }
  //    ]
  //  }
  //];


  fileUploadone!: File;
  fileUploadtwo!: File;
  fileUploadthree!: File;
  file1Url: string = '';
  file2Url: string = '';
  file3Url: string = '';

  constructor(private fb: FormBuilder, private apiService: ApiService, public dialog: MatDialog, 
    private authService: AuthorizeService, private utilService: UtilityService, public dialogRef: MatDialogRef<AddupdatecustomercontractComponent>,
    private notifyService: NotificationService, private validationService: ValidationService) {
    super(authService)

      this.filteredContApprovAuthCodes = this.catApproveAuthControl.valueChanges.pipe(
      startWith(''),
      debounceTime(utilService.autoDelay()),
      distinctUntilChanged(),
      switchMap((val: string) => {
        if (val.trim() !== '')
          this.isDataLoading = true;
        return this.filterContApprovAuthCodes(val || '')
      })
    );


    this.filteredContSuperVisorCodes = this.catSuperVisorCodeControl.valueChanges.pipe(
      startWith(''),
      debounceTime(utilService.autoDelay()),
      distinctUntilChanged(),
      switchMap((val: string) => {
        if (val.trim() !== '')
          this.isDataLoading = true;
        return this.filterContSuperVisorCodes(val || '')
      })
    );

    this.filteredContProjectManagerCodes = this.catProjectManagerControl.valueChanges.pipe(
      startWith(''),
      debounceTime(utilService.autoDelay()),
      distinctUntilChanged(),
      switchMap((val: string) => {
        if (val.trim() !== '')
          this.isDataLoading = true;
        return this.filterContProjectManagerCodes(val || '')
      })
    );
 
  };

  
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

  ngOnInit(): void {
    //this.form = this.fb.group({
    //  startDate: ['', Validators.required],
    //  endDate: ['', Validators.required]
    //}, { validator: this.dateRangeValidator('startDate', 'endDate') });
   
    this.loadData();
    this.setForm();
    if (this.id > 0)
      this.setEditForm();
  }


  setForm() {
    this.form = this.fb.group(
      {
        'contractCode': ['', [Validators.required, Validators.maxLength(20)]],
        'custCode': ['', Validators.required],
        'custSiteCode': ['', Validators.required],
        'contStartDate': ['', Validators.required],
        'contEndDate': ['', Validators.required ],
        'contDeptCode': [[], Validators.required],
        'contDeptSoftCode': [[], Validators.required],
        'contDeptSpecialCode': [[], Validators.required],
        'contProjManager': ['', Validators.required],
        'contProjSupervisor': ['', Validators.required],
        'remarks': ['', Validators.required],
        'contApprAuthorities': ['', Validators.required],
        'isAppreoved': [false],
        'isSheduleRequired':[false],
        'approvedDate': [''],
        'isActive': [false],

      }
    );
    this.isReadOnly = false;
  }
  setEditForm() {
      this.apiService.get('FomCustomerContract', this.id).subscribe(res => {
      this.form.value['id'] = this.id;
      console.log(res);
          if (res) {
              this.catApproveAuthControl.setValue(res['contApprAuthorities']);
              this.catProjectManagerControl.setValue(res['contProjManager']);
              this.catSuperVisorCodeControl.setValue(res['contProjSupervisor']);
                this.isReadOnly = true;
                const filterArray = res['contDeptCode'].split(',');
                var deptdata = this.DepartmentCodeList as Array<any>;
                res['contDeptCode'] = deptdata.filter(item => filterArray.includes(item.deptCode));
                var deptSoftData = this.DepartmentSoftCodeList as Array<any>;
            res['contDeptSoftCode'] = deptSoftData.filter(item => filterArray.includes(item.deptCode));
            var deptSpecialData = this.DepartmentSpecializedCodeList as Array<any>;
            res['contDeptSpecialCode'] = deptSpecialData.filter(item => filterArray.includes(item.deptCode));

            this.form.patchValue(res);
            this.file1Url = res.file1;
            this.file2Url = res.file2;
            this.file3Url = res.file3;
      }
    });
  }

  onCustSiteCode(custCode: string) {
    if (custCode != null) {

      this.apiService.getall(`fomCustomerContract/GetSelectCustomerSiteByCustCode?custCode=${custCode}`).subscribe(res => {
        if (res) {
          this.SiteCodeList = res;
        }
      })
    }
  }
  //After Deployment
  //onDeptCodeChange(selectedCodes: any) {
  //  //const deptCodes1 = selectedCodes.map((dept: any) => dept.id); // Extract the department codes

  //  this.deptActCodes = selectedCodes.map((dept: any) => dept.deptCode);

  //  //this.apiService.getall(`FomCustomerContract/GetScheduleById/${deptCode}/${contractCode}`).subscribe(res => {
  //  //  console.log(res);

  //  //if (deptCodes != null) {
  //  //  const codes = deptCodes.join(',');

  //  //  this.apiService.getall(`fomCustomerContract/GetActivitiesByDeptCodes/${codes}`)
  //  //    .subscribe({
  //  //      next: (res) => {
  //  //        console.log('API Response:', res); // Log to see what the response contains
  //  //        if (Array.isArray(res)) {
  //  //          this.SiteCodeList = res; // Only assign if the response is an array
  //  //        } else {
  //  //          console.error('Unexpected API Response:', res);
  //  //        }
  //  //      },
  //  //      error: (err) => {
  //  //        console.error('API Error:', err); // Log errors to debug failed requests
  //  //      }
  //  //    });
  //  //}

  //}




  // Flag to keep track of "Select All" state
  selectAllChecked = false;

  // Method to toggle "Select All"
  onDeptCodeChange(event: any) {
    if (event && event.includes('Select All')) {
      this.selectAllChecked = !this.selectAllChecked;
      if (this.selectAllChecked) {
        // Select all department codes
        const allCodes = this.DepartmentCodeList.map(dept => dept);
        this.form.controls['contDeptCode'].setValue(allCodes);
      } else {
        // Deselect all department codes
        this.form.controls['contDeptCode'].setValue([]);
      }
    }

    // Call getSelectedDeptCodes to get the updated selected codes
    const selectedCodes = this.getSelectedDeptCodes();
    console.log('Selected Department Codes:', selectedCodes);

    // Perform additional actions with selectedCodes if needed
  }






  
  //onDeptCodeChange(event: any) {
  //  const selectedCodes = this.getSelectedDeptCodes();
  //  // Perform necessary actions with the selected codes
  //  console.log('Selected Department Codes on change:', selectedCodes);
  //}

  
  onDeptSoftCodeChange(event: any) {
    if (event && event.includes('Select All')) {
      this.selectAllChecked = !this.selectAllChecked;
      if (this.selectAllChecked) {
        // Select all department codes
      //  const codes = selectedCodes.map((dept: any) => dept.deptCode);
        const allCodes = this.DepartmentSoftCodeList.map((dept: any) => dept.deptCode);
        this.form.controls['contDeptSoftCode'].setValue(allCodes);
      } else {
        // Deselect all department codes
        this.form.controls['contDeptSoftCode'].setValue([]);
      }
    }

    const selectedCodes = this.getSelectedDeptSoftCodes();
    // Perform necessary actions with the selected codes
    console.log('Selected Department Codes on change:', selectedCodes);
  }

  onDeptSpecialCodeChange(event: any) {
    if (event && event.includes('Select All')) {
      this.selectAllChecked = !this.selectAllChecked;
      if (this.selectAllChecked) {
        // Select all department codes
        //  const codes = selectedCodes.map((dept: any) => dept.deptCode);
        const allCodes = this.DepartmentSpecializedCodeList.map((dept: any) => dept.deptCode);
        this.form.controls['contDeptSpecialCode'].setValue(allCodes);
      } else {
        // Deselect all department codes
        this.form.controls['contDeptSpecialCode'].setValue([]);
      }
    }

    const selectedCodes = this.getSelectedDeptSpecialCodes();
    // Perform necessary actions with the selected codes
    console.log('Selected Department Codes on change:', selectedCodes);
  }

  getSelectedDeptCodes() {
    return this.form.get('contDeptCode')?.value || [];
  }


  getSelectedDeptSoftCodes() {
    return this.form.get('contDeptSoftCode')?.value || [];
  }

  getSelectedDeptSpecialCodes() {
    return this.form.get('contDeptSpecialCode')?.value || [];
  }

  openModal() {
   
    const selectedCodes = this.getSelectedDeptCodes();
    if (selectedCodes.length === 0) {
      alert('Please select at least one Department Code');
      return;
    }

    const codes  = selectedCodes.map((dept: any) => dept.deptCode);
   // const codes = this.deptActCodes;
    this.apiService.getall(`fomCustomerContract/GetActivitiesByDeptCodes/${codes}`).subscribe(
      (data) => {
        this.disciplines = data;
        this.isModalOpen = true;
      },
      (error) => {
        console.error('Error fetching activities:', error);
      }
    );
  }



  openSoftModal() {
    const selectedCodes = this.getSelectedDeptSoftCodes();
    if (selectedCodes.length === 0) {
      alert('Please select at least one Department Code');
      return;
    }

    const codes = selectedCodes.map((dept: any) => dept.deptCode);
    // const codes = this.deptActCodes;
    this.apiService.getall(`fomCustomerContract/GetActivitiesByDeptCodes/${codes}`).subscribe(
      (data) => {
        this.disciplines = data;
        this.isModalOpen = true;
      },
      (error) => {
        console.error('Error fetching activities:', error);
      }
    );
  }


  openSpecialModal() {
    const selectedCodes = this.getSelectedDeptSpecialCodes();
    if (selectedCodes.length === 0) {
      alert('Please select at least one Department Code');
      return;
    }

    const codes = selectedCodes.map((dept: any) => dept.deptCode);
    // const codes = this.deptActCodes;
    this.apiService.getall(`fomCustomerContract/GetActivitiesByDeptCodes/${codes}`).subscribe(
      (data) => {
        this.disciplines = data;
        this.isModalOpen = true;
      },
      (error) => {
        console.error('Error fetching activities:', error);
      }
    );
  }


  closeModel() {
    this.dialogRef.close();
  }


 
    



  loadData() {
    //this.apiService.getPagination('fomCustomerSite/getCustomerSitesPagedList', this.utilService.getQueryString(0, 1000, '', '')).subscribe(res => {
    //  if (res)
    //    this.SiteCodeList = res['items'];
    //});


    this.apiService.getall('fomCustomerContract/GetSelectCustomerSiteList').subscribe(res => {
      if (res) {
        this.SiteCodeList = res;
      }
    })
    

    this.apiService.getPagination('fomDiscipline', this.utilService.getQueryString(0, 1000, '', '')).subscribe(res => {
      if (res) {
        var deptdata = res['items'] as Array<any>;
        this.DepartmentCodeList = deptdata.filter(item => item.deptServType == 'Hard Services');
        this.DepartmentSoftCodeList = deptdata.filter(item => item.deptServType == 'Soft Services');
        this.DepartmentSpecializedCodeList = deptdata.filter(item => item.deptServType == 'Special Services');
        if (this.id > 0)
          this.setEditForm();
      }

    });

    this.apiService.getPagination('FomCustomer', this.utilService.getQueryString(0, 1000, '', '')).subscribe(res => {
      if (res)
        this.CustomerCodeList = res['items'];
    });

  }

    submit() {
        console.log(this.id);
        this.form.value['id'] = this.id;
        let contApprAuthorities = this.catApproveAuthControl.value as string;
        let contProjManager = this.catProjectManagerControl.value as string;
        let contProjSupervisor = this.catSuperVisorCodeControl.value as string;
        if (this.utilService.hasValue(contApprAuthorities)) {
            this.form.value['contApprAuthorities'] = contApprAuthorities;
            this.form.controls['contApprAuthorities'].setValue(this.utilService.removeSqueres(contApprAuthorities));
            this.catApproveAuthControl.setValue(contApprAuthorities);
        }
        else {
            console.log("contApprAuthorities-");
        }
        if (this.utilService.hasValue(contProjManager)) {
            this.form.value['contProjManager'] = contProjManager;
            this.form.controls['contProjManager'].setValue(this.utilService.removeSqueres(contProjManager));
            this.catProjectManagerControl.setValue(contProjManager);
        }
        else {
            console.log("contProjManager-");
        }
        if (this.utilService.hasValue(contProjSupervisor)) {
            this.form.value['contProjSupervisor'] = contProjSupervisor;
            this.form.controls['contProjSupervisor'].setValue(this.utilService.removeSqueres(contProjSupervisor));
            this.catSuperVisorCodeControl.setValue(contProjSupervisor);
        }
        else {
            console.log("contProjSupervisor-");
        }
      console.log(this.form);

    if (this.form.valid) {
      if (this.id > 0)
            this.form.value['id'] = this.id;
        
      var deptdata = this.form.value['contDeptCode'] as Array<any>;
      var deptSoftData = this.form.value['contDeptSoftCode'] as Array<any>;
      var deptSpecialData = this.form.value['contDeptSpecialCode'] as Array<any>;
      deptdata = [...deptdata, ...deptSoftData];

      deptdata = [...deptdata, ...deptSpecialData];
      this.form.value['contDeptCode'] = deptdata.map(item => item.deptCode);
      this.form.value['contStartDate'] = this.utilService.selectedDateTime(this.form.value['contStartDate']);
      this.form.value['contEndDate'] = this.utilService.selectedDateTime(this.form.value['contEndDate']);
      this.form.value['approvedDate'] = this.utilService.selectedDateTime(this.form.value['approvedDate']);
      this.apiService.post('FomCustomerContract', this.form.value)
        .subscribe(res => {
          if (res && this.fileUploadone != null && this.fileUploadone != undefined) {
            const custContractRes = res as any;
            const formData = new FormData();
            formData.append("id", custContractRes.id.toString());
            formData.append("WebRoot", this.authService.ApiEndPoint().replace("api", "") + 'CustomerContractfiles/');
            formData.append("Image1IForm", this.fileUploadone);
            formData.append("Image2IForm", this.fileUploadtwo);
            formData.append("Image3IForm", this.fileUploadthree);
            this.apiService.post('FomCustomerContract/UploadCustomerContractFiles', formData)
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
            this.dialogRef.close(true);
            this.reset();
          } else {
            this.notifyService.showWarning("error");
          }
        },
          error => {
            this.utilService.ShowApiErrorMessage(error);
          });
    }
    else
      this.utilService.FillUpFields();
  }

  reset() {
    this.form.controls['contractCode'].setValue('');
    this.form.controls['custCode'].setValue('');
    this.form.controls['custSiteCode'].setValue('');
    this.form.controls['contStartDate'].setValue('');
    this.form.controls['contEndDate'].setValue(''); 
    this.form.controls['contDeptCode'].setValue('');
    this.form.controls['contDeptSoftCode'].setValue('');
    this.form.controls['contDeptSpecialCode'].setValue('');
    this.form.controls['contProjManager'].setValue('');
    this.form.controls['contProjSupervisor'].setValue('');
    this.form.controls['remarks'].setValue('');
    this.form.controls['contApprAuthorities'].setValue('');
    //this.form.controls['IsAppreoved'].setValue(false);
    //this.form.controls['IsSheduleRequired'].setValue(false);
    this.form.controls['approvedDate'].setValue('');
    //this.form.controls['isActive'].setValue(false);

  }


  filterContSuperVisorCodes(val: string): Observable<Array<CustomSelectListItem>> {
    return this.apiService.getall(`fomCustomerContract/getSelectAuthResourcesList?search=${val}`)
      .pipe(
        map(response => {
          const res = response as Array<CustomSelectListItem>;
          this.isDataLoading = false;
            /* if (res.length == 0) { this.categoryCodeControl.setValue('');}*/
            const filteredRes = res.filter(option => option.value === 'SUPERVISOR');

          return filteredRes;

          //return filteredRes;
        })
      );
  }

  filterContProjectManagerCodes(val: string): Observable<Array<CustomSelectListItem>> {
    return this.apiService.getall(`fomCustomerContract/getSelectAuthResourcesList?search=${val}`)
      .pipe(
        map(response => {
          const res = response as Array<CustomSelectListItem>;
          this.isDataLoading = false;
          /* if (res.length == 0) { this.categoryCodeControl.setValue('');}*/

            const filteredRes = res.filter(option => option.value === 'PROJECT MANAGER');

          return filteredRes;

          //  return res;
        })
      );
  }

  
  filterContApprovAuthCodes(val: string): Observable<Array<CustomSelectListItem>> {
    return this.apiService.getall(`fomCustomerContract/getSelectAuthResourcesList?search=${val}`)
      .pipe(
        map(response => {
          const res = response as Array<CustomSelectListItem>;
          this.isDataLoading = false;
          /* if (res.length == 0) { this.categoryCodeControl.setValue('');}*/
          return res;
        })
      );
  }


  //openActivity(): void {
  //  alert('hi');
  //  console.log("Activities button clicked!");
  //  // Add your dialog opening or other logic here
  //}





  //openActivity(): void {
  //  alert('hiiii');
  //  const dialogRef = this.dialog.open(ActivityDialogComponent, {
  //    //width: '400px',
  //    //testdata: this.testdata

  //  });
   
  //  dialogRef.afterClosed().subscribe(result => {
  //    if (result) {
  //      //this.testdata = result;  // Handle the returned data if needed
  //      //console.log('Updated activities:', this.testdata);
  //    }
  //  });
  //}

  toggleDisabled() {
    const car: any = this.cars[1];
    car.disabled = !car.disabled;
  }

  


//  public dateRangeValidator(): ValidatorFn {
//    return (control: AbstractControl): { [key: string]: any } | null => {

//      const startDate = this.form.controls['contStartDate'].value;
//      const endDate = this.form.controls['contEndDate'].value;

//    if (startDate && endDate && new Date(startDate) > new Date(endDate)) {
//      return { 'dateRange': true };
//    }
//    return null;
//  };
//}
  //openModal() {
  //  this.isModalOpen = true;

  //  alert('hi ModalOpen');
  // // console.log("Activities button clicked!");
  //}

  
  

  saveActivities() {
    console.log('Selected activities:', this.disciplines);
    // Perform the save logic here, such as making an API call
    this.closeModal();
  }


  // Function to close the modal
  closeModal() {
    this.isModalOpen = false;
  }
}


