using CIN.Application.GeneralLedgerDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIN.Application.FomMgtDtos
{
    record SetGroupingDto(string Prop1, string Prop2);
    public class CommonCompanyReportDto
    {
        public string CompanyName { get; set; } = string.Empty;
        public string CompanyAddress { get; set; } = string.Empty;
        public string LogoURL { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
    public class AssetMaintenanceReportDto
    {
        public string AssetCode { get; set; }
        public string CustomerCode { get; set; }
        public string ChildCode { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string ContractCode { get; set; }
        public string DeptCode { get; set; }
        public DateTime PlanStartDate { get; set; }
        public string JobPlanCode { get; set; }
        public string Status { get; set; }
    }
    public class AssetMaintenanceReportListDto
    {
        public CommonCompanyReportDto Company { get; set; }
        public List<AssetMaintenanceReportDto> List { get; set; }
    }

    public class JobPlanSummaryReportDto
    {
        public string AssetCode { get; set; }
        public string CustomerCode { get; set; }
        public string Name { get; set; }
        public string ContractCode { get; set; }
        public DateTime PlanStartDate { get; set; }
        public bool Status { get; set; }
        public int TotalJobs { get; set; }
        public int CompletedJobs { get; set; }
        public int OpenJobs { get; set; }
    }
    public class JobPlanSummaryReportListDto
    {
        public CommonCompanyReportDto Company { get; set; }
        public List<JobPlanSummaryReportDto> List { get; set; }
    }
    public class TblErpFomAssetMasterListDto
    {
        public CommonCompanyReportDto Company { get; set; }
        public List<TblErpFomAssetMasterDto> List { get; set; }
    }
    public class TblErpFomJobPlanScheduleClosureItemReportDto : TblErpFomJobPlanScheduleClosureDto
    {
        public string ContractCode { get; set; }
        public string CustomerCode { get; set; }
        public string Name { get; set; }
    }
    public class TblErpFomJobPlanScheduleClosureItemReportListDto
    {
        public CommonCompanyReportDto Company { get; set; }
        public List<TblErpFomJobPlanScheduleClosureItemReportDto> List { get; set; }
    }


}
