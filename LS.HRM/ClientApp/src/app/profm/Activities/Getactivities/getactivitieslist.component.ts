import { Component, OnInit, ViewChild } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { AuthorizeService } from 'src/app/api-authorization/AuthorizeService';
import { ApiService } from 'src/app/services/api.service';
import { NotificationService } from 'src/app/services/notification.service';
import { DBOperation } from 'src/app/services/utility.constants';
import { UtilityService } from 'src/app/services/utility.service';
import { ParentHrmAdminComponent } from 'src/app/sharedcomponent/Parenthrmadmin.component';
import { DeleteConfirmDialogComponent } from 'src/app/sharedcomponent/delete-confirm-dialog';
import { PaginationService } from 'src/app/sharedcomponent/pagination.service';
import { AddupdatedisciplinesComponent } from '../../Disciplines/Sharedpages/addupdatedisciplines.component';
import { ListofdepartmentComponent } from '../../ListOfDepartment/listofdepartment.component';
import { AddupdateactivitesComponent } from '../Sharedpages/addupdateactivites.component';
import { ParentFomMgtComponent } from '../../../sharedcomponent/parentfommgt.component';
import { CustomSelectListItem } from '../../../models/MenuItemListDto';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-getactivitieslist',
  templateUrl: './getactivitieslist.component.html',
  styleUrls: ['./getactivitieslist.component.scss'
  ]
})
export class GetactivitieslistComponent extends ParentFomMgtComponent implements OnInit {

  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort!: MatSort;

  searchValue: string = '';
 // deptCode: string = '';
  sortingOrder: string = 'id desc';
  isLoading: boolean = false;
  totalItemsCount: number = 0;
  //data!: MatTableDataSource<any>;
  data!: MatTableDataSource<any> | null;
  list: any;
  DisciplineCodeList: Array<CustomSelectListItem> = [];
  displayedColumns: string[] = ['thumbNailImage','actCode','deptCode', 'actName', 'actNameAr',  'isB2B', 'isB2C','isActive','Actions'];

  // displayedColumns1: string[] = ['deptCode', 'actName', 'actNameAr', 'isB2B', 'isB2C', 'createdBy', 'createdOn', 'isActive','Actions'];

  // displayedColumns2: string[] = ['deptCode', 'actName', 'actNameAr', 'isB2B', 'isB2C', 'createdBy', 'createdOn','isActive', 'Actions'];

  // displayedColumns3: string[] = ['deptCode', 'actName', 'actNameAr', 'isB2B', 'isB2C', 'createdBy', 'createdOn', 'isActive','Actions'];

  isArab: boolean = false;
  tabType: string = 'Electrical';
  constructor(private apiService: ApiService, private authService: AuthorizeService, private translate: TranslateService,
    private utilService: UtilityService, private notifyService: NotificationService, public dialog: MatDialog,
    public pageService: PaginationService) {
    super(authService);
  }
  //get(): Array<any> {
  //  return [{ id: 1, deptCode: 'Test Code', deptName: 'Test Dept', deptNameAR: 'Test Dept Ar', deptServType: 'Direct', createdBy: 'Ali', createdDate: '26-11-2023', isActive: 'true' }]
  //}
  // get2(): Array<any> {
  //   return [{ id: 1, deptCode: 'Test Code', deptName: 'Test Dept', deptNameAR: 'Test Dept Ar', deptServType: 'Direct', createdBy: 'Ali', createdDate: '26-11-2023', isActive: 'true' }]
  // }
  // get3(): Array<any> {
  //   return [{ id: 1, deptCode: 'Test Code', deptName: 'Test Dept', deptNameAR: 'Test Dept Ar', deptServType: 'Direct', createdBy: 'Ali', createdDate: '26-11-2023', isActive: 'true' }]
  // }
  // get4(): Array<any> {
  //   return [{ id: 1, deptCode: 'Test Code', deptName: 'Test Dept', deptNameAR: 'Test Dept Ar', deptServType: 'Direct', createdBy: 'Ali', createdDate: '26-11-2023', isActive: 'true' }]
  // }
  ngOnInit(): void {
    this.isArab = this.utilService.isArabic();
    this.initialLoading();
    this.loadData();
    
    
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

  handleButtonClick(deptCode:string) {
    this.loadActivity(0, this.pageService.pageCount, deptCode, this.sortingOrder);
  }


  //private loadList(page: number | undefined, pageCount: number | undefined, query: string | null | undefined, orderBy: string | null | undefined) {
  //  this.isLoading = true;
  //  //this.data = new MatTableDataSource(this.get());
  //  this.apiService.getPagination('fomActivities', this.utilService.getQueryString(page, pageCount, query, orderBy)).subscribe(result => {
  //  //  this.data = new MatTableDataSource(result.items);
  //    this.totalItemsCount = 0;
  //    this.data = new MatTableDataSource(result.items);

  //    this.totalItemsCount = result.totalCount;

  //    setTimeout(() => {
  //      this.paginator.pageIndex = page as number;
  //      this.paginator.length = this.totalItemsCount;
  //    });
  //    this.data.sort = this.sort;

  //    //console.log(this.data.sort)
  //    //console.log(this.data.paginator)

  //    this.isLoading = false;
  //  }, error => this.utilService.ShowApiErrorMessage(error));
  //}


  private loadList(page: number | undefined, pageCount: number | undefined, query: string | null | undefined, orderBy: string | null | undefined) {
    this.isLoading = true;

    this.apiService.getPagination('fomActivities', this.utilService.getQueryString(page, pageCount, query, orderBy)).subscribe(result => {
      this.totalItemsCount = 0;
      this.data = new MatTableDataSource(result.items);

      this.totalItemsCount = result.totalCount;

      this.data.paginator = this.paginator;
      this.data.sort = this.sort;

      //console.log(this.data.sort)
      //console.log(this.data.paginator)

      this.isLoading = false;
    }, error => this.utilService.ShowApiErrorMessage(error));
  }


  private loadActivity(page: number | undefined, pageCount: number | undefined, deptCode: string | null | undefined, orderBy: string | null | undefined) {
    this.isLoading = true;
    
    this.apiService.getPagination('fomActivities/GetGetActivitityListByDeptCode', this.utilService.getQueryString(page, pageCount, deptCode, orderBy)).subscribe(result => {
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

  public createlist() {
    this.openDialogWindow(0, DBOperation.create, 'List Of Disciplines', 'Add', ListofdepartmentComponent);

  }
  public create() {
    this.openDialogManage(0, DBOperation.create, 'Adding_Activities', 'Add', AddupdateactivitesComponent);

  }
  public edit(id: number) {
    console.log("id=" + id);
    this.openDialogManage(id, DBOperation.update, 'Updating_Activities', 'Update', AddupdateactivitesComponent);
  }

  public delete(id: number) {
    const dialogRef = this.utilService.openDeleteConfirmDialog(this.dialog, DeleteConfirmDialogComponent);
    dialogRef.afterClosed().subscribe(canDelete => {
      if (canDelete && id > 0) {
        this.apiService.delete('fomActivities', id).subscribe(res => {
          this.refresh();
          this.utilService.OkMessage();
        },
        );
      }
    },
      error => this.utilService.ShowApiErrorMessage(error));
  }

  loadData() {
    this.apiService.getPagination('fomDiscipline', this.utilService.getQueryString(0, 1000, '', '')).subscribe(res => {
      if (res)
        this.DisciplineCodeList = res['items'];
    });

  }

}


