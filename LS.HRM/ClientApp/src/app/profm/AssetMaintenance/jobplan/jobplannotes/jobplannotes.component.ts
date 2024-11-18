import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { AuthorizeService } from 'src/app/api-authorization/AuthorizeService';
import { ApiService } from 'src/app/services/api.service';
import { NotificationService } from 'src/app/services/notification.service';
import { DBOperation } from 'src/app/services/utility.constants';
import { UtilityService } from 'src/app/services/utility.service';
import { ParentHrmAdminComponent } from 'src/app/sharedcomponent/Parenthrmadmin.component';
import { ValidationService } from 'src/app/sharedcomponent/ValidationService';
import { LanCustomSelectListItem } from '../../../../models/MenuItemListDto';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';
import { AddupdatejobplanscheduleComponent } from '../addupdatejobplanschedule/addupdatejobplanschedule.component';

@Component({
  selector: 'app-jobplannotes',
  templateUrl: './jobplannotes.component.html',
  styles: [
  ]
})
export class JobplannotesComponent implements OnInit {
  modalTitle!: string;
  modalBtnTitle!: string;
  dbops!: DBOperation;
  form!: FormGroup;
  id: string = '';

  isSaving: boolean = false;
  isLoading: boolean = false;
  jobNotesList: Array<any> = [];
  constructor(private fb: FormBuilder, private apiService: ApiService, public dialog: MatDialog,
    private authService: AuthorizeService, private utilService: UtilityService, public dialogRef: MatDialogRef<JobplannotesComponent>,
    private notifyService: NotificationService, private validationService: ValidationService) {

  };
  ngOnInit(): void {
    this.setForm();
    this.loadData();
  }


  setForm() {
    this.form = this.fb.group(
      {
        'jobPlanCode': [this.id],
        'message': ['', Validators.required],
        'messageDate': ['', Validators.required]
      }
    );
  }

  loadData() {
    this.isLoading = true;
    this.apiService.getall(`assetMaintenance/getJobPlanNotesByJobCode?jobPlanCode=${this.id}`).subscribe(res => {
      if (res) {
        this.jobNotesList = res;
        this.isLoading = false;
      }
    });
  }

  submit() {
    if (this.form.valid) {
      //if (this.id != '')
      //  this.form.value['jobPlanCode'] = this.id;

      this.apiService.post('assetMaintenance/addJobPlanNotes', this.form.value)
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

  closeModel() {
    this.dialogRef.close();
  }

}

