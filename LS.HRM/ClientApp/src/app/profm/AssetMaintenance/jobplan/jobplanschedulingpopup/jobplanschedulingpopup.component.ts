import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-jobplanschedulingpopup',
  templateUrl: './jobplanschedulingpopup.component.html',
  styles: [
  ]
})
export class JobplanschedulingpopupComponent implements OnInit {
  jobPlanCode: string = '';
  constructor(public dialogRef: MatDialogRef<JobplanschedulingpopupComponent>) { }

  ngOnInit(): void {
  }

  closeModel() {
    this.dialogRef.close();
  }
}
