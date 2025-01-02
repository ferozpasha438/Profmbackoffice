using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CIN.Application.Common;
using CIN.Application.FomMobDtos;
using CIN.DB;
using CIN.Domain.FomMgt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;

namespace CIN.Application.FomMobQuery
{

    #region GetWebDashboardData

    public class WebDashboardMonths
    {
        public int month { get; set; }
        public int year { get; set; }
        public string name { get; set; }
    }

    public class GetWebDashboardData1Query : IRequest<Out_FomWebDashBoardTicketsData>
    {
        //  public UserIdentityDto User { get; set; }


    }

    public class GetWebDashboardData1QueryHandler : IRequestHandler<GetWebDashboardData1Query, Out_FomWebDashBoardTicketsData>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetWebDashboardData1QueryHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Out_FomWebDashBoardTicketsData> Handle(GetWebDashboardData1Query request, CancellationToken cancellationToken)
        {
            int maxNumberOfDepartments = 5;
            int maxNumberOfRecentTickets = 20;
            List<WebDashboardMonths> months = new();
            List<string> departments = new();

            try
            {
                Out_FomWebDashBoardTicketsData res = new();
                DateTime toDate = DateTime.UtcNow;
                DateTime fromDate = toDate.AddDays(-365);
                DateTime curMonthfromDate = toDate.AddDays(-30);
                DateTime temp = toDate.AddMonths(-5);

                while (months.Count <= 5)
                {
                    months.Add(new WebDashboardMonths
                    {
                        month = temp.Month,
                        year = temp.Year,
                        name = temp.ToString("MMM")
                    });
                    temp = temp.AddMonths(1);
                }

                var Departments = _context.ErpFomDepartments.AsNoTracking();
                var TotalTickets = await _context.FomB2CJobTickets.Where(e => e.JODate >= fromDate && e.JODate <= toDate && e.IsActive && !e.IsClosed && !e.IsForeClosed && !e.IsVoid).ToListAsync();
                var ClosedTickets = await _context.FomB2CJobTickets.Where(e => e.JODate >= fromDate && e.JODate <= toDate && e.IsActive && e.IsClosed).ToListAsync();

                res.TotalTickets = TotalTickets.Count;
                res.ClosedTickets = ClosedTickets.Count;
                res.PendingTickets = res.TotalTickets - res.ClosedTickets;

                foreach (var m in months)
                {
                    int daysinmonth = DateTime.DaysInMonth(m.year, m.month);
                    res.MonthWiseData.Add(new Out_FomWebDashBoardMonthWiseTicketsData
                    {
                        TotalTickets = TotalTickets.Count(e => e.JODate >= new DateTime(m.year, m.month, 1) && e.JODate <= new DateTime(m.year, m.month, daysinmonth)),
                        ClosedTickets = ClosedTickets.Count(e => e.JODate >= new DateTime(m.year, m.month, 1) && e.JODate <= new DateTime(m.year, m.month, daysinmonth)),
                        PendingTickets = TotalTickets.Count(e => e.JODate >= new DateTime(m.year, m.month, 1) && e.JODate <= new DateTime(m.year, m.month, daysinmonth)) - ClosedTickets.Count(e => e.JODate >= new DateTime(m.year, m.month, 1) && e.JODate <= new DateTime(m.year, m.month, daysinmonth)),
                        Month = m.name
                    });

                    res.MonthsNames.Add(m.name);
                    res.MonthlyTotalTickets.Add(TotalTickets.Count(e => e.JODate >= new DateTime(m.year, m.month, 1) && e.JODate <= new DateTime(m.year, m.month, daysinmonth)));
                }

                departments = TotalTickets.GroupBy(e => e.JODeptCode).Select(g => g.Key).ToList();

                foreach (var d in departments)
                {
                    res.DepWiseData.Add(
                        new Out_FomWebDashBoardDepWiseTicketsData
                        {
                            TotalTickets = TotalTickets.Count(e => e.JODate >= curMonthfromDate && e.JODeptCode == d),
                            ClosedTickets = ClosedTickets.Count(e => e.JODate >= curMonthfromDate && e.JODeptCode == d),
                            PendingTickets = TotalTickets.Count(e => e.JODate >= curMonthfromDate && e.JODeptCode == d) - ClosedTickets.Count(e => e.JODate >= curMonthfromDate && e.JODeptCode == d),
                            Department = d,
                            DepartmentNameEng = Departments.FirstOrDefault(dp => dp.DeptCode == d).NameEng,
                            DepartmentNameArb = Departments.FirstOrDefault(dp => dp.DeptCode == d).NameArabic,
                        });

                }




                res.Last30DaysData.TotalTickets = res.DepWiseData.Sum(e => e.TotalTickets);
                res.Last30DaysData.ClosedTickets = res.DepWiseData.Sum(e => e.ClosedTickets);
                res.Last30DaysData.PendingTickets = res.DepWiseData.Sum(e => e.PendingTickets);

                List<string> statusTypes = new List<string> { MetadataJoStatusEnum.Open.ToString(), MetadataJoStatusEnum.Closed.ToString(), MetadataJoStatusEnum.WorkInProgress.ToString() };
                foreach (string s in statusTypes)
                {
                    List<Out_FomWebDashBoardDepStatusWiseTicketsCount> depwisedata = new();
                    foreach (var d in departments)
                    {
                        depwisedata.Add(
                        new Out_FomWebDashBoardDepStatusWiseTicketsCount
                        {
                            Count = TotalTickets.Count(e => e.JODate >= curMonthfromDate && e.JODeptCode == d && (((MetadataJoStatusEnum)e.JOStatus).ToString()) == s),
                            Department = d,
                            DepartmentNameEng = Departments.FirstOrDefault(dp => dp.DeptCode == d).NameEng,
                            DepartmentNameArb = Departments.FirstOrDefault(dp => dp.DeptCode == d).NameArabic,
                        });

                    }

                    res.DepAndStatusWiseData.Add(new Out_FomWebDashBoardDepAndStatusWiseTicketsData
                    {
                        StatusStr = s,
                        DepsDataList = depwisedata.OrderByDescending(e => e.Count).ToList()
                    });
                }
                if (res.DepWiseData.Count > 5)
                {
                    List<string> TopDeps = res.DepWiseData.OrderByDescending(e => e.TotalTickets).Take(maxNumberOfDepartments).Select(e => e.Department).ToList();
                    res.DepWiseData.Add(
                        new Out_FomWebDashBoardDepWiseTicketsData
                        {
                            TotalTickets = TotalTickets.Count(e => e.JODate >= curMonthfromDate && !TopDeps.Contains(e.JODeptCode)),
                            ClosedTickets = ClosedTickets.Count(e => e.JODate >= curMonthfromDate && !TopDeps.Contains(e.JODeptCode)),
                            PendingTickets = TotalTickets.Count(e => e.JODate >= curMonthfromDate && !TopDeps.Contains(e.JODeptCode)) - ClosedTickets.Count(e => e.JODate >= curMonthfromDate && !TopDeps.Contains(e.JODeptCode)),
                            Department = "Others",
                            DepartmentNameEng = "Others",
                            DepartmentNameArb = "Others",
                        });
                }


                res.RecentTickets = TotalTickets.OrderByDescending(e => e.Id).Select(s => new Out_FomWebDashBoardRecenetTickets { Date = s.JODate, MaintananceType = s.JobMaintenanceType, ProjectNameEng = "", ProjectNameArb = "", StatusStr = ((MetadataJoStatusEnum)s.JOStatus).ToString(), TicketNumber = s.TicketNumber }).Take(maxNumberOfRecentTickets).ToList();


                return res;
            }
            catch (Exception e)
            {

                return new();
            }
        }
    }
    #endregion




}
