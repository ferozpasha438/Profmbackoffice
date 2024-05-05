import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { AuthorizeService } from 'src/app/api-authorization/AuthorizeService';
import { ApiService } from 'src/app/services/api.service';
import { NotificationService } from 'src/app/services/notification.service';
import { DBOperation } from 'src/app/services/utility.constants';
import { UtilityService } from 'src/app/services/utility.service';
import { ParentHrmAdminComponent } from 'src/app/sharedcomponent/Parenthrmadmin.component';
import { ValidationService } from 'src/app/sharedcomponent/ValidationService';
import { ParentFomMgtComponent } from 'src/app/sharedcomponent/parentfommgt.component';

@Component({
  selector: 'app-addupdatecustomercategory',
  templateUrl: './addupdatecustomercategory.component.html',
  styles: [
  ]
})
export class AddupdatecustomercategoryComponent implements OnInit {
  id: number = 0;
  form!: FormGroup;
  modalTitle!: string;
  row: any;
  maxLength: number = 5;
  defaultMaxLength: number = 5;
  prefixCatCode: boolean = false;

  constructor(private fb: FormBuilder, private apiService: ApiService,
    private authService: AuthorizeService, private utilService: UtilityService, public dialogRef: MatDialogRef<AddupdatecustomercategoryComponent>,
    private notifyService: NotificationService) {

  }

  ngOnInit(): void {
    this.autoGenerateCustCode();
    this.setForm();
    if (this.row) {
      this.form.patchValue(this.row);
      this.id = parseInt(this.row['id']);
    }
  }
  setForm() {
    //let cDate: IMyDateModel = { isRange: false, singleDate: {}, dateRange: null };
    this.form = this.fb.group({
      'custCatCode': ['', [Validators.required, Validators.maxLength(20)]],
      'custCatName': ['', Validators.required],
      'custCatDesc': ['', Validators.required],
      'catPrefix': [''],
      'isActive': [false],

    });
  }

  autoGenerateCustCode() {
    this.apiService.getall('salesConfig').subscribe(res => {
      this.prefixCatCode = res.prefixCatCode as boolean;
      const categoryLength = res.categoryLength as number
      this.maxLength = categoryLength == 0 ? this.defaultMaxLength : res.categoryLength as number;
    });
  }

  submit() {


    if (this.form.valid) {

      if (this.prefixCatCode) {
        const catPrefix = this.form.controls['catPrefix'].value;
        if (!this.utilService.hasValue(catPrefix as string)) {
          this.notifyService.showError('Please enter CategoryPrefix');
          return;
        }
      }

      if (this.id > 0)
        this.form.value['id'] = this.id;

      this.apiService.post('fomCustomerCategory', this.form.value)
        .subscribe(res => {
          this.utilService.OkMessage();
          this.reset();
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

  reset() {
    this.form.reset();
  }
  closeModel() {
    this.dialogRef.close();
  }
}
