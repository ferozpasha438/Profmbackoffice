import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../../../services/api.service';
import { UtilityService } from '../../../../services/utility.service';
import { NotificationService } from '../../../../services/notification.service';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { AuthorizeService } from '../../../../api-authorization/AuthorizeService';
import { AssetclosinginfoComponent } from '../assetclosinginfo/assetclosinginfo.component';

@Component({
  selector: 'app-assetjoborderchilditems',
  templateUrl: './assetjoborderchilditems.component.html',
  styles: [
  ]
})
export class AssetjoborderchilditemsComponent implements OnInit {
  jobPlanCode: string = '';
  Status: string = 'P';
  isShowList: boolean = true;

  childItems: Array<any> = [];
  constructor(private apiService: ApiService,
    private authService: AuthorizeService, private utilService: UtilityService, public dialogRef: MatDialogRef<AssetjoborderchilditemsComponent>,
    private notifyService: NotificationService, public dialog: MatDialog) {

  };

  ngOnInit(): void {
    this.loadChildData();
  }

  loadChildData() {
    this.isShowList = true;
    this.apiService.getall(`assetMaintenance/getAssetjoborderchilditemsByJob?jobPlanCode=${this.jobPlanCode}&status=${this.Status}`).subscribe(res => {
      if (res) {
        this.isShowList = false;
        this.childItems = res;
      }
    });
  }
  loadChildsForAsset(evt: any) {
    this.Status = evt.target.value;
    this.loadChildData();
  }
  requireInfo(citem: any) {
    //this.notifyService.showError("errr")
    let dialogRef = this.utilService.openCrudDialog(this.dialog, AssetclosinginfoComponent,'60%');
    (dialogRef.componentInstance as any).data = { jobPlanCode: this.jobPlanCode, ...citem };
    dialogRef.afterClosed().subscribe(res => {
      if (res && res === true) {
        this.loadChildData();
      }
    });

  }

  closeModel() {
    this.dialogRef.close();
  }

}
