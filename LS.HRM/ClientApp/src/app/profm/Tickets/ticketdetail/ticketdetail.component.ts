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
  selector: 'app-ticketdetail',
  templateUrl: './ticketdetail.component.html',
  styleUrls: ['./ticketdetail.component.scss']
})
export class TicketdetailComponent extends ParentFomMgtComponent implements OnInit {
  id: any;
  ticketNumber: string='';
  ticketHead: any;
  constructor(private fb: FormBuilder, private apiService: ApiService, private authService: AuthorizeService,
    private utilService: UtilityService, private notifyService: NotificationService, public dialog: MatDialog,
    public pageService: PaginationService, private sharedService: FomSharedService, private router: Router) {
    super(authService);
    this.ticketNumber = this.sharedService.ticketNumber;
  }
  //constructor(private fb: FormBuilder, private validationService: ValidationService,
  //  private apiService: ApiService, private notifyService: NotificationService, private http: HttpClient, private router: Router, private route: ActivatedRoute
  //  ,private sharedService: FomSharedService,
  //) {
  //  this.ticketNumber= this.sharedService.ticketNumber;
  //}

  ngOnInit(): void {
   
    this.getTicketDetails(this.ticketNumber)
    
  }

  getTicketDetails(ticketNumber: string) {
    this.apiService.getFomCpUrl(`FomCustomerContract/viewTicketByTicketNumber/${ticketNumber}`).subscribe((res: any) => {
      this.ticketHead = res;

    });
  }
 
}
