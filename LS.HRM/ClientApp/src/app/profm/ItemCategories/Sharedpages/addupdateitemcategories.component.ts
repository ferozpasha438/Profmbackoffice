import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { AuthorizeService } from 'src/app/api-authorization/AuthorizeService';
import { ApiService } from 'src/app/services/api.service';
import { NotificationService } from 'src/app/services/notification.service';
import { DBOperation } from 'src/app/services/utility.constants';
import { UtilityService } from 'src/app/services/utility.service';
import { ValidationService } from 'src/app/sharedcomponent/ValidationService';
import { ParentSystemSetupComponent } from 'src/app/sharedcomponent/parentsystemsetup.component';
import { ParentFomMgtComponent } from '../../../sharedcomponent/parentfommgt.component';

@Component({
  selector: 'app-addupdateitemcategories',
  templateUrl: './addupdateitemcategories.component.html',
  styles: [
  ]
})
export class AddupdateitemcategoriesComponent extends ParentFomMgtComponent implements OnInit {

  modalTitle!: string;
  modalBtnTitle!: string;
  dbops!: DBOperation;
  form!: FormGroup;
  id: number = 0;
  /* isCompanyLoading: boolean = false;*/
  isReadOnly: boolean = false;
  //companyControl = new FormControl();
  //filteredOptions: Observable<Array<CustomSelectListItem>>;
  //cityList: Array<CustomSelectListItem> = [];

  constructor(private fb: FormBuilder, private apiService: ApiService,
    private authService: AuthorizeService, private utilService: UtilityService, public dialogRef: MatDialogRef<AddupdateitemcategoriesComponent>,
    private notifyService: NotificationService, private validationService: ValidationService) {
    //this.filteredOptions = this.companyControl.valueChanges.pipe(
    //  startWith(''),
    //  debounceTime(utilService.autoDelay()),
    //  distinctUntilChanged(),
    //  switchMap((val: string) => {
    //    if (val.trim() !== '')
    //      this.isCompanyLoading = true;
    //    return this.filter(val || '')
    //  })
    //);
    super(authService);
  }

  //getAll() {
  //  this.apiService.getall('').subscribe(data => {

  //  });
  //}


  ngOnInit(): void {
    this.setForm();

    if (this.id > 0)
      this.setEditForm();
  }

  keyPressNumbers(event: any) {
    var charCode = (event.which) ? event.which : event.keyCode;
    // Only Numbers 0-9
    if (charCode != 46 && charCode > 31
      && (charCode < 48 || charCode > 57)) {
      event.preventDefault();
      return false;
    } else {
      return true;
    }
  }

  setForm() {
    this.form = this.fb.group({
      'itemCatCode': ['', [Validators.required, Validators.maxLength(20)]],
      'itemCatName': ['', Validators.required],
      'itemCatName_Ar': ['', Validators.required],
      'itemCatDesc': '',
      'itemCatPrefix': '',
      'isActive': [false],


    });
  }

  setEditForm() {
    this.apiService.get('fomItemCategory', this.id).subscribe(res => {
      if (res) {
        this.form.patchValue(res);

      }
    })
  }
  submit() {
    if (this.form.valid) {
      if (this.id > 0)
        this.form.value['id'] = this.id;

      this.apiService.post('fomItemCategory', this.form.value)
        .subscribe(res => {
          this.utilService.OkMessage();
          this.reset();
          this.dialogRef.close(true);
        },
          error => {
            console.error(error);
            this.utilService.ShowApiErrorMessage(error);
          });
    }
    else
      this.utilService.FillUpFields();
  }
  reset() {
    this.form.controls['itemCatCode'].setValue('');
    this.form.controls['itemCatName'].setValue('');
    this.form.controls['itemCatName_Ar'].setValue('');
    this.form.controls['itemCatDesc'].setValue('');
    this.form.controls['itemCatPrefix'].setValue('');
    this.form.controls['isActive'].setValue('');


  }

  closeModel() {
    this.dialogRef.close();
  }
  onTextchange(Value: string) {
    if (Value != null) {
      this.apiService.getall(`producthierarchy/GetCategoryItem?CatCode=${Value}`).subscribe(res => {
        if (res) {
          this.isReadOnly = true;
          this.form.patchValue(res);
          this.id = res.id;
        }
      })
    }
  }
}
