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
import { FileUploadComponent } from '../../../sharedcomponent/fileupload.component';
import { AddupdatejobplanComponent } from './addupdatejobplan/addupdatejobplan.component';
import { AddupdatejobplanscheduleComponent } from './addupdatejobplanschedule/addupdatejobplanschedule.component';
import { JobplannotesComponent } from './jobplannotes/jobplannotes.component';
import { JobplanschedulingComponent } from './shared/jobplanscheduling/jobplanscheduling.component';
import { JobplanschedulingpopupComponent } from './jobplanschedulingpopup/jobplanschedulingpopup.component';
import { AssettasklistpopupComponent } from './assettasklistpopup/assettasklistpopup.component';
import { JobplanscheduleprintComponent } from './jobplanscheduleprint/jobplanscheduleprint.component';

@Component({
  selector: 'app-jobplanlist',
  templateUrl: './jobplanlist.component.html',
  styles: [
  ]
})
export class JobplanlistComponent extends ParentB2CFrontComponent implements OnInit {

  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort!: MatSort;

  searchValue: string = '';
  sortingOrder: string = 'id desc';
  isLoading: boolean = false;
  totalItemsCount: number = 0;
  data: MatTableDataSource<any> = new MatTableDataSource();
  displayedColumns: string[] = ['jobPlanCode', 'assetCode', 'contractCode', 'deptCode', 'createdDate', 'isActive', 'isClosed', 'Actions'];// 'isVoid',
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
    this.initialLoading();
  }

  initialLoading() {
    this.loadList(0, this.pageService.pageCount, "", this.sortingOrder);
  }

  onPageSwitch(event: PageEvent) {
    this.pageService.change(event);
    this.loadList(event.pageIndex, event.pageSize, "", this.sortingOrder);
  }

  private loadList(page: number | undefined, pageCount: number | undefined, query: string | null | undefined, orderBy: string | null | undefined) {
    this.isLoading = true;

    this.apiService.getPagination('assetMaintenance/getFomJobPlanMasterList', this.utilService.getQueryString(page, pageCount, query, orderBy)).subscribe(result => {
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

  private openDialogManage(id: any, dbops: DBOperation, modalTitle: string, modalBtnTitle: string, component: any, width: string = '95%') {
    let dialogRef = this.utilService.openCrudDialog(this.dialog, component, width);
    (dialogRef.componentInstance as any).dbops = dbops;
    (dialogRef.componentInstance as any).modalTitle = modalTitle;
    (dialogRef.componentInstance as any).modalBtnTitle = modalBtnTitle;
    (dialogRef.componentInstance as any).id = id;
    (dialogRef.componentInstance as any).jobPlanCode = id;
    (dialogRef.componentInstance as any).assetCode = id;


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

  //private openDialogManage_dupli(data: any, component: any) {
  //  let dialogRef = this.utilService.openCrudDialog(this.dialog, component, '95%');
  //  (dialogRef.componentInstance as any).data = data;
  //  dialogRef.afterClosed().subscribe(res => {
  //    if (res && res === true) {

  //    }

  //  });
  //}

  private openDialogManage_CrEdit(row: any, dbops: DBOperation, modalTitle: string, modalBtnTitle: string, component: any, width: string = '95%') {
    let dialogRef = this.utilService.openCrudDialog(this.dialog, component, width);
    (dialogRef.componentInstance as any).dbops = dbops;
    (dialogRef.componentInstance as any).modalTitle = modalTitle;
    (dialogRef.componentInstance as any).modalBtnTitle = modalBtnTitle;
    (dialogRef.componentInstance as any).data = row;

    //(dialogRef.componentInstance as any).id = id;
    //(dialogRef.componentInstance as any).jobPlanCode = id;
    //(dialogRef.componentInstance as any).assetCode = id;


    dialogRef.afterClosed().subscribe(res => {
      if (res && res === true) {
        this.initialLoading();
        //location.reload();
      }
      this.isLoading = false;

    });
  }

  public create() {
    this.openDialogManage_CrEdit({ id: 0, approve: false }, DBOperation.create, 'Add_New_JobPlanmaster', 'Add', AddupdatejobplanComponent);

    //this.openDialogManage_dupli({
    //  "title": "Add_New_JobPlanSchedule",
    //  "jobPlanCode": "",
    //  "assetCode": "ASSETMASTER2",
    //  "frequency": "Quarterly",
    //  "planStartDate": this.utilService.selectedDateTime("2024-08-31"),
    //  "childHasDiffFreq": false
    //}, AddupdatejobplanscheduleComponent);


  }
  public edit(row: any) {
    this.openDialogManage_CrEdit({ id: row.id, approve: row.approve }, DBOperation.update, 'Update_JobPlanmaster', 'Update', AddupdatejobplanComponent);
  }

  public delete(id: number) {
    const dialogRef = this.utilService.openDeleteConfirmDialog(this.dialog, DeleteConfirmDialogComponent);
    dialogRef.afterClosed().subscribe(canDelete => {
      if (canDelete && id > 0) {
        this.apiService.delete('assetMaintenance/deleteJobMaster', id).subscribe(res => {
          this.refresh();
          this.utilService.OkMessage();
        },
        );
      }
    },
      error => this.utilService.ShowApiErrorMessage(error));
  }

  uploadFile(id: any) {
    this.openFileUploadDialogManage(this.translate.instant('Document_Upload'), FileUploadComponent, { module: 'PPM', action: 'PPM', id: id, sourceId: id });
  }

  private openFileUploadDialogManage<T>(modalTitle: string = '', component: T, moduleFile: any, width: number = 80) {
    let dialogRef = this.utilService.openDialogCongif(this.dialog, component, width);
    (dialogRef.componentInstance as any).modalTitle = modalTitle;
    (dialogRef.componentInstance as any).moduleFile = moduleFile;

    dialogRef.afterClosed().subscribe(res => {
    });
  }

  approve(id: any) {
    this.setStatus(id, 'approve');
  }

  canClose(id: any) {
    this.setStatus(id, 'closed');
  }

  canVoid(id: any) {
    this.setStatus(id, 'void');
  }

  notes(id: any) {
    this.openDialogManage(id, DBOperation.create, 'Add_Notes', 'Add', JobplannotesComponent, '45%');
  }

  schedules(jobPlanCode: string) {
    this.openDialogManage(jobPlanCode, DBOperation.create, 'Add_Notes', 'Add', JobplanschedulingpopupComponent, '100%');
  }
  tasks(assetCode: string) {
    this.openDialogManage(assetCode, DBOperation.create, 'Task_List', 'Add', AssettasklistpopupComponent, '50%');

  }
  public print(jobPlanCode: string) {
    this.openDialogManage(jobPlanCode, DBOperation.update, 'Update_JobPlanmaster', 'Update', JobplanscheduleprintComponent);
  }

  setStatus(id: any, type: string) {
    const dialogRef = this.utilService.openDeleteConfirmDialog(this.dialog, DeleteConfirmDialogComponent);
    dialogRef.afterClosed().subscribe(canDelete => {
      if (canDelete && id > 0) {
        this.apiService.post('assetMaintenance/approveJobMaster', { id: id, approve: true, type: type }).subscribe(res => {
          this.refresh();
          this.utilService.OkMessage();
        },
        );
      }
    },
      error => this.utilService.ShowApiErrorMessage(error));

  }


}


