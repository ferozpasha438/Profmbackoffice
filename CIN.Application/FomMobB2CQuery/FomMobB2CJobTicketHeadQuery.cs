using AutoMapper;
using CIN.Application.Common;
using CIN.Application.FomMobDtos;
using CIN.DB;
using CIN.Domain.FomMgt;
using CIN.Domain.FomMob;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;

namespace CIN.Application.FomMobB2CQuery
{


    #region CreateUpdateB2CJobTicketHeadQuery          //by Customer from mobile

    public class CreateUpdateB2CJobTicketHeadQuery : IRequest<(bool, string)>
    {
        public UserIdentityDto User { get; set; }
        public InputCreateUpdateB2CJobTicketDto Input { get; set; }
    }

    public class CreateUpdateB2CJobTicketHeadQueryHandler : IRequestHandler<CreateUpdateB2CJobTicketHeadQuery, (bool, string)>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;

        public CreateUpdateB2CJobTicketHeadQueryHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<(bool, string)> Handle(CreateUpdateB2CJobTicketHeadQuery request, CancellationToken cancellationToken)
        {


            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    Log.Info("----Info Create Update Profm Company method start----");

                    var obj = request.Input;


                    var customer = await _context.ErpFomDefCustomerMaster.FirstOrDefaultAsync(e => e.CustCode == request.Input.CustomerCode);
                    if (customer is null)
                    {
                        return (false, "Invalid customer");
                    }

                    var JobTicketHead = await _context.FomB2CJobTickets.FirstOrDefaultAsync(e => e.Id == obj.Id) ?? new() { Id = 0, TicketType = "B2C", SchType = "D" };

                    decimal geoLatitude = 0, geoLongitude = 0;

                    var custSite = await _context.ProfmDefSiteMaster.Select(e => new { e.CustomerCode, e.SiteGeoLatitude, e.SiteGeoLongitude })
                                                 .FirstOrDefaultAsync(e => e.CustomerCode == obj.CustomerCode);
                    if (obj.GeoLatitude > 0 && obj.GeoLongitude > 0)
                    {
                        geoLatitude = obj.GeoLatitude;
                        geoLongitude = obj.GeoLongitude;
                    }
                    else if (custSite is not null)
                    {
                        geoLatitude = custSite.SiteGeoLatitude;
                        geoLongitude = custSite.SiteGeoLongitude;
                    }

                    JobTicketHead.CustomerCode = obj.CustomerCode;
                    JobTicketHead.JODeptCode = obj.JODeptCode;
                    JobTicketHead.JOSubject = obj.JOSubject;
                    JobTicketHead.JODescription = obj.JODescription;
                    JobTicketHead.JOBookedBy = string.IsNullOrEmpty(obj.JOBookedBy) ? request.User.Email : obj.JOBookedBy;
                    JobTicketHead.JobMaintenanceType = "Corrective";
                    JobTicketHead.IsActive = true;
                    JobTicketHead.JOStatus = (int)MetadataJoStatusEnum.Open;
                    JobTicketHead.JODate = DateTime.Now;
                    JobTicketHead.CustRegEmail = customer.CustEmail1;
                    JobTicketHead.IsCreatedByCustomer = true;
                    JobTicketHead.IsApproved = false;
                    JobTicketHead.IsOpen = true;
                    JobTicketHead.IsInScope = false;
                    JobTicketHead.JOSupervisor = obj.JOSupervisor;

                    JobTicketHead.GeoLatitude = geoLatitude;
                    JobTicketHead.GeoLongitude = geoLongitude;

                    JobTicketHead.OnlyTime = TimeSpan.Parse(obj.TimeValue ?? "00:00:00");
                    JobTicketHead.TicketNumber = string.Empty;

                    if (obj.Id > 0)
                    {
                        JobTicketHead.ModifiedBy = request.User.Email;
                        JobTicketHead.ModifiedOn = DateTime.UtcNow;
                        _context.FomB2CJobTickets.Update(JobTicketHead);

                    }
                    else
                    {
                        var SNG = await _context.FomMobSequenceNumberGenerator.AsNoTracking().FirstOrDefaultAsync(e => e.IsForJobTicket.Value == true);
                        if (SNG is not null)
                        {
                            SNG.SequenceNumber++;
                            JobTicketHead.SpTicketNumber = "SCKT" + SNG.SequenceNumber.ToString().PadLeft(SNG.Length, '0');

                            _context.FomMobSequenceNumberGenerator.Update(SNG);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            TblFomMobSequenceNumberGenerator sng = new()
                            {
                                IsForJobTicket = true,
                                Length = 6,
                                Prefix = "TCKT",
                                SequenceNumber = 1
                            };
                            JobTicketHead.SpTicketNumber = "SCKT0000001";
                            await _context.FomMobSequenceNumberGenerator.AddAsync(sng);
                            await _context.SaveChangesAsync();
                        }

                        if (await _context.FomB2CJobTickets.AnyAsync(e => e.SpTicketNumber == JobTicketHead.SpTicketNumber))
                        {
                            await transaction.RollbackAsync();
                            return (false, "ticket already exist");
                        }

                        JobTicketHead.CreatedBy = request.User.Email;
                        JobTicketHead.CreatedOn = DateTime.UtcNow;
                        await _context.FomB2CJobTickets.AddAsync(JobTicketHead);
                        await _context.SaveChangesAsync();
                    }

                    _context.FomB2CJobTickets.Update(JobTicketHead);
                    await _context.SaveChangesAsync();

                    Log.Info("----Info Create Update Profm JobTicketHead method Exit----");
                    await transaction.CommitAsync();
                    return (true, JobTicketHead.SpTicketNumber);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Log.Error("Error in Create Upda te Profm JobTicketHead Method");
                    Log.Error("Error occured time : " + DateTime.UtcNow);
                    Log.Error("Error message : " + ex.Message);
                    Log.Error("Error StackTrace : " + ex.StackTrace);
                    return (false, ex.Message);
                }
            }
        }

    }

    #endregion



    #region CreateUpdateB2CMonthlyYearlyJobTicket          //by Customer from mobile

    public class CreateUpdateB2CMonthlyYearlyJobTicket : IRequest<(bool, string)>
    {
        public UserIdentityDto User { get; set; }
        public InputCreateUpdateB2CJobTicketDto Input { get; set; }
    }

    public class CreateUpdateB2CMonthlyYearlyJobTicketHandler : IRequestHandler<CreateUpdateB2CMonthlyYearlyJobTicket, (bool, string)>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;

        public CreateUpdateB2CMonthlyYearlyJobTicketHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<(bool, string)> Handle(CreateUpdateB2CMonthlyYearlyJobTicket request, CancellationToken cancellationToken)
        {


            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    Log.Info("----Info Create Update Profm Company method start----");

                    var obj = request.Input;


                    var customer = await _context.ErpFomDefCustomerMaster.FirstOrDefaultAsync(e => e.CustCode == request.Input.CustomerCode);
                    if (customer is null)
                    {
                        return (false, "Invalid customer");
                    }

                    var JobTicketHead = await _context.FomB2CJobTickets.FirstOrDefaultAsync(e => e.Id == obj.Id) ?? new() { Id = 0, TicketType = "B2C" };

                    JobTicketHead.CustomerCode = obj.CustomerCode;
                    JobTicketHead.JODeptCode = obj.JODeptCode;
                    JobTicketHead.JOSubject = obj.JOSubject;
                    JobTicketHead.JODescription = obj.JODescription;
                    JobTicketHead.JOBookedBy = string.IsNullOrEmpty(obj.JOBookedBy) ? request.User.Email : obj.JOBookedBy;
                    JobTicketHead.JobMaintenanceType = "Corrective";
                    JobTicketHead.IsActive = true;
                    JobTicketHead.JOStatus = (int)MetadataJoStatusEnum.Open;
                    JobTicketHead.JODate = DateTime.Now;
                    JobTicketHead.CustRegEmail = customer.CustEmail1;
                    JobTicketHead.IsCreatedByCustomer = true;
                    JobTicketHead.IsApproved = false;
                    JobTicketHead.IsOpen = true;
                    JobTicketHead.IsInScope = false;
                    JobTicketHead.JOSupervisor = obj.JOSupervisor;
                    JobTicketHead.GeoLatitude = obj.GeoLatitude;
                    JobTicketHead.GeoLongitude = obj.GeoLongitude;
                    JobTicketHead.OnlyTime = TimeSpan.Parse(obj.TimeValue ?? "00:00:00");
                    JobTicketHead.TicketNumber = string.Empty;

                    if (obj.Id > 0)
                    {
                        JobTicketHead.ModifiedBy = request.User.Email;
                        JobTicketHead.ModifiedOn = DateTime.UtcNow;
                        _context.FomB2CJobTickets.Update(JobTicketHead);

                    }
                    else
                    {
                        var SNG = await _context.FomMobSequenceNumberGenerator.AsNoTracking().FirstOrDefaultAsync(e => e.IsForJobTicket.Value == true);
                        if (SNG is not null)
                        {
                            SNG.SequenceNumber++;
                            JobTicketHead.SpTicketNumber = "SCKT" + SNG.SequenceNumber.ToString().PadLeft(SNG.Length, '0');

                            _context.FomMobSequenceNumberGenerator.Update(SNG);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            TblFomMobSequenceNumberGenerator sng = new()
                            {
                                IsForJobTicket = true,
                                Length = 6,
                                Prefix = "TCKT",
                                SequenceNumber = 1
                            };
                            JobTicketHead.SpTicketNumber = "SCKT0000001";
                            await _context.FomMobSequenceNumberGenerator.AddAsync(sng);
                            await _context.SaveChangesAsync();
                        }

                        if (await _context.FomB2CJobTickets.AnyAsync(e => e.SpTicketNumber == JobTicketHead.SpTicketNumber))
                        {
                            await transaction.RollbackAsync();
                            return (false, "ticket already exist");
                        }

                        JobTicketHead.CreatedBy = request.User.Email;
                        JobTicketHead.CreatedOn = DateTime.UtcNow;
                        await _context.FomB2CJobTickets.AddAsync(JobTicketHead);
                        await _context.SaveChangesAsync();
                    }

                    _context.FomB2CJobTickets.Update(JobTicketHead);
                    await _context.SaveChangesAsync();

                    Log.Info("----Info Create Update Profm JobTicketHead method Exit----");
                    await transaction.CommitAsync();
                    return (true, JobTicketHead.SpTicketNumber);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Log.Error("Error in Create Upda te Profm JobTicketHead Method");
                    Log.Error("Error occured time : " + DateTime.UtcNow);
                    Log.Error("Error message : " + ex.Message);
                    Log.Error("Error StackTrace : " + ex.StackTrace);
                    return (false, ex.Message);
                }
            }
        }

    }

    #endregion




    #region CreateB2CJobTicketPaymentQuery          //by Customer from mobile

    public class CreateB2CJobTicketPaymentQuery : IRequest<(bool, string)>
    {
        public UserIdentityDto User { get; set; }
        public InputCreateUpdateB2CJobTicketDto Input { get; set; }
    }

    public class CreateB2CJobTicketPaymentQueryHandler : IRequestHandler<CreateB2CJobTicketPaymentQuery, (bool, string)>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;

        public CreateB2CJobTicketPaymentQueryHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<(bool, string)> Handle(CreateB2CJobTicketPaymentQuery request, CancellationToken cancellationToken)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    Log.Info("----Info CreateB2CJobTicketPaymentQuery method start----");
                    var obj = request.Input;
                    var SNG = await _context.FomMobSequenceNumberGenerator.AsNoTracking().FirstOrDefaultAsync(e => e.IsForJobTicket.Value == true);

                    if (obj.ContractId > 0 && obj.ServiceType.HasValue() && obj.ServiceType == "nooneday")
                    {
                        var scheduleDetailIds = _context.FomScheduleDetails.AsNoTracking().Where(e => e.ContractId == obj.ContractId).Select(e => e.Id);
                        if (scheduleDetailIds.Any(id => id > 0))
                        {
                            var b2cTickets = await _context.FomB2CJobTickets.Where(e => scheduleDetailIds.Any(sid => sid == e.SchDetailId)).ToListAsync();
                            foreach (var ticket in b2cTickets)
                            {
                                ticket.TicketNumber = $"{SNG.Prefix}{(ticket.SpTicketNumber.Replace("SCKT", ""))}";
                            }

                            _context.FomB2CJobTickets.UpdateRange(b2cTickets);
                            await _context.SaveChangesAsync();

                            var customerContract = await _context.FomCustomerContracts.Select(e => new { e.ContractCode, e.Id }).FirstOrDefaultAsync(e => e.Id == obj.ContractId);
                            TblFomJobTicketPayment payment = new() { TicketNumber = customerContract.ContractCode, TokenNumber = obj.TokenNumber, IsDayService = false, Response = obj.Response, Date = DateTime.Now };

                            await _context.FomJobTicketPayments.AddAsync(payment);
                            await _context.SaveChangesAsync();
                        }
                        else
                            return (false, "No JobTicket Found");
                    }
                    else
                    {
                        var JobTicketHead = await _context.FomB2CJobTickets.FirstOrDefaultAsync(e => e.SpTicketNumber == obj.TicketNumber);
                        if (JobTicketHead is null)
                        {
                            return (false, "No JobTicket Found");
                        }

                        JobTicketHead.TicketNumber = $"{SNG.Prefix}{(JobTicketHead.SpTicketNumber.Replace("SCKT", ""))}";

                        _context.FomB2CJobTickets.Update(JobTicketHead);
                        await _context.SaveChangesAsync();


                        TblFomJobTicketPayment payment = new() { TicketNumber = JobTicketHead.TicketNumber, TokenNumber = obj.TokenNumber, IsDayService = true, Response = obj.Response, Date = DateTime.Now };

                        await _context.FomJobTicketPayments.AddAsync(payment);
                        await _context.SaveChangesAsync();
                    }

                    Log.Info("----Info CreateB2CJobTicketPaymentQuery method Exit----");
                    await transaction.CommitAsync();
                    return (true, "");

                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Log.Error("Error in CreateB2CJobTicketPaymentQuery Method");
                    Log.Error("Error occured time : " + DateTime.UtcNow);
                    Log.Error("Error message : " + ex.Message);
                    Log.Error("Error StackTrace : " + ex.StackTrace);
                    return (false, ex.Message);
                }
            }

        }

    }

    #endregion


    #region GetB2CDefaultPaymentPrices

    public class GetB2CDefaultPaymentPricesQuery : IRequest<List<CustomSelectListItem>>
    {
        public UserIdentityDto User { get; set; }
        public string Type { get; set; }
    }

    public class GetB2CDefaultPaymentPricesQueryHandler : IRequestHandler<GetB2CDefaultPaymentPricesQuery, List<CustomSelectListItem>>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetB2CDefaultPaymentPricesQueryHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<CustomSelectListItem>> Handle(GetB2CDefaultPaymentPricesQuery request, CancellationToken cancellationToken)
        {
            return await _context.FomB2CDefaultPaymentPrices
                .Where(e => e.PayType == request.Type)
                .Select(e => new CustomSelectListItem
                {
                    Text = e.PayType,
                    Value = e.Price.ToString()
                }).ToListAsync();
        }
    }

    #endregion


    #region GetB2CTicketsListPaginationWithFilter

    public class GetB2CTicketsListPaginationWithFilterQuery : IRequest<PaginatedList<RecentB2CTicketsDto>>
    {
        public UserIdentityDto User { get; set; }
        public InputUserTicketsPaginationFilterDto Input { get; set; }
    }

    public class GetB2CTicketsListPaginationWithFilterQueryHandler : IRequestHandler<GetB2CTicketsListPaginationWithFilterQuery, PaginatedList<RecentB2CTicketsDto>>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetB2CTicketsListPaginationWithFilterQueryHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedList<RecentB2CTicketsDto>> Handle(GetB2CTicketsListPaginationWithFilterQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Input.ToDate is not null)
                {
                    request.Input.ToDate = request.Input.ToDate.Value.AddDays(1);
                }
                var contracts = _context.FomCustomerContracts.Where(e => e.ContProjSupervisor == request.User.UserName || e.ContProjManager == request.User.UserName).Select(s => new { s.CustCode, s.CustSiteCode }).AsNoTracking();
                var search = request.Input.Query;
                var Customers = _context.ErpFomDefCustomerMaster.AsNoTracking();
                var departments = _context.ErpFomDepartments.AsNoTracking();
                //var logNotes = request.User.LoginType == "client" ? _context.FomJobTicketLogNotes.Where(e => e.ShowToCustomer).AsNoTracking()
                //    : _context.FomJobTicketLogNotes.AsNoTracking();

                var list = _context.FomB2CJobTickets.AsNoTracking()
                    .Where(e => e.IsCreatedByCustomer == true && e.TicketType == "B2C")
                    .Select(e => new RecentB2CTicketsDto
                    {
                        TicketNumber = e.TicketNumber,
                        WorkStartDate = e.WorkStartDate,
                        WorkOrders = e.WorkOrders,
                        //SiteCode = e.SiteCode,
                        QuotationNumber = e.QuotationNumber,
                        QuotationDate = e.QuotationDate,
                        ActWorkEndDate = e.ActWorkEndDate,
                        ApprovedBy = e.ApprovedBy,
                        ApprovedDate = e.ApprovedDate,
                        CancelBy = e.CancelBy,
                        CancelDate = e.CancelDate ?? DateTime.Now,
                        CancelReasonCode = e.CancelReasonCode,
                        ClosedBy = e.ClosedBy,
                        ClosingRemarks = e.ClosingRemarks,
                        CreatedBy = e.CreatedBy,
                        CreatedOn = e.CreatedOn,
                        CustomerCode = e.CustomerCode,
                        CustRegEmail = e.CustRegEmail,
                        ExpWorkEndDate = e.ExpWorkEndDate,
                        ForecloseBy = e.ForecloseBy,
                        ForecloseDate = e.ForecloseDate,
                        ForecloseReasonCode = e.ForecloseReasonCode,
                        Id = e.Id,
                        IsActive = e.IsActive,
                        IsApproved = e.IsApproved,

                        IsClosed = e.IsClosed,
                        IsCompleted = e.IsCompleted,
                        IsConvertedToWorkOrder = e.IsConvertedToWorkOrder,
                        IsCreatedByCustomer = e.IsCreatedByCustomer,
                        IsForeClosed = e.IsForeClosed,
                        IsInScope = e.IsInScope,
                        IsLateResponse = e.IsLateResponse,
                        IsOpen = e.IsOpen,
                        IsRead = e.IsRead,
                        IsReconcile = e.IsReconcile,
                        IsSurvey = e.IsSurvey,
                        IsTransit = e.IsTransit,
                        IsVoid = e.IsVoid,
                        IsWorkInProgress = e.IsWorkInProgress,
                        JobMaintenanceType = e.JobMaintenanceType,
                        JOBookedBy = e.JOBookedBy,
                        JobType = e.JobType,
                        JODate = e.JODate,
                        JODeptCode = e.JODeptCode,
                        JODescription = e.JODescription,
                        JODocNum = e.JODocNum,
                        //JOImg1 = e.JOImg1,
                        //JOImg2 = e.JOImg2,
                        //JOImg3 = e.JOImg3,
                        GeoLongitude = e.GeoLongitude,
                        GeoLatitude = e.GeoLatitude,
                        OnlyTime = e.OnlyTime,
                        JOStatus = e.JOStatus,
                        JOSubject = e.JOSubject,
                        JOSupervisor = e.JOSupervisor,
                        ModifiedBy = e.ModifiedBy,
                        ModifiedOn = e.ModifiedOn,
                        CustomerNameEng = Customers.FirstOrDefault(c => c.CustCode == e.CustomerCode).CustName ?? "",
                        CustomerNameArb = Customers.FirstOrDefault(c => c.CustCode == e.CustomerCode).CustArbName ?? "",
                        // ProjectNameEng = Sites.FirstOrDefault(c => c.SiteCode == e.SiteCode).SiteName ?? "",
                        // ProjectNameArb = Sites.FirstOrDefault(c => c.SiteCode == e.SiteCode).SiteArbName ?? "",
                        DepNameEng = departments.FirstOrDefault(c => c.DeptCode == e.JODeptCode).NameEng ?? "-NA-",
                        DepNameArb = departments.FirstOrDefault(c => c.DeptCode == e.JODeptCode).NameArabic ?? "-NA-",
                        StatusStr = ((MetadataJoStatusEnum)e.JOStatus).ToString(),
                        // LogNotesCount = logNotes.Count(l => l.TicketNumber == e.TicketNumber && l.Type == "N")
                    });

                //if (request.Input.ProjectName.HasValue()) 
                //{
                //    list = list.Where(e => e.ProjectNameArb.Contains(search) || e.ProjectNameEng.Contains(search));
                //}
                //if (request.Input.StatusStr.HasValue())
                //{
                //    list = list.Where(e => e.StatusStr.Contains(search));
                //}
                //if (request.Input.ServiceType.HasValue())
                //{
                //    list = list.Where(e => e.StatusStr.Contains(search));
                //}

                //if (request.Input.JODeptCode.HasValue())
                //{
                //    list = list.Where(e => e.JODeptCode.Contains(search));
                //}

                if (request.Input.Status is not null)
                {
                    list = list.Where(e => e.JOStatus == request.Input.Status);
                }


                //if (request.Input.StatusStr.HasValue())
                //{
                //    if (request.Input.StatusStr == "incomplete")
                //    {
                //        list = list.Where(e => (e.JOStatus == (short)MetadataJoStatusEnum.Open) || (e.JOStatus == (short)MetadataJoStatusEnum.Read));
                //    }
                //    else if (request.Input.StatusStr == "outofscope")
                //    {
                //        list = list.Where(e => (e.JOStatus == (short)MetadataJoStatusEnum.Open || e.JOStatus == (short)MetadataJoStatusEnum.Read || e.JOStatus == (short)MetadataJoStatusEnum.LateResponse));  //arrived
                //        list = list.Where(e => e.IsApproved && !e.IsInScope && (!e.IsQuotationSubmitted || !e.IsPoRecieved));
                //    }
                //}

                if (request.Input.FromDate is not null && request.Input.ToDate is not null)
                {
                    list = list.Where(e => e.JODate < request.Input.ToDate && e.JODate >= request.Input.FromDate);
                }

                var nreports = await list.OrderBy(e => e.Id).PaginationListAsync(request.Input.Page, request.Input.PageCount, cancellationToken);

                return nreports;

            }
            catch (Exception ex)
            {
                return await Array.Empty<RecentB2CTicketsDto>().EmptyListAsync(cancellationToken);
            }
        }


    }


    #endregion



    #region GetFrontOfficeB2CTicketsListPaginationWithFilterQuery

    public class GetFrontOfficeB2CTicketsListPaginationWithFilterQuery : IRequest<PaginatedList<RecentB2CTicketsDto>>
    {
        public UserIdentityDto User { get; set; }
        public InputUserTicketsPaginationFilterDto Input { get; set; }
    }

    public class GetFrontOfficeB2CTicketsListPaginationWithFilterQueryHandler : IRequestHandler<GetFrontOfficeB2CTicketsListPaginationWithFilterQuery, PaginatedList<RecentB2CTicketsDto>>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetFrontOfficeB2CTicketsListPaginationWithFilterQueryHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedList<RecentB2CTicketsDto>> Handle(GetFrontOfficeB2CTicketsListPaginationWithFilterQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Input.ToDate is not null)
                {
                    request.Input.ToDate = request.Input.ToDate.Value.AddDays(1);
                }

                var contracts = _context.FomCustomerContracts.Where(e => e.ContProjSupervisor == request.User.UserName || e.ContProjManager == request.User.UserName).Select(s => new { s.CustCode, s.CustSiteCode }).AsNoTracking();
                var search = request.Input.Query;
                var Customers = _context.ErpFomDefCustomerMaster.AsNoTracking();
                var departments = _context.ErpFomDepartments.AsNoTracking();
                //var logNotes = request.User.LoginType == "client" ? _context.FomJobTicketLogNotes.Where(e => e.ShowToCustomer).AsNoTracking()
                //    : _context.FomJobTicketLogNotes.AsNoTracking();

                var list = _context.FomB2CJobTickets.AsNoTracking()
                    .Where(e => e.IsCreatedByCustomer == true && e.TicketType == "B2C" && e.TicketNumber != null && e.TicketNumber.Length > 0);

                if (request.Input.ServiceType.HasValue() && request.Input.ServiceType != "All")
                {
                    list = list.Where(e => e.SchType == request.Input.ServiceType);
                }
                if (request.Input.Status is not null && request.Input.Status > 0)
                {
                    list = list.Where(e => e.JOStatus == request.Input.Status);
                }

                if (request.Input.FromDate is not null)
                {
                    list = list.Where(e => e.JODate >= request.Input.FromDate);
                }
                var ticketList = list.Select(e => new RecentB2CTicketsDto
                {
                    TicketNumber = e.TicketNumber,
                    WorkStartDate = e.WorkStartDate,
                    WorkOrders = e.WorkOrders,
                    ApprovedBy = e.ApprovedBy,
                    ApprovedDate = e.ApprovedDate,

                    CreatedOn = e.CreatedOn,
                    CustomerCode = e.CustomerCode,
                    CustRegEmail = e.CustRegEmail,


                    Id = e.Id,
                    IsActive = e.IsActive,
                    IsApproved = e.IsApproved,

                    IsClosed = e.IsClosed,
                    IsCompleted = e.IsCompleted,
                    IsConvertedToWorkOrder = e.IsConvertedToWorkOrder,
                    IsCreatedByCustomer = e.IsCreatedByCustomer,
                    IsForeClosed = e.IsForeClosed,
                    IsInScope = e.IsInScope,
                    IsLateResponse = e.IsLateResponse,
                    IsOpen = e.IsOpen,
                    IsRead = e.IsRead,
                    IsReconcile = e.IsReconcile,
                    IsSurvey = e.IsSurvey,
                    IsTransit = e.IsTransit,
                    IsVoid = e.IsVoid,
                    IsWorkInProgress = e.IsWorkInProgress,
                    JobMaintenanceType = e.JobMaintenanceType,

                    JOBookedBy = e.JOBookedBy,
                    JobType = e.JobType,
                    JODate = e.JODate,
                    JODeptCode = e.JODeptCode,
                    JODescription = e.JODescription,
                    JODocNum = e.JODocNum,
                    //JOImg1 = e.JOImg1,
                    //JOImg2 = e.JOImg2,
                    //JOImg3 = e.JOImg3,
                    GeoLongitude = e.GeoLongitude,
                    GeoLatitude = e.GeoLatitude,
                    //OnlyTime = e.OnlyTime,
                    TimeValue = e.OnlyTime.ToString(@"hh\:mm"),
                    //TimeValue = $"{e.OnlyTime.Hours:D2}:{e.OnlyTime.Minutes:D2}",
                    JOStatus = e.JOStatus,
                    JOSubject = e.JOSubject,
                    JOSupervisor = e.JOSupervisor,

                    CustomerNameEng = Customers.FirstOrDefault(c => c.CustCode == e.CustomerCode).CustName ?? "",
                    CustomerNameArb = Customers.FirstOrDefault(c => c.CustCode == e.CustomerCode).CustArbName ?? "",
                    // ProjectNameEng = Sites.FirstOrDefault(c => c.SiteCode == e.SiteCode).SiteName ?? "",
                    // ProjectNameArb = Sites.FirstOrDefault(c => c.SiteCode == e.SiteCode).SiteArbName ?? "",
                    DepNameEng = departments.FirstOrDefault(c => c.DeptCode == e.JODeptCode).NameEng ?? "-NA-",
                    DepNameArb = departments.FirstOrDefault(c => c.DeptCode == e.JODeptCode).NameArabic ?? "-NA-",
                    StatusStr = ((MetadataJoStatusEnum)e.JOStatus).ToString(),
                });

                //var toDay = DateTime.Now.Date;
                //ticketList = ticketList.Where(e => (e.JODate.Date == toDay));

                //if (request.Input.FromDate is not null && request.Input.ToDate is not null)
                //{
                //    list = list.Where(e => e.JODate < request.Input.ToDate && e.JODate >= request.Input.FromDate);
                //}

                var nreports = await ticketList.OrderByDescending(e => e.JODate).PaginationListAsync(request.Input.Page, request.Input.PageCount, cancellationToken);

                return nreports;

            }
            catch (Exception ex)
            {
                return await Array.Empty<RecentB2CTicketsDto>().EmptyListAsync(cancellationToken);
            }
        }


    }


    #endregion


    #region GetB2CTicketCountListQuery

    public class GetB2CTicketCountListQuery : IRequest<List<StatusWiseAllTicketsCount>>
    {
        public UserIdentityDto User { get; set; }
    }

    public class GetB2CTicketCountListQueryHandler : IRequestHandler<GetB2CTicketCountListQuery, List<StatusWiseAllTicketsCount>>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetB2CTicketCountListQueryHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<StatusWiseAllTicketsCount>> Handle(GetB2CTicketCountListQuery request, CancellationToken cancellationToken)
        {
            var list = _context.FomB2CJobTickets.AsNoTracking()
                   .Where(e => e.IsCreatedByCustomer == true && e.TicketType == "B2C" && e.TicketNumber != null && e.TicketNumber.Length > 0);

            var ticketList = await list.Select(e => new StatusWiseAllTicketsCount
            {
                SatatusStr = ((MetadataJoStatusEnum)e.JOStatus).ToString(),
                Count = e.JOStatus,
            }).ToListAsync();

            return ticketList.GroupBy(e => e.SatatusStr).Select(e => new StatusWiseAllTicketsCount() { SatatusStr = e.Key, Count = e.Count() }).ToList();
        }


    }


    #endregion

    #region FrontOfficeB2CTicketAction

    public class FrontOfficeB2CTicketAction : IRequest<AppCtrollerDto>
    {
        public UserIdentityDto User { get; set; }
        public FrontOfficeB2CTicketDto Input { get; set; }
    }

    public class FrontOfficeB2CTicketActionHandler : IRequestHandler<FrontOfficeB2CTicketAction, AppCtrollerDto>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public FrontOfficeB2CTicketActionHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<AppCtrollerDto> Handle(FrontOfficeB2CTicketAction request, CancellationToken cancellationToken)
        {
            try
            {
                var JobTicketHead = await _context.FomB2CJobTickets.FirstOrDefaultAsync(e => e.TicketNumber == request.Input.TicketNumber);
                if (JobTicketHead is null)
                    return ApiMessageInfo.Status(ApiMessageInfo.NotFound, 0);

                string actionType = request.Input.ActionType;

                if (actionType == "approve")
                {
                    JobTicketHead.IsApproved = true;
                    JobTicketHead.ApprovedBy = request.User.Email;
                    JobTicketHead.ApprovedDate = DateTime.Now;
                    JobTicketHead.JOStatus = (short)MetadataJoStatusEnum.Approved;
                }
                else if (actionType == "cancel")
                {

                    JobTicketHead.IsVoid = true;
                    JobTicketHead.CancelBy = request.User.Email;
                    JobTicketHead.CancelDate = DateTime.Now;
                    JobTicketHead.CancelReasonCode = request.Input.ReasonCode;
                    JobTicketHead.JOStatus = (short)MetadataJoStatusEnum.Void;
                }
                else if (actionType == "foreclose")
                {
                    JobTicketHead.IsForeClosed = true;
                    JobTicketHead.ForecloseBy = request.User.Email;
                    JobTicketHead.ForecloseDate = DateTime.Now;
                    JobTicketHead.ForecloseReasonCode = request.Input.ReasonCode;
                    JobTicketHead.JOStatus = (short)MetadataJoStatusEnum.ForeClosed;
                }
                else if (actionType == "close")
                {
                    JobTicketHead.IsClosed = true;
                    JobTicketHead.ClosingRemarks = request.Input.ReasonCode;
                    JobTicketHead.ClosedBy = request.User.Email;
                    JobTicketHead.JOStatus = (short)MetadataJoStatusEnum.Closed;
                }
                else if (actionType == "complete")
                {
                    JobTicketHead.IsCompleted = true;
                    JobTicketHead.Feedback = request.Input.ReasonCode;
                    JobTicketHead.JOStatus = (short)MetadataJoStatusEnum.Completed;
                }
                else if (actionType == "open")
                {
                    JobTicketHead.IsOpen = true;
                    JobTicketHead.JOStatus = (short)MetadataJoStatusEnum.Open;
                }
                else if (actionType == "progress")
                {
                    JobTicketHead.IsWorkInProgress = true;
                    JobTicketHead.JOStatus = (short)MetadataJoStatusEnum.WorkInProgress;
                }
                else if (actionType == "note")
                {
                    TblFomJobTicketLogNote Note = new()
                    {
                        TicketNumber = request.Input.TicketNumber,
                        Type = "N",
                        ShowToCustomer = true,
                        Date = DateTime.UtcNow,
                        CreatedBy = request.User.Email,
                        Note = request.Input.ReasonCode,
                        IsB2c = true
                    };

                    await _context.FomJobTicketLogNotes.AddAsync(Note);
                    await _context.SaveChangesAsync();
                }

                await _context.SaveChangesAsync();

                return ApiMessageInfo.Status(1, JobTicketHead.Id);
            }
            catch (Exception ex)
            {
                Log.Error("Error in Create Upda te Profm JobTicketHead Method");
                Log.Error("Error occured time : " + DateTime.UtcNow);
                Log.Error("Error message : " + ex.Message);
                Log.Error("Error StackTrace : " + ex.StackTrace);
                return ApiMessageInfo.Status(0);
            }
        }


    }
    #endregion

}
