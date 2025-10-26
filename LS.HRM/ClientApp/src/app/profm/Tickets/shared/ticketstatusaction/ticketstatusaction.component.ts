import { Component } from "@angular/core";
import { MatDialogRef } from "@angular/material/dialog";
import { UtilityService } from "../../../../services/utility.service";
import { NotificationService } from "../../../../services/notification.service";

@Component({
  selector: 'app-ticketstatusaction',
  templateUrl: './ticketstatusaction.component.html'
})
export class TicketstatusactionComponent {
  remarks: string = "";
  modalTitle: string = "";
  hasFile: boolean = false;
  fileUploadone!: File;
  fileUploadtwo!: File;
  constructor(private utilService: UtilityService, private notifyService: NotificationService, public dialogRef: MatDialogRef<TicketstatusactionComponent>) {

  }
  onSelectFile1(fileInput: any) {
    this.fileUploadone = <File>fileInput.target.files[0];
  }
  onSelectFile2(fileInput: any) {
    this.fileUploadtwo  = <File>fileInput.target.files[0];
  }
  closeModel() {
    this.dialogRef.close('');
  }
  save() {
    if (this.remarks.length > 0) {
      if (this.hasFile) {
        if (this.fileUploadone && this.fileUploadtwo)
          this.dialogRef.close({ remarks: this.remarks, uploadfile: this.fileUploadone, uploadfileTwo: this.fileUploadtwo });
        else
          this.notifyService.showError("Upload Both Images");
      }
      else
        this.dialogRef.close(this.remarks);
    }
    else
      this.utilService.FillUpFields();

  }
}
