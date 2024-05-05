import { Component, OnInit } from '@angular/core';
//import { FormGroup, FormBuilder, Validators, FormControl, ValidatorFn, AbstractControl } from '@angular/forms';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { AuthorizeService } from 'src/app/api-authorization/AuthorizeService';
import { ApiService } from 'src/app/services/api.service';
import { startWith, debounceTime, distinctUntilChanged, switchMap, map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { NotificationService } from 'src/app/services/notification.service';
import { DBOperation } from 'src/app/services/utility.constants';
import { UtilityService } from 'src/app/services/utility.service';
import { ParentHrmAdminComponent } from 'src/app/sharedcomponent/Parenthrmadmin.component';
import { ValidationService } from 'src/app/sharedcomponent/ValidationService';
import { CustomSelectListItem } from '../../../models/MenuItemListDto';
import { ParentFomMgtComponent } from '../../../sharedcomponent/parentfommgt.component';
import { WeekDay } from '@angular/common';
import { MatTableDataSource } from '@angular/material/table';
import { error } from '@angular/compiler/src/util';

@Component({
  selector: 'app-getschedules',
  templateUrl: './getschedules.component.html',
})
export class GetschedulesComponent extends ParentFomMgtComponent implements OnInit {
  modalTitle!: string;
  modalBtnTitle!: string;
  dbops!: DBOperation;
  form!: FormGroup;
  id: number = 0;
  row: any;
  shId: number = 0;
  isReadOnly: boolean = false;
  isDataLoading: boolean = false;
  isSchGenerated: boolean = false;
  contractSummaryList: Array<any> = [];
  data!: MatTableDataSource<any>;
  data2!: MatTableDataSource<any>;
  data3: Array<any> = [];
  contractCode: string = '';
  DepartmentCodeList: Array<any> = [];
  deptCodeList : Array<any>= [];


  constructor(private fb: FormBuilder, private apiService: ApiService,
    private authService: AuthorizeService, private utilService: UtilityService, public dialogRef: MatDialogRef<GetschedulesComponent>,
    private notifyService: NotificationService, private validationService: ValidationService) {
    super(authService)
  }
  

  ngOnInit(): void {
    this.id = this.row.id;
    const genButton = document.getElementById("genButton") as HTMLButtonElement | null;
    const saveButton = document.getElementById("saveButton") as HTMLButtonElement | null;
    if (genButton) {
     // genButton.disabled = true;
    }
    if (saveButton) {
    //  saveButton.disabled = true;
    }
    this.loadData();
    this.setForm();
    if (this.id > 0)
      this.setEditForm();
   // this.setEditShedule();
  }

  loadData() {
    this.apiService.getPagination('fomDiscipline', this.utilService.getQueryString(0, 1000, '', '')).subscribe(res => {
      if (res) {
        this.DepartmentCodeList = res['items'];
        this.DepartmentCodeList = this.DepartmentCodeList.filter(x => this.row.contDeptCode.split(",").includes(x.deptCode));
        if (this.id > 0)
          this.setEditForm();
      }

    });
  }

  setForm() {
    this.form = this.fb.group(
      {
        'contractCode': [''],
        'custCode': [''],
        'contStartDate': [''],
        'contEndDate': [''],
        'deptCode': [''],
        'contDeptCode':[''],
        'selectSun': [false],
        'selectMon': [false],
        'selectTue': [false],
        'selectWed': [false],
        'selectThu': [false],
        'selectFri': [false],
        'selectSat': [false],
        'timeSun': ['00:00'],
        'timeMon': ['00:00'],
        'timeTue': ['00:00'],
        'timeWed': ['00:00'],
        'timeThu': ['00:00'],
        'timeFri': ['00:00'],
        'timeSat': ['00:00'],
        'remarkSun': [''],
        'remarkMon': [''],
        'remarkTue': [''],
        'remarkWed': [''],
        'remarkThu': [''],
        'remarkFri': [''],
        'remarkSat': [''],
       
      }
    );
    this.isReadOnly = false;
  }
  setEditForm() {
    
    this.apiService.get('FomCustomerContract', this.id).subscribe(res => {
    //  this.form.value['id'] = this.id;
      
      console.log(res);
      if (res) {
        this.form.value['contractCode'] = res['contractCode'];

        this.form.patchValue(res);
      
        
      }
    });
  }


  setEditShedule() {
    const genButton = document.getElementById("genButton") as HTMLButtonElement | null;
    const saveButton = document.getElementById("saveButton") as HTMLButtonElement | null;
    const deptCode: string = this.form.value['deptCode'] as string;
    const contractCode: string = this.form.value['contractCode'] as string;
    if (genButton) {
      genButton.disabled = false;
    }
    if (saveButton) {
      saveButton.disabled = false;
    }
    this.apiService.getall(`FomCustomerContract/GetScheduleById/${deptCode}/${contractCode}`).subscribe(res => {
      console.log(res);
      if (res) {
        this.isSchGenerated = res['isSchGenerated'];
       // this.resetShedule();

        this.form.value['id'] = res['id'];
        this.shId = res['id'];
        if (res['tableRows'][0]['weekDay'] == "Sunday") {
          this.form.controls.remarkSun.setValue(res['tableRows'][0]['remarks']);
          this.form.controls.selectSun.setValue(res['tableRows'][0]['isActive']);
          this.form.controls.timeSun.setValue(res['tableRows'][0]['time']);
        }
        if (res['tableRows'][1]['weekDay'] == "Monday") {
          this.form.controls.remarkMon.setValue(res['tableRows'][1]['remarks']);
          this.form.controls.selectMon.setValue(res['tableRows'][1]['isActive']);
          this.form.controls.timeMon.setValue(res['tableRows'][1]['time']);
        }
        if (res['tableRows'][2]['weekDay'] == "Tuesday") {
          this.form.controls.remarkTue.setValue(res['tableRows'][2]['remarks']);
          this.form.controls.selectTue.setValue(res['tableRows'][2]['isActive']);
          this.form.controls.timeTue.setValue(res['tableRows'][2]['time']);
        }
        if (res['tableRows'][3]['weekDay'] == "Wednesday") {
          this.form.controls.remarkWed.setValue(res['tableRows'][3]['remarks']);
          this.form.controls.selectWed.setValue(res['tableRows'][3]['isActive']);
          this.form.controls.timeWed.setValue(res['tableRows'][3]['time']);
        }
        if (res['tableRows'][4]['weekDay'] == "Thursday") {
          this.form.controls.remarkThu.setValue(res['tableRows'][4]['remarks']);
          this.form.controls.selectThu.setValue(res['tableRows'][4]['isActive']);
          this.form.controls.timeThu.setValue(res['tableRows'][4]['time']);
        }
        if (res['tableRows'][5]['weekDay'] == "Friday") {
          this.form.controls.remarkFri.setValue(res['tableRows'][5]['remarks']);
          this.form.controls.selectFri.setValue(res['tableRows'][5]['isActive']);
          this.form.controls.timeFri.setValue(res['tableRows'][5]['time']);
        }
        if (res['tableRows'][6]['weekDay'] == "Saturday") {
          this.form.controls.remarkSat.setValue(res['tableRows'][6]['remarks']);
          this.form.controls.selectSat.setValue(res['tableRows'][6]['isActive']);
          this.form.controls.timeSat.setValue(res['tableRows'][6]['time']);
        }
        if (res['isSchGenerated'] == true && deptCode == res['deptCode']) {
          if (genButton) {
            genButton.disabled = true;
          }
          if (saveButton) {
            saveButton.disabled = true;
          }
        } else {
          if (genButton) {
            genButton.disabled = false;
          }
          if (saveButton) {
            saveButton.disabled = false;
          }
        }
        
       
      } else {
        if (genButton) {
          genButton.disabled = false;
        }
        if (saveButton) {
          saveButton.disabled = false;
        }
      }
      
    },
      error => {
        if (genButton) {
          genButton.disabled = false;
        }
        if (saveButton) {
          saveButton.disabled = false;
        }
      });

    

    
   
    //if (genButton) {
    //  genButton.disabled = true;
    //}
    //if (saveButton) {
    // saveButton.disabled = true;
    //}


    this.resetShedule();

  }


  submit() {

    
    if (this.id > 0)
      if (this.shId > 0) {
        this.form.value['id'] = this.shId;
      } else {
        this.form.value['id'] = this.id;
      }
      
    
    if (this.form.valid) {
     
  
      // Construct details data
      const detailsData = [
        { weekDay: 'Sunday',   time: this.form.controls['timeSun'].value,    remarks: this.form.controls['remarkSun'].value,    isActive: this.form.controls['selectSun'].value==''?false:true },
        { weekDay: 'Monday', time: this.form.controls['timeMon'].value, remarks: this.form.controls['remarkMon'].value, isActive: this.form.controls['selectMon'].value == '' ? false : true },
        { weekDay: 'Tuesday', time: this.form.controls['timeTue'].value, remarks: this.form.controls['remarkTue'].value, isActive: this.form.controls['selectTue'].value == '' ? false : true },
        { weekDay: 'Wednesday', time: this.form.controls['timeWed'].value, remarks: this.form.controls['remarkWed'].value, isActive: this.form.controls['selectWed'].value == '' ? false : true},
        { weekDay: 'Thursday', time: this.form.controls['timeThu'].value, remarks: this.form.controls['remarkThu'].value, isActive: this.form.controls['selectThu'].value == '' ? false : true },
        { weekDay: 'Friday', time: this.form.controls['timeFri'].value, remarks: this.form.controls['remarkFri'].value, isActive: this.form.controls['selectFri'].value == '' ? false : true},
        { weekDay: 'Saturday', time: this.form.controls['timeSat'].value, remarks: this.form.controls['remarkSat'].value, isActive: this.form.controls['selectSat'].value == '' ? false : true }
      ];
    
      const formData = {
        id: this.shId,
       
        contractCode: this.form.controls['contractCode'].value,
        custCode: this.form.controls['custCode'].value,
        //contStartDate: this.utilService.selectedDate(this.form.controls['contStartDate'].value),
        //contEndDate: this.utilService.selectedDate(this.form.controls['contEndDate'].value),
        deptCode: this.form.controls['deptCode'].value,
        tableRows: detailsData
        // Add more header fields as needed
      };
      this.apiService.post('FomCustomerContract/CreateScheduleSummary', formData)
        .subscribe(res => {
          this.utilService.OkMessage();
          this.form.patchValue(res);
          this.reset();
          
          this.dialogRef.close(true);
        },
          error => {
            console.error(error);
            this.utilService.ShowApiErrorMessage(error);
          });
    } else {
      this.utilService.FillUpFields();
    }
  }

  generate() {
    if (this.id > 0)
      if (this.shId > 0) {
        this.form.value['id'] = this.shId;
      } else {
        this.form.value['id'] = this.id;
      }


    if (this.form.valid) {

      const genButton = document.getElementById("genButton") as HTMLButtonElement | null;
      const saveButton = document.getElementById("saveButton") as HTMLButtonElement | null;

      // Construct details data
      const detailsData = [
        { weekDay: 'Sunday', time: this.form.controls['timeSun'].value, remarks: this.form.controls['remarkSun'].value, isActive: this.form.controls['selectSun'].value },
        { weekDay: 'Monday', time: this.form.controls['timeMon'].value, remarks: this.form.controls['remarkMon'].value, isActive: this.form.controls['selectMon'].value },
        { weekDay: 'Tuesday', time: this.form.controls['timeTue'].value, remarks: this.form.controls['remarkTue'].value, isActive: this.form.controls['selectTue'].value },
        { weekDay: 'Wednesday', time: this.form.controls['timeWed'].value, remarks: this.form.controls['remarkWed'].value, isActive: this.form.controls['selectWed'].value },
        { weekDay: 'Thursday', time: this.form.controls['timeThu'].value, remarks: this.form.controls['remarkThu'].value, isActive: this.form.controls['selectThu'].value },
        { weekDay: 'Friday', time: this.form.controls['timeFri'].value, remarks: this.form.controls['remarkFri'].value, isActive: this.form.controls['selectFri'].value },
        { weekDay: 'Saturday', time: this.form.controls['timeSat'].value, remarks: this.form.controls['remarkSat'].value, isActive: this.form.controls['selectSat'].value }
      ];

      const summaryData = {
        id: this.shId,
        contractCode: this.form.controls['contractCode'].value,
        custCode: this.form.controls['custCode'].value,
        contStartDate: this.utilService.selectedDate(this.form.controls['contStartDate'].value),
        contEndDate: this.utilService.selectedDate(this.form.controls['contEndDate'].value),
        deptCode: this.form.controls['deptCode'].value,
        TableRows: detailsData
        // Add more header fields as needed
      };
      this.apiService.post('FomCustomerContract/GenerateScheduleSummary', summaryData)
        .subscribe(res => {
          this.utilService.OkMessage();
          if (genButton) {
            genButton.disabled = true;
          }
          if (saveButton) {
            saveButton.disabled = true;
          }

          this.form.patchValue(res);
          this.reset();

          this.dialogRef.close(true);
        },
          error => {
            console.error(error);
            this.utilService.ShowApiErrorMessage(error);
          });
    } else {
      this.utilService.FillUpFields();
    }


  
  


  }



  //submit() {
  //  if (this.form.valid) {
  //    if (this.id > 0)
  //      this.form.value['id'] = this.id;
     

  //    const formData = new FormData();
  //    formData.append("id", this.id.toString());
  //    formData.append("contractCode", this.form.controls['contractCode'].value);
  //    formData.append("custCode", this.form.controls['custCode'].value);
  //    formData.append("contStartDate", this.utilService.selectedDate(this.form.controls['contStartDate'].value));
  //    formData.append("contEndDate", this.utilService.selectedDate(this.form.controls['contEndDate'].value));
  //    formData.append("deptCode", this.form.controls['deptCode'].value);
  //    formData.append("selectSun", this.form.controls['selectSun'].value);
  //    formData.append("selectMon", this.form.controls['selectMon'].value);
  //    formData.append("selectTue", this.form.controls['selectSun'].value);
  //    formData.append("selectWed", this.form.controls['selectMon'].value);
  //    formData.append("selectThu", this.form.controls['selectSun'].value);
  //    formData.append("selectFri", this.form.controls['selectMon'].value);
  //    formData.append("selectSat", this.form.controls['selectSun'].value);
     

  //    formData.append("timeSun", this.form.controls['timeTue'].value);
  //    formData.append("timeMon", this.form.controls['timeWed'].value);
  //    formData.append("timeTue", this.form.controls['timeThu'].value);
  //    formData.append("timeWed", this.form.controls['timeTue'].value);
  //    formData.append("timeThu", this.form.controls['timeWed'].value);
  //    formData.append("timeFri", this.form.controls['timeThu'].value);
  //    formData.append("timeSat", this.form.controls['timeFri'].value);

  //    formData.append("remarkSun", this.form.controls['timeTue'].value);
  //    formData.append("remarkMon", this.form.controls['timeWed'].value);
  //    formData.append("remarkTue", this.form.controls['timeThu'].value);
  //    formData.append("remarkWed", this.form.controls['timeTue'].value);
  //    formData.append("remarkThu", this.form.controls['timeWed'].value);
  //    formData.append("remarkFri", this.form.controls['timeThu'].value);
  //    formData.append("remarkSat", this.form.controls['timeFri'].value);

      
  //    this.apiService.post('FomCustomerContract/CreateScheduleSummary', formData)
  //      .subscribe(res => {
  //        this.utilService.OkMessage();
  //        this.reset();
  //        this.dialogRef.close(true);
  //      },
  //        error => {
  //          console.error(error);
  //          this.utilService.ShowApiErrorMessage(error);
  //        });
  //  }
  //  else
  //    this.utilService.FillUpFields();
  //}

  reset() {
  
    this.form.controls['custSiteCode'].setValue('');
    this.form.controls['contStartDate'].setValue('');
    this.form.controls['contEndDate'].setValue('');
    this.form.controls['contDeptCode'].setValue('');
    this.form.controls['contProjManager'].setValue('');
    this.form.controls['contProjSupervisor'].setValue('');
    this.form.controls['remarks'].setValue('');
    this.form.controls['contApprAuthorities'].setValue('');
    this.form.controls['IsAppreoved'].setValue(false);
    this.form.controls['IsSheduleRequired'].setValue(false);
    this.form.controls['approvedDate'].setValue('');
    this.form.controls['isActive'].setValue(false);

  }


  resetShedule() {
    //const genButton = document.getElementById("genButton") as HTMLButtonElement | null;
    //const saveButton = document.getElementById("saveButton") as HTMLButtonElement | null;
    this.form.controls.remarkSun.setValue('');
    this.form.controls.selectSun.setValue('');
    this.form.controls.timeSun.setValue('');
    this.form.controls.remarkMon.setValue('');
    this.form.controls.selectMon.setValue('');
    this.form.controls.timeMon.setValue('');
    this.form.controls.remarkTue.setValue('');
    this.form.controls.selectTue.setValue('');
    this.form.controls.timeTue.setValue('');
    this.form.controls.remarkWed.setValue('');
    this.form.controls.selectWed.setValue('');
    this.form.controls.timeWed.setValue('');
    this.form.controls.remarkThu.setValue('');
    this.form.controls.selectThu.setValue('');
    this.form.controls.timeThu.setValue('');
    this.form.controls.remarkFri.setValue('');
    this.form.controls.selectFri.setValue('');
    this.form.controls.timeFri.setValue('');
    this.form.controls.remarkSat.setValue('');
    this.form.controls.selectSat.setValue('');
    this.form.controls.timeSat.setValue('');


    //if (genButton) {
    //  genButton.disabled = true;
    //}
    //if (saveButton) {
    //  saveButton.disabled = true;
    //}

  }

  closeModel() {
    this.dialogRef.close();
  }

}
