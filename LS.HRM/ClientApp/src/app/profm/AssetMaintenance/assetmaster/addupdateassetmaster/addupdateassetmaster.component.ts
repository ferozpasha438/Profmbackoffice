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
import { LanCustomSelectListItem } from '../../../../models/MenuItemListDto';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';

@Component({
  selector: 'app-addupdateassetmaster',
  templateUrl: './addupdateassetmaster.component.html',
  styles: [
  ]
})
export class AddupdateassetmasterComponent implements OnInit {
  modalTitle!: string;
  modalBtnTitle!: string;
  dbops!: DBOperation;
  form!: FormGroup;
  fileUploadone!: File;
  //thumbNailImageUrl: string | ArrayBuffer | null = null;
  departmentSelectList: Array<LanCustomSelectListItem> = [];
  sectionSelectList: Array<LanCustomSelectListItem> = [];
  contractSelectList: Array<LanCustomSelectListItem> = [];
  resourseSelectList: Array<LanCustomSelectListItem> = [];
  activitySelectList: Array<any> = [];

  file1Url: string = '';
  id: number = 0;
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
  scales: any[] = [];
  assetScaleValue: string = '';
  constructor(private fb: FormBuilder, private apiService: ApiService,
    private authService: AuthorizeService, private utilService: UtilityService, public dialogRef: MatDialogRef<AddupdateassetmasterComponent>,
    private notifyService: NotificationService, private validationService: ValidationService) {

  };
  ngOnInit(): void {
    this.scales = this.utilService.getAssetScales();
    this.isArab = this.utilService.isArabic();
    this.setForm();
    this.loadData();
    if (this.id > 0) {
      this.setEditForm();
    }
  }



  setForm() {
    this.assetScaleValue = this.scales[0];
    this.form = this.fb.group(
      {
        'assetCode': ['', [Validators.required, Validators.maxLength(20)]],
        'name': ['', Validators.required],
        'nameAr': ['', Validators.required],
        'sectionCode': [''],// [Validators.required, Validators.maxLength(20)]],
        "deptCode": ['', Validators.required],
        "contractCode": ['', Validators.required],
        "description": '',
        "location": '',
        "classification": '',
        "routeGroup": '',
        "assetScale": 0,
        "installDate": null,
        "replacementDate": null,
        "jobPlan": '',
        "hasChild": [false],
        'isActive': [true],
        'isWrittenOff': [false],
      }
    );
    this.isReadOnly = false;
  }


  setEditForm() {
    this.apiService.get('assetMaintenance', this.id).subscribe(res => {
      if (res) {
        this.assetScaleValue = this.scales[res.assetScale];
        this.form.patchValue(res);
        this.loadActivityFor_Dept(res.deptCode);
        if (res.hasChild) {
          this.displayChilds(res.assetChilds)
          this.isChildSelected = true;
        }
        else
          this.isChildSelected = false;
        this.displayTasks(res.assetTasks)
      }
    });
  }

  displayChilds(result: any) {
    let items = result as Array<any>;
    items.forEach(item => {
      this.childCode = item.childCode;
      this.name = item.name,
        this.addChild();
    });
  }
  displayTasks(result: any) {
    let items = result as Array<any>;
    items.forEach(item => {
      this.actCode = item.actCode;
      this.resTypeCode = item.resTypeCode,
        this.addTask();
    });
  }

  loadData() {
    this.apiService.getall(`fomDiscipline/getDepartmentSelectList`).subscribe(res => {
      if (res) {
        this.departmentSelectList = res;
      }
    });
    this.apiService.getall(`fomSection/getSectiomSelectList?isForAssetMgt=true`).subscribe(res => {
      if (res) {
        this.sectionSelectList = res;
      }
    });
    this.apiService.getall(`fomDiscipline/getSelectResourceTypesQuery`).subscribe(res => {
      if (res) {
        this.resourseSelectList = res;
      }
    });
    this.apiService.getall(`fomCustomerContract/getLanCustomerContractSelectList`).subscribe(res => {
      if (res) {
        this.contractSelectList = res;
      }
    });
  }

  loadActivityForDept(evt: any) {
    this.loadActivityFor_Dept(evt.target.value);
  }
  loadActivityFor_Dept(dept: any) {
    this.apiService.getall(`fomActivities/GetActivitiesByDeptCode/${dept}`).subscribe(res => {
      if (res) {
        this.activitySelectList = res;
      }
    });
  }

  closeModel() {
    this.dialogRef.close();
  }

  hasChildEvent(evt: MatSlideToggleChange) {
    this.isChildSelected = evt.checked;
  }

  addChild() {
    if (this.childCode.trim() && this.name.trim()) {
      if (this.childeditsequence > 0) {
        var index: number = this.listOfAssetChild.findIndex(a => a.sequence === this.childeditsequence);
        let pItem = this.listOfAssetChild[index];
        pItem.childCode = this.childCode;
        pItem.name = this.name;
        this.childeditsequence = 0;
      }
      else {
        this.listOfAssetChild.push({
          sequence: this.getSequence(), childCode: this.childCode, name: this.name
        })
      }
      this.setToDefault();
    }
  }

  deleteChild(item: any) {
    this.removeChild(item.sequence);
  }
  removeChild(sequence: number) {
    let index: number = this.listOfAssetChild.findIndex(a => a.sequence === sequence);
    this.listOfAssetChild.splice(index, 1);
  }
  editChild(item: any) {
    this.childeditsequence = item.sequence;
    this.childCode = item.childCode;
    this.name = item.name;
  }
  setToDefault() {
    this.childCode = ''; this.name = '';
  }
  getSequence(): number { return this.sequence = this.sequence + 1 };


  addTask() {
    if (this.actCode.trim() && this.resTypeCode.trim()) {
      if (this.childeditsequence > 0) {
        var index: number = this.listOfAssetTasks.findIndex(a => a.sequence === this.childeditsequence);
        let pItem = this.listOfAssetTasks[index];
        pItem.actCode = this.actCode;
        pItem.resTypeCode = this.resTypeCode;
        this.childeditsequence = 0;
      }
      else {
        this.listOfAssetTasks.push({
          sequence: this.getTaskSequence(), actCode: this.actCode, resTypeCode: this.resTypeCode
        })
      }
      this.setTaskToDefault();
    }
  }
  deleteTask(item: any) {
    this.removeTask(item.sequence);
  }
  removeTask(sequence: number) {
    let index: number = this.listOfAssetTasks.findIndex(a => a.sequence === sequence);
    this.listOfAssetTasks.splice(index, 1);
  }
  editTask(item: any) {
    this.childeditsequence = item.sequence;
    this.actCode = item.actCode;
    this.resTypeCode = item.resTypeCode;
  }
  setTaskToDefault() {
    this.actCode = ''; this.resTypeCode = '';
  }
  getTaskSequence(): number { return this.sequenceTask = this.sequenceTask + 1 };

  assetScale(evt: any) {
    //let scales: any[] = ['Worst', 'Bad', 'Medium', 'Medium', 'Good', 'Good', 'Better', 'Better', 'Better', 'Best', 'Best'];
    //let scales :any[] = [{0:'Worst'},{1:'Bad'},{2:'Medium'},{3:'Good'},{4:'Good'},{5:'Good'},{6:'Better'},{7:'Better'},{8:'Better'},{9:'Best'},{10:'Best'}];
    //console.log(evt.target.value, scales[evt.target.value]);

    this.assetScaleValue = this.scales[evt.target.value];
  }
  submit() {
    if (this.form.valid) {

      if (this.form.controls['installDate'].value)
        this.form.controls['installDate'].setValue(this.utilService.selectedDate(this.form.controls['installDate'].value));
      if (this.form.controls['replacementDate'].value)
        this.form.controls['replacementDate'].setValue(this.utilService.selectedDate(this.form.controls['replacementDate'].value));

      if (this.id > 0)
        this.form.value['id'] = this.id;

      if (this.listOfAssetChild.length > 0) {
        this.form.value['assetChilds'] = this.listOfAssetChild;
      }
      if (this.listOfAssetTasks.length > 0) {
        this.form.value['assetTasks'] = this.listOfAssetTasks;
      }
      else {
        this.notifyService.showError('Pls add Activities')
        return;
      }



      this.apiService.post('assetMaintenance/createUpdateFomAssetMaster', this.form.value)
        .subscribe(res => {
          this.utilService.OkMessage();
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
    this.form.controls['name'].setValue('');
    this.form.controls['nameAr'].setValue('');
    this.form.controls['isActive'].setValue('');
  }
}
