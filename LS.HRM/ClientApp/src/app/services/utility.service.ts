import { HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { Router } from "@angular/router";
import { ApiMessageDto } from "../models/CINServerMetaDataDto";
import { NotificationService } from "./notification.service";
import { ErrorMessage } from "./utility.constants";
import * as moment from 'moment/moment';
import { TranslateService } from "@ngx-translate/core";

@Injectable({
  providedIn: 'root'
})
export class UtilityService {
  constructor(private notifyService: NotificationService, private router: Router, private translate: TranslateService) {

  }

  formatToTimeSpanTime(timeObj: any): string {
    const hours = timeObj.hours < 10 ? '0' + timeObj.hours : timeObj.hours;
    const minutes = timeObj.minutes < 10 ? '0' + timeObj.minutes : timeObj.minutes;
    // const seconds = timeObj.seconds < 10 ? '0' + timeObj.seconds : timeObj.seconds;
    return hours + ':' + minutes;
  }

  getQueryString(page: number | undefined, pageCount: number | undefined, query: string | null | undefined, orderBy: string | null | undefined, approval: string = "", statusId: string = "", id: number = 0, code: string = ""): string {
    let queryParam = `Page=${encodeURIComponent("" + page)}&PageCount=${encodeURIComponent("" + pageCount)}&Query=${encodeURIComponent("" + query)}&OrderBy=${encodeURIComponent("" + orderBy)}&Approval=${encodeURIComponent("" + approval)}&statusId=${encodeURIComponent("" + statusId)}&Id=${encodeURIComponent("" + id)}&code=${encodeURIComponent("" + code)}`;
    return queryParam;
    //return params.join('');
  }
  getQueryStringWithContractData(page: number | undefined, pageCount: number | undefined, query: string | null | undefined, orderBy: string | null | undefined, contractId: number = 0, startDate: string | null | undefined, endDate: string | null | undefined, approval: string = "", statusId: string = "", id: number = 0, code: string = ""): string {
    let queryParam = `Page=${encodeURIComponent("" + page)}&PageCount=${encodeURIComponent("" + pageCount)}&Query=${encodeURIComponent("" + query)}&OrderBy=${encodeURIComponent("" + orderBy)}&ContractId=${encodeURIComponent("" + contractId)}&StartDate=${encodeURIComponent("" + startDate)}&EndDate=${encodeURIComponent("" + endDate)}&Approval=${encodeURIComponent("" + approval)}&StatusId=${encodeURIComponent("" + statusId)}&Id=${encodeURIComponent("" + id)}&Code=${encodeURIComponent("" + code)}`;
    return queryParam;
    //return params.join('');
  }

  getQueryFilterContractData(page: number | undefined, pageCount: number | undefined, query: string | null | undefined, orderBy: string | null | undefined, contractCode: string | null | undefined, startDate: string | null | undefined, endDate: string | null | undefined,deptCode: string | null | undefined): string {
    let queryParam = `Page=${encodeURIComponent("" + page)}&PageCount=${encodeURIComponent("" + pageCount)}&Query=${encodeURIComponent("" + query)}&OrderBy=${encodeURIComponent("" + orderBy)}&ContractCode=${encodeURIComponent("" + contractCode)}&StartDate=${encodeURIComponent("" + startDate)}&EndDate=${encodeURIComponent("" + endDate)}&DeptCode=${encodeURIComponent("" + deptCode)}`;
    return queryParam;
    //return params.join('');
  }



  getStudentQueryString(stuAdmNum: string = "",page: number | undefined, pageCount: number | undefined, query: string | null | undefined, orderBy: string | null | undefined, approval: string = "", statusId: string = "", id: number = 0): string {
    let queryParam = `StuAdmNum=${encodeURIComponent("" + stuAdmNum)}&Page=${encodeURIComponent("" + page)}&PageCount=${encodeURIComponent("" + pageCount)}&Query=${encodeURIComponent("" + query)}&OrderBy=${encodeURIComponent("" + orderBy)}&Approval=${encodeURIComponent("" + approval)}&statusId=${encodeURIComponent("" + statusId)}&Id=${encodeURIComponent("" + id)}`;
    return queryParam;
  }
  getTeacherQueryString(teacherCode: string = "", page: number | undefined, pageCount: number | undefined, query: string | null | undefined, orderBy: string | null | undefined, approval: string = "", statusId: string = "", id: number = 0): string {
    let queryParam = `TeacherCode=${encodeURIComponent("" + teacherCode)}&Page=${encodeURIComponent("" + page)}&PageCount=${encodeURIComponent("" + pageCount)}&Query=${encodeURIComponent("" + query)}&OrderBy=${encodeURIComponent("" + orderBy)}&Approval=${encodeURIComponent("" + approval)}&statusId=${encodeURIComponent("" + statusId)}&Id=${encodeURIComponent("" + id)}`;
    return queryParam;
  }

  getOprQueryString(page: number | undefined, pageCount: number | undefined, query: string | null | undefined, orderBy: string | null | undefined, approval: string = "", statusId: string = "", id: number = 0, code: string = "",listType:string=""): string {
    let queryParam = `Page=${encodeURIComponent("" + page)}&PageCount=${encodeURIComponent("" + pageCount)}&Query=${encodeURIComponent("" + query)}&OrderBy=${encodeURIComponent("" + orderBy)}&Approval=${encodeURIComponent("" + approval)}&statusId=${encodeURIComponent("" + statusId)}&Id=${encodeURIComponent("" + id)}&code=${encodeURIComponent("" + code)}&listType=${encodeURIComponent("" + listType)}`;
    return queryParam;
    //return params.join('');
  }
  getCalendarQueryString(branchCode: string | undefined, startDate: any | undefined, endDate: any | undefined): string {
    let queryParam = `BranchCode=${encodeURIComponent("" + branchCode)}&StartDate=${encodeURIComponent("" + startDate)}&EndDate=${encodeURIComponent("" + endDate)}`;
    return queryParam;
  }

  getPrintForLocale = (printContent: HTMLElement) => `<div dir='${this.isArabic() ? 'rtl' : 'ltr'}'>${printContent.innerHTML}</div>`
  printForLocale(printContent: HTMLElement) {         
      const WindowPrt: any = window.open('', '', 'left=0,top=0,width=2000,height=1000,toolbar=0,scrollbars=0,status=0');
      setTimeout(() => {
        WindowPrt.document.write(this.getPrintForLocale(printContent));
        WindowPrt.document.close();
        WindowPrt.focus();
        WindowPrt.print();
        WindowPrt.close();
      }, 50);    
  }

  selectedDateDays = (date: any): number => date ? this.getDate(date).getDate() : 0;
  selectedDate = (date: any): string => this.getCommonDate(date);
  selectedDateTime = (date: any): string => this.getCommonDate(date, 'T00:00:00');

  getStrtingYearDate(): any {
    return `${new Date().getFullYear()}-01-01T00:00:00`;
  }

  getCurrentDate(): any{
    let mInvDate1 = moment as any;
    const mInvDate = mInvDate1(new Date().toLocaleDateString());
    return mInvDate.format('YYYY-MM-DD') + 'T00:00:00';
  }

  getCommonDate(date: any, time: string = '') {
    if (date) {
      let selectedDate = this.getDate(date);
      const datePart = (selectedDate.getDate() < 10 ? '0' : '');
      const monthPart = (selectedDate.getMonth() + 1 < 10 ? '0' : '');
      return `${selectedDate.getFullYear()}-${monthPart}${selectedDate.getMonth() + 1}-${datePart}${selectedDate.getDate()}${time}`;
    }
    return '';
  }

  getDate(date: any) {
    return new Date(date);
  }

  getDir = (): string => this.isArabic() ? 'rtl' : 'ltr';

  autoDelay = (): number => 500;

  selectedLanguage() {
    return localStorage.getItem('language') ?? 'en-US'
  }

  isArabic() {
    return this.selectedLanguage() == 'ar';
  }

  Lang = (): string => this.selectedLanguage();

  //SuccessMessage1 = (): string => "Successful";
  //FillUpMessage1 = (): string => "Fill up all fields";
  ValidateMessage() { this.notifyService.showSuccess(this.isArabic() ? 'الرجاء التحقق من صحة اسم المستخدم' : 'Please validate username'); }
  OkMessage() { this.notifyService.showSuccess(this.isArabic() ? 'ناجح' : 'Successful'); }
  FillUpFields() { this.notifyService.showError(this.isArabic() ? 'املأ جميع الحقول' : 'Fill up all fields'); }
  logoUrl = (): string => localStorage.getItem('logoURL') as string ?? '';
  localizeError = (key: string) => this.notifyService.showError((this.isArabic() ? 'املأ ' : 'Fill ') +  this.translate.instant(key) ?? '');



  //openCrudDialogOne(dialog: any, component: any, width: string) {
  //  const dialogRef = dialog.open(component, {
  //    disableClose: true,
  //    width: width
  //  });
  //  return dialogRef;
  //}


  openCrudDialog(dialog: MatDialog, component: any, width: string = '80%') {
    const widthNumber = width.includes('%') ? width.split('%')[0] : width;
    const dialogRef = dialog.open(component, {
      disableClose: true,
      width: `${widthNumber}%`,
      maxWidth: `${widthNumber}vw`,
      height: '90%',
      direction: this.isArabic() ? "rtl" :"ltr"
    });
    return dialogRef;
  }
  openDeleteConfirmDialog(dialog: MatDialog, component: any, width: string = '300px') {
    const dialogRef = dialog.open(component, {
      disableClose: true,
      width: width,
      direction: this.isArabic() ? "rtl" : "ltr"
    });
    return dialogRef;
  }
  openDialogCongif(dialog: MatDialog, component: any, width: number = 100) {
    const dialogRef = dialog.open(component, {
      disableClose: true,
      width: `${width}%`,
      maxWidth: `${width}vw`,
      height: '100%',
      direction: this.isArabic() ? "rtl" : "ltr"
    });
    return dialogRef;
  }
   openNormalDialogConfig(dialog: MatDialog, component: any, width: number = 100) {
    const dialogRef = dialog.open(component, {
      disableClose: true,
      width: `${width}%`,
      maxWidth: `${width}vw`,
      height: '100%',      
    });
    return dialogRef;
  }

  //openSnackBar(message: string) {
  //  this._snackBar.open(message, 'Close', {
  //    duration: 2500,
  //  });
  //}

  ShowApiErrorMessage(error: HttpErrorResponse) {
    try {
      let httpError = error.error as ApiMessageDto;
      if (error && error.status === 401) {
        localStorage.clear();
        // window.location.href = "/";
        location.replace('');
      }
      else
        this.notifyService.showError(httpError?.message, "Error")

      //console.log(Object.keys(error.error));

      //Object.keys(error.error).forEach(key => {
      //  console.log('Key : ' + key + ', Error : ' + error.error[key])
      //})

      //if (error.error ) {
      //  alert(error.error.message);
      //alert(error.error.title);
      //}
      //else {
      //  alert(ErrorMessage);
      //}      

    } catch (e) {
      if (error && error.status === 401) {
        localStorage.clear();
        //window.location.href = "/";
        location.replace('');
      }
      else
        this.notifyService.showError(ErrorMessage, "Error")
    }
  }

  hasValue(param: string): boolean {
    return param && param.trim() !== '' ? true : false;;
  }

  removeSqueres(param: string): string {
    return param && param.trim() !== '' ? param.split(')')[0].replace('(', '') : '';
  }

  hasPaymenRoute(): boolean {
    const paymenRoute = localStorage.getItem('hasPaymenRoute');
    return paymenRoute !== null;
  }



}
