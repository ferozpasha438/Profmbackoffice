import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../../../services/api.service';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-assettasklistpopup',
  templateUrl: './assettasklistpopup.component.html',
  styles: [
  ]
})
export class AssettasklistpopupComponent implements OnInit {
  listOfAssetTasks: Array<any> = [];
  assetCode: string = '';
  constructor(private apiService: ApiService, public dialogRef: MatDialogRef<AssettasklistpopupComponent>) {

  }

  ngOnInit(): void {
    this.apiService.getall(`assetMaintenance/getFomAssetMasterTaskByAsset?assetCode=${this.assetCode}`).subscribe(res => {
      if (res) {
        this.listOfAssetTasks = res.assetTasks;
      }
    });
  }
  closeModel() {
    this.dialogRef.close();
  }

}
