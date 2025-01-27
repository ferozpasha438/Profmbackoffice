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
import * as moment from 'moment/moment';
import { GeneratedatescheduleComponent } from '../generatedateschedule/generatedateschedule.component';


@Component({
  selector: 'app-addupdatejobplanschedule',
  templateUrl: './addupdatejobplanschedule.component.html',
  styles: [
  ]
})
export class AddupdatejobplanscheduleComponent implements OnInit {
  data: any;
  savedList: Array<any> = [];

  assetChildList: Array<any> = [];
  frequencyList: Array<LanCustomSelectListItem> = [];
  canOpenTabs: boolean = false;
  isLoading: boolean = false;
  listofgeneratedschedules: Array<any> = [];

  constructor(private fb: FormBuilder, private apiService: ApiService, public dialog: MatDialog,
    private authService: AuthorizeService, private utilService: UtilityService, public dialogRef: MatDialogRef<AddupdatejobplanscheduleComponent>,
    private notifyService: NotificationService, private validationService: ValidationService) {

  };

  ngOnInit(): void {
    console.log(this.data);
    this.loadData();
    //const elArr = document.querySelectorAll('.all-date'); // all

    //  const slides = this.getAllDateElements()
    //  for (let i = 0; i < slides.length; i++) {      
    //    (slides.item(i) as HTMLInputElement).value = this.data.planStartDate;
    //  }
  }
  //getAllDateElements(): HTMLCollectionOf<HTMLInputElement> { return document.getElementsByClassName('all-date') as HTMLCollectionOf<HTMLInputElement>; }

  loadData() {
    this.isLoading = true;
    this.apiService.getall(`assetMaintenance/getFomAssetMasterChildsByAssetCode?assetCode=${this.data.assetCode}&id=${this.data.id}`).subscribe(res => {
      this.isLoading = false;
      if (res) {

        this.assetChildList = res;
        this.assetChildList.forEach(item => {
          //console.log('item.value',item.value);
          item.value = item.value ? item.value : this.data.frequency;
          item.list = [];
          item.isGenerated = false;
        });


      }
    });
    this.apiService.getall(`assetMaintenance/getJobPlanFrequecies`).subscribe(res => {
      if (res) {
        this.frequencyList = res;
      }
    });
  }
  openShcDates(item: any) {
    this.apiService.post(`assetMaintenance/calculateDatesForFrequencySelected`, { frequency: item.value, id: this.data.id, childCode: item.textTwo, planStartDate: this.utilService.selectedDate(this.data.planStartDate) }).subscribe(res => {
      if (res) {
         console.log(res);

        if (this.listofgeneratedschedules.length > 0) {
          this.listofgeneratedschedules.forEach(litem => {            
            let schItem = (res as Array<any>).find(rs => item.textTwo === litem.childCode && (rs.planStartDate as string).split('T')[0] == litem.date)
            if (schItem)
              schItem.remarks = litem.remarks;
          })
        }

        let dialogRef = this.utilService.openCrudDialog(this.dialog, GeneratedatescheduleComponent, '95%');
        (dialogRef.componentInstance as any).data = { list: res, frequency: item.value, id: this.data.id, jobPlanCode: this.data.jobPlanCode, childCode: item.textTwo, assetCode: item.text, childHasDiffFreq: this.data.childHasDiffFreq };
        dialogRef.afterClosed().subscribe(res => {
          if (res) {
            this.checkDuplicateChilds(res);
            this.listofgeneratedschedules.push(...res);
          }

        });
      }
    });
    //console.log(item);
    //this.canOpenTabs = true;
  }
  checkDuplicateChilds(array: Array<any>) {
    const singlebj = array[0];
    // console.log('singlebj', singlebj);
    this.listofgeneratedschedules = this.listofgeneratedschedules.filter(item => item.assetCode == singlebj.assetCode && item.childCode != singlebj.childCode);
    this.isGenerated(singlebj);
  }
  isGenerated(singlebj: any) {
    this.assetChildList.find(astitem => singlebj.childCode == astitem.textTwo).isGenerated = true;
    //this.listofgeneratedschedules.forEach(item => {
    //  if (this.assetChildList.find(astitem => item.childCode == astitem.childCode))

    //  this.assetChildList.find(astitem => item.childCode == astitem.childCode).isGenerated = false;
    //});
    //return this.listofgeneratedschedules.includes(childCode)
  }
  save() {
    for (var i = 0; i < this.assetChildList.length; i++) {
      if (!this.assetChildList[i].isGenerated && this.data.id <= 0) {
        this.notifyService.showError("generate '" + this.assetChildList[i].textTwo + "' schedules");
        return;
      }
    }
    //this.assetChildList.forEach(item => {
    //  console.log(item.isGenerated);
    //});
    //console.log(this.listofgeneratedschedules);
    // if()
    this.dialogRef.close(this.listofgeneratedschedules);
    //const uniqueArray = this.listofgeneratedschedules.filter(
    //  (item, index, self) =>
    //    index === self.findIndex((obj) => obj.assetCode === item.assetCode && obj.childCode === item.childCode)
    //);
    //console.log(uniqueArray);
  }

  //setFrequencyDates(freq: string): any {
  //  let mInvDate1 = moment as any;
  //  const mInvDate = mInvDate1(this.data.planStartDate);
  //  console.log(mInvDate1().add(6, 'months').calendar());
  //  //return mInvDate.format('YYYY-MM-DD') + 'T00:00:00';

  //  switch (freq) {
  //    case 'Annual':
  //      return mInvDate1().add(1, 'months').format('YYYY-MM-DD') + 'T00:00:00';
  //      break;
  //    case 'SemiAnnual':
  //      return mInvDate1().add(6, 'months').format('YYYY-MM-DD') + 'T00:00:00';
  //      break;
  //    case 'Quarterly':
  //      return mInvDate1().add(4, 'months').format('YYYY-MM-DD') + 'T00:00:00';
  //      break;
  //    case 'Monthly':
  //      return mInvDate1().add(1, 'months').format('YYYY-MM-DD') + 'T00:00:00';
  //      break;
  //    case 'Weekly':
  //      return mInvDate1().add(6, 'months').format('YYYY-MM-DD') + 'T00:00:00';
  //      break;
  //    case 'Day':
  //      return mInvDate1().add(6, 'months').format('YYYY-MM-DD') + 'T00:00:00';
  //      break;
  //  }
  //}

  //fakeArray(freq: string): any {
  //  switch (freq) {
  //    case 'Annual':
  //      return new Array(1);
  //      break;
  //    case 'SemiAnnual':
  //      return new Array(2);
  //      break;
  //    case 'Quarterly':
  //      return new Array(4);
  //      break;
  //    case 'Monthly':
  //      return new Array(12);
  //      break;
  //    case 'Weekly':
  //      return new Array(48);
  //      break;
  //    case 'Day':
  //      return new Array(365);
  //      break;
  //  }

  //}

  closeModel() {
    this.dialogRef.close();
  }
}
