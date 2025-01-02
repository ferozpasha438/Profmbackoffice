import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../../../services/api.service';
import { UtilityService } from '../../../../services/utility.service';
import { NotificationService } from '../../../../services/notification.service';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { AuthorizeService } from '../../../../api-authorization/AuthorizeService';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';


@Component({
  selector: 'app-assetclosinginfo',
  templateUrl: './assetclosinginfo.component.html',
  styles: [
  ]
})
export class AssetclosinginfoComponent implements OnInit {
  data: any;
  form!: FormGroup;

  matsequence: number = 1;
  matdescription: string = '';
  matquantity: string = '';
  mateditsequence: number = 0;
  listOfmat: Array<any> = [];

  toolsequence: number = 1;
  tooldescription: string = '';
  toolquantity: string = '';
  tooleditsequence: number = 0;
  listOftool: Array<any> = [];

  laborhrsequence: number = 1;
  laborhrdescription: string = '';
  laborhrquantity: string = '';
  laborhrhours: number = 0;
  laborhreditsequence: number = 0;
  listOflaborhr: Array<any> = [];

  fileone: string = '';
  fileUploadone!: File;
  formData!: FormData;

  constructor(private fb: FormBuilder, private apiService: ApiService,
    private authService: AuthorizeService, private utilService: UtilityService, public dialogRef: MatDialogRef<AssetclosinginfoComponent>,
    private notifyService: NotificationService, public dialog: MatDialog) {

  }

  ngOnInit(): void {
    this.setForm();
    console.log(this.data.childCode ?? '-');
  }

  setForm() {
    this.form = this.fb.group(
      {
        'jobPlanCode': [this.data.jobPlanCode, Validators.required],
        'assetCode': [this.data.assetCode, Validators.required],
        'frequency': [this.data.frequency, Validators.required],
        'childCode': [this.data.childCode],//this.utilService.hasValue(this.data.childCode) ? this.data.childCode : '-', Validators.required],
        "closedBy": [this.authService.getUserName(), Validators.required],
        "remarks": '',
      }
    );
  }

  addmat() {
    if (this.matdescription.trim() && this.matquantity) {
      if (this.mateditsequence > 0) {
        var index: number = this.listOfmat.findIndex(a => a.sequence === this.mateditsequence);
        let pItem = this.listOfmat[index];
        pItem.description = this.matdescription;
        pItem.quantity = this.matquantity;
        this.mateditsequence = 0;
      }
      else {
        this.listOfmat.push({
          sequence: this.getmatSequence(), source: 'mat', description: this.matdescription, quantity: this.matquantity
        })
      }
      this.setmatToDefault();
    }
  }
  deletemat(item: any) {
    this.removemat(item.sequence);
  }
  removemat(sequence: number) {
    let index: number = this.listOfmat.findIndex(a => a.sequence === sequence);
    this.listOfmat.splice(index, 1);
  }
  editmat(item: any) {
    this.mateditsequence = item.sequence;
    this.matdescription = item.description;
    this.matquantity = item.quantity;
  }
  setmatToDefault() {
    this.matdescription = ''; this.matquantity = '';
  }
  getmatSequence(): number { return this.matsequence = this.matsequence + 1 };



  addtool() {
    if (this.tooldescription.trim() && this.toolquantity) {
      if (this.tooleditsequence > 0) {
        var index: number = this.listOftool.findIndex(a => a.sequence === this.tooleditsequence);
        let pItem = this.listOftool[index];
        pItem.description = this.tooldescription;
        pItem.quantity = this.toolquantity;
        this.tooleditsequence = 0;
      }
      else {
        this.listOftool.push({
          sequence: this.gettoolSequence(), source: 'tl', description: this.tooldescription, quantity: this.toolquantity
        })
      }
      this.settoolToDefault();
    }
  }
  deletetool(item: any) {
    this.removetool(item.sequence);
  }
  removetool(sequence: number) {
    let index: number = this.listOftool.findIndex(a => a.sequence === sequence);
    this.listOftool.splice(index, 1);
  }
  edittool(item: any) {
    this.tooleditsequence = item.sequence;
    this.tooldescription = item.description;
    this.toolquantity = item.quantity;
  }
  settoolToDefault() {
    this.tooldescription = ''; this.toolquantity = '';
  }
  gettoolSequence(): number { return this.toolsequence = this.toolsequence + 1 };



  addlaborhr() {
    if (this.laborhrdescription.trim() && this.laborhrhours > 0 && this.laborhrquantity) {
      if (this.laborhreditsequence > 0) {
        var index: number = this.listOflaborhr.findIndex(a => a.sequence === this.laborhreditsequence);
        let pItem = this.listOflaborhr[index];
        pItem.description = this.laborhrdescription;
        pItem.quantity = this.laborhrquantity;
        pItem.hours = this.laborhrhours;
        this.laborhreditsequence = 0;
      }
      else {
        this.listOflaborhr.push({
          sequence: this.getlaborhrSequence(), source: 'labh', description: this.laborhrdescription, quantity: this.laborhrquantity, hours: this.laborhrhours
        })
      }
      this.setlaborhrToDefault();
    }
  }
  deletelaborhr(item: any) {
    this.removelaborhr(item.sequence);
  }
  removelaborhr(sequence: number) {
    let index: number = this.listOflaborhr.findIndex(a => a.sequence === sequence);
    this.listOflaborhr.splice(index, 1);
  }
  editlaborhr(item: any) {
    this.laborhreditsequence = item.sequence;
    this.laborhrdescription = item.description;
    this.laborhrquantity = item.quantity;
    this.laborhrhours = item.hours;
  }
  setlaborhrToDefault() {
    this.laborhrdescription = '';
    this.laborhrquantity = '';
    this.laborhrhours = 0;
  }
  getlaborhrSequence(): number { return this.laborhrsequence = this.laborhrsequence + 1 };

  onSelectFile1(fileInput: any) {
    this.fileUploadone = <File>fileInput.target.files[0];
  }

  submit() {

    console.log(this.form.errors);
    if (this.form.valid) {

      this.form.value['childScheduleId'] = this.data.id;
      this.form.value['materials'] = this.listOfmat;
      this.form.value['tools'] = this.listOftool;
      this.form.value['laborHours'] = this.listOflaborhr;

      this.formData = new FormData();

      if (this.fileUploadone) {
        this.formData.append("fileone", this.fileUploadone, this.fileUploadone.name);
        this.formData.append("fileone", this.fileone);
      }
      else {
        this.notifyService.showError("Pls Upload File");
        return;
      }

      this.formData.append("input", JSON.stringify(this.form.value));

      //if (this.listOfmat.length > 0) {
      //}
      //if (this.listOftool.length > 0) {
      //}
      //if (this.listOflaborhr.length > 0) {
      //}


      this.apiService.post('assetMaintenance/createAssetClosingInfo', this.formData)
        .subscribe(res => {
          this.utilService.OkMessage();
          this.dialogRef.close(true);
        },
          error => {
            console.error(error);
            this.utilService.ShowApiErrorMessage(error);
          });


    }
  }
  closeModel() {
    this.dialogRef.close();
  }

}
