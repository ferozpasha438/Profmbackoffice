import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { AuthorizeService } from 'src/app/api-authorization/AuthorizeService';
import { ApiService } from 'src/app/services/api.service';
import { NotificationService } from 'src/app/services/notification.service';
import { UtilityService } from 'src/app/services/utility.service';
import { PaginationService } from 'src/app/sharedcomponent/pagination.service';
import { FomSharedService } from '../../services/fomShared.service';
import { ParentB2CComponent } from '../../sharedcomponent/parentb2c.component';
import { CustomSelectListItem } from '../../models/MenuItemListDto';
import { ParentB2CFrontComponent } from '../../sharedcomponent/parentb2cfront.component';


@Component({
  selector: 'app-btcresourceallocate',
  templateUrl: './btcresourceallocate.component.html',
  styles: [
  ]
})
export class BtcresourceallocateComponent extends ParentB2CFrontComponent implements OnInit {

  row: any;
  resources: Array<CustomSelectListItem> = [];
  ticketNumber: string = '';
  resource: string = '';
  approvedDate: any;
  constructor(private fb: FormBuilder, private apiService: ApiService, private authService: AuthorizeService, public dialogRef: MatDialogRef<BtcresourceallocateComponent>,
    private utilService: UtilityService, private notifyService: NotificationService, public dialog: MatDialog,
    public pageService: PaginationService, private sharedService: FomSharedService, private router: Router) {
    super(authService);
  }

  ngOnInit(): void {
    this.loadResources();
    this.ticketNumber = this.row.ticketNumber;

  }

  loadResources() {
    this.apiService.getall('fomMobB2CService/getAssignResourceSelectList').subscribe(res => {
      this.resources = res;
    });
  }

  Submit() {
    if (this.utilService.hasValue(this.ticketNumber) && this.utilService.hasValue(this.resource)) { // && this.utilService.hasValue(this.approvedDate)) {      
      const date = this.approvedDate ? this.utilService.selectedDateTime(this.approvedDate) : null;
      const data = { ticketNumber: this.ticketNumber, resCode: this.resource, approvedDate: date };
      console.log(data);
      this.apiService.post('fomMobB2CService/createAssignresource', data)
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

  closeModel() {
    this.dialogRef.close();
  }
}
