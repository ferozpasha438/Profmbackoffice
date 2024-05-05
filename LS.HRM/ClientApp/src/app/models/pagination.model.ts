export class PaginationModel {
    selectItemsPerPage: number[] = [10, 25, 50, 100];
    pageSize = this.selectItemsPerPage[0];
    pageIndex = 0;
    allItemsLength = 0;
}

export class ReportPaginationModel {
  selectItemsPerPage: number[] = [100, 150, 200, 250, 300];
    pageSize = this.selectItemsPerPage[2];
    pageIndex = 0;   
}
