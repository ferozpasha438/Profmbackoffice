using AutoMapper;
using CIN.Application.Common;
using CIN.Application.FomMgtDtos;
using CIN.DB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace CIN.Application.FomMgtQuery.ProfmQuery
{

    #region GetProjectSelectListByCustomerCode

    public class GetProjectSelectListByCustomerCode : IRequest<List<CustomSelectListItem>>
    {
        public UserIdentityDto User { get; set; }
        public int CustomerId { get; set; }
    }

    public class GetProjectSelectListByCustomerCodeHandler : IRequestHandler<GetProjectSelectListByCustomerCode, List<CustomSelectListItem>>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetProjectSelectListByCustomerCodeHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<CustomSelectListItem>> Handle(GetProjectSelectListByCustomerCode request, CancellationToken cancellationToken)
        {
            var customer = await _context.ErpFomDefCustomerMaster.Select(e => new { e.Id, e.CustCode }).FirstOrDefaultAsync(e => e.Id == request.CustomerId);
            var list = await _context.FomCustomerContracts.AsNoTracking().Where(s => s.CustCode == customer.CustCode)
              .Select(e => new CustomSelectListItem { Text = e.ContractCode, Value = e.ContractCode })
                 .ToListAsync(cancellationToken);
            return list;
        }
    }

    #endregion

    #region CafmDayWiseDetails

    public class CafmDayWiseDetails : IRequest<AssetMaintenanceReportListDto>
    {
        public UserIdentityDto User { get; set; }
        public string CustomerCode { get; set; }
        public string ProjCode { get; set; }
        public string DeptCode { get; set; }
        public string Status { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }

    public class CafmDayWiseDetailsHandler : IRequestHandler<CafmDayWiseDetails, AssetMaintenanceReportListDto>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public CafmDayWiseDetailsHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<AssetMaintenanceReportListDto> Handle(CafmDayWiseDetails request, CancellationToken cancellationToken)
        {
            //bool isDate = DateTime.TryParse(request.From, out DateTime trnDate);
            var list = _context.FomJobPlanMasters.Include(e => e.AssetMaster).AsNoTracking();
            var asstChilds = _context.FomJobPlanChildSchedules.AsNoTracking();

            var astMaintenaceList = from ast in list
                                    join ch in asstChilds
                                    on ast.JobPlanCode equals ch.JobPlanCode into chGp
                                    from chg in chGp.DefaultIfEmpty()
                                    select new AssetMaintenanceReportDto
                                    {
                                        AssetCode = ast.AssetCode,
                                        CustomerCode = ast.CustomerContract.CustCode,
                                        ChildCode = chg.ChildCode,
                                        Name = ast.AssetMaster.Name,
                                        ContractCode = ast.ContractCode,
                                        DeptCode = ast.DeptCode,
                                        Location = ast.AssetMaster.Location,
                                        PlanStartDate = chg.PlanStartDate,
                                        JobPlanCode = chg.JobPlanCode,
                                        Status = chg.IsClosed ? "Closed" : "Open"
                                    };

            if (request.CustomerCode.HasValue())
            {
                var customer = await _context.ErpFomDefCustomerMaster.Select(e => new { e.Id, e.CustCode }).FirstOrDefaultAsync(e => e.Id == Convert.ToInt32(request.CustomerCode));
                astMaintenaceList = astMaintenaceList.Where(e => e.CustomerCode == customer.CustCode);
            }

            if (request.ProjCode.HasValue())
                astMaintenaceList = astMaintenaceList.Where(e => e.ContractCode == request.ProjCode);

            if (request.DeptCode.HasValue())
            {
                astMaintenaceList = astMaintenaceList.Where(e => e.DeptCode == request.DeptCode);
            }

            if (request.Status.HasValue() && request.Status != "All")
            {
                astMaintenaceList = astMaintenaceList.Where(e => e.Status == request.Status);
            }

            if (request.From is not null && request.To is not null)
            {
                astMaintenaceList = astMaintenaceList.Where(e => EF.Functions.DateFromParts(e.PlanStartDate.Year, e.PlanStartDate.Month, e.PlanStartDate.Day) >= request.From
                && EF.Functions.DateFromParts(e.PlanStartDate.Year, e.PlanStartDate.Month, e.PlanStartDate.Day) <= request.To);

                //var list1 = list.Where(e => EF.Functions.DateFromParts(e.JobPlanDate.Year, e.JobPlanDate.Month, e.JobPlanDate.Day) < request.From);

                //list = list.Where(e => EF.Functions.DateFromParts(e.JobPlanDate.Year, e.JobPlanDate.Month, e.JobPlanDate.Day) >= request.From
                //&& EF.Functions.DateFromParts(e.JobPlanDate.Year, e.JobPlanDate.Month, e.JobPlanDate.Day) <= request.To);
            }



            var asstAndChild = new AssetMaintenanceReportListDto
            {
                List = await astMaintenaceList.OrderBy(e => e.PlanStartDate).ToListAsync(),
            };

            var company = await _context.Companies.FirstOrDefaultAsync(e => e.Id == request.User.CompanyId);
            if (company is not null)
            {
                asstAndChild.Company = new()
                {
                    CompanyName = company.CompanyName,
                    CompanyAddress = company.CompanyAddress,
                    Phone = company.Phone,
                    LogoURL = company.LogoURL,
                    //BranchName = branch.BranchName,
                };
            }

            return asstAndChild;
        }
    }

    #endregion


    #region CafmDayWiseSummary && Cafmjobanalysisprojectwise

    public class CafmDayWiseSummary : IRequest<JobPlanSummaryReportListDto>
    {
        public UserIdentityDto User { get; set; }
        public string CustomerCode { get; set; }
        public string ProjCode { get; set; }
        public string Projectwise { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }

    public class CafmDayWiseSummaryHandler : IRequestHandler<CafmDayWiseSummary, JobPlanSummaryReportListDto>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public CafmDayWiseSummaryHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<JobPlanSummaryReportListDto> Handle(CafmDayWiseSummary request, CancellationToken cancellationToken)
        {
            var list = _context.FomJobPlanMasters.Include(e => e.AssetMaster).AsNoTracking();
            var asstChilds = _context.FomJobPlanChildSchedules.AsNoTracking();

            var astMaintenaceList = from ast in list
                                    join ch in asstChilds
                                    on ast.JobPlanCode equals ch.JobPlanCode into chGp
                                    from chg in chGp.DefaultIfEmpty()
                                    select new JobPlanSummaryReportDto
                                    {
                                        AssetCode = ast.AssetCode,
                                        Name = ast.AssetMaster.Name,
                                        CustomerCode = ast.CustomerContract.CustCode,
                                        ContractCode = ast.ContractCode,
                                        PlanStartDate = chg.PlanStartDate,
                                        Status = chg.IsClosed
                                    };

            if (request.CustomerCode.HasValue())
            {
                var customer = await _context.ErpFomDefCustomerMaster.Select(e => new { e.Id, e.CustCode }).FirstOrDefaultAsync(e => e.Id == Convert.ToInt32(request.CustomerCode));
                astMaintenaceList = astMaintenaceList.Where(e => e.CustomerCode == customer.CustCode);
            }

            if (request.ProjCode.HasValue())
                astMaintenaceList = astMaintenaceList.Where(e => e.ContractCode == request.ProjCode);

            if (request.From is not null && request.To is not null)
            {
                astMaintenaceList = astMaintenaceList.Where(e => EF.Functions.DateFromParts(e.PlanStartDate.Year, e.PlanStartDate.Month, e.PlanStartDate.Day) >= request.From
                && EF.Functions.DateFromParts(e.PlanStartDate.Year, e.PlanStartDate.Month, e.PlanStartDate.Day) <= request.To);
            }

            bool hasProjectwise = request.Projectwise.HasValue();
            Func<string, string, SetGroupingDto> setGrouping = (prop1, prop2) => new SetGroupingDto(prop1, prop2);

            var gpdList = (await astMaintenaceList.OrderBy(e => e.PlanStartDate).ToListAsync()).GroupBy(e =>
                                 hasProjectwise ? setGrouping(e.CustomerCode, e.ContractCode) : setGrouping(e.AssetCode, e.Name)
                          ).ToList();

            JobPlanSummaryReportListDto asstAndChild = new JobPlanSummaryReportListDto
            {
                List = gpdList.Select(e => new JobPlanSummaryReportDto
                {
                    CustomerCode = hasProjectwise ? e.Key.Prop1 : string.Empty,
                    ContractCode = hasProjectwise ? e.Key.Prop2 : string.Empty,
                    AssetCode = !hasProjectwise ? e.Key.Prop2 : string.Empty,
                    Name = !hasProjectwise ? e.Key.Prop2 : string.Empty,
                    TotalJobs = e.Count(),
                    CompletedJobs = e.Where(e => e.Status == true).Count(),
                    OpenJobs = e.Where(e => e.Status == false).Count(),
                }).ToList()
            };

            var company = await _context.Companies.FirstOrDefaultAsync(e => e.Id == request.User.CompanyId);
            if (company is not null)
            {
                asstAndChild.Company = new()
                {
                    CompanyName = company.CompanyName,
                    CompanyAddress = company.CompanyAddress,
                    Phone = company.Phone,
                    LogoURL = company.LogoURL,
                    //BranchName = branch.BranchName,
                };
            }

            return asstAndChild;
        }
    }

    #endregion


    #region Cafmassetdetails

    public class Cafmassetdetails : IRequest<TblErpFomAssetMasterListDto>
    {
        public UserIdentityDto User { get; set; }
        public string CustomerCode { get; set; }
        public string ProjCode { get; set; }
    }

    public class CafmassetdetailsHandler : IRequestHandler<Cafmassetdetails, TblErpFomAssetMasterListDto>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public CafmassetdetailsHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<TblErpFomAssetMasterListDto> Handle(Cafmassetdetails request, CancellationToken cancellationToken)
        {
            var list = _context.FomAssetMasters.Include(e => e.CustomerContract).AsNoTracking();

            var astMaintenaceList = list.Select(ast => new TblErpFomAssetMasterDto
            {
                AssetCode = ast.AssetCode,
                Name = ast.Name,
                DeptCode = ast.DeptCode,
                AssetScale = ast.AssetScale,
                InstallDate = ast.InstallDate,
                ReplacementDate = ast.ReplacementDate,
                Location = ast.Location,
                SectionCode = ast.SectionCode,
                CustomerCode = ast.CustomerContract.CustCode,
                ContractCode = ast.ContractCode,
            });


            if (request.CustomerCode.HasValue())
            {
                var customer = await _context.ErpFomDefCustomerMaster.Select(e => new { e.Id, e.CustCode }).FirstOrDefaultAsync(e => e.Id == Convert.ToInt32(request.CustomerCode));
                astMaintenaceList = astMaintenaceList.Where(e => e.CustomerCode == customer.CustCode);
            }

            if (request.ProjCode.HasValue())
                astMaintenaceList = astMaintenaceList.Where(e => e.ContractCode == request.ProjCode);

            var asstAndChild = new TblErpFomAssetMasterListDto { List = await astMaintenaceList.ToListAsync() };

            var company = await _context.Companies.FirstOrDefaultAsync(e => e.Id == request.User.CompanyId);
            if (company is not null)
            {
                asstAndChild.Company = new()
                {
                    CompanyName = company.CompanyName,
                    CompanyAddress = company.CompanyAddress,
                    Phone = company.Phone,
                    LogoURL = company.LogoURL,
                    //BranchName = branch.BranchName,
                };
            }

            return asstAndChild;
        }
    }

    #endregion

    #region Cafmassetcostanalysis

    public class Cafmassetcostanalysis : IRequest<TblErpFomJobPlanScheduleClosureItemReportListDto>
    {
        public UserIdentityDto User { get; set; }
        public string CustomerCode { get; set; }
        public string ProjCode { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }

    public class CafmassetcostanalysisHandler : IRequestHandler<Cafmassetcostanalysis, TblErpFomJobPlanScheduleClosureItemReportListDto>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public CafmassetcostanalysisHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<TblErpFomJobPlanScheduleClosureItemReportListDto> Handle(Cafmassetcostanalysis request, CancellationToken cancellationToken)
        {

            var jobPlanMastList = _context.FomJobPlanMasters.Include(e => e.AssetMaster).Include(e => e.CustomerContract).AsNoTracking()
                .Select(e => new { e.JobPlanCode, e.ContractCode, e.CustomerContract.CustCode, e.PlanStartDate, e.AssetMaster.Name });

            var jobPlanSchList = _context.FomJobPlanScheduleClosures.AsNoTracking();

            var jobPlanSchItems = _context.FomJobPlanScheduleClosureItems.AsNoTracking();


            if (request.From is not null && request.To is not null)
            {
                jobPlanMastList = jobPlanMastList.Where(e => EF.Functions.DateFromParts(e.PlanStartDate.Year, e.PlanStartDate.Month, e.PlanStartDate.Day) >= request.From
                && EF.Functions.DateFromParts(e.PlanStartDate.Year, e.PlanStartDate.Month, e.PlanStartDate.Day) <= request.To);
            }

            if (request.CustomerCode.HasValue())
            {
                var customer = await _context.ErpFomDefCustomerMaster.Select(e => new { e.Id, e.CustCode }).FirstOrDefaultAsync(e => e.Id == Convert.ToInt32(request.CustomerCode));
                jobPlanMastList = jobPlanMastList.Where(e => e.CustCode == customer.CustCode);
            }

            if (request.ProjCode.HasValue())
                jobPlanMastList = jobPlanMastList.Where(e => e.ContractCode == request.ProjCode);


            var astMaintenaceList = from sch in jobPlanSchList
                                    join job in jobPlanMastList
                                    on sch.JobPlanCode equals job.JobPlanCode
                                    select new TblErpFomJobPlanScheduleClosureItemReportDto
                                    {
                                        CustomerCode = job.CustCode,
                                        JobPlanCode = sch.JobPlanCode,
                                        AssetCode = sch.AssetCode,
                                        Name = job.Name,
                                        ContractCode = job.ContractCode,
                                        Materials = (jobPlanSchItems.Where(sitem => sitem.Source == "mat" && sitem.ScheduleClosureId == sch.Id)
                                        .Select(sitem => new TblErpFomJobPlanScheduleClosureItemDto
                                        {
                                            Description = sitem.Description,
                                            Quantity = sitem.Quantity,
                                        })).ToList(),
                                        Tools = (jobPlanSchItems.Where(sitem => sitem.Source == "tl" && sitem.ScheduleClosureId == sch.Id)
                                        .Select(sitem => new TblErpFomJobPlanScheduleClosureItemDto
                                        {
                                            Description = sitem.Description,
                                            Quantity = sitem.Quantity,
                                        })).ToList(),
                                        LaborHours = (jobPlanSchItems.Where(sitem => sitem.Source == "labh" && sitem.ScheduleClosureId == sch.Id)
                                        .Select(sitem => new TblErpFomJobPlanScheduleClosureItemDto
                                        {
                                            Description = sitem.Description,
                                            Quantity = sitem.Quantity,
                                            Hours = sitem.Hours,
                                        })).ToList()
                                    };


            //if (request.CustomerCode.HasValue())
            //    astMaintenaceList = astMaintenaceList.Where(e => e.CustomerCode == request.CustomerCode);

            //if (request.ProjCode.HasValue())
            //    astMaintenaceList = astMaintenaceList.Where(e => e.ContractCode == request.ProjCode);

            var asstAndChild = new TblErpFomJobPlanScheduleClosureItemReportListDto { List = await astMaintenaceList.ToListAsync() };

            var company = await _context.Companies.FirstOrDefaultAsync(e => e.Id == request.User.CompanyId);
            if (company is not null)
            {
                asstAndChild.Company = new()
                {
                    CompanyName = company.CompanyName,
                    CompanyAddress = company.CompanyAddress,
                    Phone = company.Phone,
                    LogoURL = company.LogoURL,
                    //BranchName = branch.BranchName,
                };
            }

            return asstAndChild;
        }
    }

    #endregion
}
