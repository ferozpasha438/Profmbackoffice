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
import { CustomSelectListItem } from '../../../models/MenuItemListDto';
import { ParentFomMgtComponent } from '../../../sharedcomponent/parentfommgt.component';

@Component({
  selector: 'app-addupdateactivites',
  templateUrl: './addupdateactivites.component.html',
  styles: [
  ]
})
export class AddupdateactivitesComponent extends ParentFomMgtComponent implements OnInit {
  modalTitle!: string;
  modalBtnTitle!: string;
  dbops!: DBOperation;
  form!: FormGroup;
  fileUploadone!: File;
  file1Url: string = '';
  id: number = 0;
  isReadOnly: boolean = false;
  DisciplineCodeList: Array<CustomSelectListItem> = [];
  constructor(private fb: FormBuilder, private apiService: ApiService,
    private authService: AuthorizeService, private utilService: UtilityService, public dialogRef: MatDialogRef<AddupdateactivitesComponent>,
    private notifyService: NotificationService, private validationService: ValidationService) {
    super(authService)
  };

  ngOnInit(): void {
    this.loadData();
    this.setForm();
    if (this.id > 0)
      this.setEditForm();
  }

  setForm() {
    this.form = this.fb.group(
      {
        'actCode': ['', [Validators.required, Validators.maxLength(20)]],
        'deptCode': ['', Validators.required],
        'actName': ['', Validators.required],
        'actNameAr': ['', Validators.required],
        'thumbNailImage':[''],
        'isB2B': ['', Validators.required],
        'isB2C': ['', Validators.required],
        'isActive': [false],
      }
    );
    this.isReadOnly = false;
  }

  setEditForm() {
    this.apiService.get('fomActivities', this.id).subscribe(res => {
      if (res) {
        this.isReadOnly = true;
        this.form.patchValue(res);
        this.file1Url = res.thumbNailImage;

      }
    });
  }


  onSelectFiles(fileInput: any) {
    if (fileInput.target.files.length > 1) {
      this.notifyService.showWarning("Select Only 1 Image");
    } else if (fileInput.target.files.length > 0) {
      this.fileUploadone = <File>fileInput.target.files[0];
      //if (fileInput.target.files.length > 1) {
      //  this.fileUploadtwo = <File>fileInput.target.files[1];
      //}
      //if (fileInput.target.files.length > 2) {
      //  this.fileUploadthree = <File>fileInput.target.files[2];
      //}
    }
  }


  closeModel() {
    this.dialogRef.close();
  }

  submit() {
    if (this.id > 0)
      this.form.value['id'] = this.id;
    
    if (this.form.valid) {
      this.apiService.post('fomActivities', this.form.value)
        .subscribe(res => {
          if (res && this.fileUploadone != null && this.fileUploadone != undefined) {
            const custRes = res as any;
            const formData = new FormData();
            formData.append("id", custRes.id.toString());
            formData.append("WebRoot", this.authService.ApiEndPoint().replace("/api", "") + '/ActImages/');
            formData.append("Image1IForm", this.fileUploadone);
            this.apiService.post('fomActivities/UploadThumbnailFiles', formData)
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

    //if (this.form.valid) {
    //  if (this.id > 0)
    //    this.form.value['id'] = this.id;
    //  this.apiService.post('fomActivities', this.form.value)
    //    .subscribe(res => {
    //      this.utilService.OkMessage();
    //      this.reset();
    //      this.dialogRef.close(true);
    //    },
    //      error => {
    //        this.utilService.ShowApiErrorMessage(error);
    //      });
    //}
    //else
    //  this.utilService.FillUpFields();
  }

  reset() {
    this.form.controls['actCode'].setValue('');
    this.form.controls['deptCode'].setValue('');
    this.form.controls['actName'].setValue('');
    this.form.controls['actNameAr'].setValue('');
    this.form.controls['thumbNailImage'].setValue('');
    this.form.controls['isB2B'].setValue('');
    this.form.controls['isB2C'].setValue('');
    this.form.controls['isActive'].setValue('');

  }

  loadData() {
    this.apiService.getPagination('fomDiscipline', this.utilService.getQueryString(0, 1000, '', '')).subscribe(res => {
      if (res)
        this.DisciplineCodeList = res['items'];
    });

  }
}
