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
  selector: 'app-addupdateresources',
  templateUrl: './addupdateresources.component.html',
  styles: [
  ]
})

export class AddupdateresourcesComponent extends ParentFomMgtComponent implements OnInit {
  modalTitle!: string;
  modalBtnTitle!: string;
  dbops!: DBOperation;
  form!: FormGroup;
  id: number = 0;
  isReadOnly: boolean = false;
  DepartmentCodeList: Array<CustomSelectListItem> = [];
  ResourceTypeCodeList: Array<CustomSelectListItem> = [];
  ResourceLoginUserList: Array<CustomSelectListItem> = [];
  selectedCars = [2];
  deptCodes = [
        { id: 1, name: 'Electrical Department'},
        { id: 2, name: 'Janotorial Department' },
        { id: 3, name: 'Plumbing Department' },
        { id: 4, name: 'A/C Department' },
        { id: 5, name: 'Refrigator Service Department' },

    ];

  constructor(private fb: FormBuilder, private apiService: ApiService,
    private authService: AuthorizeService, private utilService: UtilityService, public dialogRef: MatDialogRef<AddupdateresourcesComponent>,
    private notifyService: NotificationService, private validationService: ValidationService) {
    super(authService)
  };


  ngOnInit(): void {
    this.setForm();
    this.loadData();
    this.loadResourceUsers();
    
  }

  setForm() {
    this.form = this.fb.group(
      {
        'deptCodes': [[], Validators.required],
        'resCode': ['', [Validators.required, Validators.maxLength(20)]],
        'resTypeCode': ['', Validators.required],
        'nameEng': ['', Validators.required],
        'nameAr': ['', Validators.required],
        'approvalAuth': [false],
        'loginUser': [''],
        'isActive': [false],
      }
    );
    this.isReadOnly = false;
  }
  setEditForm() {
    this.apiService.get('resource/getResourceById', this.id).subscribe(res => {
      if (res) {
        this.isReadOnly = true;
        const filterArray = res['deptCodes'].split(',');
        var deptdata = this.DepartmentCodeList as Array<any>;
        res['deptCodes'] = deptdata.filter(item => filterArray.includes(item.deptCode));
        this.form.patchValue(res);
      }
    });
  }

  loadResourceUsers() {
    this.apiService.getall('Resource/getResourceLoginUsersList').subscribe(res => {
      if (res) {
        this.ResourceLoginUserList = res;
      }
    })
  }


  closeModel() {
    this.dialogRef.close();
  }

  loadData() {
    this.apiService.getPagination('fomDiscipline', this.utilService.getQueryString(0, 1000, '', '')).subscribe(res => {
      if (res) {
        this.DepartmentCodeList = res['items'];
        if (this.id > 0)
          this.setEditForm();
      }
        
    });

    this.apiService.getPagination('resourceType/getResourceTypesPaginationList', this.utilService.getQueryString(0, 1000, '', '')).subscribe(res => {
      if (res) {
        this.ResourceTypeCodeList = res['items'];
        if (this.id > 0)
          this.setEditForm();
      }

    });


  }

  submit() {
    if (this.form.valid) {
      if (this.id > 0)
        this.form.value['id'] = this.id;
      var deptdata = this.form.value['deptCodes'] as Array<any>;
      this.form.value['deptCodes'] = deptdata.map(item => item.deptCode);
      this.apiService.post('resource/createUpdateResource', this.form.value)
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
    this.form.controls['resCode'].setValue('');
    this.form.controls['resTypeCode'].setValue('');
    this.form.controls['nameEng'].setValue('');
    this.form.controls['nameAr'].setValue('');
    this.form.controls['approvalAuth'].setValue('');
    this.form.controls['isActive'].setValue('');
    this.form.controls['loginUser'].setValue('');

  }
}
