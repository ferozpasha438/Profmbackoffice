import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { TranslateService } from '@ngx-translate/core';
import { AuthorizeService } from 'src/app/api-authorization/AuthorizeService';
import { ApiService } from 'src/app/services/api.service';
import { NotificationService } from 'src/app/services/notification.service';
import { DBOperation } from 'src/app/services/utility.constants';
import { UtilityService } from 'src/app/services/utility.service';
import { ParentHrmAdminComponent } from 'src/app/sharedcomponent/Parenthrmadmin.component';
import { DeleteConfirmDialogComponent } from 'src/app/sharedcomponent/delete-confirm-dialog';
import { PaginationService } from 'src/app/sharedcomponent/pagination.service';
import { ParentB2CFrontComponent } from '../../../sharedcomponent/parentb2cfront.component';
import { AddupdateassetmasterComponent } from './addupdateassetmaster/addupdateassetmaster.component';
import { FileUploadComponent } from '../../../sharedcomponent/fileupload.component';

@Component({
  selector: 'app-assetmaster',
  templateUrl: './assetmaster.component.html',
  styles: [
  ]
})
export class AssetmasterComponent extends ParentB2CFrontComponent implements OnInit {

  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort!: MatSort;

  searchValue: string = '';
  sortingOrder: string = 'id desc';
  isLoading: boolean = false;
  isImporting: boolean = false;
  totalItemsCount: number = 0;
  data: MatTableDataSource<any> = new MatTableDataSource();
  displayedColumns: string[] = ['assetCode', 'nameEng', 'nameArabic', 'sectionCode', 'deptCode', 'location', 'contractCode', 'jobQuantity', 'createdDate', 'isActive', 'Actions'];
  isArab: boolean = false;

  constructor(private apiService: ApiService, private authService: AuthorizeService, private translate: TranslateService,
    private utilService: UtilityService, private notifyService: NotificationService, public dialog: MatDialog,
    public pageService: PaginationService) {
    super(authService);
  }

  ngOnInit(): void {
    this.isArab = this.utilService.isArabic();
    this.initialLoading();
  }

  refresh() {
    this.searchValue = '';
    (document.getElementById('excel_file') as HTMLInputElement).value = '';
    this.hasFile = false;
    this.initialLoading();
  }

  initialLoading() {
    this.loadList(0, this.pageService.pageCount, "", this.sortingOrder);
  }

  onPageSwitch(event: PageEvent) {
    this.pageService.change(event);
    this.loadList(event.pageIndex, event.pageSize, this.searchValue, this.sortingOrder);
  }

  private loadList(page: number | undefined, pageCount: number | undefined, query: string | null | undefined, orderBy: string | null | undefined) {
    this.isLoading = true;

    this.apiService.getPagination('assetMaintenance', this.utilService.getQueryString(page, pageCount, query, orderBy)).subscribe(result => {
      this.totalItemsCount = 0;
      this.data = new MatTableDataSource(result.items);

      this.totalItemsCount = result.totalCount;
      setTimeout(() => {
        this.paginator.pageIndex = page as number;
        this.paginator.length = this.totalItemsCount;
      });
      this.data.sort = this.sort;

      //console.log(this.data.sort)
      //console.log(this.data.paginator)

      this.isLoading = false;
    }, error => this.utilService.ShowApiErrorMessage(error));
  }

  applyFilter(searchValue: any) {    
    if (searchValue) {      
      this.loadList(0, this.pageService.pageCount, this.searchValue, this.sortingOrder);
    }
  }

  private openDialogManage(id: number, dbops: DBOperation, modalTitle: string, modalBtnTitle: string, component: any) {
    let dialogRef = this.utilService.openCrudDialog(this.dialog, component, '95%');
    (dialogRef.componentInstance as any).dbops = dbops;
    (dialogRef.componentInstance as any).modalTitle = modalTitle;
    (dialogRef.componentInstance as any).modalBtnTitle = modalBtnTitle;
    (dialogRef.componentInstance as any).id = id;


    dialogRef.afterClosed().subscribe(res => {
      if (res && res === true) {
        this.initialLoading();
        //location.reload();
      }
      this.isLoading = false;

    });
  }

  // private openDialogWindow(id: number, dbops: DBOperation, modalTitle: string, modalBtnTitle: string, component: any) {
  //   let dialogRef = this.utilService.openDialogCongif(this.dialog, component);
  //   (dialogRef.componentInstance as any).dbops = dbops;
  //   (dialogRef.componentInstance as any).modalTitle = modalTitle;
  //   (dialogRef.componentInstance as any).modalBtnTitle = modalBtnTitle;
  //   (dialogRef.componentInstance as any).id = id;


  //   dialogRef.afterClosed().subscribe(res => {
  //     if (res && res === true) {
  //       //this.initialLoading();
  //       location.reload();
  //     }
  //     this.isLoading = false;

  //   });
  // }

  public create() {
    this.openDialogManage(0, DBOperation.create, 'Add_New_assetmaster', 'Add', AddupdateassetmasterComponent);

  }
  public edit(id: number) {
    this.openDialogManage(id, DBOperation.update, 'Update_assetmaster', 'Update', AddupdateassetmasterComponent);
  }

  public delete(id: number) {
    const dialogRef = this.utilService.openDeleteConfirmDialog(this.dialog, DeleteConfirmDialogComponent);
    dialogRef.afterClosed().subscribe(canDelete => {
      if (canDelete && id > 0) {
        this.apiService.delete('assetMaintenance/deleteAssetMaster', id).subscribe(res => {
          this.refresh();
          this.utilService.OkMessage();
        },
        );
      }
    },
      error => this.utilService.ShowApiErrorMessage(error));
  }

  uploadFile(id: any) {
    this.openFileUploadDialogManage(this.translate.instant('Document_Upload'), FileUploadComponent, { module: 'ASMT', action: 'ASTM', id: id, sourceId: id });
  }
  private openFileUploadDialogManage<T>(modalTitle: string = '', component: T, moduleFile: any, width: number = 80) {
    let dialogRef = this.utilService.openDialogCongif(this.dialog, component, width);
    (dialogRef.componentInstance as any).modalTitle = modalTitle;
    (dialogRef.componentInstance as any).moduleFile = moduleFile;

    dialogRef.afterClosed().subscribe(res => {
    });
  }

  files!: File;
  hasFile: boolean = false;
  onFileChanged(event: any) {
    let reader = new FileReader();
    if (event.target.files && event.target.files.length > 0) {
      this.files = event.target.files[0];
      this.hasFile = true;
    }
  }

  importFile() {
    if (this.hasFile) {
      let formData = new FormData();
      formData.append('file', this.files, this.files.name);

      const dialogRef = this.utilService.openDeleteConfirmDialog(this.dialog, DeleteConfirmDialogComponent);
      (dialogRef.componentInstance as any).modalTitle = `Are you sure to import ${this.files.name}?`;

      dialogRef.afterClosed().subscribe(canDelete => {
        if (canDelete) {
          this.isImporting = true;
          this.apiService
            .post('AssetMaintenance/importExcelFomAssetMaster', formData)
            .subscribe(
              (res) => {
                this.isImporting = false;
                this.utilService.OkMessage();                
                this.refresh();
              },
              (error) => {
                this.isImporting = false;
                this.utilService.ShowApiErrorMessage(error);
              }
            );
        }
      })

     
    }
    else
      this.notifyService.showError('please import excel file');
  }
}


