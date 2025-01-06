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
import { CustomSelectListItem, LanCustomSelectListItem } from '../../../models/MenuItemListDto';
import { ParentFomMgtComponent } from '../../../sharedcomponent/parentfommgt.component';

@Component({
  selector: 'app-addupdatedisciplines',
  templateUrl: './addupdatedisciplines.component.html',
  styles: [
  ]
})
export class AddupdatedisciplinesComponent extends ParentFomMgtComponent implements OnInit {
  modalTitle!: string;
  modalBtnTitle!: string;
  dbops!: DBOperation;
  form!: FormGroup;
  fileUploadone!: File;
  //thumbNailImageUrl: string | ArrayBuffer | null = null;
  TimePeriodList: Array<LanCustomSelectListItem> = [];
  id: number = 0;
  selectedTimePeriods = [];
  file1Url: string = '';
 
  isReadOnly: boolean = false;
  constructor(private fb: FormBuilder, private apiService: ApiService,
    private authService: AuthorizeService, private utilService: UtilityService, public dialogRef: MatDialogRef<AddupdatedisciplinesComponent>,
    private notifyService: NotificationService, private validationService: ValidationService) {
    super(authService)
  };
  ngOnInit(): void {
    this.setForm();
    if (this.id > 0) {
      this.setEditForm();
    } else {
      this.loadPeriod();
    }
      
  }
  setForm() {
    this.form = this.fb.group(
      {
        'id':0,
        'serviceTimePeriods': [[]],
        'deptCode': ['', [Validators.required, Validators.maxLength(20)]],
        'nameEng': ['', Validators.required],
        'nameArabic': ['', Validators.required],
        'deptServType': ['', Validators.required],
        'isSheduleRequired1': [false],
        'isSheduleRequired2': [false],
        'thumbNailImage': [''],
        'isActive': [false],
      }
    );
    this.isReadOnly = false;
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
  setEditForm() {
    this.apiService.getall('FomDiscipline/getSelectTimePeriodsList').subscribe(res => {
      this.TimePeriodList = res;
      this.apiService.get('FomDiscipline', this.id).subscribe(res => {
        if (res) {
          this.isReadOnly = true;
          if (res['serviceTimePeriods'] != null) {
            const filterArray = res['serviceTimePeriods'].split(',');
            var perioddata = this.TimePeriodList as Array<any>;
            res['serviceTimePeriods'] = perioddata.filter(item => filterArray.includes(item.text));
            this.form.patchValue(res);

            this.file1Url = res.thumbNailImage;
          }
          this.form.patchValue({ 'id': 0 });
        }
      });
    });
  }
  closeModel() {
    this.dialogRef.close();
  }

  

  loadPeriod() {
    this.apiService.getall('FomDiscipline/getSelectTimePeriodsList').subscribe(res => {
      this.TimePeriodList = res;
    });
  }



  selectAllChecked = false;

  onTimePeriodChange(event: any) {
    if (event && event.includes('Select All')) {
      this.selectAllChecked = !this.selectAllChecked;
      if (this.selectAllChecked) {
        // Select all department codes
        //  const codes = selectedCodes.map((dept: any) => dept.deptCode);
        const allCodes = this.TimePeriodList.map((timeP: any) => timeP.text);
        this.form.controls['serviceTimePeriods'].setValue(allCodes);
      } else {
        // Deselect all department codes
        this.form.controls['serviceTimePeriods'].setValue([]);
      }
    }

    const selectedTimePeriod = this.getSelectedTimePeriods();
    // Perform necessary actions with the selected codes
    console.log('Selected Time Period on change:', selectedTimePeriod);
  }
 

  getSelectedTimePeriods() {
    return this.form.get('serviceTimePeriods')?.value || [];
  }


 

  //onFileChanged(event: any, type: number) {
  //  let reader = new FileReader();
  //  if (event.target.files && event.target.files.length > 0) {
  //    let file = event.target.files[0];
  //    reader.readAsDataURL(file);
  //    reader.onload = () => {
  //      if (type === 1) {
  //        this.thumbNailImageUrl = reader.result;
  //        this.form.patchValue({
  //          'thumbNailImage': file,
  //        });
  //      }
  //    };
  //  }
  //}





  //submit() {
  //  if (this.form.valid) {
  //    if (this.id > 0)
  //      this.form.value['id'] = this.id;
  //    var periodData = this.form.value['serviceTimePeriods'] as Array<any>;
  //    this.form.value['serviceTimePeriods'] = periodData.map(item => item.text);
  //    this.apiService.post('FomDiscipline', this.form.value)
  //      .subscribe(res => {

  //        this.utilService.OkMessage();
  //        this.reset();
  //        this.dialogRef.close(true);
  //      },
  //        error => {
  //          this.utilService.ShowApiErrorMessage(error);
  //        });
  //  }
  //  else
  //    this.utilService.FillUpFields();
  //}

  //submit() {
  //  if (this.form.valid) {
  //      if (this.id > 0)
  //      this.form.value['id'] = this.id;

  //    var periodData = this.form.value['serviceTimePeriods'] as Array<any>;
     
  //    this.form.controls['serviceTimePeriods'].setValue(periodData);
  //    this.form.patchValue({ id: this.id });
  //    this.apiService.post('FomDiscipline', this.form.value)
  //      .subscribe(res => {
  //        if (res && this.fileUploadone != null && this.fileUploadone != undefined) {
  //          const deptRes = res as any;
  //          const formData = new FormData();
  //          formData.append("id", deptRes.id.toString());
  //          formData.append("WebRoot", this.authService.ApiEndPoint().replace("/api", "") + '/DeptImages/');
  //          formData.append("Image1IForm", this.fileUploadone);
  //          this.apiService.post('FomDiscipline/UploadThumbnailFiles', formData)
  //            .subscribe(res => {
  //              this.utilService.OkMessage();
  //              this.dialogRef.close(true);
  //            },
  //              error => {
  //                console.error(error);
  //                this.utilService.ShowApiErrorMessage(error);
  //              });
  //        } else if (res) {
  //          this.utilService.OkMessage();
  //          // this.reset();
  //          this.dialogRef.close(true);
  //        } else {
  //          this.notifyService.showWarning("error");
  //        }

  //      },
  //        error => {
  //          console.error(error);
  //          this.utilService.ShowApiErrorMessage(error);
  //        });

  //  }
  //  else
  //    this.utilService.FillUpFields();

  //}

  submit() {
    if (this.form.valid) {
      // Ensure 'id' is always part of the form and updated
      this.form.patchValue({ id: this.id });

      // Process serviceTimePeriods to ensure it's an array of strings
      var periodData = this.form.value['serviceTimePeriods'] as Array<any>;
      const cleanedPeriodData = periodData.map(item => item?.text || item); // Ensure strings
      this.form.controls['serviceTimePeriods'].setValue(cleanedPeriodData);

      // Make API call to save the form data
      this.apiService.post('FomDiscipline', this.form.value).subscribe(
        (res) => {
          if (res && this.fileUploadone) {
            // Handle file upload
            this.uploadThumbnailFile(res);
          } else if (res) {
            this.utilService.OkMessage();
            this.dialogRef.close(true); // Close the dialog with success status
          } else {
            this.notifyService.showWarning("Error: No response received");
          }
        },
        (error) => {
          console.error('API Error during FomDiscipline:', error);
          this.utilService.ShowApiErrorMessage(error);
        }
      );
    } else {
      this.utilService.FillUpFields(); // Handle invalid form
    }
  }

  // Helper function for file upload
  uploadThumbnailFile(response: any) {
    const deptRes = response as any;
    const formData = new FormData();
    formData.append("id", deptRes.id.toString());
    formData.append(
      "WebRoot",
      this.authService.ApiEndPoint().replace("/api", "") + '/DeptImages/'
    );
    formData.append("Image1IForm", this.fileUploadone);

    this.apiService.post('FomDiscipline/UploadThumbnailFiles', formData).subscribe(
      (res) => {
        this.utilService.OkMessage();
        this.dialogRef.close(true); // Close the dialog with success status
      },
      (error) => {
        console.error('API Error during file upload:', error);
        this.utilService.ShowApiErrorMessage(error);
      }
    );
  }


  reset() {
    this.form.controls['serviceTimePeriods'].setValue('');
    this.form.controls['nameEng'].setValue('');
    this.form.controls['nameArabic'].setValue('');
    this.form.controls['deptServType'].setValue('');
    this.form.controls['isSheduleRequired1'].setValue('');
    this.form.controls['isSheduleRequired2'].setValue('');
    this.form.controls['thumbNailImage'].setValue('');
    this.form.controls['isActive'].setValue('');
  }
}
