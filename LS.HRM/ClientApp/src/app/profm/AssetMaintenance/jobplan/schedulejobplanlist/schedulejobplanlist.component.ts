import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatCalendar, MatCalendarCell, MatCalendarCellCssClasses } from '@angular/material/datepicker';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { Moment } from 'moment';
import { AuthorizeService } from 'src/app/api-authorization/AuthorizeService';
import { ApiService } from 'src/app/services/api.service';
import { NotificationService } from 'src/app/services/notification.service';
import { DBOperation } from 'src/app/services/utility.constants';
import { UtilityService } from 'src/app/services/utility.service';
import { DeleteConfirmDialogComponent } from 'src/app/sharedcomponent/delete-confirm-dialog';
import { PaginationService } from 'src/app/sharedcomponent/pagination.service';
import { ParentB2CFrontComponent } from '../../../../sharedcomponent/parentb2cfront.component';
import { ValidationService } from '../../../../sharedcomponent/ValidationService';
import { JobplanschedulingComponent } from '../shared/jobplanscheduling/jobplanscheduling.component';


@Component({
  selector: 'app-schedulejobplanlist',
  templateUrl: './schedulejobplanlist.component.html',
  styles: [
  ]
})
export class SchedulejobplanlistComponent extends ParentB2CFrontComponent {
  constructor(private fb: FormBuilder, private apiService: ApiService, private translate: TranslateService,
    private authService: AuthorizeService, private utilService: UtilityService,
    private notifyService: NotificationService, public dialog: MatDialog, public pageService: PaginationService, private validationService: ValidationService, private router: Router) {
    super(authService)
    //this.utilService.openCrudDialog(this.dialog, JobplanschedulingComponent,'100%');
  }
}
