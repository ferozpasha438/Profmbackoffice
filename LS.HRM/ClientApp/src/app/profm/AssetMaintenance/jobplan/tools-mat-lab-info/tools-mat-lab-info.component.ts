import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-tools-mat-lab-info',
  templateUrl: './tools-mat-lab-info.component.html',
  styles: [
  ]
})
export class ToolsMatLabInfoComponent implements OnInit {
  data: any;
  constructor(public dialogRef: MatDialogRef<ToolsMatLabInfoComponent>,) { }

  ngOnInit(): void {
  }

  closeModel() {
    this.dialogRef.close();
  }
}
