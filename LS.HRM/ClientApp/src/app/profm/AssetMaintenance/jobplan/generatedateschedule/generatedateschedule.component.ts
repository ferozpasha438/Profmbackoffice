import { Component, OnInit } from '@angular/core';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { UtilityService } from '../../../../services/utility.service';
import { Moment } from 'moment';

@Component({
  selector: 'app-generatedateschedule',
  templateUrl: './generatedateschedule.component.html',
  styles: [
    `.mat-calendar-header {
      display: none!important;
    }`
  ],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class GeneratedatescheduleComponent implements OnInit {
  data: any;
  readonly exampleHeader = ExampleHeader;
  listofgeneratedschedules: Array<any> = [];
  constructor(private utilService: UtilityService, public dialog: MatDialog,
    public dialogRef: MatDialogRef<GeneratedatescheduleComponent>,) { }

  ngOnInit(): void {
   // console.log('generate_schedule ', this.data);
  }
  changeDate(idx: number, arr: any, newDate: any) {
    //arr.planStartDate = this.utilService.getFormattedDate(arr.planStartDate);
    arr.planStartDate = arr.planStartDate;
    (document.getElementById(idx.toString()) as HTMLInputElement).value = this.utilService.getFormattedDate(arr.planStartDate);
    //console.log(this.utilService.selectedDateTime(planStartDate));
    console.log(this.utilService.selectedDateTime(newDate.value));
  }

  blurchangeDate(idx: number, arr: any, newDate: any) {
    //arr.planStartDate = this.utilService.getFormattedDate(arr.planStartDate);
    arr.planStartDate = arr.planStartDate;
    (document.getElementById(idx.toString()) as HTMLInputElement).value = this.utilService.getFormattedDate(arr.planStartDate);
    //console.log(this.utilService.selectedDateTime(planStartDate));
    //console.log(this.utilService.selectedDateTime(date.value));
  }
  handleMonthSelected(normalizedMonth: Moment) {
    console.log(this.utilService.getFormattedDate(normalizedMonth));
  }
  readOnlyDate(date: any): any {
    return this.utilService.getFormattedDate(date);
  }
  save() {
    this.listofgeneratedschedules = [];
    for (var i = 1; i <= this.data.list.length; i++) {
      this.listofgeneratedschedules.push({
        'date': this.utilService.selectedDate((document.getElementById('input_' + i.toString()) as HTMLInputElement).value),
        'remarks': (document.getElementById('remarks_' + i.toString()) as HTMLInputElement).value,
        'assetCode': this.data.assetCode,
        'frequency': this.data.frequency,
        'childCode': this.data.childCode,
      });
    }
    console.log(this.listofgeneratedschedules);
    this.dialogRef.close(this.listofgeneratedschedules);
  }

  closeModel() {
    this.dialogRef.close();
  }
}


import { ChangeDetectionStrategy, OnDestroy, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { DateAdapter, MAT_DATE_FORMATS } from '@angular/material/core';
import { MatCalendar, MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { Subject } from 'rxjs';
import { startWith, takeUntil } from 'rxjs/operators';

///////** @title Datepicker with custom calendar header */
//////@Component({
//////  selector: 'datepicker-custom-header-example',
//////  templateUrl: 'datepicker-custom-header-example.html',
//////  //standalone: true,
//////  //providers: [provideNativeDateAdapter()],
////// // imports: [MatFormFieldModule, MatInputModule, MatDatepickerModule],
//////  changeDetection: ChangeDetectionStrategy.OnPush,
//////})
//////export class DatepickerCustomHeaderExample {
//////  readonly exampleHeader = ExampleHeader;
//////}

/** Custom header component for datepicker. */
@Component({
  selector: 'example-header',
  styles: [`
    .example-header {
      display: flex;
      align-items: center;
      padding: 0.5em;
    }

    .example-header-label {
      flex: 1;
      height: 1em;
      font-weight: 500;
      text-align: center;
    }
  `],
  template: `
    <div class="example-header">
     
      <span class="example-header-label">oct</span>
    
    </div>
  `,
  //  standalone: true,
  // imports: [MatButtonModule, MatIconModule],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ExampleHeader<D> implements OnDestroy {
  private _calendar = inject<MatCalendar<D>>(MatCalendar);
  private _dateAdapter = inject<DateAdapter<D>>(DateAdapter);
  private _dateFormats = inject(MAT_DATE_FORMATS);

  private _destroyed = new Subject<void>();

  //readonly periodLabel = signal('');

  constructor() {
    this._calendar.stateChanges.pipe(startWith(null), takeUntil(this._destroyed)).subscribe(() => {
      //this.periodLabel.set(
      //  this._dateAdapter
      //    .format(this._calendar.activeDate, this._dateFormats.display.monthYearLabel)
      //    .toLocaleUpperCase(),
      //);
    });
  }

  ngOnDestroy() {
    this._destroyed.next();
    this._destroyed.complete();
  }

  //previousClicked(mode: 'month' | 'year') {
  //  this._calendar.activeDate =
  //    mode === 'month'
  //      ? this._dateAdapter.addCalendarMonths(this._calendar.activeDate, -1)
  //      : this._dateAdapter.addCalendarYears(this._calendar.activeDate, -1);
  //}

  //nextClicked(mode: 'month' | 'year') {
  //  this._calendar.activeDate =
  //    mode === 'month'
  //      ? this._dateAdapter.addCalendarMonths(this._calendar.activeDate, 1)
  //      : this._dateAdapter.addCalendarYears(this._calendar.activeDate, 1);
  //}
}
