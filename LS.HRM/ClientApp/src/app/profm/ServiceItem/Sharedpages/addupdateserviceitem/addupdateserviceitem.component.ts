import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { AuthorizeService } from 'src/app/api-authorization/AuthorizeService';
import { ApiService } from 'src/app/services/api.service';
import { NotificationService } from 'src/app/services/notification.service';
import { DBOperation } from 'src/app/services/utility.constants';
import { UtilityService } from 'src/app/services/utility.service';
import { ValidationService } from 'src/app/sharedcomponent/ValidationService';
import { ParentFomMgtComponent } from 'src/app/sharedcomponent/parentfommgt.component';

@Component({
  selector: 'app-addupdateserviceitem',
  templateUrl: './addupdateserviceitem.component.html',
})
export class AddupdateserviceitemComponent extends ParentFomMgtComponent implements OnInit {
  modalTitle!: string;
  modalBtnTitle!: string;
  dbops!: DBOperation;
  form!: FormGroup;
  id: number = 0;
  isReadOnly: boolean = false;

  constructor(private fb: FormBuilder, private apiService: ApiService,
    private authService: AuthorizeService, private utilService: UtilityService, public dialogRef: MatDialogRef<AddupdateserviceitemComponent>,
    private notifyService: NotificationService, private validationService: ValidationService) {
    super(authService)
  };

  ngOnInit(): void {
    this.setForm();
    if (this.id > 0)
      this.setEditForm();
  }

  selectedCars = [2];
  cars = [
        { id: 1, name: 'Daily Service'},
        { id: 2, name: 'Monthly Service' },
        { id: 3, name: 'Yearly Service' },
    ];


  setForm() {
    this.form = this.fb.group(
      {
        'serviceCode': [''],
        'deptCode': [''],
        'activityCode': [''],
        'serviceShortDesc': [''],
        'serviceShortDescAr': [''],
        'serviceDetails': [''],
        'serviceDetailsAr': [''],
        'timeUnitPrimary': [''],
        'resourceUnitPrimary': [''],
        'potentialCost': [''],
        'applicableDiscount': [''],
        'isOnOffer': [''],
        'offerPrice': [''],
        'offerStartDate': [''],
        'offerEndDate': [''],
        'remarks1': [''],
        'remarks2': [''],
        'thumbNailImagePath': [''],
        'minRequiredHrs': [''],
        'minReqResource': [''],
        'primaryUnitPrice': [''],
        'fullImagePath': [''],
        'isActive': [false],
      }
    );
    this.isReadOnly = false;
  }
  setEditForm() {
    this.apiService.get('', this.id).subscribe(res => {
      if (res) {
        this.isReadOnly = true;
        this.form.patchValue(res);
      }
    });
  }
  closeModel() {
    this.dialogRef.close();
  }

  submit() {
    if (this.form.valid) {
      if (this.id > 0)
        this.form.value['id'] = this.id;
      this.apiService.post('', this.form.value)
        .subscribe(res => {
          this.utilService.OkMessage();
          this.reset();
          this.dialogRef.close(true);
        },
          error => {
            this.utilService.ShowApiErrorMessage(error);
          });
    }
    else
      this.utilService.FillUpFields();
  }

  reset() {
    this.form.controls['serviceCode'].setValue('');
    this.form.controls['deptCode'].setValue('');
    this.form.controls['activityCode'].setValue('');
    this.form.controls['serviceShortDesc'].setValue('');
    this.form.controls['serviceShortDescAr'].setValue('');
    this.form.controls['serviceDetails'].setValue('');
    this.form.controls['serviceDetailsAr'].setValue('');
    this.form.controls['timeUnitPrimary'].setValue('');
    this.form.controls['resourceUnitPrimary'].setValue('');
    this.form.controls['potentialCost'].setValue('');
    this.form.controls['applicableDiscount'].setValue('');
    this.form.controls['isOnOffer'].setValue('');
    this.form.controls['offerPrice'].setValue('');
    this.form.controls['offerStartDate'].setValue('');
    this.form.controls['offerEndDate'].setValue('');
    this.form.controls['remarks1'].setValue('');
    this.form.controls['remarks2'].setValue('');
    this.form.controls['thumbNailImagePath'].setValue('');
    this.form.controls['fullImagePath'].setValue('');
    this.form.controls['minRequiredHrs'].setValue('');
    this.form.controls['minReqResource'].setValue('');
    this.form.controls['primaryUnitPrice'].setValue('');
    this.form.controls['isActive'].setValue('');

  }
}
