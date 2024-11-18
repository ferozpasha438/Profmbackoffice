import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../../../services/api.service';
import { UtilityService } from '../../../../services/utility.service';
import { MatDialogRef } from '@angular/material/dialog';
import { NotificationService } from '../../../../services/notification.service';

@Component({
  selector: 'app-jobplanscheduleprint',
  templateUrl: './jobplanscheduleprint.component.html',
  styles: [
  ]
})
export class JobplanscheduleprintComponent implements OnInit {
  jobPlanCode: string = '';
  printItems: any;
  constructor(private apiService: ApiService,
    public utilService: UtilityService, public dialogRef: MatDialogRef<JobplanscheduleprintComponent>,
    private notifyService: NotificationService) {

  }

  ngOnInit(): void {
   // this.user = this.authService.getUserName();
    this.loadData();
  }
  loadData() {

    this.apiService.getall(`assetMaintenance/getFomJobPlanChildSchedulePrintByJobCode?jobPlanCode=${this.jobPlanCode}`).subscribe(res => {
      if (res)
        this.printItems = res as any;
      console.log(res);
    });
  }

  printInvoice() {
    this.openPrint();
  }

  openPrint() {
    const printContent = document.getElementById("printcontainer") as HTMLElement;
    this.utilService.printForLocale(printContent);
  }

  closeModel() {
    this.dialogRef.close();
  }
}
