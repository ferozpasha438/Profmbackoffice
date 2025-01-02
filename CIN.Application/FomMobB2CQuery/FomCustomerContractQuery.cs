using AutoMapper;
using AutoMapper.QueryableExtensions;
using CIN.Application.Common;
using CIN.Application.FomMgtDtos;
using CIN.Application.FomMobB2CDtos;
//using CIN.Application.SalesSetupDtos;
using CIN.DB;
using CIN.Domain.FomMgt;
using CIN.Domain.FomMob;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;

namespace CIN.Application.FomMobB2CQuery
{

    //#region GetAll
    //public class GetFomCustomerContractList : IRequest<PaginatedList<TblErpFomCustomerContractDto>>
    //{
    //    public UserIdentityDto User { get; set; }
    //    public PaginationFilterDto Input { get; set; }

    //}

    //public class GetFomCustomerContractListHandler : IRequestHandler<GetFomCustomerContractList, PaginatedList<TblErpFomCustomerContractDto>>
    //{
    //    private readonly CINDBOneContext _context;
    //    // private readonly DBContext _context;
    //    private readonly IMapper _mapper;
    //    public GetFomCustomerContractListHandler(CINDBOneContext context, IMapper mapper)
    //    {
    //        _context = context;
    //        _mapper = mapper;
    //    }
    //    public async Task<PaginatedList<TblErpFomCustomerContractDto>> Handle(GetFomCustomerContractList request, CancellationToken cancellationToken)
    //    {


    //        var list = await _context.FomCustomerContracts.AsNoTracking().ProjectTo<TblErpFomCustomerContractDto>(_mapper.ConfigurationProvider)
    //                   .PaginationListAsync(request.Input.Page, request.Input.PageCount, cancellationToken);


    //        return list;
    //    }


    //}


    //#endregion

    //#region GetById

    //public class GetFomCustomerContractById : IRequest<TblErpFomCustomerContractDto>
    //{
    //    public UserIdentityDto User { get; set; }
    //    public int Id { get; set; }
    //}

    //public class GetFomCustomerContractByIdHandler : IRequestHandler<GetFomCustomerContractById, TblErpFomCustomerContractDto>
    //{
    //    private readonly CINDBOneContext _context;
    //    // private readonly DBContext _context;
    //    private readonly IMapper _mapper;
    //    public GetFomCustomerContractByIdHandler(CINDBOneContext context, IMapper mapper)
    //    {
    //        _context = context;
    //        _mapper = mapper;
    //    }

    //    public async Task<TblErpFomCustomerContractDto> Handle(GetFomCustomerContractById request, CancellationToken cancellationToken)
    //    {

    //        TblErpFomCustomerContract obj = new();
    //        var customerContract = await _context.FomCustomerContracts.AsNoTracking().ProjectTo<TblErpFomCustomerContractDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(e => e.Id == request.Id);
    //        return customerContract;
    //        // throw new NotImplementedException();
    //    }
    //}
    //#endregion


    #region Monthly and Yearly Scheduling


    #region Create_And_Update

    public class CreateUpdateFomCustomerContract : IRequest<AppCtrollerDto>
    {
        public UserIdentityDto User { get; set; }
        public ErpFomCustContractScheduleSummaryDto Input { get; set; }
    }

    public class CreateUpdateFomCustomerContractHandler : IRequestHandler<CreateUpdateFomCustomerContract, AppCtrollerDto>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;

        public CreateUpdateFomCustomerContractHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AppCtrollerDto> Handle(CreateUpdateFomCustomerContract request, CancellationToken cancellationToken)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    Log.Info("----Info Create Update  Fom Item Category method start----");

                    //Creating CustomerContract starts
                    var obj = request.Input.CustomerContractDto;
                    var scheduleTime = request.Input.ScheduleSummaryDto;

                    TblErpFomCustomerContract CustomerContract = new();
                    if (obj.Id > 0)
                        CustomerContract = await _context.FomCustomerContracts.AsNoTracking().FirstOrDefaultAsync(e => e.Id == obj.Id);

                    var SNG = await _context.FomMobSequenceNumberGenerator.AsNoTracking().FirstOrDefaultAsync();
                    if (SNG is not null)
                    {
                        SNG.CustomerContractSeq++;
                        CustomerContract.ContractCode = "CUST" + SNG.CustomerContractSeq.ToString().PadLeft(SNG.Length, '0');

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
                            CustomerContractSeq = 1,
                            SequenceNumber = 0
                        };
                        CustomerContract.ContractCode = "CUST0000001";
                        await _context.FomMobSequenceNumberGenerator.AddAsync(sng);
                        await _context.SaveChangesAsync();
                    }


                    CustomerContract.Id = obj.Id;
                    CustomerContract.CustCode = obj.CustCode;
                    //CustomerContract.CustSiteCode = obj.CustSiteCode;
                    CustomerContract.CustContNumber = obj.CustContNumber;
                    CustomerContract.ContStartDate = obj.ContStartDate;
                    CustomerContract.ContEndDate = obj.ContEndDate;
                    //CustomerContract.ContDeptCode = obj.ContDeptCode;
                    CustomerContract.ContDeptCode = scheduleTime.DeptCode;
                    CustomerContract.ContProjManager = "Admin";
                    CustomerContract.ContProjSupervisor = "Admin";
                    CustomerContract.ContApprAuthorities = "Admin";
                    CustomerContract.IsAppreoved = true;
                    CustomerContract.IsSheduleRequired = true;
                    CustomerContract.IsActive = true;
                    CustomerContract.Remarks = obj.Remarks;
                    CustomerContract.ApprovedBy = request.User.UserName;
                    CustomerContract.ApprovedDate = DateTime.Now;
                    CustomerContract.CreatedBy = request.User.UserName;
                    CustomerContract.CreatedDate = DateTime.Now;

                    if (obj.Id > 0)
                    {
                        _context.FomCustomerContracts.Update(CustomerContract);
                    }
                    else
                    {
                        await _context.FomCustomerContracts.AddAsync(CustomerContract);
                        CustomerContract.CreatedDate = obj.CreatedDate;

                    }
                    await _context.SaveChangesAsync();

                    //Creating CustomerContract ends


                    //Creating CreateUpdateSchedule starts


                    Log.Info("----Info CreateUpdateSchedule method start----");



                    //var contractDetails = await _context.FomCustomerContracts.FirstOrDefaultAsync(x => x.Id == CustomerContract.Id);

                    //if (obj.Id > 0)
                    //{
                    //    var summary = await _context.FomScheduleSummary.AsNoTracking().Where(e => e.ContractId == contractDetails.Id && e.DeptCode==obj.DeptCode).ToListAsync();
                    //    _context.FomScheduleSummary.RemoveRange(summary);
                    //    await _context.SaveChangesAsync();
                    //}

                    TblErpFomScheduleSummary scheduleSummarytHeader = new();
                    if (scheduleTime.Id > 0)
                        scheduleSummarytHeader = await _context.FomScheduleSummary.AsNoTracking().FirstOrDefaultAsync(e => e.Id == scheduleTime.Id);
                    scheduleSummarytHeader.Id = scheduleTime.Id;
                    scheduleSummarytHeader.ContractId = CustomerContract.Id;
                    scheduleSummarytHeader.DeptCode = scheduleTime.DeptCode;
                    scheduleSummarytHeader.IsSchGenerated = true;



                    if (scheduleTime.Id > 0)
                    {
                        scheduleSummarytHeader.ApproveDate = DateTime.Now;
                        scheduleSummarytHeader.ApprovedBy = request.User.UserId.ToString();
                        _context.FomScheduleSummary.Update(scheduleSummarytHeader);
                    }
                    else
                    {
                        scheduleSummarytHeader.ApproveDate = DateTime.Now;
                        scheduleSummarytHeader.ApprovedBy = request.User.UserId.ToString();
                        await _context.FomScheduleSummary.AddAsync(scheduleSummarytHeader);
                    }
                    await _context.SaveChangesAsync();

                    if (scheduleTime.Id > 0)
                    {
                        var details = await _context.FomScheduleWeekdays.AsNoTracking().Where(e => e.SchId == scheduleTime.Id).ToListAsync();
                        _context.FomScheduleWeekdays.RemoveRange(details);
                        await _context.SaveChangesAsync();
                    }
                    List<TblErpFomScheduleWeekdays> resData = new();
                    foreach (var item in scheduleTime.TableRows)
                    {
                        TblErpFomScheduleWeekdays scheduleWeekdaysDetails = new();
                        scheduleWeekdaysDetails.SchId = scheduleSummarytHeader.Id;
                        scheduleWeekdaysDetails.ContractId = scheduleSummarytHeader.ContractId;
                        scheduleWeekdaysDetails.WeekDay = item.WeekDay;
                        if (!string.IsNullOrEmpty(item.Time) && item.IsActive)
                            scheduleWeekdaysDetails.Time = TimeSpan.Parse("0:" + item.Time.Split(':')[0] + ":" + item.Time.Split(':')[1] + ":0.0000000");  // item.Time;
                        else
                            scheduleWeekdaysDetails.Time = TimeSpan.Zero;
                        scheduleWeekdaysDetails.AllDayLong = false;
                        scheduleWeekdaysDetails.IsActive = item.IsActive;
                        scheduleWeekdaysDetails.Remarks = item.Remarks;
                        resData.Add(scheduleWeekdaysDetails);

                    }
                    if (resData.Count > 0)
                    {
                        await _context.FomScheduleWeekdays.AddRangeAsync(resData);
                        await _context.SaveChangesAsync();
                    }

                    //Creating CreateUpdateSchedule ends



                    //Creating GenerateSchedule starts


                    var scheduleDetailItems = scheduleTime.TableRows.Where(x => x.IsActive == true).ToList();

                    var start = obj.ContStartDate;
                    var end = obj.ContEndDate;
                    var contractId = CustomerContract.Id;
                    var dept = scheduleTime.DeptCode;
                    var schId = scheduleSummarytHeader.Id;

                    if (scheduleDetailItems.Count > 0)
                    {

                        // Date start=now.date(), end=now.date();

                        List<TblErpFomScheduleDetails> scheduleDetailsItemList = new();
                        TblErpFomScheduleDetails TblErpFomScheduleDetailsObj = new();

                        List<DayOfWeek> DoW = new List<DayOfWeek>();
                        DayOfWeek Dow2;
                        int iDow = 0;
                        DoW.Add(DayOfWeek.Monday);
                        DoW.Add(DayOfWeek.Tuesday);
                        DoW.Add(DayOfWeek.Wednesday);
                        DoW.Add(DayOfWeek.Thursday);
                        DoW.Add(DayOfWeek.Friday);
                        DoW.Add(DayOfWeek.Saturday);
                        DoW.Add(DayOfWeek.Sunday);

                        var customer = await _context.ErpFomDefCustomerMaster.FirstOrDefaultAsync(e => e.CustCode == obj.CustCode);
                        List<TblFomB2CJobTicket> TblFomB2CJobTicketList = new();
                        // Dictionary<int,TblFomB2CJobTicket> TblFomB2CJobTicketList = new();
                        var ticketGen = await _context.FomMobSequenceNumberGenerator
                            //.AsNoTracking().
                            .FirstOrDefaultAsync(e => e.IsForJobTicket.Value == true);

                        foreach (var scheduleItem in scheduleDetailItems)
                        {
                            if (scheduleItem.WeekDay == "Monday")
                            {
                                iDow = 0;
                            }
                            else if (scheduleItem.WeekDay == "Tuesday")
                            {
                                iDow = 1;
                            }
                            else if (scheduleItem.WeekDay == "Wednesday")
                            {
                                iDow = 2;
                            }
                            else if (scheduleItem.WeekDay == "Thursday")
                            {
                                iDow = 3;
                            }
                            else if (scheduleItem.WeekDay == "Friday")
                            {
                                iDow = 4;
                            }
                            else if (scheduleItem.WeekDay == "Saturday")
                            {
                                iDow = 5;
                            }
                            else if (scheduleItem.WeekDay == "Sunday")
                            {
                                iDow = 6;
                            }

                            Dow2 = DoW[iDow];
                            var DateDOW = Enumerable.Range(start.DayOfYear, end.Subtract(start).Days + 1).Select(n => start.AddDays(n - start.DayOfYear)).Where(d => d.DayOfWeek == Dow2);

                            foreach (var Dt in DateDOW)
                            {

                                TblErpFomScheduleDetailsObj = new();
                                TblErpFomScheduleDetailsObj.ContractId = contractId;
                                TblErpFomScheduleDetailsObj.Department = dept;
                                TblErpFomScheduleDetailsObj.Id = 0;
                                TblErpFomScheduleDetailsObj.IsActive = scheduleItem.IsActive;
                                TblErpFomScheduleDetailsObj.IsReschedule = false;
                                TblErpFomScheduleDetailsObj.SchDate = Dt;
                                TblErpFomScheduleDetailsObj.SchId = schId;
                                TblErpFomScheduleDetailsObj.SerType = "";
                                TblErpFomScheduleDetailsObj.ServiceItem = "";
                                TblErpFomScheduleDetailsObj.Remarks = scheduleItem.Remarks;
                                TblErpFomScheduleDetailsObj.Time = TimeSpan.Parse("0:" + scheduleItem.Time.Split(':')[0] + ":" + scheduleItem.Time.Split(':')[1] + ":0.0000000");
                                TblErpFomScheduleDetailsObj.TranNumber = contractId.ToString(); //Contract Id;
                                TblErpFomScheduleDetailsObj.IsB2c = true;
                                scheduleDetailsItemList.Add(TblErpFomScheduleDetailsObj);

                            }
                        }

                        if (scheduleDetailsItemList.Count > 0)
                        {
                            {
                                await _context.FomScheduleDetails.AddRangeAsync(scheduleDetailsItemList);
                                await _context.SaveChangesAsync();

                                decimal geoLatitude = 0, geoLongitude = 0;

                                var custSite = await _context.ProfmDefSiteMaster.Select(e => new { e.CustomerCode, e.SiteGeoLatitude, e.SiteGeoLongitude })
                                                             .FirstOrDefaultAsync(e => e.CustomerCode == obj.CustCode);
                                if (request.Input.GeoLatitude > 0 && request.Input.GeoLongitude > 0)
                                {
                                    geoLatitude = request.Input.GeoLatitude;
                                    geoLongitude = request.Input.GeoLongitude;
                                }
                                else if (custSite is not null)
                                {
                                    geoLatitude = custSite.SiteGeoLatitude;
                                    geoLongitude = custSite.SiteGeoLongitude;
                                }


                                foreach (var item in scheduleDetailsItemList)
                                {
                                    //Adding TblFomB2CJobTicket and with Time Starts

                                    TblFomB2CJobTicket JobTicketHead = new() { Id = 0, TicketType = "B2C", SchType = request.Input.SchType };
                                    JobTicketHead.CustomerCode = obj.CustCode;
                                    JobTicketHead.JODeptCode = scheduleTime.DeptCode;
                                    JobTicketHead.JOSubject = "-";
                                    JobTicketHead.JODescription = obj.Remarks;
                                    JobTicketHead.JOBookedBy = request.User.Email;
                                    JobTicketHead.JobMaintenanceType = "Corrective";
                                    JobTicketHead.IsActive = true;
                                    JobTicketHead.JOStatus = (int)MetadataJoStatusEnum.Open;
                                    JobTicketHead.JODate = item.SchDate;// DateTime.Now;
                                    JobTicketHead.CustRegEmail = customer.CustEmail1;
                                    JobTicketHead.IsCreatedByCustomer = true;
                                    JobTicketHead.IsApproved = false;
                                    JobTicketHead.IsOpen = true;
                                    JobTicketHead.IsInScope = false;
                                    JobTicketHead.JOSupervisor = "Admin";

                                    JobTicketHead.GeoLatitude = geoLatitude;
                                    JobTicketHead.GeoLongitude = geoLongitude;

                                    JobTicketHead.OnlyTime = item.Time; // TimeSpan.Parse(obj.TimeValue ?? "00:00:00");                                                                                              
                                    JobTicketHead.SchDetailId = item.Id;
                                    JobTicketHead.TicketNumber = string.Empty;

                                    if (ticketGen is not null)
                                    {
                                        ticketGen.SequenceNumber++;
                                        JobTicketHead.SpTicketNumber = "SCKT" + ticketGen.SequenceNumber.ToString().PadLeft(ticketGen.Length, '0');

                                        _context.FomMobSequenceNumberGenerator.Update(ticketGen);
                                        await _context.SaveChangesAsync();
                                    }

                                    //else
                                    //{
                                    //    TblFomMobSequenceNumberGenerator sng = new()
                                    //    {
                                    //        IsForJobTicket = true,
                                    //        Length = 6,
                                    //        Prefix = "TCKT",
                                    //        SequenceNumber = 1,
                                    //        CustomerContractSeq = 0,
                                    //    };
                                    //    JobTicketHead.SpTicketNumber = "SCKT0000001";
                                    //    await _context.FomMobSequenceNumberGenerator.AddAsync(sng);
                                    //    await _context.SaveChangesAsync();
                                    //}

                                    JobTicketHead.CreatedBy = request.User.Email;
                                    JobTicketHead.CreatedOn = DateTime.UtcNow;
                                    TblFomB2CJobTicketList.Add(JobTicketHead);
                                }

                                //Adding TblFomB2CJobTicket and with Time Ends                               
                            }

                        }

                        //Creating JobTicket starts

                        await _context.FomB2CJobTickets.AddRangeAsync(TblFomB2CJobTicketList);
                        await _context.SaveChangesAsync();

                        //Creating JobTicket ends

                    }


                    //Creating GenerateSchedule ends


                    #region JobTicket Old Commented Code  



                    //JobTicketHead.CustomerCode = obj.CustCode;
                    //JobTicketHead.JODeptCode = scheduleTime.DeptCode;
                    //JobTicketHead.JOSubject = "-";
                    //JobTicketHead.JODescription = obj.Remarks;
                    //JobTicketHead.JOBookedBy = request.User.Email;
                    //JobTicketHead.JobMaintenanceType = "Corrective";
                    //JobTicketHead.IsActive = true;
                    //JobTicketHead.JOStatus = (int)MetadataJoStatusEnum.Open;
                    //JobTicketHead.JODate = DateTime.Now;
                    //JobTicketHead.CustRegEmail = customer.CustEmail1;
                    //JobTicketHead.IsCreatedByCustomer = true;
                    //JobTicketHead.IsApproved = false;
                    //JobTicketHead.IsOpen = true;
                    //JobTicketHead.IsInScope = false;
                    //JobTicketHead.JOSupervisor = "Admin";

                    //JobTicketHead.GeoLatitude = obj.GeoLatitude;
                    //JobTicketHead.GeoLongitude = obj.GeoLongitude;
                    //JobTicketHead.OnlyTime = TimeSpan.Parse(obj.TimeValue ?? "00:00:00");

                    //JobTicketHead.TicketNumber = string.Empty;

                    //if (obj.Id > 0)
                    //{
                    //    JobTicketHead.ModifiedBy = request.User.Email;
                    //    JobTicketHead.ModifiedOn = DateTime.UtcNow;
                    //    _context.FomB2CJobTickets.Update(JobTicketHead);

                    //}
                    //else
                    //{

                    //    var ticketGen = await _context.FomMobSequenceNumberGenerator.AsNoTracking().FirstOrDefaultAsync(e => e.IsForJobTicket.Value == true);
                    //    if (ticketGen is not null)
                    //    {
                    //        ticketGen.SequenceNumber++;
                    //        JobTicketHead.SpTicketNumber = "SCKT" + ticketGen.SequenceNumber.ToString().PadLeft(ticketGen.Length, '0');

                    //        _context.FomMobSequenceNumberGenerator.Update(ticketGen);
                    //        await _context.SaveChangesAsync();
                    //    }
                    //    else
                    //    {
                    //        TblFomMobSequenceNumberGenerator sng = new()
                    //        {
                    //            IsForJobTicket = true,
                    //            Length = 6,
                    //            Prefix = "TCKT",
                    //            SequenceNumber = 1,
                    //            CustomerContractSeq = 0,
                    //        };
                    //        JobTicketHead.SpTicketNumber = "SCKT0000001";
                    //        await _context.FomMobSequenceNumberGenerator.AddAsync(sng);
                    //        await _context.SaveChangesAsync();
                    //    }

                    //    JobTicketHead.CreatedBy = request.User.Email;
                    //    JobTicketHead.CreatedOn = DateTime.UtcNow;
                    //    await _context.FomB2CJobTickets.AddAsync(JobTicketHead);
                    //    await _context.SaveChangesAsync();
                    //}

                    #endregion

                    await transaction.CommitAsync();
                    Log.Info("----Info Create Update Fom Customer Contract method Exit----");
                    return ApiMessageInfo.Status(1, CustomerContract.Id);

                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Log.Error("Error in Create Update Fom Customer Contract Method");
                    Log.Error("Error occured time : " + DateTime.UtcNow);
                    Log.Error("Error message : " + ex.Message);
                    Log.Error("Error StackTrace : " + ex.StackTrace);
                    return ApiMessageInfo.Status(0);
                }
            }
        }

    }



    #endregion



    #region GetB2CFrontOfficeSchedulingList
    public class GetB2CFrontOfficeSchedulingList : IRequest<PaginatedList<CIN.Application.FomMobB2CDtos.RsErpFomScheduleDetailsDto>>
    {
        public UserIdentityDto User { get; set; }
        public PaginationFilterDto Input { get; set; }

    }

    public class GetB2CFrontOfficeSchedulingListHandler : IRequestHandler<GetB2CFrontOfficeSchedulingList, PaginatedList<CIN.Application.FomMobB2CDtos.RsErpFomScheduleDetailsDto>>
    {
        private readonly CINDBOneContext _context;
        // private readonly DBContext _context;
        private readonly IMapper _mapper;
        public GetB2CFrontOfficeSchedulingListHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedList<CIN.Application.FomMobB2CDtos.RsErpFomScheduleDetailsDto>> Handle(GetB2CFrontOfficeSchedulingList request, CancellationToken cancellationToken)
        {
            PaginatedList<CIN.Application.FomMobB2CDtos.RsErpFomScheduleDetailsDto> list;
            IQueryable<TblErpFomScheduleDetails> scheduleDetails = _context.FomScheduleDetails.AsNoTracking().Where(e => e.IsB2c == true);


            if (request.Input.ContractId > 0 && !string.IsNullOrEmpty(request.Input.StartDate) && !string.IsNullOrEmpty(request.Input.EndDate))
            {
                var startDate = new DateTime(Convert.ToInt32(request.Input.StartDate.Split('-')[0]), Convert.ToInt32(request.Input.StartDate.Split('-')[1]), Convert.ToInt32(request.Input.StartDate.Split('-')[2]));
                var endDate = new DateTime(Convert.ToInt32(request.Input.EndDate.Split('-')[0]), Convert.ToInt32(request.Input.EndDate.Split('-')[1]), Convert.ToInt32(request.Input.EndDate.Split('-')[2]));
                scheduleDetails = scheduleDetails
                                                   .Where(e => (e.SchDate >= startDate && e.SchDate <= endDate) && e.ContractId == request.Input.ContractId);
            }
            else if (request.Input.ContractId == 0 && !string.IsNullOrEmpty(request.Input.StartDate) && !string.IsNullOrEmpty(request.Input.EndDate))
            {
                var startDate = new DateTime(Convert.ToInt32(request.Input.StartDate.Split('-')[0]), Convert.ToInt32(request.Input.StartDate.Split('-')[1]), Convert.ToInt32(request.Input.StartDate.Split('-')[2]));
                var endDate = new DateTime(Convert.ToInt32(request.Input.EndDate.Split('-')[0]), Convert.ToInt32(request.Input.EndDate.Split('-')[1]), Convert.ToInt32(request.Input.EndDate.Split('-')[2]));
                scheduleDetails = scheduleDetails
                                                   .Where(e => (e.SchDate >= startDate && e.SchDate <= endDate));

            }
            else if (request.Input.ContractId > 0 && !string.IsNullOrEmpty(request.Input.StartDate) && string.IsNullOrEmpty(request.Input.EndDate))
            {
                var startDate = new DateTime(Convert.ToInt32(request.Input.StartDate.Split('-')[0]), Convert.ToInt32(request.Input.StartDate.Split('-')[1]), Convert.ToInt32(request.Input.StartDate.Split('-')[2]));
                scheduleDetails = scheduleDetails
                                                   .Where(e => (e.SchDate >= startDate) && e.ContractId == request.Input.ContractId);

            }
            else if (request.Input.ContractId > 0 && string.IsNullOrEmpty(request.Input.StartDate) && !string.IsNullOrEmpty(request.Input.EndDate))
            {
                var endDate = new DateTime(Convert.ToInt32(request.Input.EndDate.Split('-')[0]), Convert.ToInt32(request.Input.EndDate.Split('-')[1]), Convert.ToInt32(request.Input.EndDate.Split('-')[2]));
                scheduleDetails = scheduleDetails
                                                   .Where(e => (e.SchDate <= endDate) && e.ContractId == request.Input.ContractId);

            }
            else if (request.Input.ContractId > 0 && string.IsNullOrEmpty(request.Input.StartDate) && string.IsNullOrEmpty(request.Input.EndDate))
            {
                scheduleDetails = scheduleDetails
                                                    .Where(e => e.ContractId == request.Input.ContractId);
            }
            else if (request.Input.ContractId == 0 && !string.IsNullOrEmpty(request.Input.StartDate) && string.IsNullOrEmpty(request.Input.EndDate))
            {
                var startDate = new DateTime(Convert.ToInt32(request.Input.StartDate.Split('-')[0]), Convert.ToInt32(request.Input.StartDate.Split('-')[1]), Convert.ToInt32(request.Input.StartDate.Split('-')[2]));
                scheduleDetails = scheduleDetails
                                                    .Where(e => (e.SchDate >= startDate));

            }
            else if (request.Input.ContractId == 0 && string.IsNullOrEmpty(request.Input.StartDate) && !string.IsNullOrEmpty(request.Input.EndDate))
            {
                var endDate = new DateTime(Convert.ToInt32(request.Input.EndDate.Split('-')[0]), Convert.ToInt32(request.Input.EndDate.Split('-')[1]), Convert.ToInt32(request.Input.EndDate.Split('-')[2]));
                scheduleDetails = scheduleDetails
                                                     .Where(e => (e.SchDate <= endDate));
            }
            else
            {

                //var toDay = DateTime.Now.Date;
                //scheduleDetails = _context.FomScheduleDetails.AsNoTracking().Where(e => (e.SchDate.Date == toDay));
                //scheduleDetails = _context.FomScheduleDetails.AsNoTracking();

            }

            list = await scheduleDetails.Select(x => new CIN.Application.FomMobB2CDtos.RsErpFomScheduleDetailsDto
            {
                ContractId = x.ContractId,
                Department = x.Department,
                Frequency = x.Frequency,
                Id = x.Id,
                IsActive = x.IsActive,
                IsReschedule = x.IsReschedule,
                Remarks = x.Remarks,
                SchDate = x.SchDate,
                SchId = x.SchId,
                SerType = x.SerType,
                ServiceItem = x.ServiceItem,
                Time = x.Time,
                TranNumber = x.TranNumber,
            })
                .OrderByDescending(x => x.SchDate)
                .PaginationListAsync(request.Input.Page, request.Input.PageCount, cancellationToken);


            var grpSchedules = from sch in list.Items
                               group sch by sch.ContractId into g
                               select g.Key;

            foreach (var schContractId in grpSchedules)
            {
                var contractDetails = await _context.FomCustomerContracts.AsNoTracking()
                                              .Select(e => new { e.Id, e.CustCode, e.ContractCode })
                                                .FirstOrDefaultAsync(e => e.Id == schContractId);

                var custDetails = await _context.OprCustomers.AsNoTracking()
                                                .Select(e => new { e.CustCode, e.CustName, e.CustArbName })
                                                .FirstOrDefaultAsync(e => e.CustCode == contractDetails.CustCode);

                list.Items.Where(e => e.ContractId == schContractId).ToList().ForEach(e =>
                {
                    e.ContractCode = contractDetails.ContractCode;
                    e.CustomerName = custDetails.CustName;
                    e.CustomerNameAr = custDetails.CustArbName;
                });
            }


            //if (list.TotalCount > 0)
            //{
            //    foreach (var item in list.Items)
            //    {
            //        var contractDetails = await _context.FomCustomerContracts.AsNoTracking()
            //                                    .ProjectTo<TblErpFomCustomerContractDto>(_mapper.ConfigurationProvider)
            //                                    .SingleOrDefaultAsync(e => e.Id == item.ContractId);
            //        if (contractDetails != null)
            //        {
            //            item.ContractCode = contractDetails.ContractCode;
            //            var custDetails = await _context.OprCustomers.AsNoTracking()
            //                                    .ProjectTo<TblSndDefCustomerMasterDto>(_mapper.ConfigurationProvider)
            //                                    .SingleOrDefaultAsync(e => e.CustCode == contractDetails.CustCode);
            //            if (custDetails != null)
            //            {
            //                item.CustomerName = custDetails.CustName;
            //                item.CustomerNameAr = custDetails.CustArbName;
            //            }
            //            var siteDetails = await _context.OprSites.AsNoTracking()
            //                                    .ProjectTo<TblSndDefSiteMasterDto>(_mapper.ConfigurationProvider)
            //                                    .SingleOrDefaultAsync(e => e.SiteCode == contractDetails.CustSiteCode);
            //            if (siteDetails != null)
            //            {
            //                item.SiteName = siteDetails.SiteName;
            //                item.SiteNameAr = siteDetails.SiteArbName;
            //            }
            //        }

            //    }

            //}

            return list;
        }
    }



    #endregion



    #region AllB2CFomCalenderScheduleList
    public class AllB2CFomCalenderScheduleList : IRequest<List<CIN.Application.FomMobB2CDtos.FomScheduleDetailsDto>>
    {
        public UserIdentityDto User { get; set; }
    }

    public class AllB2CFomCalenderScheduleListHandler : IRequestHandler<AllB2CFomCalenderScheduleList, List<CIN.Application.FomMobB2CDtos.FomScheduleDetailsDto>>
    {
        private readonly CINDBOneContext _context;
        // private readonly DBContext _context;
        private readonly IMapper _mapper;
        public AllB2CFomCalenderScheduleListHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<CIN.Application.FomMobB2CDtos.FomScheduleDetailsDto>> Handle(AllB2CFomCalenderScheduleList request, CancellationToken cancellationToken)
        {
            try
            {


                var startDate = DateTime.Now.AddMonths(-2);
                var endDate = DateTime.Now.AddDays(365);
                var list = await _context.FomScheduleDetails
                                                      .AsNoTracking()
                                                      .Where(e => e.IsB2c == true && e.SchDate >= startDate && e.SchDate <= endDate)
                                                      .Select(e => new CIN.Application.FomMobB2CDtos.FomScheduleDetailsDto
                                                      {
                                                          ContractId = e.ContractId,
                                                          SchDate = e.SchDate,
                                                          Department = e.Department,
                                                          SerType = e.SerType,
                                                          Frequency = e.Frequency,
                                                          TranNumber = e.TranNumber,
                                                          ServiceItem = e.ServiceItem,
                                                          Remarks = e.Remarks,
                                                          Time = e.Time.ToString(@"hh\:mm"),
                                                          IsReschedule = e.IsReschedule,
                                                          IsActive = e.IsActive

                                                      }).OrderBy(e => e.SchDate).ToListAsync();

                var grpSchedules = from sch in list
                                   group sch by sch.ContractId into g
                                   select g.Key;

                foreach (var schContractId in grpSchedules)
                {
                    var contractDetails = await _context.FomCustomerContracts.AsNoTracking()
                                                  .Select(e => new { e.Id, e.CustCode, e.ContractCode })
                                                    .FirstOrDefaultAsync(e => e.Id == schContractId);

                    var custDetails = await _context.OprCustomers.AsNoTracking()
                                                    .Select(e => new { e.CustCode, e.CustName, e.CustArbName })
                                                    .FirstOrDefaultAsync(e => e.CustCode == contractDetails.CustCode);

                    list.Where(e => e.ContractId == schContractId).ToList().ForEach(e =>
                    {
                        e.ContractCode = contractDetails.ContractCode;
                        e.CustomerName = custDetails.CustName;
                        e.CustomerNameAr = custDetails.CustArbName;
                    });
                }


                //if (list.Count > 0)
                //{
                //    foreach (var item in list)
                //    {
                //        var contractDetails = await _context.FomCustomerContracts.AsNoTracking()
                //                                    .ProjectTo<TblErpFomCustomerContractDto>(_mapper.ConfigurationProvider)
                //                                    .SingleOrDefaultAsync(e => e.Id == item.ContractId);
                //        if (contractDetails != null)
                //        {
                //            item.ContractCode = contractDetails.ContractCode;
                //            var custDetails = await _context.OprCustomers.AsNoTracking()
                //                                    .ProjectTo<TblSndDefCustomerMasterDto>(_mapper.ConfigurationProvider)
                //                                    .SingleOrDefaultAsync(e => e.CustCode == contractDetails.CustCode);
                //            if (custDetails != null)
                //            {
                //                item.CustomerName = custDetails.CustName;
                //                item.CustomerNameAr = custDetails.CustArbName;
                //            }
                //            var siteDetails = await _context.OprSites.AsNoTracking()
                //                                    .ProjectTo<TblSndDefSiteMasterDto>(_mapper.ConfigurationProvider)
                //                                    .SingleOrDefaultAsync(e => e.SiteCode == contractDetails.CustSiteCode);
                //            if (siteDetails != null)
                //            {
                //                item.SiteName = siteDetails.SiteName;
                //                item.SiteNameAr = siteDetails.SiteArbName;
                //            }
                //        }

                //    }
                //}

                return list;
            }
            catch (Exception ex)
            {

                throw;
            }
        }


    }


    #endregion


    #region GetCustomerContractSelectList

    public class GetCustomerContractSelectList : IRequest<List<CustomSelectListItem>>
    {
        public UserIdentityDto User { get; set; }
        //   public bool? IsPayment { get; set; }
    }

    public class GetCustomerContractSelectListHandler : IRequestHandler<GetCustomerContractSelectList, List<CustomSelectListItem>>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetCustomerContractSelectListHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<CustomSelectListItem>> Handle(GetCustomerContractSelectList request, CancellationToken cancellationToken)
        {

            Log.Info("----Info GetCustomerContractSelectList method start----");
            var list = _context.FomCustomerContracts.Where(e => e.IsActive);

            var newList = await list.AsNoTracking()
            .OrderBy(e => e.Id)
                 .Select(e => new CustomSelectListItem { Text = e.ContractCode + " - " + e.CustCode, TextTwo = e.CustCode, Value = e.Id.ToString() })
                  .ToListAsync(cancellationToken);
            Log.Info("----Info GetCustomerContractSelectList method Ends----");
            return newList;
        }
    }


    #endregion

    #region GetLanCustomerContractSelectList
    public class GetLanCustomerContractSelectList : IRequest<List<LanCustomSelectListItem>>
    {
        public UserIdentityDto User { get; set; }
    }

    public class GetLanCustomerContractSelectListHandler : IRequestHandler<GetLanCustomerContractSelectList, List<LanCustomSelectListItem>>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetLanCustomerContractSelectListHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<LanCustomSelectListItem>> Handle(GetLanCustomerContractSelectList request, CancellationToken cancellationToken)
        {

            Log.Info("----Info GetLanCustomerContractSelectList method start----");
            var list = _context.FomCustomerContracts.Where(e => e.IsActive);

            var newList = await list.AsNoTracking()
            .OrderBy(e => e.Id)
                 .Select(e => new LanCustomSelectListItem { Text = e.ContractCode + " - " + e.CustCode, TextTwo = e.CustCode, Value = e.ContractCode })
                  .ToListAsync(cancellationToken);
            Log.Info("----Info GetLanCustomerContractSelectList method Ends----");
            return newList;
        }
    }


    #endregion

    #endregion


    //#region GetSelectforAuthList

    //public class GetSelectResourcesQuery : IRequest<List<LanCustomSelectListItem>>
    //{
    //    public UserIdentityDto User { get; set; }

    //    public string Input { get; set; }
    //}

    //public class GetSelectResourcesQueryHandler : IRequestHandler<GetSelectResourcesQuery, List<LanCustomSelectListItem>>
    //{
    //    private readonly CINDBOneContext _context;
    //    private readonly IMapper _mapper;
    //    public GetSelectResourcesQueryHandler(CINDBOneContext context, IMapper mapper)
    //    {
    //        _context = context;
    //        _mapper = mapper;
    //    }

    //    public async Task<List<LanCustomSelectListItem>> Handle(GetSelectResourcesQuery request, CancellationToken cancellationToken)
    //    {

    //        var search = request.Input;

    //        var list = await _context.FomResources
    //              .Where(e => e.ApprovalAuth == true || e.ResTypeCode.Contains(search) || search == null)
    //              .Join(
    //                _context.FomResourceType,  // Joining with ResourceType table
    //                resource => resource.ResTypeCode,  // Joining on FomResource.ResourceTypeId
    //                resourceType => resourceType.ResTypeCode,      // Joining on ResourceType.Id
    //                (resource, resourceType) => new LanCustomSelectListItem
    //                {
    //                    Text = resource.NameEng,
    //                    Value = resource.ResTypeCode,
    //                    TextTwo = resource.ResCode,
    //                    TextAr = resource.NameAr,
    //                    //  ResourceTypeText = resourceType.Name // Assuming ResourceType has a Name property
    //                }
    //        )
    //        .AsNoTracking()
    //        .ToListAsync(cancellationToken);

    //        return list;
    //        //var list = await _context.FomResources
    //        //    .Where(e => e.ApprovalAuth == true)
    //        //  .AsNoTracking()
    //        // .Select(e => new LanCustomSelectListItem { Text = e.ResCode, Value = e.Id.ToString(), TextTwo = e.NameEng, TextAr = e.NameAr })
    //        //    .ToListAsync(cancellationToken);
    //        //return list;
    //    }
    //}
    //#endregion



    #region CreateUpdateSchedule

    public class CreateUpdateSchedule : IRequest<int>
    {
        public UserIdentityDto User { get; set; }
        public CIN.Application.FomMobB2CDtos.ErpFomScheduleSummaryDto ScheduleSummaryDto { get; set; }
    }

    public class CreateUpdateScheduleHandler : IRequestHandler<CreateUpdateSchedule, int>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;

        public CreateUpdateScheduleHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateUpdateSchedule request, CancellationToken cancellationToken)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    Log.Info("----Info CreateUpdateSchedule method start----");
                    var obj = request.ScheduleSummaryDto;
                    var contractDetails = await _context.FomCustomerContracts.FirstOrDefaultAsync(x => x.ContractCode == obj.ContractCode);

                    //if (obj.Id > 0)
                    //{
                    //    var summary = await _context.FomScheduleSummary.AsNoTracking().Where(e => e.ContractId == contractDetails.Id && e.DeptCode==obj.DeptCode).ToListAsync();
                    //    _context.FomScheduleSummary.RemoveRange(summary);
                    //    await _context.SaveChangesAsync();
                    //}

                    TblErpFomScheduleSummary scheduleSummarytHeader = new();
                    if (obj.Id > 0)
                        scheduleSummarytHeader = await _context.FomScheduleSummary.AsNoTracking().FirstOrDefaultAsync(e => e.Id == obj.Id);
                    scheduleSummarytHeader.Id = obj.Id;
                    scheduleSummarytHeader.ContractId = contractDetails.Id;
                    scheduleSummarytHeader.DeptCode = obj.DeptCode;
                    scheduleSummarytHeader.IsSchGenerated = false;



                    if (obj.Id > 0)
                    {
                        scheduleSummarytHeader.ApproveDate = DateTime.Now;
                        scheduleSummarytHeader.ApprovedBy = request.User.UserId.ToString();
                        _context.FomScheduleSummary.Update(scheduleSummarytHeader);
                    }
                    else
                    {
                        scheduleSummarytHeader.ApproveDate = DateTime.Now;
                        scheduleSummarytHeader.ApprovedBy = request.User.UserId.ToString();
                        await _context.FomScheduleSummary.AddAsync(scheduleSummarytHeader);
                    }
                    await _context.SaveChangesAsync();

                    if (obj.Id > 0)
                    {
                        var details = await _context.FomScheduleWeekdays.AsNoTracking().Where(e => e.SchId == obj.Id).ToListAsync();
                        _context.FomScheduleWeekdays.RemoveRange(details);
                        await _context.SaveChangesAsync();
                    }
                    List<TblErpFomScheduleWeekdays> resData = new();
                    foreach (var item in obj.TableRows)
                    {
                        TblErpFomScheduleWeekdays scheduleWeekdaysDetails = new();
                        scheduleWeekdaysDetails.SchId = scheduleSummarytHeader.Id;
                        scheduleWeekdaysDetails.ContractId = scheduleSummarytHeader.ContractId;
                        scheduleWeekdaysDetails.WeekDay = item.WeekDay;
                        if (!string.IsNullOrEmpty(item.Time) && item.IsActive)
                            scheduleWeekdaysDetails.Time = TimeSpan.Parse("0:" + item.Time.Split(':')[0] + ":" + item.Time.Split(':')[1] + ":0.0000000");  // item.Time;
                        else
                            scheduleWeekdaysDetails.Time = TimeSpan.Zero;
                        scheduleWeekdaysDetails.AllDayLong = false;
                        scheduleWeekdaysDetails.IsActive = item.IsActive;
                        scheduleWeekdaysDetails.Remarks = item.Remarks;
                        resData.Add(scheduleWeekdaysDetails);

                    }
                    if (resData.Count > 0)
                    {
                        await _context.FomScheduleWeekdays.AddRangeAsync(resData);
                        await _context.SaveChangesAsync();
                    }
                    await transaction.CommitAsync();
                    Log.Info("----Info CreateUpdateSchedule method Exit----");
                    return scheduleSummarytHeader.Id;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Log.Error("Error in CreateUpdateSchedule Method");
                    Log.Error("Error occured time : " + DateTime.UtcNow);
                    Log.Error("Error message : " + ex.Message);
                    Log.Error("Error StackTrace : " + ex.StackTrace);
                    return 0;
                }
            }

        }


    }



    #endregion




    #region

    //#region List by 
    //public class GetScheduleSummaryList : IRequest<List<CIN.Application.FomMobB2CDtos.ErpFomScheduleSummaryDto>>
    //{
    //    public UserIdentityDto User { get; set; }
    //    public int Id { get; set; }
    //    public string ContractCode { get; set; }

    //}
    //public class GetScheduleSummaryListHandler : IRequestHandler<GetScheduleSummaryList, List<CIN.Application.FomMobB2CDtos.ErpFomScheduleSummaryDto>>
    //{
    //    private readonly CINDBOneContext _context;
    //    private readonly IMapper _mapper;
    //    public GetScheduleSummaryListHandler(CINDBOneContext context, IMapper mapper)
    //    {
    //        _context = context;
    //        _mapper = mapper;
    //    }

    //    public async Task<List<CIN.Application.FomMobB2CDtos.ErpFomScheduleSummaryDto>> Handle(GetScheduleSummaryList request, CancellationToken cancellationToken)
    //    {
    //        List<CIN.Application.FomMobB2CDtos.ErpFomScheduleSummaryDto> sList = new();

    //         var sheduleSummary = await _context.FomScheduleSummary.FirstOrDefaultAsync(x => x.Id == request.Id);

    //        //var sDetails = await _context.FomScheduleWeekdays.AsNoTracking().
    //        //                                        ProjectTo<TblCIN.Application.FomMobB2CDtos.ErpFomScheduleWeekdaysDto>(_mapper.ConfigurationProvider).
    //        //                                        OrderByDescending(x => x.Id).
    //        //                                        FirstOrDefaultAsync();
    //        if (sheduleSummary != null)
    //        {
    //            sList = await _context.FomScheduleSummary.AsNoTracking()
    //                                    .Where(e => e.Id == request.Id )
    //                                    .Select(x => new CIN.Application.FomMobB2CDtos.ErpFomScheduleSummaryDto()
    //                                    {
    //                                       Id=x.Id,
    //                                 //    ContractCode= sheduleSummary.Id,
    //                                     DeptCode=x.DeptCode,
    //                                     ApproveDate=x.ApproveDate



    //                                    })
    //                                    .ToListAsync();
    //        }

    //        if (sList != null)
    //        {
    //            sList.tabl = await _context.SchoolExaminationManagementDetails.AsNoTracking()
    //                                   .Where(e => e.ExamHeaderId == result.Id)
    //                                   .Select(e => new SchoolExamManagementDetailsDto
    //                                   {
    //                                       EndingDateTime = e.EndingDateTime,
    //                                       StartingDateTime = e.StartingDateTime,
    //                                       SubjectCode = e.SubjectCode
    //                                   })
    //                                   .ToListAsync();
    //        }
    //        return result;


    //        //foreach (var item in sList)
    //        //{
    //        //    var scheduleWeekDetails = await _context.FomScheduleWeekdays.AsNoTracking().Where(e => e.SchId == item.Id).ToListAsync();
    //        //    item.TableRows = scheduleWeekDetails;
    //        //}
    //        //return sList;
    //    }
    //}
    //#endregion



    #region GetById
    public class GetScheduleSummaryById : IRequest<CIN.Application.FomMobB2CDtos.ErpFomScheduleSummaryDto>
    {
        public UserIdentityDto User { get; set; }
        //public int Id { get; set; }
        public string ContractCode { get; set; }

        public string DeptCode { get; set; }
    }
    public class GetScheduleSummaryByIdByIdHandler : IRequestHandler<GetScheduleSummaryById, CIN.Application.FomMobB2CDtos.ErpFomScheduleSummaryDto>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetScheduleSummaryByIdByIdHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CIN.Application.FomMobB2CDtos.ErpFomScheduleSummaryDto> Handle(GetScheduleSummaryById request, CancellationToken cancellationToken)
        {
            var contractDetails = await _context.FomCustomerContracts.FirstOrDefaultAsync(x => x.ContractCode == request.ContractCode);

            //var deptCode = await _context.FomScheduleSummary.FirstOrDefaultAsync(x => x.ContractId == contractDetails.Id);

            CIN.Application.FomMobB2CDtos.ErpFomScheduleSummaryDto result = new();
            if (contractDetails != null)
            {
                result = await _context.FomScheduleSummary.AsNoTracking().Where(x => x.ContractId == contractDetails.Id && x.DeptCode == request.DeptCode)
                                 .Select(x => new CIN.Application.FomMobB2CDtos.ErpFomScheduleSummaryDto
                                 {
                                     Id = x.Id,
                                     IsApproved = x.IsApproved,
                                     DeptCode = x.DeptCode,
                                     ApproveDate = x.ApproveDate,
                                     IsSchGenerated = x.IsSchGenerated


                                 })
                                 .FirstOrDefaultAsync();
            }
            if (result != null)
            {
                result.TableRows = await _context.FomScheduleWeekdays.AsNoTracking()
                                       .Where(e => e.SchId == result.Id && e.ContractId == contractDetails.Id)
                                       .Select(e => new CIN.Application.FomMobB2CDtos.ErpFomScheduleWeekdaysDto
                                       {
                                           WeekDay = e.WeekDay,
                                           Time = e.Time.ToString(@"hh\:mm"),
                                           Remarks = e.Remarks,
                                           IsActive = e.IsActive

                                       })
                                       .ToListAsync();
            }
            return result;
        }

    }
    #endregion

    #endregion



    #region GenerateSchedule

    public class GenerateSchedule : IRequest<int>
    {
        public UserIdentityDto User { get; set; }
        public CIN.Application.FomMobB2CDtos.GenerateScheduleDto Input { get; set; }


    }

    public class GenerateScheduleHandler : IRequestHandler<GenerateSchedule, int>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;

        public GenerateScheduleHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(GenerateSchedule request, CancellationToken cancellationToken)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    Log.Info("----Info CreateUpdateSchedule method start----");
                    var contractDetails = await _context.FomCustomerContracts.FirstOrDefaultAsync(x => x.ContractCode == request.Input.ContractCode);

                    var scheduleDetailItems = request.Input.TableRows.Where(x => x.IsActive == true).ToList();

                    var start = request.Input.contStartDate;
                    var end = request.Input.contEndDate;
                    var contractId = contractDetails.Id;
                    var dept = request.Input.DeptCode;
                    var schId = request.Input.Id;



                    if (scheduleDetailItems.Count > 0)
                    {


                        // Date start=now.date(), end=now.date();

                        List<TblErpFomScheduleDetails> scheduleDetailsItemList = new();
                        TblErpFomScheduleDetails TblErpFomScheduleDetailsObj = new();

                        List<DayOfWeek> DoW = new List<DayOfWeek>();
                        DayOfWeek Dow2;
                        int iDow = 0;
                        DoW.Add(DayOfWeek.Monday);
                        DoW.Add(DayOfWeek.Tuesday);
                        DoW.Add(DayOfWeek.Wednesday);
                        DoW.Add(DayOfWeek.Thursday);
                        DoW.Add(DayOfWeek.Friday);
                        DoW.Add(DayOfWeek.Saturday);
                        DoW.Add(DayOfWeek.Sunday);

                        foreach (var obj in scheduleDetailItems)
                        {
                            if (obj.WeekDay == "Monday")
                            {
                                iDow = 0;
                            }
                            else if (obj.WeekDay == "Tuesday")
                            {
                                iDow = 1;
                            }
                            else if (obj.WeekDay == "Wednesday")
                            {
                                iDow = 2;
                            }
                            else if (obj.WeekDay == "Thursday")
                            {
                                iDow = 3;
                            }
                            else if (obj.WeekDay == "Friday")
                            {
                                iDow = 4;
                            }
                            else if (obj.WeekDay == "Saturday")
                            {
                                iDow = 5;
                            }
                            else if (obj.WeekDay == "Sunday")
                            {
                                iDow = 6;
                            }

                            Dow2 = DoW[iDow];
                            var DateDOW = Enumerable.Range(start.DayOfYear, end.Subtract(start).Days + 1).Select(n => start.AddDays(n - start.DayOfYear)).Where(d => d.DayOfWeek == Dow2);

                            foreach (var Dt in DateDOW)
                            {
                                TblErpFomScheduleDetailsObj = new();
                                TblErpFomScheduleDetailsObj.ContractId = contractId;
                                TblErpFomScheduleDetailsObj.Department = dept;
                                TblErpFomScheduleDetailsObj.Id = 0;
                                TblErpFomScheduleDetailsObj.IsActive = obj.IsActive;
                                TblErpFomScheduleDetailsObj.IsReschedule = false;
                                TblErpFomScheduleDetailsObj.SchDate = Dt;
                                TblErpFomScheduleDetailsObj.SchId = schId;
                                TblErpFomScheduleDetailsObj.SerType = "";
                                TblErpFomScheduleDetailsObj.ServiceItem = "";
                                TblErpFomScheduleDetailsObj.Remarks = obj.Remarks;
                                TblErpFomScheduleDetailsObj.Time = TimeSpan.Parse("0:" + obj.Time.Split(':')[0] + ":" + obj.Time.Split(':')[1] + ":0.0000000");
                                TblErpFomScheduleDetailsObj.TranNumber = contractId.ToString(); //Contract Id;
                                scheduleDetailsItemList.Add(TblErpFomScheduleDetailsObj);


                            }


                        }
                        //Start saving the schedule     TblErpFomScheduleSummary

                        if (scheduleDetailsItemList.Count > 0)
                        {
                            //BEgin transation
                            {
                                await _context.FomScheduleDetails.AddRangeAsync(scheduleDetailsItemList);
                                await _context.SaveChangesAsync();

                                var summaryHeader = await _context.FomScheduleSummary.FirstOrDefaultAsync(x => x.Id == schId);

                                if (summaryHeader != null)
                                {
                                    summaryHeader.IsSchGenerated = true;
                                    _context.FomScheduleSummary.Update(summaryHeader);
                                    await _context.SaveChangesAsync();

                                }

                                //update the row of sche header for IsGenerated=true;
                            }
                            //commit

                        }

                    }


                    //1 Filer List<TblErpFomScheduleDetails> into TmpList where active = true
                    //2 List Objst DEclare of type Detais List<TblErpFomScheduleDetails> TblErpFomScheduleDetailsList
                    //3 loop TmpList (you may get 3)
                    //Dow week (sun,on.tue.thur,fri,sat)



                    //Conver the string DOW to int value as in week day 11=monday, 2=tuesday.....7=sunday
                    //x-1 Get the numeric number of Sunday, Monday, Tue



                    //var sheduleWeekDay = await _context.FomScheduleWeekdays.Where(e => e.IsActive==true).AsNoTracking().ToListAsync();
                    // var nodeList = request.MenuNodeList.Nodes.Distinct();

                    //var DateDOW = Enumerable.Range(start.DayOfYear, end.Subtract(start).Days + 1).Select(n => start.AddDays(n - start.DayOfYear)).Where(d => d.DayOfWeek == Dow2);

                    //var ItemIdList = _context.FomScheduleWeekdays.Where(e => e.IsActive).AsNoTracking().Distinct()
                    //    .Select(e => new TblErpFomScheduleDetails { SchId = e.SchId, ContractId = e.ContractId, SchDate = DateTime.Now, Department = request.Input.DeptCode })
                    //    .ToList();

                    //await _context.FomScheduleDetails.AddRangeAsync(ItemIdList);
                    //await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return schId;
                    // return 1;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Log.Error("Error in CreateUpdateSchedule Method");
                    Log.Error("Error occured time : " + DateTime.UtcNow);
                    Log.Error("Error message : " + ex.Message);
                    Log.Error("Error StackTrace : " + ex.StackTrace);
                    return 0;
                }
            }

        }


    }



    #endregion



    #region GetGeneratedSchedule

    public class GetGeneratedSchedule : IRequest<CIN.Application.FomMobB2CDtos.GeneratedScheduleDetailsDto>
    {
        public UserIdentityDto User { get; set; }
        //public int Id { get; set; }
        public string ContractCode { get; set; }

        public string DeptCode { get; set; }
    }
    public class GetGeneratedScheduleHandler : IRequestHandler<GetGeneratedSchedule, CIN.Application.FomMobB2CDtos.GeneratedScheduleDetailsDto>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetGeneratedScheduleHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CIN.Application.FomMobB2CDtos.GeneratedScheduleDetailsDto> Handle(GetGeneratedSchedule request, CancellationToken cancellationToken)
        {
            var contractDetails = await _context.FomCustomerContracts.FirstOrDefaultAsync(x => x.ContractCode == request.ContractCode);

            //var deptCode = await _context.FomScheduleSummary.FirstOrDefaultAsync(x => x.ContractId == contractDetails.Id);

            CIN.Application.FomMobB2CDtos.GeneratedScheduleDetailsDto result = new();
            if (contractDetails != null)
            {
                result = await _context.FomScheduleSummary.AsNoTracking().Where(x => x.ContractId == contractDetails.Id && x.DeptCode == request.DeptCode)
                                 .Select(x => new CIN.Application.FomMobB2CDtos.GeneratedScheduleDetailsDto
                                 {
                                     Id = x.Id,
                                     IsApproved = x.IsApproved,
                                     DeptCode = x.DeptCode,
                                     ApproveDate = x.ApproveDate,
                                     IsSchGenerated = x.IsSchGenerated


                                 })
                                 .FirstOrDefaultAsync();
            }
            if (result != null)
            {
                result.DetailRows = await _context.FomScheduleDetails.AsNoTracking()
                                       .Where(e => e.SchId == result.Id && e.ContractId == contractDetails.Id && e.Department == result.DeptCode)
                                       .Select(e => new CIN.Application.FomMobB2CDtos.FomScheduleDetailsDto
                                       {
                                           ContractId = e.ContractId,
                                           SchDate = e.SchDate,
                                           Department = e.Department,
                                           SerType = e.SerType,
                                           Frequency = e.Frequency,
                                           TranNumber = e.TranNumber,
                                           ServiceItem = e.ServiceItem,
                                           Remarks = e.Remarks,
                                           Time = e.Time.ToString(@"hh\:mm"),
                                           IsReschedule = e.IsReschedule,
                                           IsActive = e.IsActive

                                       }).OrderBy(e => e.SchDate)
                                       .ToListAsync();
            }
            return result;
        }

    }
    #endregion


    #region GetAll
    public class GetFomCalenderScheduleList : IRequest<List<CIN.Application.FomMobB2CDtos.TblErpFomScheduleDetailsDto>>
    {
        public UserIdentityDto User { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ContractId { get; set; }

    }

    public class GetFomCalenderScheduleListHandler : IRequestHandler<GetFomCalenderScheduleList, List<CIN.Application.FomMobB2CDtos.TblErpFomScheduleDetailsDto>>
    {
        private readonly CINDBOneContext _context;
        // private readonly DBContext _context;
        private readonly IMapper _mapper;
        public GetFomCalenderScheduleListHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<CIN.Application.FomMobB2CDtos.TblErpFomScheduleDetailsDto>> Handle(GetFomCalenderScheduleList request, CancellationToken cancellationToken)
        {


            //var list = await _context.FomScheduleDetails.AsNoTracking().ProjectTo<CIN.Application.FomMobB2CDtos.TblErpFomScheduleDetailsDto>(_mapper.ConfigurationProvider).Where(e => e.SchDate >= request.StartDate && e.SchDate <= request.EndDate).ToListAsync();


            //return list;
            if (request.ContractId > 0)
            {
                var query = _context.FomScheduleDetails
                                                   .AsNoTracking()
                                                   .ProjectTo<CIN.Application.FomMobB2CDtos.TblErpFomScheduleDetailsDto>(_mapper.ConfigurationProvider)
                                                   .Where(e => (e.SchDate >= request.StartDate && e.SchDate <= request.EndDate) && e.ContractId == request.ContractId);

                var list = await query.ToListAsync();
                return list;
            }
            else
            {

                var query = _context.FomScheduleDetails
                                   .AsNoTracking()
                                   .ProjectTo<CIN.Application.FomMobB2CDtos.TblErpFomScheduleDetailsDto>(_mapper.ConfigurationProvider)
                                   .Where(e => (e.SchDate >= request.StartDate && e.SchDate <= request.EndDate));
                var list = await query.ToListAsync();
                return list;
            }


            //if (request.ContractId != 0)
            //{
            //    query = query.Where(e => e.ContractId == request.ContractId);
            //}





        }


    }


    #endregion


    #region GetSelectCustomerSiteListByCustomer

    public class GetSelectCustomerSiteByCustCode : IRequest<List<CustomSelectListItem>>
    {
        public UserIdentityDto User { get; set; }
        public string Code { get; set; }


    }

    public class GetSelectCustomerSiteByCustCodeHandler : IRequestHandler<GetSelectCustomerSiteByCustCode, List<CustomSelectListItem>>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetSelectCustomerSiteByCustCodeHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<CustomSelectListItem>> Handle(GetSelectCustomerSiteByCustCode request, CancellationToken cancellationToken)
        {

            Log.Info("----Info GetSelectSiteList method start----");
            var obj = _context.OprSites.AsNoTracking();

            obj = obj.Where(e => e.CustomerCode.Contains(request.Code));

            var newObj = await obj
               .OrderByDescending(e => e.Id)
               .Select(e => new CustomSelectListItem { Text = e.SiteName, TextTwo = e.SiteArbName, Value = e.SiteCode, })
                  .ToListAsync(cancellationToken);

            Log.Info("----Info GetSelectSiteList method Ends----");
            return newObj;
        }
    }

    #endregion

}
