import { Component, Inject } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-activity-dialog',
  templateUrl: './activity-dialog.component.html',
})
export class ActivityDialogComponent {
 
  constructor(
    public dialogRef: MatDialogRef<ActivityDialogComponent>,
   // @Inject(MAT_DIALOG_DATA) public data: any
 ) { }

  onClose(): void {
    this.dialogRef.close();
  }
}
