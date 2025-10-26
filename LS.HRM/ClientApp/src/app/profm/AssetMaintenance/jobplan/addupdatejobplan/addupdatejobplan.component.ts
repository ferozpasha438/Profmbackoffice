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
  selector: 'app-addupdatejobplan',
  templateUrl: './addupdatejobplan.component.html',
  styles: [
  ]
})
export class AddupdatejobplanComponent implements OnInit {
  modalTitle!: string;
  modalBtnTitle!: string;
  dbops!: DBOperation;
  form!: FormGroup;
  fileUploadone!: File;
  //thumbNailImageUrl: string | ArrayBuffer | null = null;
  assetSelectList: Array<LanCustomSelectListItem> = [];
  frequencyList: Array<LanCustomSelectListItem> = [];


  file1Url: string = '';
  data: any;
  id: number = 0;
  //isApproved: boolean = false;
  isReadOnly: boolean = false;
  isChildSelected: boolean = false;

  sequence: number = 1;
  childCode: string = '';
  name: string = '';
  childeditsequence: number = 0;
  listOfAssetChild: Array<any> = [];

  sequenceTask: number = 1;
  actCode: string = '';
  resTypeCode: string = '';
  taskeditsequence: number = 0;
  listOfAssetTasks: Array<any> = [];
  isArab: boolean = false;
  listofgeneratedschedules: Array<any> = [];
  isSaving: boolean = false;
  canGenChildSchSelected: boolean = true;

  constructor(private fb: FormBuilder, private apiService: ApiService, public dialog: MatDialog,
    private authService: AuthorizeService, private utilService: UtilityService, public dialogRef: MatDialogRef<AddupdatejobplanComponent>,
    private notifyService: NotificationService, private validationService: ValidationService) {

  };
  ngOnInit(): void {
    this.isArab = this.utilService.isArabic();
    this.id = this.data.id;
    //this.isApproved = this.data.approve as boolean;

    this.setForm();
    this.loadData();
    if (this.id > 0) {
      this.setEditForm();
    }
  }


  setForm() {
    this.form = this.fb.group(
      {
        'id': 0,
        'jobPlanCode': [''],
        'jobPlanDate': ['', Validators.required],
        'assetCode': ['', Validators.required],
        "contractCode": ['', Validators.required],
        'contStartDate': ['', Validators.required],
        'contEndDate': ['', Validators.required],
        'frequency': ['', Validators.required],
        "preFixCode": '',
        "deptCode": ['', Validators.required],
        "planStartDate": ['', Validators.required],
        'sectionCode': [''],
        "preparedBy": this.authService.getUserName(),
        "approvedBy": this.authService.getUserName(),
        "remarks": '',
        "noJobPlanKpi": [false],
        "canGenChildSch": [true],
        "childHasDiffFreq": [false],
        "approve": [false]
      }
    );
    this.isReadOnly = false;
  }


  setEditForm() {
    this.apiService.get('assetMaintenance/getFomJobPlanMasterById', this.id).subscribe(res => {
      if (res) {
        this.form.patchValue(res);
        this.canGenChildSchSelected = res.hasChild;
        //if (res.hasChild) {
        //  this.displayChilds(res.assetChilds)
        //  this.isChildSelected = true;
        //}
        //else
        //  this.isChildSelected = false;
      }
    });
  }


  loadData() {
    this.apiService.getall(`assetMaintenance/getFomAssetMasterSelectList`).subscribe(res => {
      if (res) {
        this.assetSelectList = res;
        this.assetSelectList.map((i) => {
          i.text = `(${i.value}) ${(!this.isArab ? i.text : i.textTwo)}`;
          return i;
        });
      }
    });
    this.apiService.getall(`assetMaintenance/getJobPlanFrequecies`).subscribe(res => {
      if (res) {
        this.frequencyList = res;
      }
    });
  }
  resetAssetInfo() {
    this.loadAssetDetailsByAssetCode("");
  }
  loadAssetFor_AstCode(evt: any) {
    const asstCode = evt.value;// evt.target.value;
    this.loadAssetDetailsByAssetCode(asstCode);
  }
  loadAssetDetailsByAssetCode(asstCode: any) {
    this.apiService.getall(`assetMaintenance/GetFomAssetMasterByAssetCode?assetCode=${asstCode}`).subscribe(res => {
      if (res) {
        this.form.patchValue(res);
        this.canGenChildSchSelected = res.hasChild;
      }
    });
  }

  private openDialogManage(data: any, component: any) {
    let dialogRef = this.utilService.openCrudDialog(this.dialog, component, '95%');
    (dialogRef.componentInstance as any).data = data;
    (dialogRef.componentInstance as any).savedList = this.listofgeneratedschedules;
    dialogRef.afterClosed().subscribe(res => {
      if (res) {
        this.listofgeneratedschedules = [];
        this.listofgeneratedschedules.push(...res);
        //console.log('parent_Pop', this.listofgeneratedschedules);
      }

    });
  }

  jobPlanSchedule() {
    if (this.form.valid) {
     
      this.openDialogManage({
        title: 'Add_New_JobPlanSchedule',
        jobPlanCode: this.form.controls['jobPlanCode'].value,
        assetCode: this.form.controls['assetCode'].value,
        frequency: this.form.controls['frequency'].value,
        planStartDate: this.form.controls['planStartDate'].value,
        childHasDiffFreq: this.form.controls['childHasDiffFreq'].value as boolean,
        id: this.id
      }, AddupdatejobplanscheduleComponent);

    }
    else
      this.utilService.FillUpFields();
  }

  canGenChildSchEvent(evt: MatSlideToggleChange) {
    this.canGenChildSchSelected = evt.checked;
  }



  submit() {
    if (this.form.valid) {
      if (this.id > 0)
        this.form.controls['id'].setValue(this.id);

      this.form.controls['jobPlanDate'].setValue(this.utilService.selectedDate(this.form.controls['jobPlanDate'].value));
      this.form.controls['planStartDate'].setValue(this.utilService.selectedDate(this.form.controls['planStartDate'].value));

      if (this.canGenChildSchSelected) {

        if (this.listofgeneratedschedules.length > 0) {
          this.form.value['jobPlanSchedules'] = this.listofgeneratedschedules;
        }
        else {
          //if (this.isApproved) {
          if (this.id <= 0) {
            this.notifyService.showError('please generate PPM schedules');
            return
          }
        }

        //console.log('form-submit',this.form.value );

        this.isSaving = true;
        this.apiService.post('assetMaintenance/createUpdateFomJobPlanMaster', this.form.value)
          .subscribe(res => {
            this.isSaving = false;
            this.utilService.OkMessage();
            this.dialogRef.close(true);
          },
            error => {
              this.isSaving = false;
              console.error(error);
              this.utilService.ShowApiErrorMessage(error);
            });

      }
      else {

        this.isSaving = true;
        this.apiService.post('assetMaintenance/createUpdateFomJobPlanMasterNoSchedules', this.form.value)
          .subscribe(res => {
            this.isSaving = false;
            this.utilService.OkMessage();
            this.dialogRef.close(true);
          },
            error => {
              this.isSaving = false;
              console.error(error);
              this.utilService.ShowApiErrorMessage(error);
            });
      }


    }
    else
      this.utilService.FillUpFields();

  }
  closeModel() {
    this.dialogRef.close();
  }

}
