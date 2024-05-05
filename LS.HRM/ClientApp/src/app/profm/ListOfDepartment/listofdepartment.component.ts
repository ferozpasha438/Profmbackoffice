import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
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
import { AddupdatedisciplinesComponent } from '../Disciplines/Sharedpages/addupdatedisciplines.component';

@Component({
  selector: 'app-listofdepartment',
  templateUrl: './listofdepartment.component.html',
  styleUrls: ['./listofdepartment.component.scss']
})

export class ListofdepartmentComponent extends ParentHrmAdminComponent implements OnInit {

  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort!: MatSort;

  searchValue: string = '';
  sortingOrder: string = 'id desc';
  isLoading: boolean = false;
  totalItemsCount: number = 0;
  data: MatTableDataSource<any> = new MatTableDataSource();
  displayedColumns: string[] = ['deptCode', 'deptName', 'deptNameAR', 'deptServType', 'createdBy', 'createdDate', 'isActive', 'Actions'];
  displayedColumns1: string[] = ['deptCode', 'deptName', 'deptNameAR', 'deptServType', 'createdBy', 'createdDate', 'isActive', 'Actions'];
  displayedColumns2: string[] = ['deptCode', 'deptName', 'deptNameAR', 'deptServType', 'createdBy', 'createdDate', 'isActive', 'Actions'];
  displayedColumns3: string[] = ['deptCode', 'deptName', 'deptNameAR', 'deptServType', 'createdBy', 'createdDate', 'isActive', 'Actions'];
  isArab: boolean = false;
  tabType: string = 'Electrical';
  constructor(private apiService: ApiService, private authService: AuthorizeService, private translate: TranslateService,
    private utilService: UtilityService, private notifyService: NotificationService, public dialog: MatDialog, public dialogRef: MatDialogRef<ListofdepartmentComponent>,
    public pageService: PaginationService) {
    super(authService);
  }
  get(): Array<any> {
    return [{ id: 1, deptCode: 'testCOde', deptName: 'test dept', deptNameAR: 'test dept ar', deptServType: 'Direct', createdBy: 'Ali', createdDate: '26-11-2023', isActive: 'true' }]
  }
  get2(): Array<any> {
    return [{ id: 1, deptCode: 'testCOde', deptName: 'test dept', deptNameAR: 'test dept ar', deptServType: 'Direct', createdBy: 'Ali', createdDate: '26-11-2023', isActive: 'true' }]
  }
  get3(): Array<any> {
    return [{ id: 1, deptCode: 'testCOde', deptName: 'test dept', deptNameAR: 'test dept ar', deptServType: 'Direct', createdBy: 'Ali', createdDate: '26-11-2023', isActive: 'true' }]
  }
  get4(): Array<any> {
    return [{ id: 1, deptCode: 'testCOde', deptName: 'test dept', deptNameAR: 'test dept ar', deptServType: 'Direct', createdBy: 'Ali', createdDate: '26-11-2023', isActive: 'true' }]
  }
  ngOnInit(): void {
    this.isArab = this.utilService.isArabic();
    this.initialLoading();
  }
  changeType(type: string) {
    this.tabType = type;
  }
  refresh() {
    this.searchValue = '';
    this.initialLoading();
  }

  initialLoading() {
    this.loadList(0, this.pageService.pageCount, "", this.sortingOrder);
  }

  private loadList(page: number | undefined, pageCount: number | undefined, query: string | null | undefined, orderBy: string | null | undefined) {
    this.isLoading = true;
    this.data = new MatTableDataSource(this.get());
    this.apiService.getPagination('Disciplines', this.utilService.getQueryString(page, pageCount, query, orderBy)).subscribe(result => {
      this.data = new MatTableDataSource(this.get());
      this.totalItemsCount = 0;
      //this.data = new MatTableDataSource(result.items);

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
    const search = searchValue;//.target.value as string;
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

  private openDialogWindow(id: number, dbops: DBOperation, modalTitle: string, modalBtnTitle: string, component: any) {
    let dialogRef = this.utilService.openDialogCongif(this.dialog, component);
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
  closeModel() {
    this.dialogRef.close();
  }

  public createlist() {
    this.openDialogWindow(0, DBOperation.create, 'List Of Disciplines', 'Add', ListofdepartmentComponent);

  }
  public create() {
    this.openDialogManage(0, DBOperation.create, 'Adding_New_Disciplines', 'Add', AddupdatedisciplinesComponent);

  }
  public edit(id: number) {
    console.log("id=" + id);
    this.openDialogManage(id, DBOperation.update, 'Updating_Disciplines', 'Update', AddupdatedisciplinesComponent);
  }

  public delete(id: number) {
    const dialogRef = this.utilService.openDeleteConfirmDialog(this.dialog, DeleteConfirmDialogComponent);
    dialogRef.afterClosed().subscribe(canDelete => {
      if (canDelete && id > 0) {
        this.apiService.delete('Disciplines', id).subscribe(res => {
          this.refresh();
          this.utilService.OkMessage();
        },
        );
      }
    },
      error => this.utilService.ShowApiErrorMessage(error));
  }

}
