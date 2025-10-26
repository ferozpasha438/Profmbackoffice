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
  deptCodeList: Array<any> = [];
  shiftList: Array<CustomSelectListItem> = [{ text: 'Shift A', value: 'A' }, { text: 'Shift B', value: 'B' }, { text: 'Shift C', value: 'C' }, { text: 'Shift D', value: 'D' }];
  moreItemsIndex: number = 1;
  taskremarks: string = '';
  remarkList: any[] = [];
  tempremarkList: any[] = [];
  remarkItem: any = { column: '', remarks: '', index: null };
  remarkCol: string = '';
  remarkIndex: number = 0;
  isViewed: boolean = true;
  setEditSheduleLoading: boolean = false;

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
        'contDeptCode': [''],

        'selectSun': [false],
        'selectMon': [false],
        'selectTue': [false],
        'selectWed': [false],
        'selectThu': [false],
        'selectFri': [false],
        'selectSat': [false],

        'timeSun0': ['00:00'],
        'timeMon0': ['00:00'],
        'timeTue0': ['00:00'],
        'timeWed0': ['00:00'],
        'timeThu0': ['00:00'],
        'timeFri0': ['00:00'],
        'timeSat0': ['00:00'],
        'selectSunShft0': [''],
        'selectMonShft0': [''],
        'selectTueShft0': [''],
        'selectWedShft0': [''],
        'selectThuShft0': [''],
        'selectFriShft0': [''],
        'selectSatShft0': [''],
        'remarkSun0': [''],
        'remarkMon0': [''],
        'remarkTue0': [''],
        'remarkWed0': [''],
        'remarkThu0': [''],
        'remarkFri0': [''],
        'remarkSat0': [''],

        //1st Section
        'timeSun1': ['00:00'],
        'timeMon1': ['00:00'],
        'timeTue1': ['00:00'],
        'timeWed1': ['00:00'],
        'timeThu1': ['00:00'],
        'timeFri1': ['00:00'],
        'timeSat1': ['00:00'],
        'selectSunShft1': [''],
        'selectMonShft1': [''],
        'selectTueShft1': [''],
        'selectWedShft1': [''],
        'selectThuShft1': [''],
        'selectFriShft1': [''],
        'selectSatShft1': [''],
        'remarkSun1': [''],
        'remarkMon1': [''],
        'remarkTue1': [''],
        'remarkWed1': [''],
        'remarkThu1': [''],
        'remarkFri1': [''],
        'remarkSat1': [''],

        //2nd Section
        'timeSun2': ['00:00'],
        'timeMon2': ['00:00'],
        'timeTue2': ['00:00'],
        'timeWed2': ['00:00'],
        'timeThu2': ['00:00'],
        'timeFri2': ['00:00'],
        'timeSat2': ['00:00'],
        'selectSunShft2': [''],
        'selectMonShft2': [''],
        'selectTueShft2': [''],
        'selectWedShft2': [''],
        'selectThuShft2': [''],
        'selectFriShft2': [''],
        'selectSatShft2': [''],
        'remarkSun2': [''],
        'remarkMon2': [''],
        'remarkTue2': [''],
        'remarkWed2': [''],
        'remarkThu2': [''],
        'remarkFri2': [''],
        'remarkSat2': [''],

        //3rd Section
        'timeSun3': ['00:00'],
        'timeMon3': ['00:00'],
        'timeTue3': ['00:00'],
        'timeWed3': ['00:00'],
        'timeThu3': ['00:00'],
        'timeFri3': ['00:00'],
        'timeSat3': ['00:00'],
        'selectSunShft3': [''],
        'selectMonShft3': [''],
        'selectTueShft3': [''],
        'selectWedShft3': [''],
        'selectThuShft3': [''],
        'selectFriShft3': [''],
        'selectSatShft3': [''],
        'remarkSun3': [''],
        'remarkMon3': [''],
        'remarkTue3': [''],
        'remarkWed3': [''],
        'remarkThu3': [''],
        'remarkFri3': [''],
        'remarkSat3': [''],
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


  getMultiSelectedShifts(shift: string) {
    return shift;//? this.shiftList.filter(item => shift.includes(item.value)) : '';
  }

  getListOfDesOrTasks(remarks: string): any[] {    
    if (this.utilService.hasValue(remarks)) {
      return remarks.split('✓');
    }
    return [];

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
    this.setEditSheduleLoading = true;
    this.apiService.getall(`FomCustomerContract/GetScheduleById/${deptCode}/${contractCode}`).subscribe(res => {
      console.log(res);
      if (res) {
        this.setEditSheduleLoading = false;
        this.isSchGenerated = res['isSchGenerated'];
        // this.resetShedule();

        this.form.value['id'] = res['id'];
        this.shId = res['id'];

        let shifts = res['tableRows'] as any[];

        shifts.forEach(item => {

          this.moreItemsIndex = (item['index'] ?? 0) + 1;
          let index = Number(item['index']);
          if (item['weekDay'] == "Sunday") {
            this.form.controls['selectSun'].setValue(item['isActive']);
            this.form.controls[`timeSun${index}`].setValue(item['time']);
            this.form.controls[`selectSunShft${index}`].setValue(this.getMultiSelectedShifts(item['uiShifts']));
            this.form.controls[`remarkSun${index}`].setValue(item['remarks']);

            this.getListOfDesOrTasks(item['remarks']).forEach(item => {
              this.setEditAddOrViewTask(`remarkSun${index}`, item)
            });
          }
          else if (item['weekDay'] == "Monday") {
            this.form.controls['selectMon'].setValue(item['isActive']);
            this.form.controls[`timeMon${index}`].setValue(item['time']);
            this.form.controls[`selectMonShft${index}`].setValue(this.getMultiSelectedShifts(item['uiShifts']));
            this.form.controls[`remarkMon${index}`].setValue(item['remarks']);

            this.getListOfDesOrTasks(item['remarks']).forEach(item => {
              this.setEditAddOrViewTask(`remarkMon${index}`, item)
            });
          }
          else if (item['weekDay'] == "Tuesday") {
            this.form.controls['selectTue'].setValue(item['isActive']);
            this.form.controls[`timeTue${index}`].setValue(item['time']);
            this.form.controls[`selectTueShft${index}`].setValue(this.getMultiSelectedShifts(item['uiShifts']));
            this.form.controls[`remarkTue${index}`].setValue(item['remarks']);

            this.getListOfDesOrTasks(item['remarks']).forEach(item => {
              this.setEditAddOrViewTask(`remarkTue${index}`, item)
            });
          }
          else if (item['weekDay'] == "Wednesday") {
            this.form.controls['selectWed'].setValue(item['isActive']);
            this.form.controls[`timeWed${index}`].setValue(item['time']);
            this.form.controls[`selectWedShft${index}`].setValue(this.getMultiSelectedShifts(item['uiShifts']));
            this.form.controls[`remarkWed${index}`].setValue(item['remarks']);

            this.getListOfDesOrTasks(item['remarks']).forEach(item => {
              this.setEditAddOrViewTask(`remarkWed${index}`, item)
            });
          }
          else if (item['weekDay'] == "Thursday") {
            this.form.controls['selectThu'].setValue(item['isActive']);
            this.form.controls[`timeThu${index}`].setValue(item['time']);
            this.form.controls[`selectThuShft${index}`].setValue(this.getMultiSelectedShifts(item['uiShifts']));
            this.form.controls[`remarkThu${index}`].setValue(item['remarks']);

            this.getListOfDesOrTasks(item['remarks']).forEach(item => {
              this.setEditAddOrViewTask(`remarkThu${index}`, item)
            });
          }
          else if (item['weekDay'] == "Friday") {
            this.form.controls['selectFri'].setValue(item['isActive']);
            this.form.controls[`timeFri${index}`].setValue(item['time']);
            this.form.controls[`selectFriShft${index}`].setValue(this.getMultiSelectedShifts(item['uiShifts']));
            this.form.controls[`remarkFri${index}`].setValue(item['remarks']);

            this.getListOfDesOrTasks(item['remarks']).forEach(item => {
              this.setEditAddOrViewTask(`remarkFri${index}`, item)
            });
          }
          else if (item['weekDay'] == "Saturday") {
            this.form.controls['selectSat'].setValue(item['isActive']);
            this.form.controls[`timeSat${index}`].setValue(item['time']);
            this.form.controls[`selectSatShft${index}`].setValue(this.getMultiSelectedShifts(item['uiShifts']));
            this.form.controls[`remarkSat${index}`].setValue(item['remarks']);

            this.getListOfDesOrTasks(item['remarks']).forEach(item => {
              this.setEditAddOrViewTask(`remarkSat${index}`, item);              
            });

          }
        });


        //if (res['tableRows'][0]['weekDay'] == "Sunday") {
        //  this.form.controls.selectSun.setValue(res['tableRows'][0]['isActive']);
        //  this.form.controls.timeSun.setValue(res['tableRows'][0]['time']);
        //  this.form.controls.selectSunShft.setValue(this.getMultiSelectedShifts(res['tableRows'][0]['uiShifts']));
        //  this.form.controls.remarkSun.setValue(res['tableRows'][0]['remarks']);
        //}

        //if (res['tableRows'][1]['weekDay'] == "Monday") {
        //  this.form.controls.selectMon.setValue(res['tableRows'][1]['isActive']);
        //  this.form.controls.timeMon.setValue(res['tableRows'][1]['time']);
        //  this.form.controls.selectMonShft.setValue(this.getMultiSelectedShifts(res['tableRows'][1]['uiShifts']));
        //  this.form.controls.remarkMon.setValue(res['tableRows'][1]['remarks']);
        //}
        //if (res['tableRows'][2]['weekDay'] == "Tuesday") {
        //  this.form.controls.selectTue.setValue(res['tableRows'][2]['isActive']);
        //  this.form.controls.timeTue.setValue(res['tableRows'][2]['time']);
        //  this.form.controls.selectTueShft.setValue(this.getMultiSelectedShifts(res['tableRows'][2]['uiShifts']));
        //  this.form.controls.remarkTue.setValue(res['tableRows'][2]['remarks']);
        //}
        //if (res['tableRows'][3]['weekDay'] == "Wednesday") {
        //  this.form.controls.selectWed.setValue(res['tableRows'][3]['isActive']);
        //  this.form.controls.timeWed.setValue(res['tableRows'][3]['time']);
        //  this.form.controls.selectWedShft.setValue(this.getMultiSelectedShifts(res['tableRows'][3]['uiShifts']));
        //  this.form.controls.remarkWed.setValue(res['tableRows'][3]['remarks']);
        //}
        //if (res['tableRows'][4]['weekDay'] == "Thursday") {
        //  this.form.controls.selectThu.setValue(res['tableRows'][4]['isActive']);
        //  this.form.controls.timeThu.setValue(res['tableRows'][4]['time']);
        //  this.form.controls.selectThuShft.setValue(this.getMultiSelectedShifts(res['tableRows'][4]['uiShifts']));
        //  this.form.controls.remarkThu.setValue(res['tableRows'][4]['remarks']);
        //}
        //if (res['tableRows'][5]['weekDay'] == "Friday") {
        //  this.form.controls.selectFri.setValue(res['tableRows'][5]['isActive']);
        //  this.form.controls.timeFri.setValue(res['tableRows'][5]['time']);
        //  this.form.controls.selectFriShft.setValue(this.getMultiSelectedShifts(res['tableRows'][5]['uiShifts']));
        //  this.form.controls.remarkFri.setValue(res['tableRows'][5]['remarks']);
        //}
        //if (res['tableRows'][6]['weekDay'] == "Saturday") {
        //  this.form.controls.selectSat.setValue(res['tableRows'][6]['isActive']);
        //  this.form.controls.timeSat.setValue(res['tableRows'][6]['time']);
        //  this.form.controls.selectSatShft.setValue(this.getMultiSelectedShifts(res['tableRows'][6]['uiShifts']));
        //  this.form.controls.remarkSat.setValue(res['tableRows'][6]['remarks']);
        //}

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
        this.setEditSheduleLoading = false;
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
  addMore() {
    if (this.moreItemsIndex <= 3)
      this.moreItemsIndex++;
  }
  removeMore() {
    this.moreItemsIndex--;
  }
  submit() {

    if (this.id > 0)
      if (this.shId > 0) {
        this.form.value['id'] = this.shId;
      } else {
        this.form.value['id'] = this.id;
      }


    if (this.form.valid) {

      const formData = {
        id: this.shId,

        contractCode: this.form.controls['contractCode'].value,
        custCode: this.form.controls['custCode'].value,
        //contStartDate: this.utilService.selectedDate(this.form.controls['contStartDate'].value),
        //contEndDate: this.utilService.selectedDate(this.form.controls['contEndDate'].value),
        deptCode: this.form.controls['deptCode'].value,
        tableRows: this.getShiftTimings(),
        moreItemsIndex: this.moreItemsIndex
        // Add more header fields as needed
      };
      this.apiService.post('FomCustomerContract/CreateScheduleSummary', formData)
        .subscribe(res => {
          //this.setDefault();
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
      this.notifyService.showError('Fill up all fields and Shifts');
    }
  }
  setDefault() {
    this.remarkList = this.tempremarkList = [];
    this.remarkIndex = 0;
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
      const summaryData = {
        id: this.shId,
        contractCode: this.form.controls['contractCode'].value,
        custCode: this.form.controls['custCode'].value,
        contStartDate: this.utilService.selectedDate(this.form.controls['contStartDate'].value),
        contEndDate: this.utilService.selectedDate(this.form.controls['contEndDate'].value),
        deptCode: this.form.controls['deptCode'].value,
        TableRows: this.getShiftTimings(),
        moreItemsIndex: this.moreItemsIndex
        // Add more header fields as needed
      };
      this.apiService.post('FomCustomerContract/GenerateScheduleSummary', summaryData)
        .subscribe(res => {
          this.setDefault();
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
      this.notifyService.showError('Fill up all fields and Shifts');
    }
  }


  getDescriptionOrTask(remarkColumnName: string): string {
    let remarksObjList = this.remarkList?.filter(item => item.column == remarkColumnName) ?? [];
    return remarksObjList.length > 0 ? remarksObjList.map(item => item.remarks).join('✓') : '';
  }
  getShiftTimings(): any[] {
    const detailsData = [
      { weekDay: 'Sunday', index: 0, time: this.form.controls['timeSun0'].value, remarks: this.getDescriptionOrTask('remarkSun0'), isActive: !!this.form.controls['selectSun'].value || false, shifts: this.form.controls['selectSunShft0'].value },
      { weekDay: 'Monday', index: 0, time: this.form.controls['timeMon0'].value, remarks: this.getDescriptionOrTask('remarkMon0'), isActive: !!this.form.controls['selectMon'].value || false, shifts: this.form.controls['selectMonShft0'].value },
      { weekDay: 'Tuesday', index: 0, time: this.form.controls['timeTue0'].value, remarks: this.getDescriptionOrTask('remarkTue0'), isActive: !!this.form.controls['selectTue'].value || false, shifts: this.form.controls['selectTueShft0'].value },
      { weekDay: 'Wednesday', index: 0, time: this.form.controls['timeWed0'].value, remarks: this.getDescriptionOrTask('remarkWed0'), isActive: !!this.form.controls['selectWed'].value || false, shifts: this.form.controls['selectWedShft0'].value },
      { weekDay: 'Thursday', index: 0, time: this.form.controls['timeThu0'].value, remarks: this.getDescriptionOrTask('remarkThu0'), isActive: !!this.form.controls['selectThu'].value || false, shifts: this.form.controls['selectThuShft0'].value },
      { weekDay: 'Friday', index: 0, time: this.form.controls['timeFri0'].value, remarks: this.getDescriptionOrTask('remarkFri0'), isActive: !!this.form.controls['selectFri'].value || false, shifts: this.form.controls['selectFriShft0'].value },
      { weekDay: 'Saturday', index: 0, time: this.form.controls['timeSat0'].value, remarks: this.getDescriptionOrTask('remarkSat0'), isActive: !!this.form.controls['selectSat'].value || false, shifts: this.form.controls['selectSatShft0'].value },

      //1st Section              
      { weekDay: 'Sunday', index: 1, time: this.form.controls['timeSun1'].value, remarks: this.getDescriptionOrTask('remarkSun1'), isActive: !!this.form.controls['selectSun'].value || false, shifts: this.form.controls['selectSunShft1'].value },
      { weekDay: 'Monday', index: 1, time: this.form.controls['timeMon1'].value, remarks: this.getDescriptionOrTask('remarkMon1'), isActive: !!this.form.controls['selectMon'].value || false, shifts: this.form.controls['selectMonShft1'].value },
      { weekDay: 'Tuesday', index: 1, time: this.form.controls['timeTue1'].value, remarks: this.getDescriptionOrTask('remarkTue1'), isActive: !!this.form.controls['selectTue'].value || false, shifts: this.form.controls['selectTueShft1'].value },
      { weekDay: 'Wednesday', index: 1, time: this.form.controls['timeWed1'].value, remarks: this.getDescriptionOrTask('remarkWed1'), isActive: !!this.form.controls['selectWed'].value || false, shifts: this.form.controls['selectWedShft1'].value },
      { weekDay: 'Thursday', index: 1, time: this.form.controls['timeThu1'].value, remarks: this.getDescriptionOrTask('remarkThu1'), isActive: !!this.form.controls['selectThu'].value || false, shifts: this.form.controls['selectThuShft1'].value },
      { weekDay: 'Friday', index: 1, time: this.form.controls['timeFri1'].value, remarks: this.getDescriptionOrTask('remarkFri1'), isActive: !!this.form.controls['selectFri'].value || false, shifts: this.form.controls['selectFriShft1'].value },
      { weekDay: 'Saturday', index: 1, time: this.form.controls['timeSat1'].value, remarks: this.getDescriptionOrTask('remarkSat1'), isActive: !!this.form.controls['selectSat'].value || false, shifts: this.form.controls['selectSatShft1'].value },

      //2rd Section        
      { weekDay: 'Sunday', index: 2, time: this.form.controls['timeSun2'].value, remarks: this.getDescriptionOrTask('remarkSun2'), isActive: !!this.form.controls['selectSun'].value || false, shifts: this.form.controls['selectSunShft2'].value },
      { weekDay: 'Monday', index: 2, time: this.form.controls['timeMon2'].value, remarks: this.getDescriptionOrTask('remarkMon2'), isActive: !!this.form.controls['selectMon'].value || false, shifts: this.form.controls['selectMonShft2'].value },
      { weekDay: 'Tuesday', index: 2, time: this.form.controls['timeTue2'].value, remarks: this.getDescriptionOrTask('remarkTue2'), isActive: !!this.form.controls['selectTue'].value || false, shifts: this.form.controls['selectTueShft2'].value },
      { weekDay: 'Wednesday', index: 2, time: this.form.controls['timeWed2'].value, remarks: this.getDescriptionOrTask('remarkWed2'), isActive: !!this.form.controls['selectWed'].value || false, shifts: this.form.controls['selectWedShft2'].value },
      { weekDay: 'Thursday', index: 2, time: this.form.controls['timeThu2'].value, remarks: this.getDescriptionOrTask('remarkThu2'), isActive: !!this.form.controls['selectThu'].value || false, shifts: this.form.controls['selectThuShft2'].value },
      { weekDay: 'Friday', index: 2, time: this.form.controls['timeFri2'].value, remarks: this.getDescriptionOrTask('remarkFri2'), isActive: !!this.form.controls['selectFri'].value || false, shifts: this.form.controls['selectFriShft2'].value },
      { weekDay: 'Saturday', index: 2, time: this.form.controls['timeSat2'].value, remarks: this.getDescriptionOrTask('remarkSat2'), isActive: !!this.form.controls['selectSat'].value || false, shifts: this.form.controls['selectSatShft2'].value },

      //3rd Section        
      { weekDay: 'Sunday', index: 3, time: this.form.controls['timeSun3'].value, remarks: this.getDescriptionOrTask('remarkSun3'), isActive: !!this.form.controls['selectSun'].value || false, shifts: this.form.controls['selectSunShft3'].value },
      { weekDay: 'Monday', index: 3, time: this.form.controls['timeMon3'].value, remarks: this.getDescriptionOrTask('remarkMon3'), isActive: !!this.form.controls['selectMon'].value || false, shifts: this.form.controls['selectMonShft3'].value },
      { weekDay: 'Tuesday', index: 3, time: this.form.controls['timeTue3'].value, remarks: this.getDescriptionOrTask('remarkTue3'), isActive: !!this.form.controls['selectTue'].value || false, shifts: this.form.controls['selectTueShft3'].value },
      { weekDay: 'Wednesday', index: 3, time: this.form.controls['timeWed3'].value, remarks: this.getDescriptionOrTask('remarkWed3'), isActive: !!this.form.controls['selectWed'].value || false, shifts: this.form.controls['selectWedShft3'].value },
      { weekDay: 'Thursday', index: 3, time: this.form.controls['timeThu3'].value, remarks: this.getDescriptionOrTask('remarkThu3'), isActive: !!this.form.controls['selectThu'].value || false, shifts: this.form.controls['selectThuShft3'].value },
      { weekDay: 'Friday', index: 3, time: this.form.controls['timeFri3'].value, remarks: this.getDescriptionOrTask('remarkFri3'), isActive: !!this.form.controls['selectFri'].value || false, shifts: this.form.controls['selectFriShft3'].value },
      { weekDay: 'Saturday', index: 3, time: this.form.controls['timeSat3'].value, remarks: this.getDescriptionOrTask('remarkSat3'), isActive: !!this.form.controls['selectSat'].value || false, shifts: this.form.controls['selectSatShft3'].value },
    ];

    return detailsData;
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


    //this.form.controls.selectSun.setValue(false);
    //this.form.controls.remarkSun.setValue('');
    //this.form.controls.timeSun.setValue('');
    //this.form.controls.remarkMon.setValue('');
    //this.form.controls.selectMon.setValue(false);
    //this.form.controls.timeMon.setValue('');
    //this.form.controls.remarkTue.setValue('');
    //this.form.controls.selectTue.setValue(false);
    //this.form.controls.timeTue.setValue('');
    //this.form.controls.remarkWed.setValue('');
    //this.form.controls.selectWed.setValue(false);
    //this.form.controls.timeWed.setValue('');
    //this.form.controls.remarkThu.setValue('');
    //this.form.controls.selectThu.setValue(false);
    //this.form.controls.timeThu.setValue('');
    //this.form.controls.remarkFri.setValue('');
    //this.form.controls.selectFri.setValue(false);
    //this.form.controls.timeFri.setValue('');
    //this.form.controls.remarkSat.setValue('');
    //this.form.controls.selectSat.setValue(false);
    //this.form.controls.timeSat.setValue('');


    //if (genButton) {
    //  genButton.disabled = true;
    //}
    //if (saveButton) {
    //  saveButton.disabled = true;
    //}

  }

  setEditAddOrViewTask(remarkcol: string, remarks:string, isView: boolean = false) {
    this.remarkCol = remarkcol;
    //this.remarkIndex = index;
    this.isViewed = isView;
    this.taskremarks = remarks;
    this.saveTask(1);

  }
  addOrViewTask(remarkcol: string, index: number, isView: boolean = false) {    
    this.remarkCol = remarkcol;
    //this.remarkIndex = index;
    this.isViewed = isView;
    this.tempremarkList = this.remarkList?.filter(item => item.column === remarkcol) ?? [];
  }
  //pushDesOrTaskData() {
  //  const remarkObj = { column: this.remarkCol, remarks: this.taskremarks, index: this.remarkIndex++ };
  //  this.remarkList.push(remarkObj);
  //}
  saveTask(saveMode: number) {
    if (this.taskremarks.trim().length > 0) {
      const remarkObj = { column: this.remarkCol, remarks: this.taskremarks, index: this.remarkIndex++ };
      this.remarkList.push(remarkObj);
      this.tempremarkList = this.filterItems(remarkObj);
      if (saveMode === 0)
        this.notifyService.showSuccess("Description/Task added");
      this.taskremarks = '';
    }
    else
      this.notifyService.showError("Enter Description/Task");
  }
  removeItem(remarkObj: any) {
    this.remarkList = this.deletefilterItems(remarkObj);
    this.tempremarkList = this.filterItems(remarkObj);
  }
  display() {
    console.log(this.remarkList, this.tempremarkList);
  }

  filterItems = (remark: any) => this.remarkList?.filter(item => item.column === remark.column);
  deletefilterItems = (remark: any) => this.remarkList?.filter(item => item.index !== remark.index);

  removeBModal() {
    this.taskremarks = '';
    this.isViewed = false;
  }

  closeModel() {
    this.dialogRef.close();
  }

}
