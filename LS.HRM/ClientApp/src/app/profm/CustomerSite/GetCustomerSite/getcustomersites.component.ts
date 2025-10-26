import { HttpClient } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router, ActivatedRoute } from '@angular/router';
import { OprServicesService } from 'src/app/Operationalmgt/opr-services.service';
import { AuthorizeService } from 'src/app/api-authorization/AuthorizeService';
import { ApiService } from 'src/app/services/api.service';
import { NotificationService } from 'src/app/services/notification.service';
import { DBOperation } from 'src/app/services/utility.constants';
import { UtilityService } from 'src/app/services/utility.service';
import { ValidationService } from 'src/app/sharedcomponent/ValidationService';
import { DeleteConfirmDialogComponent } from 'src/app/sharedcomponent/delete-confirm-dialog';
import { PaginationService } from 'src/app/sharedcomponent/pagination.service';
import { ParentOptMgtComponent } from 'src/app/sharedcomponent/parentoptmgt.component';
import { AddupdatesiteComponent } from '../Sharedpages/addupdatesite.component';
import * as XLSX from "xlsx"
import { AddupdatesitetocustomerComponent } from '../../sharedcomponent/AddUpdateSiteToCustomer/addupdatesitetocustomer.component';
import { ParentFomMgtComponent } from '../../../sharedcomponent/parentfommgt.component';
import { AddupdatemultiloginComponent } from '../../Customer/Sharedpages/addupdatemultilogin.component';
import { MaplogintositeComponent } from '../Sharedpages/maplogintosite.component';
@Component({
  selector: 'app-getcustomersites',
  templateUrl: './getcustomersites.component.html',
  styles: [
    '.deptBreakAll{white-space: pre-wrap; word-break: break-word;}'
  ]
})
export class GetcustomersitesComponent extends ParentFomMgtComponent implements OnInit {

  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort!: MatSort;
  displayedColumns: string[] = ['customerCode', 'siteCode', 'siteName', 'siteArbName', 'siteAddress', 'isActive', 'Actions'];
  data!: MatTableDataSource<any>;
  totalItemsCount!: number;
  searchValue: string = '';
  sortingOrder: string = 'customerCode desc';
  isLoading: boolean = false;
  form!: FormGroup;
  customerCode!: string;
  id!: number;
  surveyorCode!: string;
  custId!: number;

  modalTitle!: string;
  modalBtnTitle!: string;
  dbops!: DBOperation;

  filter: any = { customerCode: '', branchCode: '' };
  reportData: Array<any> = [];
  isArab: boolean = false;
  customerSelectionList: Array<any> = [];
  citySelectionList: Array<any> = [];

  constructor(private fb: FormBuilder, private http: HttpClient, private router: Router, private apiService: ApiService, private authService: AuthorizeService,
    private notifyService: NotificationService, private utilService: UtilityService, private validationService: ValidationService, public dialog: MatDialog, 
    public pageService: PaginationService, private route: ActivatedRoute, private oprService: OprServicesService) {
    super(authService);

  }

  ngOnInit(): void {
    this.isArab = this.utilService.isArabic();
    //this.route.queryParams.subscribe(params => {


    //  this.customerCode = params.customerCode;

    //});
    this.initialLoading();
  }

  refresh() {
    this.reportData = [];
    this.resetFilter();
    // location.reload();
    //this.loadList(0, this.pageService.pageCount,this.searchValue, this.sortingOrder);


  }

  resetFilter() {
    this.filter = { customerCode: '', branchCode: '' };
    this.applyFilter('');
  }
  initialLoading() {
    this.searchValue = '';
    this.loadCitiesList();
    this.loadCustomersList();
    this.loadList(0, this.pageService.pageCount, this.searchValue, this.sortingOrder);
  }

  onSortOrder(sort: any) {
    this.reportData = [];
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
    // query = "";
    this.oprService.getPaginationWithFilter('FomCustomerSite/getSitesPagedListWithFilter', this.utilService.getQueryString(page, pageCount, query, orderBy), this.filter).subscribe(result => {
      this.totalItemsCount = 0;

      this.data = new MatTableDataSource(result.items);
      this.totalItemsCount = result.totalCount

      setTimeout(() => {
        this.paginator.pageIndex = page as number;
        this.paginator.length = this.totalItemsCount;
      });
      //this.data.paginator = this.paginator;

      this.data.sort = this.sort;
      this.isLoading = false;
    }, error => this.utilService.ShowApiErrorMessage(error));
  }


  applyFilter(searchVal: any) {
    this.loadList(0, this.pageService.pageCount, this.searchValue, this.sortingOrder);
    this.reportData = [];
  }
  private openDialogManage(id: number = 0, dbops: DBOperation, modalTitle: string, modalBtnTitle: string, component: any) {
    let dialogRef = this.utilService.openCrudDialog(this.dialog, component);
    (dialogRef.componentInstance as any).dbops = dbops;
    (dialogRef.componentInstance as any).modalTitle = modalTitle;
    (dialogRef.componentInstance as any).modalBtnTitle = modalBtnTitle;
    (dialogRef.componentInstance as any).id = id;

    dialogRef.afterClosed().subscribe(res => {
      if (res && res === true)
        this.initialLoading();
    });
  }
  closeModel() {
    this.dialog.closeAll();
  }



  public create() {
    this.openDialogManage(0, DBOperation.create, 'Adding_New_Site', 'Add', AddupdatesitetocustomerComponent);
  }


  public mapsite() {
    this.openDialogManage(0, DBOperation.create, 'Adding_New_Site', 'Add', MaplogintositeComponent);
  }

  public editSite(id: number) {
    this.openDialogManage(id, DBOperation.update, 'Updating_Site', 'Update', AddupdatesiteComponent);
  }

  //public addSuveyorToEnquiry(enquiryID: number) {
  //  this.openDialogManage(enquiryID, DBOperation.create, 'Adding_Surveyor_To_Enquiry', 'Save', AddSurveyorToEnquiryComponent);

  //}


  public delete(id: number) {
    const dialogRef = this.utilService.openDeleteConfirmDialog(this.dialog, DeleteConfirmDialogComponent);
    dialogRef.afterClosed().subscribe(canDelete => {
      if (canDelete && id > 0) {
        this.apiService.delete('FomCustomerSite', id).subscribe(res => {
          this.utilService.OkMessage();
          this.ngOnInit();
        },
        );
      }
    },
      error => this.utilService.ShowApiErrorMessage(error));
  }
  submit() {

    // this.apiService.post('CustomerSite', this.form.value)
    // .subscribe(res => {
    //   this.utilService.OkMessage();
    //   this.dialogRef.close(true);
    // },
    //   error => {
    //     console.error(error);
    //     this.utilService.ShowApiErrorMessage(error);
    //   });
  }
  



  cahngeEnquiryStatus(enquiryID: number, statusEnquiry: string) {
    this.apiService.post('ServiceEnquiries/changeEnquiryStatus?enquiryID=' + enquiryID + '&statusEnquiry=' + statusEnquiry, null)
      .subscribe(res => {
        this.utilService.OkMessage();
        this.ngOnInit();
      },
        error => {
          console.error(error);
          this.utilService.ShowApiErrorMessage(error);
        });


  }
  public addsitetocustomer(custId: number) {
    this.openDialogManage(custId, DBOperation.create, 'Adding_New_Site_to_Customer', 'Add', AddupdatesitetocustomerComponent);
  }
  clearReport() {
    this.reportData = [];
  }

  generateReport() {

    this.reportData = [];
    for (let i = 0; i <= Math.floor(this.totalItemsCount / 100); i++) {
      let event: any = { pageIndex: i, pageSize: 100, previousPageIndex: i == 0 ? 0 : i - 1, length: 0 };
      //this.pageService.change(event);
      this.loadListReport(event.pageIndex, event.pageSize, this.searchValue, this.sortingOrder);

    }


  }

  private loadListReport(page: number | undefined, pageCount: number | undefined, query: string | null | undefined, orderBy: string | null | undefined) {
    this.isLoading = true;
    this.oprService.getPaginationWithFilter('fomCustomerSite/getSitesPagedListWithFilter', this.utilService.getQueryString(page, pageCount, query, orderBy), this.filter).subscribe(result => {

      this.isLoading = false;
      this.reportData = this.reportData.length == 0 ? result.items : this.reportData.concat(result.items);
    }, error => this.utilService.ShowApiErrorMessage(error));
  }

  loadCustomersList() {
    this.apiService.getall('fomCustomer/getSelectCustomerList').subscribe((res: any) => {
      this.customerSelectionList = res as Array<any>;

      this.customerSelectionList.forEach(e => {
        e.lable = this.isArab ? e.value + "-" + e.textTwo : e.value + "-" + e.text;
      });
    });

  }

  loadCitiesList() {
    //  this.apiService.getall('City/getCitiesSelectList').subscribe((res: any) => {
    this.apiService.getall('fomCustomerSite/getSelectBranchCodeList').subscribe((res: any) => {
      this.citySelectionList = res as Array<any>;

      this.citySelectionList.forEach(e => {
        e.lable = e.value + "-" + e.text;
      });
    });

  }
  exportexcel(): void {
    let element = document.getElementById('printcontainer');
    const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(element);
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');
    XLSX.writeFile(wb, "CustomerSitesReport.xlsx");
  }
}
