import { HttpClient } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { AuthorizeService } from 'src/app/api-authorization/AuthorizeService';
import { ApiService } from 'src/app/services/api.service';
import { NotificationService } from 'src/app/services/notification.service';
import { DBOperation } from 'src/app/services/utility.constants';
import { UtilityService } from 'src/app/services/utility.service';
import { ValidationService } from 'src/app/sharedcomponent/ValidationService';
import { DeleteConfirmDialogComponent } from 'src/app/sharedcomponent/delete-confirm-dialog';
import { FileUploadComponent } from 'src/app/sharedcomponent/fileupload.component';
import { PaginationService } from 'src/app/sharedcomponent/pagination.service';
import { ParentSalesMgtComponent } from 'src/app/sharedcomponent/parentsalesmgt.component';
import { AddupdatecustomerComponent } from '../Sharedpages/addupdatecustomer.component';
import { GetcustomersitesComponent } from '../../CustomerSite/GetCustomerSite/getcustomersites.component';
import { CustomerinvoicestatementComponent } from '../../sharedcomponent/CustomerInvoiceStatement/customerinvoicestatement.component';
import { CustomerstatementComponent } from '../../sharedcomponent/CustomerStatement/customerstatement.component';
import { ParentFomMgtComponent } from '../../../sharedcomponent/parentfommgt.component';
import { AddupdatemultiloginComponent } from '../Sharedpages/addupdatemultilogin.component';

@Component({
  selector: 'app-getcustomerlists',
  templateUrl: './getcustomerlists.component.html',
  styles: [
  ]
})
export class GetcustomerlistsComponent extends ParentFomMgtComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort!: MatSort;
  displayedColumns: string[] = ['custCode', 'custName', 'custAlias', 'custAddress1', 'custCatCode', 'isActive', 'Actions'];
  //data!: MatTableDataSource<any>;
  data!: MatTableDataSource<any> | null;
  totalItemsCount!: number;
  searchValue: string = '';
  sortingOrder: string = 'id desc';
  isLoading: boolean = false;
  id: number = 0;
  form!: FormGroup;
  isArab: boolean = false;
  constructor(private fb: FormBuilder, private http: HttpClient, private router: Router, private apiService: ApiService,
    private authService: AuthorizeService, private translate: TranslateService,
    private notifyService: NotificationService, private utilService: UtilityService, private validationService: ValidationService, public dialog: MatDialog,
    public pageService: PaginationService) {
    super(authService);
  }
  get(): Array<any> {
    return [{ custCode: 1, custName: 'Test Code', custAlias: 'Test Dept', custAddress1: 'Test Dept Ar', custCatCode: 'Direct', isActive: 'true' }]
  }

  ngOnInit(): void {
    this.isArab = this.utilService.isArabic();
    this.initialLoading();
  }

  refresh() {
    this.searchValue = '';
    this.sortingOrder = 'id desc';
    this.initialLoading();
  }

  initialLoading() {
    this.loadList(0, this.pageService.pageCount, this.searchValue, this.sortingOrder);
  }

  onSortOrder(sort: any) {
    this.totalItemsCount = 0;
    this.sortingOrder = sort.active + ' ' + sort.direction;
    this.loadList(0, this.pageService.pageCount, this.searchValue, this.sortingOrder);
  }


  onPageSwitch(event: PageEvent) {
    this.pageService.change(event);
    this.loadList(event.pageIndex, event.pageSize, this.searchValue, this.sortingOrder);
  }

  private loadList(page: number | undefined, pageCount: number | undefined, query: string | null | undefined, orderBy: string | null | undefined) {
    this.isLoading = true;

    this.apiService.getPagination('fomCustomer', this.utilService.getQueryString(page, pageCount, query, orderBy)).subscribe(result => {
      this.totalItemsCount = 0;
      this.data = new MatTableDataSource(result.items);

      this.totalItemsCount = result.totalCount;

      this.data.paginator = this.paginator;
      this.data.sort = this.sort;

      this.isLoading = false;
    }, error => this.utilService.ShowApiErrorMessage(error));
  }


  applyFilter(searchVal: any) {
    const search = searchVal;//.target.value as string;
    //if (search && search.length >= 3) {
    if (search) {
      this.searchValue = search;
      this.loadList(0, this.pageService.pageCount, this.searchValue, this.sortingOrder);
    }
  }
  private openDialogManage(id: number, dbops: DBOperation, modalTitle: string, modalBtnTitle: string, component: any) {
    let dialogRef = this.utilService.openCrudDialog(this.dialog, component);
    (dialogRef.componentInstance as any).dbops = dbops;
    (dialogRef.componentInstance as any).modalTitle = modalTitle;
    (dialogRef.componentInstance as any).modalBtnTitle = modalBtnTitle;
    (dialogRef.componentInstance as any).id = id;

    dialogRef.afterClosed().subscribe(res => {
      if (res && res === true) {
        //this.initialLoading();
        location.reload();
      }
      this.isLoading = false;
    });
  }

  private openDialogManageOne<T>(id: string = '', dbops: DBOperation, modalTitle: string = '', component: T) {
    let dialogRef = this.utilService.openDialogCongif(this.dialog, component);
    (dialogRef.componentInstance as any).dbops = dbops;
    (dialogRef.componentInstance as any).modalTitle = modalTitle;
    (dialogRef.componentInstance as any).customerCode = id;

    //dialogRef.afterClosed().subscribe(res => {
    //  if (res && res === true)
    //    this.initialLoading();
    //});
  }

  private openDialogManageTwo<T>(id: string = '', dbops: DBOperation, modalTitle: string = '', component: T) {
    let dialogRef = this.utilService.openCrudDialog(this.dialog, component);
    (dialogRef.componentInstance as any).dbops = dbops;
    (dialogRef.componentInstance as any).modalTitle = modalTitle;
    (dialogRef.componentInstance as any).customerCode = id;

    //dialogRef.afterClosed().subscribe(res => {
    //  if (res && res === true)
    //    this.initialLoading();
    //});
  }



  public createsite() {
    this.openDialogManage(0, DBOperation.create, 'Customer Sites', 'Add', GetcustomersitesComponent);

  }
  public create() {
    this.openDialogManage(0, DBOperation.create, 'Adding_New_Customer', 'Add', AddupdatecustomerComponent);

  }
  public edit(id: number) {
    console.log("id=" + id);
    this.openDialogManage(id, DBOperation.update, 'Updating_Customer', 'Update', AddupdatecustomerComponent);
  }


  public userMap(customerCode: string) {
    //console.log("id=" + id);
    this.openDialogManageTwo(customerCode, DBOperation.update, 'AddUpdate_Customer', AddupdatemultiloginComponent);
  }

  public print(customerCode: string) {
    this.openDialogManageOne(customerCode, DBOperation.update, this.translate.instant('Create_New_Sales_Invoice'), CustomerstatementComponent);

  }

  public printCustInvoices(customerCode: string) {
    this.openDialogManageOne(customerCode, DBOperation.update, this.translate.instant('Create_New_Sales_Invoice'), CustomerinvoicestatementComponent);

  }


  public delete(id: number) {
    const dialogRef = this.utilService.openDeleteConfirmDialog(this.dialog, DeleteConfirmDialogComponent);
    dialogRef.afterClosed().subscribe(canDelete => {
      if (canDelete && id > 0) {
        this.apiService.delete('FomCustomer', id).subscribe(res => {
          this.utilService.OkMessage();
          this.ngOnInit();

        },
        );
      }
    },
      error => this.utilService.ShowApiErrorMessage(error));
  }
  submit() {

  }

  private openFileUploadDialogManage<T>(modalTitle: string = '', component: T, moduleFile: any, width: number = 80) {
    let dialogRef = this.utilService.openDialogCongif(this.dialog, component, width);
    (dialogRef.componentInstance as any).modalTitle = modalTitle;
    (dialogRef.componentInstance as any).moduleFile = moduleFile;

    dialogRef.afterClosed().subscribe(res => {
    });
  }

  uploadFile(id: any) {
    this.openFileUploadDialogManage(this.translate.instant('Document_Upload'), FileUploadComponent, { module: 'SND', action: 'SND_CUST', id: id, sourceId: id });
  }
}

