import { Component, OnInit, ViewChild } from '@angular/core';
import { AddupdatecustomercategoryComponent } from '../Sharedpages/addupdatecustomercategory.component';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { AuthorizeService } from 'src/app/api-authorization/AuthorizeService';
import { ApiService } from 'src/app/services/api.service';
import { NotificationService } from 'src/app/services/notification.service';
import { UtilityService } from 'src/app/services/utility.service';
import { PaginationService } from 'src/app/sharedcomponent/pagination.service';
//import { ParentSystemSetupComponent } from 'src/app/sharedcomponent/parentsystemsetup.component';
import { ParentFomMgtComponent } from 'src/app/sharedcomponent/parentfommgt.component';


@Component({
  selector: 'app-getcustomercategorylist',
  templateUrl: './getcustomercategorylist.component.html',
  styles: [
  ]
})
export class GetcustomercategorylistComponent extends ParentFomMgtComponent implements OnInit {

  displayedColumns: string[] = ['custCatCode', 'custCatName', 'custCatDesc', 'catPrefix', 'isActive', 'Actions'];
  data!: MatTableDataSource<any>;
  list: any;
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  totalItemsCount!: number;
  isLoading!: boolean;
  sortingOrder: string = '';
  canAddCustCateg: boolean = true;
  constructor(private apiService: ApiService, private authService: AuthorizeService,
    private utilService: UtilityService, private notifyService: NotificationService, public dialog: MatDialog,
    public pageService: PaginationService) { super(authService); }

  ngOnInit(): void {
    this.initialLoading();
  }

  initialLoading() {
    this.loadList(0, this.pageService.pageCount, "", this.sortingOrder);
  }

  private loadList(page: number | undefined, pageCount: number | undefined, query: string | null | undefined, orderBy: string | null | undefined) {
    this.isLoading = true;

    this.apiService.getPagination('fomCustomerCategory', this.utilService.getQueryString(page, pageCount, query, orderBy)).subscribe(result => {
      this.totalItemsCount = 0;
     
      this.data = new MatTableDataSource(result.items);
      this.data.paginator = this.paginator;
      this.canAddCustCateg = result.length <= 0
      this.isLoading = false;
    }, error => this.utilService.ShowApiErrorMessage(error));
  }

  searchFilter(value: string) {
    this.data.filter = value.trim().toLowerCase();
  }

  private openDialogManage(row: any = null) {
    let dialogRef = this.utilService.openCrudDialog(this.dialog, AddupdatecustomercategoryComponent);
    (dialogRef.componentInstance as any).row = row;
    dialogRef.afterClosed().subscribe(res => {
      if (res && res === true)
        this.initialLoading();
    });
  }

  public create() {
    this.openDialogManage();
  }

  public edit(row: any) {
    this.openDialogManage(row);
  }

}

