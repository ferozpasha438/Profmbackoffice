import { Component } from "@angular/core";
import { MatDialogRef } from "@angular/material/dialog";
import { UtilityService } from "../../services/utility.service";

@Component({
  selector: 'app-commonremarkcomponent',
  template:
    `<div class="modal-header">
    <h3 class="modal-title">{{title}}</h3>
    <button type="button" class="close" aria-hidden="true" (click)="closeModel()"> ×</button>
    </div>
    <div class="row">
    <div class="col-md-3 col-lg-3"></div>
    <div class="col-md-6 col-lg-6">
      <div class="form-group">
        <label for="contractCode">Enter {{title}}</label>
        <input class="form-control" id="contractCode" type="text" [(ngModel)]="remark">
      </div>
    </div>
    <div class="col-md-3 col-lg-3"></div>
</div>
 <div class="row mt-3 ">
    <div class="col-md-12 col-lg-12 text-center">
      <input type="button" class="btn waves-effect waves-light btn-rounded   btn-secondary btn-midblock"
             value="Cancel" (click)="closeModel()" /> &nbsp;
      <input type="submit" class="btn waves-effect waves-light btn-rounded btn-primary btn-midblock"
             value="Save" (click)="save()" />
    </div>
  </div>
  `

})
export class CommonRemarkComponent {
  remarks: string = '';
  constructor(private utilService: UtilityService, public dialogRef: MatDialogRef<CommonRemarkComponent>) { }
  title!: string;
  remark!: string;

  save() {
    if (this.utilService.hasValue(this.remark)) {
      this.dialogRef.close(this.remark);
    }
    else
      this.utilService.FillUpFields();
  }
  closeModel() {
    this.dialogRef.close();
  }
}
