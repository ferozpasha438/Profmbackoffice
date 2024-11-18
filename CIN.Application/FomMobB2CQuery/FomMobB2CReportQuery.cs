using AutoMapper;
using CIN.Application.Common;
using CIN.Application.FomMobB2CDtos;
using CIN.DB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CIN.Application.FomMobB2CQuery
{


    #region GetB2CTickethistoryQuery

    public class GetB2CTickethistoryQuery : IRequest<B2CReportTickethistoryListDto>
    {
        public UserIdentityDto User { get; set; }
        public B2CReportTicketSearchDto Input { get; set; }
    }

    public class GetB2CTickethistoryQueryHandler : IRequestHandler<GetB2CTickethistoryQuery, B2CReportTickethistoryListDto>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetB2CTickethistoryQueryHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<B2CReportTickethistoryListDto> Handle(GetB2CTickethistoryQuery request, CancellationToken cancellationToken)
        {
            bool isArab = request.User.Culture.IsArab();
            var search = request.Input;
            var resources = _context.FomResources.AsNoTracking().Select(e => new { e.ResCode, e.NameEng, e.NameAr });
            var cStatements = _context.FomB2CJobTickets.Where(e => e.TicketNumber != null)// && e.TicketNumber.Length > 0
                 .Include(e => e.SysCustomer)
                 .OrderByDescending(e => e.Id)
                 .AsNoTracking();

            if (search.CustCode.HasValue())
                cStatements = cStatements.Where(e => e.CustomerCode == search.CustCode);

            if (search.ResourceCode.HasValue())
                cStatements = cStatements.Where(e => e.ResCode == search.ResourceCode);

            if (search.ServiceType.HasValue())
                cStatements = cStatements.Where(e => e.SchType == search.ServiceType);

            if (search.TicketStatus is not null && search.TicketStatus > 0)
                cStatements = cStatements.Where(e => e.JOStatus == search.TicketStatus);

            if (search.DateFrom is not null && search.DateTo is not null)
                cStatements = cStatements.Where(e => EF.Functions.DateFromParts(e.JODate.Year, e.JODate.Month, e.JODate.Day) >= search.DateFrom && EF.Functions.DateFromParts(e.JODate.Year, e.JODate.Month, e.JODate.Day) <= search.DateTo);


            //var reportCount = await cStatements.CountAsync();
            //cStatements = cStatements.Pagination(search.ReportIndex, search.ReportCount);

            var statements = await cStatements.Select(e => new B2CTickethistoryListDto
            {
                TicketNumber = e.TicketNumber,
                CustCode = e.CustomerCode,
                CustName = isArab ? e.SysCustomer.CustArbName : e.SysCustomer.CustName,
                Date = e.JODate,
                Time = e.OnlyTime.ToString(),
                StatusStr = ((MetadataJoStatusEnum)e.JOStatus).ToString(),
                ServiceType = e.SchType.B2cFrequency(),
                ResourceName = isArab ? resources.FirstOrDefault(res => res.ResCode == e.ResCode).NameAr : resources.FirstOrDefault(res => res.ResCode == e.ResCode).NameEng,

            }).ToListAsync();

            B2CReportTickethistoryListDto historyReport = new()
            {
                ListItems = new() { List = statements, ReportCount = 0 }
            };

            return historyReport;

        }
    }


    #endregion


    #region GetB2CTicketSummarybycustQuery

    public class GetB2CTicketSummarybycustQuery : IRequest<B2CReportTicketSummarybycustListDto>
    {
        public UserIdentityDto User { get; set; }
        public B2CReportTicketSearchDto Input { get; set; }
    }

    public class GetB2CTicketSummarybycustQueryHandler : IRequestHandler<GetB2CTicketSummarybycustQuery, B2CReportTicketSummarybycustListDto>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetB2CTicketSummarybycustQueryHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<B2CReportTicketSummarybycustListDto> Handle(GetB2CTicketSummarybycustQuery request, CancellationToken cancellationToken)
        {
            bool isArab = request.User.Culture.IsArab();
            var search = request.Input;
            var customers = _context.OprCustomers.AsNoTracking();
            var resources = _context.FomResources.AsNoTracking().Select(e => new { e.ResCode, e.NameEng, e.NameAr });
            var cStatements = _context.FomB2CJobTickets.Where(e => e.TicketNumber != null)// && e.TicketNumber.Length > 0
                                                                                          // .Include(e => e.SysCustomer)
                 .OrderByDescending(e => e.Id)
                 .AsNoTracking();

            if (search.CustCode.HasValue())
                cStatements = cStatements.Where(e => e.CustomerCode == search.CustCode);

            if (search.ResourceCode.HasValue())
                cStatements = cStatements.Where(e => e.ResCode == search.ResourceCode);

            if (search.ServiceType.HasValue())
                cStatements = cStatements.Where(e => e.SchType == search.ServiceType);

            if (search.DateFrom is not null && search.DateTo is not null)
                cStatements = cStatements.Where(e => EF.Functions.DateFromParts(e.JODate.Year, e.JODate.Month, e.JODate.Day) >= search.DateFrom && EF.Functions.DateFromParts(e.JODate.Year, e.JODate.Month, e.JODate.Day) <= search.DateTo);


            //var reportCount = await cStatements.CountAsync();
            //cStatements = cStatements.Pagination(search.ReportIndex, search.ReportCount);

            var ticketSummary = (await cStatements.ToListAsync()).GroupBy(e => e.CustomerCode);// e.SndCustomerMaster.CustName });

            List<B2CTicketSummarybycustListDto> summaries = new();
            foreach (var item in ticketSummary)
            {
                var customer = await customers.Select(e => new { e.CustName, e.CustArbName, e.CustCode }).FirstOrDefaultAsync(e => e.CustCode == item.Key);
                summaries.Add(new B2CTicketSummarybycustListDto
                {
                    CustCode = item.Key,
                    CustName = isArab ? customer.CustArbName : customer.CustName,
                    Open = item.Where(e => e.JOStatus == 0).Count(),
                    Approved = item.Where(e => e.JOStatus == 5).Count(),
                    Closed = item.Where(e => e.JOStatus == 9).Count(),
                    Completed = item.Where(e => e.JOStatus == 11).Count(),
                    Void = item.Where(e => e.JOStatus == 3).Count(),
                    Total = item.Count()
                });
            }

            B2CReportTicketSummarybycustListDto historyReport = new()
            {
                ListItems = new() { List = summaries, ReportCount = 0 }
            };

            return historyReport;

        }
    }


    #endregion



    #region GetB2CTicketSummarybyservicetypeQuery

    public class GetB2CTicketSummarybyservicetypeQuery : IRequest<B2CReportTicketSummarybycustListDto>
    {
        public UserIdentityDto User { get; set; }
        public B2CReportTicketSearchDto Input { get; set; }
    }

    public class GetB2CTicketSummarybyservicetypeQueryHandler : IRequestHandler<GetB2CTicketSummarybyservicetypeQuery, B2CReportTicketSummarybycustListDto>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetB2CTicketSummarybyservicetypeQueryHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<B2CReportTicketSummarybycustListDto> Handle(GetB2CTicketSummarybyservicetypeQuery request, CancellationToken cancellationToken)
        {
            bool isArab = request.User.Culture.IsArab();
            var search = request.Input;
            var customers = _context.OprCustomers.AsNoTracking();
            var resources = _context.FomResources.AsNoTracking().Select(e => new { e.ResCode, e.NameEng, e.NameAr });
            var cStatements = _context.FomB2CJobTickets.Where(e => e.TicketNumber != null)// && e.TicketNumber.Length > 0
                                                                                          // .Include(e => e.SysCustomer)
                 .OrderByDescending(e => e.Id)
                 .AsNoTracking();

            if (search.CustCode.HasValue())
                cStatements = cStatements.Where(e => e.CustomerCode == search.CustCode);

            if (search.ResourceCode.HasValue())
                cStatements = cStatements.Where(e => e.ResCode == search.ResourceCode);

            //if (search.ServiceType.HasValue())
            //    cStatements = cStatements.Where(e => e.SchType == search.ServiceType);

            if (search.DateFrom is not null && search.DateTo is not null)
                cStatements = cStatements.Where(e => EF.Functions.DateFromParts(e.JODate.Year, e.JODate.Month, e.JODate.Day) >= search.DateFrom && EF.Functions.DateFromParts(e.JODate.Year, e.JODate.Month, e.JODate.Day) <= search.DateTo);


            //var reportCount = await cStatements.CountAsync();
            //cStatements = cStatements.Pagination(search.ReportIndex, search.ReportCount);

            var ticketSummary = (await cStatements.ToListAsync()).GroupBy(e => e.SchType);// e.SndCustomerMaster.CustName });

            List<B2CTicketSummarybycustListDto> summaries = new();
            foreach (var item in ticketSummary)
            {
                var customer = await customers.Select(e => new { e.CustName, e.CustArbName, e.CustCode }).FirstOrDefaultAsync(e => e.CustCode == item.Key);
                summaries.Add(new B2CTicketSummarybycustListDto
                {
                    SchType = item.Key.B2cFrequency(),
                    Open = item.Where(e => e.JOStatus == 0).Count(),
                    Approved = item.Where(e => e.JOStatus == 5).Count(),
                    Closed = item.Where(e => e.JOStatus == 9).Count(),
                    Completed = item.Where(e => e.JOStatus == 11).Count(),
                    Void = item.Where(e => e.JOStatus == 3).Count(),
                    Total = item.Count()
                });
            }



            B2CReportTicketSummarybycustListDto historyReport = new()
            {
                ListItems = new() { List = summaries, ReportCount = 0 }
            };

            return historyReport;

        }
    }


    #endregion


}
