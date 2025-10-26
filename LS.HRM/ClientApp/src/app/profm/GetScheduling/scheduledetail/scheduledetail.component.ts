import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { ApiService } from '../../../services/api.service';
import { NotificationService } from '../../../services/notification.service';
import { FomSharedService } from '../../../services/fomShared.service';
import { ValidationService } from '../../../sharedcomponent/ValidationService';
import { ParentFomMgtComponent } from '../../../sharedcomponent/parentfommgt.component';
import { UtilityService } from '../../../services/utility.service';
import { PaginationService } from '../../../sharedcomponent/pagination.service';
import { AuthorizeService } from '../../../api-authorization/AuthorizeService';

@Component({
  selector: 'app-scheduledetail',
  templateUrl: './scheduledetail.component.html',
  styleUrls: ['./scheduledetail.component.scss']
})
export class ScheduleDetailComponent extends ParentFomMgtComponent implements OnInit {
  scheduleId: any;
  data: any;
  ticketHead: any;

  image1WithFullPath: string = '';
  image2WithFullPath: string = '';
  constructor(private fb: FormBuilder, private apiService: ApiService, private authService: AuthorizeService,
    private utilService: UtilityService, private notifyService: NotificationService, public dialog: MatDialog, public dialogRef: MatDialogRef<ScheduleDetailComponent>,
    public pageService: PaginationService, private sharedService: FomSharedService, private router: Router) {
    super(authService);
  }

  ngOnInit(): void {   
    this.getTicketDetails(this.scheduleId)    
  }

  
  getUserImagePath(path: string): string {
    return `${this.authService.GetFomCpApiEndPoint().replace('/api', '/')}CustomerContractfiles/${path}`;
  }
  getTicketDetails(id: number) {
    this.apiService.getFomCpUrl(`FomCustomerContract/viewScheduleById/${id}`).subscribe((res: any) => {
      this.ticketHead = res;
      this.image1WithFullPath = res.image1WithFullPath ? this.getUserImagePath(res.image1WithFullPath) : '';
      this.image2WithFullPath = res.image2WithFullPath ? this.getUserImagePath(res.image2WithFullPath) : '';
    });
  }
  closeModel() {
    this.dialogRef.close('');
  }
}
