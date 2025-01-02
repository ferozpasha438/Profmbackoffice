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
import { LanCustomSelectListItem } from '../../models/MenuItemListDto';

@Component({
  selector: 'app-addupdatesection',
  templateUrl: './addupdatesection.component.html',
  styles: [
  ]
})
export class AddupdatesectionComponent implements OnInit {
  modalTitle!: string;
  modalBtnTitle!: string;
  dbops!: DBOperation;
  form!: FormGroup;
  fileUploadone!: File;
  //thumbNailImageUrl: string | ArrayBuffer | null = null;
  TimePeriodList: Array<LanCustomSelectListItem> = [];
  file1Url: string = '';
  id: number = 0;
  isReadOnly: boolean = false;
  constructor(private fb: FormBuilder, private apiService: ApiService,
    private authService: AuthorizeService, private utilService: UtilityService, public dialogRef: MatDialogRef<AddupdatesectionComponent>,
    private notifyService: NotificationService, private validationService: ValidationService) {

  };
  ngOnInit(): void {
    this.setForm();
    if (this.id > 0) {
      this.setEditForm();
    }
  }
  setForm() {
    this.form = this.fb.group(
      {
        'sectionCode': ['', [Validators.required, Validators.maxLength(20)]],
        'name': ['', Validators.required],
        'nameAr': ['', Validators.required],
        'description': ['', Validators.required],
        'forAssetMgt': [false],
        'isActive': [true],
      }
    );
    this.isReadOnly = false;
  }


  setEditForm() {
    this.apiService.get('fomSection', this.id).subscribe(res => {
      if (res) {
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

      this.apiService.post('fomSection/createUpdateFomSection', this.form.value)
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



  reset() {
    this.form.controls['name'].setValue('');
    this.form.controls['nameAr'].setValue('');
    this.form.controls['isActive'].setValue('');
  }
}
