import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AuthorizeService } from 'src/app/api-authorization/AuthorizeService';
import { ApiService } from 'src/app/services/api.service';
import { NotificationService } from 'src/app/services/notification.service';
import { DBOperation } from 'src/app/services/utility.constants';
import { UtilityService } from 'src/app/services/utility.service';
import { DeleteConfirmDialogComponent } from 'src/app/sharedcomponent/delete-confirm-dialog';
import { PaginationService } from 'src/app/sharedcomponent/pagination.service';
import { ParentSystemSetupComponent } from 'src/app/sharedcomponent/parentsystemsetup.component';
import { ParentFomMgtComponent } from '../../../sharedcomponent/parentfommgt.component';
import { AddupdateitemsubcategoriesComponent } from '../Sharedpages/addupdateitemsubcategories.component';

@Component({
  selector: 'app-getitemsubcategories',
  templateUrl: './getitemsubcategories.component.html',
  styles: [
  ]
})
export class GetitemsubcategoriesComponent extends ParentFomMgtComponent implements OnInit {

  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort!: MatSort;

  displayedColumns: string[] = ['itemCatCode', 'itemSubCatCode', 'itemSubCatName','itemSubCatNameAr', 'itemSubCatDesc','isActive', 'Actions'];
  data!: MatTableDataSource<any> | null;
  totalItemsCount!: number;
  sortingOrder: string = 'id desc';
  searchValue: string = '';
  isLoading: boolean = false;

  constructor(private apiService: ApiService, private authService: AuthorizeService,
    private utilService: UtilityService, private notifyService: NotificationService, public dialog: MatDialog,
    public pageService: PaginationService) { super(authService); }


  //private loadUser(page: number | undefined, pageCount: number | undefined, query: string | null | undefined, orderBy: string | null | undefined) {

  //  this.data = new MatTableDataSource(this.getData());

  //  this.totalItemsCount = 2;

  //}
  //getData(): Array<any> {
  //  let data: Array<any> = [
  //    { "categoryCode": "201201244", "categoryName": "Households", "alternateName": "Households_001 ", "IsActive": true, "id": 1 },
  //    { "categoryCode": "14251544", "categoryName": "Households", "alternateName": "Households_001", "isActive": false, "id": 2 }
  //  ]
  //  return data;
  //}


  ngOnInit(): void {
    this.initialLoading();
  }

  refresh() {
    this.searchValue = '';
    this.initialLoading();
  }

  initialLoading() {
    this.loadList(0, this.pageService.pageCount, "", this.sortingOrder);
  }

  onSortOrder(sort: any) {
    this.sortingOrder = sort.active + ' ' + sort.direction;
    this.totalItemsCount = 0;
    this.loadList(0, this.pageService.pageCount, "", this.sortingOrder);
  }


  onPageSwitch(event: PageEvent) {
    this.pageService.change(event);
    this.loadList(event.pageIndex, event.pageSize, "", this.sortingOrder);
  }

  private loadList(page: number | undefined, pageCount: number | undefined, query: string | null | undefined, orderBy: string | null | undefined) {
    this.isLoading = true;

    this.apiService.getPagination('fomItemSubCategory', this.utilService.getQueryString(page, pageCount, query, orderBy)).subscribe(result => {
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


  applyFilter(searchVal: any) {
    const search = searchVal;//.target.value as string;
    //if (search && search.length >= 3) {
    if (search) {
      this.searchValue = search;
      this.loadList(0, this.pageService.pageCount, this.searchValue, this.sortingOrder);
    }
  }

  private openDialogManage(id: number = 0, dbops: DBOperation, modalTitle: string, modalBtnTitle: string) {
    let dialogRef = this.utilService.openCrudDialog(this.dialog, AddupdateitemsubcategoriesComponent);
    (dialogRef.componentInstance as any).dbops = dbops;
    (dialogRef.componentInstance as any).modalTitle = modalTitle;
    (dialogRef.componentInstance as any).modalBtnTitle = modalBtnTitle;
    (dialogRef.componentInstance as any).id = id;

    dialogRef.afterClosed().subscribe(res => {
      if (res && res === true)
        this.initialLoading();
    });
  }

  public create() {
    this.openDialogManage(0, DBOperation.create, 'Adding New Sub Category', 'Add');
  }

  public edit(id: number) {
    this.openDialogManage(id, DBOperation.update, 'Updating Sub Category', 'Update');
  }

  public delete(id: number) {
    const dialogRef = this.utilService.openDeleteConfirmDialog(this.dialog, DeleteConfirmDialogComponent);
    dialogRef.afterClosed().subscribe(canDelete => {
      if (canDelete && id > 0) {
        this.apiService.delete('SubCategory', id).subscribe(res => {
          this.refresh();
          this.utilService.OkMessage();
        },
        );
      }
    },
      error => this.utilService.ShowApiErrorMessage(error));
  }

}
