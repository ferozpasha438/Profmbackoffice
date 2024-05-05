import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { AuthorizeService } from 'src/app/api-authorization/AuthorizeService';
import { ApiService } from 'src/app/services/api.service';
import { NotificationService } from 'src/app/services/notification.service';
import { DBOperation } from 'src/app/services/utility.constants';
import { UtilityService } from 'src/app/services/utility.service';
import { ParentHrmAdminComponent } from 'src/app/sharedcomponent/Parenthrmadmin.component';
import { ValidationService } from 'src/app/sharedcomponent/ValidationService';
import { CustomSelectListItem } from '../../../models/MenuItemListDto';
import { ParentFomMgtComponent } from '../../../sharedcomponent/parentfommgt.component';

@Component({
  selector: 'app-addupdatesubcontractors',
  templateUrl: './addupdatesubcontractors.component.html',
  styles: [
  ]
})
export class AddupdatesubcontractorsComponent extends ParentFomMgtComponent implements OnInit {
  modalTitle!: string;
  modalBtnTitle!: string;
  dbops!: DBOperation;
  form!: FormGroup;
  DepartmentCodeList: Array<CustomSelectListItem> = [];
  id: number = 0;
  isReadOnly: boolean = false;

  constructor(private fb: FormBuilder, private apiService: ApiService,
    private authService: AuthorizeService, private utilService: UtilityService, public dialogRef: MatDialogRef<AddupdatesubcontractorsComponent>,
    private notifyService: NotificationService, private validationService: ValidationService) {
    super(authService)
  };


  ngOnInit(): void {
    this.loadData();
    this.setForm();
    if (this.id > 0)
      this.setEditForm();
  }


  

  setForm() {     
    this.form = this.fb.group(
      {
        'deptCodes': [[], Validators.required],
        'subContCode': ['', [Validators.required, Validators.maxLength(20)]],
        'nameEng': ['', Validators.required],
        'nameArabic': ['', Validators.required],
        'address': ['', Validators.required],
        'city': ['', Validators.required],
        'phone': ['', [Validators.required, Validators.pattern('[0-9]{10}')]],
        'mobile': ['', [Validators.required, Validators.pattern('[0-9]{10}')]],
        'contactPerson1': ['', Validators.required],
        'desgContactPerson1': ['', Validators.required],
        'contactPerson1Phone': ['', [Validators.required, Validators.pattern('[0-9]{10}')]],
        'contactPerson2': ['', Validators.required],
        'desgContactPerson2': ['', Validators.required],
        'contactPerson2Phone': ['', [Validators.required, Validators.pattern('[0-9]{10}')]],
        /* 'website': ['', [Validators.required, Validators.pattern('https?://.+\\.com')]],*/
        'website': ['', [Validators.required, Validators.pattern('http?://.+\\.com')]],
        'isActive': [false],
      }
    );
    this.isReadOnly = false;
  }
  setEditForm() {
    this.apiService.get('fomSubContractor', this.id).subscribe(res => {
      if (res) {
       // this.isReadOnly = true;
        const filterArray = res['deptCodes'].split(',');
        var deptdata = this.DepartmentCodeList as Array<any>;
        res['deptCodes'] = deptdata.filter(item => filterArray.includes(item.deptCode));
        this.form.patchValue(res);
      }
    });
  }


  closeModel() {
    this.dialogRef.close();
  }

  submit() {
    if (this.form.valid) {
      if (this.id > 0)
        this.form.value['id'] = this.id;
      var deptdata = this.form.value['deptCodes'] as Array<any>;
      this.form.value['deptCodes'] = deptdata.map(item => item.deptCode);
      this.apiService.post('fomSubContractor', this.form.value)
        .subscribe(res => {
          this.utilService.OkMessage();
          this.reset();
          this.dialogRef.close(true);
        },
          error => {
            this.utilService.ShowApiErrorMessage(error);
          });
    }
    else
      this.utilService.FillUpFields();
  }

  reset() {
    this.form.controls['deptCodes'].setValue('');
    this.form.controls['subContCode'].setValue('');
    this.form.controls['nameEng'].setValue('');
    this.form.controls['nameArabic'].setValue('');
    this.form.controls['address'].setValue('');
    this.form.controls['city'].setValue('');
    this.form.controls['phone'].setValue('');
    this.form.controls['mobile'].setValue('');
    this.form.controls['contactPerson1'].setValue('');
    this.form.controls['desgContactPerson1'].setValue('');
    this.form.controls['contactPerson1Phone'].setValue('');
    this.form.controls['contactPerson2'].setValue('');
    this.form.controls['desgContactPerson2'].setValue('');
    this.form.controls['contactPerson2Phone'].setValue('');
    this.form.controls['website'].setValue('');
    this.form.controls['isActive'].setValue('');

  }

  loadData() {
    this.apiService.getPagination('fomDiscipline', this.utilService.getQueryString(0, 1000, '', '')).subscribe(res => {
      if (res) {
        this.DepartmentCodeList = res['items'];
        if (this.id > 0)
          this.setEditForm();
      }

    });
  }
}

