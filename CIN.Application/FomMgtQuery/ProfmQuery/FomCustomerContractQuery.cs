using AutoMapper;
using CIN.Application;
using CIN.Application.Common;
using CIN.Application.FomMgtDtos;
//using CIN.Application.SalesSetupDtos;
using CIN.DB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using System.Linq.Dynamic.Core;
using CIN.Domain.SalesSetup;
using CIN.Domain.FomMgt;
using CIN.Application.ProfmQuery;

namespace CIN.Application.FomMgtQuery.ProfmQuery
{
    #region GetAll
    public class GetFomCustomerContractList : IRequest<PaginatedList<TblErpFomCustomerContractDto>>
    {
        public UserIdentityDto User { get; set; }
        public PaginationFilterDto Input { get; set; }

    }

    public class GetFomCustomerContractListHandler : IRequestHandler<GetFomCustomerContractList, PaginatedList<TblErpFomCustomerContractDto>>
    {
        private readonly CINDBOneContext _context;
        // private readonly DBContext _context;
        private readonly IMapper _mapper;
        public GetFomCustomerContractListHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedList<TblErpFomCustomerContractDto>> Handle(GetFomCustomerContractList request, CancellationToken cancellationToken)
        {

            var list = await _context.FomCustomerContracts.AsNoTracking().ProjectTo<TblErpFomCustomerContractDto>(_mapper.ConfigurationProvider)
                       .OrderByDescending(x=>x.Id)
                       .PaginationListAsync(request.Input.Page, request.Input.PageCount, cancellationToken);
            return list;
        }


    }


    #endregion

    #region GetById

    public class GetFomCustomerContractById : IRequest<TblErpFomCustomerContractDto>
    {
        public UserIdentityDto User { get; set; }
        public int Id { get; set; }
    }

    public class GetFomCustomerContractByIdHandler : IRequestHandler<GetFomCustomerContractById, TblErpFomCustomerContractDto>
    {
        private readonly CINDBOneContext _context;
        // private readonly DBContext _context;
        private readonly IMapper _mapper;
        public GetFomCustomerContractByIdHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TblErpFomCustomerContractDto> Handle(GetFomCustomerContractById request, CancellationToken cancellationToken)
        {

            TblErpFomCustomerContract obj = new();
            var customerContract = await _context.FomCustomerContracts.AsNoTracking().ProjectTo<TblErpFomCustomerContractDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(e => e.Id == request.Id);
            return customerContract;
            // throw new NotImplementedException();
        }
    }
    #endregion

    #region Create_And_Update

    public class CreateUpdateFomCustomerContract : IRequest<int>
    {
        public UserIdentityDto User { get; set; }
        public ErpFomCustomerContractDto CustomerContractDto { get; set; }
    }

    public class CreateUpdateFomCustomerContractHandler : IRequestHandler<CreateUpdateFomCustomerContract, int>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;

        public CreateUpdateFomCustomerContractHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateUpdateFomCustomerContract request, CancellationToken cancellationToken)
        {
            try
            {
                Log.Info("----Info Create Update  Fom Item Category method start----");

                var obj = request.CustomerContractDto;


                TblErpFomCustomerContract CustomerContract = new();
                if (obj.Id > 0)
                    CustomerContract = await _context.FomCustomerContracts.AsNoTracking().FirstOrDefaultAsync(e => e.Id == obj.Id);


                CustomerContract.Id = obj.Id;
                CustomerContract.ContractCode = obj.ContractCode;
                CustomerContract.CustCode = obj.CustCode;
                CustomerContract.CustSiteCode = obj.CustSiteCode;
                CustomerContract.CustContNumber = obj.CustContNumber;
                CustomerContract.ContStartDate = obj.ContStartDate;
                CustomerContract.ContEndDate = obj.ContEndDate;
                //CustomerContract.ContDeptCode = obj.ContDeptCode;
                CustomerContract.ContDeptCode = string.Join(",", obj.ContDeptCode);
                CustomerContract.ContProjManager = obj.ContProjManager;
                CustomerContract.ContProjSupervisor = obj.ContProjSupervisor;
                CustomerContract.ContApprAuthorities = obj.ContApprAuthorities;
                CustomerContract.IsAppreoved = obj.IsAppreoved;
                CustomerContract.IsSheduleRequired = obj.IsSheduleRequired;
                CustomerContract.ApprovedBy = obj.ApprovedBy;
                CustomerContract.ApprovedDate = obj.ApprovedDate;
                CustomerContract.Remarks = obj.Remarks;
                CustomerContract.CreatedBy = obj.CreatedBy;
                CustomerContract.CreatedDate = obj.CreatedDate;
                CustomerContract.IsActive = obj.IsActive;

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
                Log.Info("----Info Create Update Fom Customer Contract method Exit----");
                return CustomerContract.Id;
            }
            catch (Exception ex)
            {
                Log.Error("Error in Create Update Fom Customer Contract Method");
                Log.Error("Error occured time : " + DateTime.UtcNow);
                Log.Error("Error message : " + ex.Message);
                Log.Error("Error StackTrace : " + ex.StackTrace);
                return 0;
            }
        }


    }



    #endregion



    #region GetSelectforAuthList

    public class GetSelectResourcesQuery : IRequest<List<LanCustomSelectListItem>>
    {
        public UserIdentityDto User { get; set; }

        public string Input { get; set; }
    }

    public class GetSelectResourcesQueryHandler : IRequestHandler<GetSelectResourcesQuery, List<LanCustomSelectListItem>>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetSelectResourcesQueryHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<LanCustomSelectListItem>> Handle(GetSelectResourcesQuery request, CancellationToken cancellationToken)
        {

            var search = request.Input;

            var list = await _context.FomResources
                  .Where(e => e.ApprovalAuth == true || e.ResTypeCode.Contains(search) || search == null)
                  .Join(
                    _context.FomResourceType,  // Joining with ResourceType table
                    resource => resource.ResTypeCode,  // Joining on FomResource.ResourceTypeId
                    resourceType => resourceType.ResTypeCode,      // Joining on ResourceType.Id
                    (resource, resourceType) => new LanCustomSelectListItem
                    {
                        Text = resource.NameEng,
                        Value = resource.ResTypeCode,
                        TextTwo = resource.ResCode,
                        TextAr = resource.NameAr,
                        //  ResourceTypeText = resourceType.Name // Assuming ResourceType has a Name property
                    }
            )
            .AsNoTracking()
            .ToListAsync(cancellationToken);

            return list;
            //var list = await _context.FomResources
            //    .Where(e => e.ApprovalAuth == true)
            //  .AsNoTracking()
            // .Select(e => new LanCustomSelectListItem { Text = e.ResCode, Value = e.Id.ToString(), TextTwo = e.NameEng, TextAr = e.NameAr })
            //    .ToListAsync(cancellationToken);
            //return list;
        }
    }
    #endregion


    #region CreateUpdateSchedule

    public class CreateUpdateSchedule : IRequest<int>
    {
        public UserIdentityDto User { get; set; }
        public ErpFomScheduleSummaryDto ScheduleSummaryDto { get; set; }
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
    //public class GetScheduleSummaryList : IRequest<List<ErpFomScheduleSummaryDto>>
    //{
    //    public UserIdentityDto User { get; set; }
    //    public int Id { get; set; }
    //    public string ContractCode { get; set; }

    //}
    //public class GetScheduleSummaryListHandler : IRequestHandler<GetScheduleSummaryList, List<ErpFomScheduleSummaryDto>>
    //{
    //    private readonly CINDBOneContext _context;
    //    private readonly IMapper _mapper;
    //    public GetScheduleSummaryListHandler(CINDBOneContext context, IMapper mapper)
    //    {
    //        _context = context;
    //        _mapper = mapper;
    //    }

    //    public async Task<List<ErpFomScheduleSummaryDto>> Handle(GetScheduleSummaryList request, CancellationToken cancellationToken)
    //    {
    //        List<ErpFomScheduleSummaryDto> sList = new();

    //         var sheduleSummary = await _context.FomScheduleSummary.FirstOrDefaultAsync(x => x.Id == request.Id);

    //        //var sDetails = await _context.FomScheduleWeekdays.AsNoTracking().
    //        //                                        ProjectTo<TblErpFomScheduleWeekdaysDto>(_mapper.ConfigurationProvider).
    //        //                                        OrderByDescending(x => x.Id).
    //        //                                        FirstOrDefaultAsync();
    //        if (sheduleSummary != null)
    //        {
    //            sList = await _context.FomScheduleSummary.AsNoTracking()
    //                                    .Where(e => e.Id == request.Id )
    //                                    .Select(x => new ErpFomScheduleSummaryDto()
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
    public class GetScheduleSummaryById : IRequest<ErpFomScheduleSummaryDto>
    {
        public UserIdentityDto User { get; set; }
        //public int Id { get; set; }
        public string ContractCode { get; set; }

        public string DeptCode { get; set; }
    }
    public class GetScheduleSummaryByIdByIdHandler : IRequestHandler<GetScheduleSummaryById, ErpFomScheduleSummaryDto>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetScheduleSummaryByIdByIdHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ErpFomScheduleSummaryDto> Handle(GetScheduleSummaryById request, CancellationToken cancellationToken)
        {
            var contractDetails = await _context.FomCustomerContracts.FirstOrDefaultAsync(x => x.ContractCode == request.ContractCode);

            //var deptCode = await _context.FomScheduleSummary.FirstOrDefaultAsync(x => x.ContractId == contractDetails.Id);

            ErpFomScheduleSummaryDto result = new();
            if (contractDetails != null)
            {
                result = await _context.FomScheduleSummary.AsNoTracking().Where(x => x.ContractId == contractDetails.Id && x.DeptCode == request.DeptCode)
                                 .Select(x => new ErpFomScheduleSummaryDto
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
                                       .Select(e => new ErpFomScheduleWeekdaysDto
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
        public GenerateScheduleDto Input { get; set; }


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

    public class GetGeneratedSchedule : IRequest<GeneratedScheduleDetailsDto>
    {
        public UserIdentityDto User { get; set; }
        //public int Id { get; set; }
        public string ContractCode { get; set; }

        public string DeptCode { get; set; }
    }
    public class GetGeneratedScheduleHandler : IRequestHandler<GetGeneratedSchedule, GeneratedScheduleDetailsDto>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetGeneratedScheduleHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GeneratedScheduleDetailsDto> Handle(GetGeneratedSchedule request, CancellationToken cancellationToken)
        {
            var contractDetails = await _context.FomCustomerContracts.FirstOrDefaultAsync(x => x.ContractCode == request.ContractCode);

            //var deptCode = await _context.FomScheduleSummary.FirstOrDefaultAsync(x => x.ContractId == contractDetails.Id);

            GeneratedScheduleDetailsDto result = new();
            if (contractDetails != null)
            {
                result = await _context.FomScheduleSummary.AsNoTracking().Where(x => x.ContractId == contractDetails.Id && x.DeptCode == request.DeptCode)
                                 .Select(x => new GeneratedScheduleDetailsDto
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
                 var list = await _context.FomScheduleDetails.AsNoTracking()
                                       .Where(e => e.SchId == result.Id && e.ContractId == contractDetails.Id && e.Department == result.DeptCode)
                                       .Select(e => new FomScheduleDetailsDto
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
                if (list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        if (contractDetails != null)
                        {
                            item.ContractCode = contractDetails.ContractCode;
                            var custDetails = await _context.OprCustomers.AsNoTracking()
                                                    .ProjectTo<TblSndDefCustomerMasterDto>(_mapper.ConfigurationProvider)
                                                    .SingleOrDefaultAsync(e => e.CustCode == contractDetails.CustCode);
                            if (custDetails != null)
                            {
                                item.CustomerName = custDetails.CustName;
                                item.CustomerNameAr = custDetails.CustArbName;
                            }
                            var siteDetails = await _context.OprSites.AsNoTracking()
                                                    .ProjectTo<TblSndDefSiteMasterDto>(_mapper.ConfigurationProvider)
                                                    .SingleOrDefaultAsync(e => e.SiteCode == contractDetails.CustSiteCode);
                            if (siteDetails != null)
                            {
                                item.SiteName = siteDetails.SiteName;
                                item.SiteNameAr = siteDetails.SiteArbName;
                            }
                        }

                    }

                }
                result.DetailRows = list;
            }
            return result;
        }

    }
    #endregion


    #region GetAll
    public class GetFomCalenderScheduleList : IRequest<List<TblErpFomScheduleDetailsDto>>
    {
        public UserIdentityDto User { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ContractId { get; set; }

    }

    public class GetFomCalenderScheduleListHandler : IRequestHandler<GetFomCalenderScheduleList, List<TblErpFomScheduleDetailsDto>>
    {
        private readonly CINDBOneContext _context;
        // private readonly DBContext _context;
        private readonly IMapper _mapper;
        public GetFomCalenderScheduleListHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<TblErpFomScheduleDetailsDto>> Handle(GetFomCalenderScheduleList request, CancellationToken cancellationToken)
        {


            //var list = await _context.FomScheduleDetails.AsNoTracking().ProjectTo<TblErpFomScheduleDetailsDto>(_mapper.ConfigurationProvider).Where(e => e.SchDate >= request.StartDate && e.SchDate <= request.EndDate).ToListAsync();


            //return list;
            if (request.ContractId > 0)
            {
                var query = _context.FomScheduleDetails
                                                   .AsNoTracking()
                                                   .ProjectTo<TblErpFomScheduleDetailsDto>(_mapper.ConfigurationProvider)
                                                   .Where(e => (e.SchDate >= request.StartDate && e.SchDate <= request.EndDate) && e.ContractId == request.ContractId);

                var list = await query.ToListAsync();
                return list;
            }
            else
            {

                var query = _context.FomScheduleDetails
                                   .AsNoTracking()
                                   .ProjectTo<TblErpFomScheduleDetailsDto>(_mapper.ConfigurationProvider)
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

    #region GetFomCalenderScheduleListByDeptCode
    public class GetFomCalenderScheduleListByDeptCode : IRequest<List<FomScheduleDetailsDto>>
    {
        public UserIdentityDto User { get; set; }
        public string DeptCode { get; set; }
        public int ContractId { get; set; }

    }

    public class GetFomCalenderScheduleListByDeptCodeHandler : IRequestHandler<GetFomCalenderScheduleListByDeptCode, List<FomScheduleDetailsDto>>
    {
        private readonly CINDBOneContext _context;
        // private readonly DBContext _context;
        private readonly IMapper _mapper;
        public GetFomCalenderScheduleListByDeptCodeHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<FomScheduleDetailsDto>> Handle(GetFomCalenderScheduleListByDeptCode request, CancellationToken cancellationToken)
        {
            var list = await _context.FomScheduleDetails
                                                  .AsNoTracking()
                                                  .ProjectTo<TblErpFomScheduleDetailsDto>(_mapper.ConfigurationProvider)
                                                  .Where(e => e.Department == request.DeptCode && e.ContractId == request.ContractId)
                                                  .Select(e => new FomScheduleDetailsDto
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

            if (list.Count > 0)
            {
                foreach (var item in list)
                {
                    var contractDetails = await _context.FomCustomerContracts.AsNoTracking()
                                                .ProjectTo<TblErpFomCustomerContractDto>(_mapper.ConfigurationProvider)
                                                .SingleOrDefaultAsync(e => e.Id == request.ContractId);
                    if (contractDetails != null)
                    {
                        item.ContractCode = contractDetails.ContractCode;
                        var custDetails = await _context.OprCustomers.AsNoTracking()
                                                .ProjectTo<TblSndDefCustomerMasterDto>(_mapper.ConfigurationProvider)
                                                .SingleOrDefaultAsync(e => e.CustCode == contractDetails.CustCode);
                        if (custDetails != null)
                        {
                            item.CustomerName = custDetails.CustName;
                            item.CustomerNameAr = custDetails.CustArbName;
                        }
                        var siteDetails = await _context.OprSites.AsNoTracking()
                                                .ProjectTo<TblSndDefSiteMasterDto>(_mapper.ConfigurationProvider)
                                                .SingleOrDefaultAsync(e => e.SiteCode == contractDetails.CustSiteCode);
                        if (siteDetails != null)
                        {
                            item.SiteName = siteDetails.SiteName;
                            item.SiteNameAr = siteDetails.SiteArbName;
                        }
                    }

                }
            }

            return list;
        }


    }


    #endregion

    #region GetFomCalenderScheduleListByDeptCode
    public class AllFomCalenderScheduleList : IRequest<List<FomScheduleDetailsDto>>
    {
        public UserIdentityDto User { get; set; }
    }

    public class AllFomCalenderScheduleListHandler : IRequestHandler<AllFomCalenderScheduleList, List<FomScheduleDetailsDto>>
    {
        private readonly CINDBOneContext _context;
        // private readonly DBContext _context;
        private readonly IMapper _mapper;
        public AllFomCalenderScheduleListHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<FomScheduleDetailsDto>> Handle(AllFomCalenderScheduleList request, CancellationToken cancellationToken)
        {
            var startDate = DateTime.Now.AddMonths(-2);
            var endDate = DateTime.Now.AddDays(365);
            var list = await _context.FomScheduleDetails
                                                  .AsNoTracking()
                                                  .ProjectTo<TblErpFomScheduleDetailsDto>(_mapper.ConfigurationProvider)
                                                  .Where(e => e.SchDate >= startDate && e.SchDate <= endDate)
                                                  .Select(e => new FomScheduleDetailsDto
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

            if (list.Count > 0)
            {
                foreach (var item in list)
                {
                    var contractDetails = await _context.FomCustomerContracts.AsNoTracking()
                                                .ProjectTo<TblErpFomCustomerContractDto>(_mapper.ConfigurationProvider)
                                                .SingleOrDefaultAsync(e => e.Id == item.ContractId);
                    if (contractDetails != null)
                    {
                        item.ContractCode = contractDetails.ContractCode;
                        var custDetails = await _context.OprCustomers.AsNoTracking()
                                                .ProjectTo<TblSndDefCustomerMasterDto>(_mapper.ConfigurationProvider)
                                                .SingleOrDefaultAsync(e => e.CustCode == contractDetails.CustCode);
                        if (custDetails != null)
                        {
                            item.CustomerName = custDetails.CustName;
                            item.CustomerNameAr = custDetails.CustArbName;
                        }
                        var siteDetails = await _context.OprSites.AsNoTracking()
                                                .ProjectTo<TblSndDefSiteMasterDto>(_mapper.ConfigurationProvider)
                                                .SingleOrDefaultAsync(e => e.SiteCode == contractDetails.CustSiteCode);
                        if (siteDetails != null)
                        {
                            item.SiteName = siteDetails.SiteName;
                            item.SiteNameAr = siteDetails.SiteArbName;
                        }
                    }

                }
            }

            return list;
        }


    }


    #endregion

    #region GetPostAll
    public class CalenderScheduleList : IRequest<List<TblErpFomScheduleDetailsDto>>
    {
        public UserIdentityDto User { get; set; }
        public RQCalenderScheduleListDto Input { get; set; }

    }

    public class CalenderScheduleListHandler : IRequestHandler<CalenderScheduleList, List<TblErpFomScheduleDetailsDto>>
    {
        private readonly CINDBOneContext _context;
        // private readonly DBContext _context;
        private readonly IMapper _mapper;
        public CalenderScheduleListHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<TblErpFomScheduleDetailsDto>> Handle(CalenderScheduleList request, CancellationToken cancellationToken)
        {
            if (request.Input.ContractId > 0 && request.Input.StartDate != null && request.Input.EndDate != null)
            {
                var query = _context.FomScheduleDetails
                                                   .AsNoTracking()
                                                   .ProjectTo<TblErpFomScheduleDetailsDto>(_mapper.ConfigurationProvider)
                                                   .Where(e => (e.SchDate >= request.Input.StartDate && e.SchDate <= request.Input.EndDate) && e.ContractId == request.Input.ContractId);

                var list = await query.ToListAsync();
                return list;
            }
            else if (request.Input.ContractId == 0 && request.Input.StartDate != null && request.Input.EndDate != null)
            {
                var query = _context.FomScheduleDetails
                                                   .AsNoTracking()
                                                   .ProjectTo<TblErpFomScheduleDetailsDto>(_mapper.ConfigurationProvider)
                                                   .Where(e => (e.SchDate >= request.Input.StartDate && e.SchDate <= request.Input.EndDate));

                var list = await query.ToListAsync();
                return list;
            }
            else if (request.Input.ContractId > 0 && request.Input.StartDate != null && request.Input.EndDate == null)
            {
                var query = _context.FomScheduleDetails
                                                   .AsNoTracking()
                                                   .ProjectTo<TblErpFomScheduleDetailsDto>(_mapper.ConfigurationProvider)
                                                   .Where(e => (e.SchDate >= request.Input.StartDate) && e.ContractId == request.Input.ContractId);

                var list = await query.ToListAsync();
                return list;
            }
            else if (request.Input.ContractId > 0 && request.Input.StartDate == null && request.Input.EndDate != null)
            {
                var query = _context.FomScheduleDetails
                                                   .AsNoTracking()
                                                   .ProjectTo<TblErpFomScheduleDetailsDto>(_mapper.ConfigurationProvider)
                                                   .Where(e => (e.SchDate <= request.Input.EndDate) && e.ContractId == request.Input.ContractId);

                var list = await query.ToListAsync();
                return list;
            }
            else if (request.Input.ContractId > 0 && request.Input.StartDate == null && request.Input.EndDate == null)
            {
                var query = _context.FomScheduleDetails
                                                   .AsNoTracking()
                                                   .ProjectTo<TblErpFomScheduleDetailsDto>(_mapper.ConfigurationProvider)
                                                   .Where(e => e.ContractId == request.Input.ContractId);

                var list = await query.ToListAsync();
                return list;
            }
            else if (request.Input.ContractId == 0 && request.Input.StartDate != null && request.Input.EndDate == null)
            {
                var query = _context.FomScheduleDetails
                                                   .AsNoTracking()
                                                   .ProjectTo<TblErpFomScheduleDetailsDto>(_mapper.ConfigurationProvider)
                                                   .Where(e => (e.SchDate >= request.Input.StartDate));

                var list = await query.ToListAsync();
                return list;
            }
            else if (request.Input.ContractId == 0 && request.Input.StartDate == null && request.Input.EndDate != null)
            {
                var query = _context.FomScheduleDetails
                                                   .AsNoTracking()
                                                   .ProjectTo<TblErpFomScheduleDetailsDto>(_mapper.ConfigurationProvider)
                                                   .Where(e => (e.SchDate <= request.Input.EndDate));

                var list = await query.ToListAsync();
                return list;
            }
            else
            {
                return new();
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



    #region GetSelectCustomerSiteList

    public class GetSelectCustomerSiteList : IRequest<List<CustomSelectListItem>>
    {
        public UserIdentityDto User { get; set; }
    }

    public class GetSelectCustomerSiteListHandler : IRequestHandler<GetSelectCustomerSiteList, List<CustomSelectListItem>>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetSelectCustomerSiteListHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<CustomSelectListItem>> Handle(GetSelectCustomerSiteList request, CancellationToken cancellationToken)
        {

            Log.Info("----Info GetSelectSiteList method start----");
            var obj = _context.OprSites.AsNoTracking();


            var newObj = await obj
               .OrderByDescending(e => e.Id)
               .Select(e => new CustomSelectListItem { Text = e.SiteName, TextTwo = e.SiteArbName, Value = e.SiteCode, })
                  .ToListAsync(cancellationToken);

            Log.Info("----Info GetSelectSiteList method Ends----");
            return newObj;
        }
    }

    #endregion


    #region GetAll
    public class GetAllSchedulingList : IRequest<PaginatedList<RsErpFomScheduleDetailsDto>>
    {
        public UserIdentityDto User { get; set; }
        public PaginationFilterDto Input { get; set; }

    }

    public class GetAllSchedulingListHandler : IRequestHandler<GetAllSchedulingList, PaginatedList<RsErpFomScheduleDetailsDto>>
    {
        private readonly CINDBOneContext _context;
        // private readonly DBContext _context;
        private readonly IMapper _mapper;
        public GetAllSchedulingListHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedList<RsErpFomScheduleDetailsDto>> Handle(GetAllSchedulingList request, CancellationToken cancellationToken)
        {
            PaginatedList<RsErpFomScheduleDetailsDto> list;
            if (request.Input.ContractId > 0 && !string.IsNullOrEmpty(request.Input.StartDate) && !string.IsNullOrEmpty(request.Input.EndDate))
            {
                var startDate = new DateTime(Convert.ToInt32(request.Input.StartDate.Split('-')[0]), Convert.ToInt32(request.Input.StartDate.Split('-')[1]), Convert.ToInt32(request.Input.StartDate.Split('-')[2]));
                var endDate = new DateTime(Convert.ToInt32(request.Input.EndDate.Split('-')[0]), Convert.ToInt32(request.Input.EndDate.Split('-')[1]), Convert.ToInt32(request.Input.EndDate.Split('-')[2]));
                list = await _context.FomScheduleDetails
                                                   .AsNoTracking()
                                                   .ProjectTo<TblErpFomScheduleDetailsDto>(_mapper.ConfigurationProvider)
                                                   .Where(e => (e.SchDate >= startDate && e.SchDate <= endDate) && e.ContractId == request.Input.ContractId)
                                                   .Select(x=>new RsErpFomScheduleDetailsDto { 
                                                       ContractId=x.ContractId,
                                                       Department=x.Department,
                                                       Frequency=x.Frequency,
                                                       Id=x.Id,
                                                       IsActive=x.IsActive,
                                                       IsReschedule=x.IsReschedule,
                                                       Remarks=x.Remarks,
                                                       SchDate=x.SchDate,
                                                       SchId=x.SchId,
                                                       SerType=x.SerType,
                                                       ServiceItem=x.ServiceItem,
                                                       Time=x.Time,
                                                       TranNumber=x.TranNumber                                                       
                                                    })
                                                   .PaginationListAsync(request.Input.Page, request.Input.PageCount, cancellationToken);
            }
            else if (request.Input.ContractId == 0 && !string.IsNullOrEmpty(request.Input.StartDate) && !string.IsNullOrEmpty(request.Input.EndDate))
            {
                var startDate = new DateTime(Convert.ToInt32(request.Input.StartDate.Split('-')[0]), Convert.ToInt32(request.Input.StartDate.Split('-')[1]), Convert.ToInt32(request.Input.StartDate.Split('-')[2]));
                var endDate = new DateTime(Convert.ToInt32(request.Input.EndDate.Split('-')[0]), Convert.ToInt32(request.Input.EndDate.Split('-')[1]), Convert.ToInt32(request.Input.EndDate.Split('-')[2]));
                list = await _context.FomScheduleDetails
                                                   .AsNoTracking()
                                                   .ProjectTo<TblErpFomScheduleDetailsDto>(_mapper.ConfigurationProvider)
                                                   .Where(e => (e.SchDate >= startDate && e.SchDate <= endDate))
                                                   .Select(x => new RsErpFomScheduleDetailsDto
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
                                                       TranNumber = x.TranNumber
                                                   })
                                                   .PaginationListAsync(request.Input.Page, request.Input.PageCount, cancellationToken);
            }
            else if (request.Input.ContractId > 0 && !string.IsNullOrEmpty(request.Input.StartDate) && string.IsNullOrEmpty(request.Input.EndDate))
            {
                var startDate = new DateTime(Convert.ToInt32(request.Input.StartDate.Split('-')[0]), Convert.ToInt32(request.Input.StartDate.Split('-')[1]), Convert.ToInt32(request.Input.StartDate.Split('-')[2]));
                list = await _context.FomScheduleDetails
                                                   .AsNoTracking()
                                                   .ProjectTo<TblErpFomScheduleDetailsDto>(_mapper.ConfigurationProvider)
                                                   .Where(e => (e.SchDate >= startDate) && e.ContractId == request.Input.ContractId)
                                                   .Select(x => new RsErpFomScheduleDetailsDto
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
                                                       TranNumber = x.TranNumber
                                                   })
                                                   .PaginationListAsync(request.Input.Page, request.Input.PageCount, cancellationToken);
            }
            else if (request.Input.ContractId > 0 && string.IsNullOrEmpty(request.Input.StartDate) && !string.IsNullOrEmpty(request.Input.EndDate))
            {
                var endDate = new DateTime(Convert.ToInt32(request.Input.EndDate.Split('-')[0]), Convert.ToInt32(request.Input.EndDate.Split('-')[1]), Convert.ToInt32(request.Input.EndDate.Split('-')[2]));
                list = await _context.FomScheduleDetails
                                                   .AsNoTracking()
                                                   .ProjectTo<TblErpFomScheduleDetailsDto>(_mapper.ConfigurationProvider)
                                                   .Where(e => (e.SchDate <= endDate) && e.ContractId == request.Input.ContractId)
                                                   .Select(x => new RsErpFomScheduleDetailsDto
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
                                                       TranNumber = x.TranNumber
                                                   })
                                                   .PaginationListAsync(request.Input.Page, request.Input.PageCount, cancellationToken);
            }
            else if (request.Input.ContractId > 0 && string.IsNullOrEmpty(request.Input.StartDate) && string.IsNullOrEmpty(request.Input.EndDate))
            {
                list = await _context.FomScheduleDetails
                                                   .AsNoTracking()
                                                   .ProjectTo<TblErpFomScheduleDetailsDto>(_mapper.ConfigurationProvider)
                                                   .Where(e => e.ContractId == request.Input.ContractId)
                                                   .Select(x => new RsErpFomScheduleDetailsDto
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
                                                       TranNumber = x.TranNumber
                                                   })
                                                   .PaginationListAsync(request.Input.Page, request.Input.PageCount, cancellationToken);
            }
            else if (request.Input.ContractId == 0 && !string.IsNullOrEmpty(request.Input.StartDate) && string.IsNullOrEmpty(request.Input.EndDate))
            {
                var startDate = new DateTime(Convert.ToInt32(request.Input.StartDate.Split('-')[0]), Convert.ToInt32(request.Input.StartDate.Split('-')[1]), Convert.ToInt32(request.Input.StartDate.Split('-')[2]));
                list = await _context.FomScheduleDetails
                                                   .AsNoTracking()
                                                   .ProjectTo<TblErpFomScheduleDetailsDto>(_mapper.ConfigurationProvider)
                                                   .Where(e => (e.SchDate >= startDate))
                                                   .Select(x => new RsErpFomScheduleDetailsDto
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
                                                       TranNumber = x.TranNumber
                                                   })
                                                   .PaginationListAsync(request.Input.Page, request.Input.PageCount, cancellationToken);
            }
            else if (request.Input.ContractId == 0 && string.IsNullOrEmpty(request.Input.StartDate) && !string.IsNullOrEmpty(request.Input.EndDate))
            {
                var endDate = new DateTime(Convert.ToInt32(request.Input.EndDate.Split('-')[0]), Convert.ToInt32(request.Input.EndDate.Split('-')[1]), Convert.ToInt32(request.Input.EndDate.Split('-')[2]));
                list = await _context.FomScheduleDetails
                                                   .AsNoTracking()
                                                   .ProjectTo<TblErpFomScheduleDetailsDto>(_mapper.ConfigurationProvider)
                                                   .Where(e => (e.SchDate <= endDate))
                                                   .Select(x => new RsErpFomScheduleDetailsDto
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
                                                       TranNumber = x.TranNumber
                                                   })
                                                   .PaginationListAsync(request.Input.Page, request.Input.PageCount, cancellationToken);

            }
            else
            {
                list = await _context.FomScheduleDetails.AsNoTracking().ProjectTo<TblErpFomScheduleDetailsDto>(_mapper.ConfigurationProvider)
                                .Select(x => new RsErpFomScheduleDetailsDto
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
                                    TranNumber = x.TranNumber
                                })
                               .PaginationListAsync(request.Input.Page, request.Input.PageCount, cancellationToken);
            }
            if (list.TotalCount>0)
            {
                foreach (var item in list.Items)
                {
                    var contractDetails = await _context.FomCustomerContracts.AsNoTracking()
                                                .ProjectTo<TblErpFomCustomerContractDto>(_mapper.ConfigurationProvider)
                                                .SingleOrDefaultAsync(e => e.Id == item.ContractId);
                    if (contractDetails!=null)
                    {
                        item.ContractCode = contractDetails.ContractCode;
                        var custDetails = await _context.OprCustomers.AsNoTracking()
                                                .ProjectTo<TblSndDefCustomerMasterDto>(_mapper.ConfigurationProvider)
                                                .SingleOrDefaultAsync(e => e.CustCode == contractDetails.CustCode);
                        if (custDetails != null)
                        {
                            item.CustomerName = custDetails.CustName;
                            item.CustomerNameAr = custDetails.CustArbName;
                        }
                        var siteDetails = await _context.OprSites.AsNoTracking()
                                                .ProjectTo<TblSndDefSiteMasterDto>(_mapper.ConfigurationProvider)
                                                .SingleOrDefaultAsync(e => e.SiteCode == contractDetails.CustSiteCode);
                        if (siteDetails != null)
                        {
                            item.SiteName = siteDetails.SiteName;
                            item.SiteNameAr = siteDetails.SiteArbName;
                        }
                    }
                    
                }

            }
            return list;
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


    #region GetTicketsListPaginationWithFilter
    public class GetTicketsListPaginationWithFilterQuery : IRequest<PaginatedList<RecentTicketsDto>>
    {
        public UserIdentityDto User { get; set; }
        public InputTicketsPaginationFilterDto Input { get; set; }

    }

    public class GetTicketsListPaginationWithFilterQueryHandler : IRequestHandler<GetTicketsListPaginationWithFilterQuery, PaginatedList<RecentTicketsDto>>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetTicketsListPaginationWithFilterQueryHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedList<RecentTicketsDto>> Handle(GetTicketsListPaginationWithFilterQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Input.ToDate is not null)
                {
                    request.Input.ToDate = request.Input.ToDate.Value.AddDays(1);
                }
                var contracts = _context.FomCustomerContracts.Where(e => e.ContProjSupervisor == request.User.UserName || e.ContProjManager == request.User.UserName).Select(s => new { s.CustCode, s.CustSiteCode }).AsNoTracking();
                var search = request.Input.Query;
                var Customers = _context.OprCustomers.AsNoTracking();
                var Sites = _context.OprSites.AsNoTracking();
                var departments = _context.ErpFomDepartments.AsNoTracking();
                var logNotes = _context.FomJobTicketLogNotes.AsNoTracking();

                var list = _context.FomMobJobTickets.Select(e => new RecentTicketsDto
                {
                    TicketNumber = e.TicketNumber,
                    WorkStartDate = e.WorkStartDate,
                    WorkOrders = e.WorkOrders,
                    SiteCode = e.SiteCode,
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
                    JOImg1 = e.JOImg1,
                    JOImg2 = e.JOImg2,
                    JOStatus = e.JOStatus,
                    JOImg3 = e.JOImg3,
                    JOSubject = e.JOSubject,
                    JOSupervisor = e.JOSupervisor,
                    ModifiedBy = e.ModifiedBy,
                    ModifiedOn = e.ModifiedOn,
                    CustomerNameEng = Customers.FirstOrDefault(c => c.CustCode == e.CustomerCode).CustName ?? "",
                    CustomerNameArb = Customers.FirstOrDefault(c => c.CustCode == e.CustomerCode).CustArbName ?? "",
                    ProjectNameEng = Sites.FirstOrDefault(c => c.SiteCode == e.SiteCode).SiteName ?? "",
                    ProjectNameArb = Sites.FirstOrDefault(c => c.SiteCode == e.SiteCode).SiteArbName ?? "",
                    DepNameEng = departments.FirstOrDefault(c => c.DeptCode == e.JODeptCode).NameEng ?? "-NA-",
                    DepNameArb = departments.FirstOrDefault(c => c.DeptCode == e.SiteCode).NameArabic ?? "-NA-",
                    StatusStr = ((MetadataJoStatusEnum)e.JOStatus).ToString(),
                    LogNotesCount = logNotes.Count(l => l.TicketNumber == e.TicketNumber && l.Type == "N")
                })
                 .Where(e => e.IsActive && !e.IsVoid &&
                                (e.CustomerCode.Contains(search) ||
                                e.SiteCode.Contains(search) ||
                                e.TicketNumber.Contains(search) ||
                                e.JOSupervisor.Contains(search) ||
                                e.CustRegEmail.Contains(search) ||
                                e.CustomerNameArb.Contains(search) ||
                                e.CustomerNameEng.Contains(search) ||
                                e.ProjectNameArb.Contains(search) ||
                                e.ProjectNameEng.Contains(search) ||
                                e.DepNameEng.Contains(search) ||
                                e.DepNameArb.Contains(search) ||
                                search == "" ||
                                search == null||
                                string.IsNullOrEmpty(search))
                            //&& (contracts.Select(c => c.CustCode).Contains(e.CustomerCode) && contracts.Select(c => c.CustSiteCode).Contains(e.SiteCode))
                         );
                if (!string.IsNullOrEmpty(request.Input.CustomerCode))
                {
                    list = list.Where(e => e.CustomerCode == request.Input.CustomerCode);
                }
                if (!string.IsNullOrEmpty(request.Input.SiteCode))
                {
                    list = list.Where(e => e.SiteCode == request.Input.SiteCode);
                }
                if ((request.Input.Status is not null))
                {
                    list = list.Where(e => e.JOStatus == request.Input.Status);
                }



                if (!string.IsNullOrEmpty(request.Input.StatusStr))
                {
                    if (request.Input.StatusStr == "incomplete")
                    {
                        list = list.Where(e => (e.JOStatus == (short)MetadataJoStatusEnum.Open) || (e.JOStatus == (short)MetadataJoStatusEnum.Read));
                    }
                    else if (request.Input.StatusStr == "outofscope")
                    {
                        list = list.Where(e => (e.JOStatus == (short)MetadataJoStatusEnum.Open || e.JOStatus == (short)MetadataJoStatusEnum.Read || e.JOStatus == (short)MetadataJoStatusEnum.LateResponse));  //arrived
                        list = list.Where(e => e.IsApproved && !e.IsInScope && (!e.IsQuotationSubmitted || !e.IsPoRecieved));
                    }
                }





                if (request.Input.FromDate is not null && request.Input.ToDate is not null)
                {
                    list = list.Where(e => e.JODate < request.Input.ToDate && e.JODate >= request.Input.FromDate);
                }


                var nreports = await list.OrderBy(request.Input.OrderBy).PaginationListAsync(request.Input.Page, request.Input.PageCount, cancellationToken);

                return nreports;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


    }


    #endregion


    #region getWorkOrderListPaginationWithFilterQuery
    public class getWorkOrderListPaginationWithFilterQuery : IRequest<PaginatedList<RecentTicketsDto>>
    {
        public UserIdentityDto User { get; set; }
        public InputTicketsPaginationFilterDto Input { get; set; }

    }

    public class getWorkOrderListPaginationWithFilterQueryHandler : IRequestHandler<getWorkOrderListPaginationWithFilterQuery, PaginatedList<RecentTicketsDto>>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public getWorkOrderListPaginationWithFilterQueryHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedList<RecentTicketsDto>> Handle(getWorkOrderListPaginationWithFilterQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Input.ToDate is not null)
                {
                    request.Input.ToDate = request.Input.ToDate.Value.AddDays(1);
                }
                //var contracts = _context.FomCustomerContracts.Where(e => e.ContProjSupervisor == request.User.UserName || e.ContProjManager == request.User.UserName).Select(s => new { s.CustCode, s.CustSiteCode }).AsNoTracking();
                var search = request.Input.Query;
                var Customers = _context.OprCustomers.AsNoTracking();
                var Sites = _context.OprSites.AsNoTracking();
                var departments = _context.ErpFomDepartments.AsNoTracking();
                var logNotes = _context.FomJobTicketLogNotes.AsNoTracking();

                var list = _context.FomJobWorkOrders.Select(e => new RecentTicketsDto
                {
                    TicketNumber = e.TicketNumber,
                    WorkStartDate = e.WorkStartDate,
                    WorkOrders = e.WorkOrders,
                    SiteCode = e.SiteCode,
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
                    JOImg1 = e.JOImg1,
                    JOImg2 = e.JOImg2,
                    JOStatus = e.JOStatus,
                    JOImg3 = e.JOImg3,
                    JOSubject = e.JOSubject,
                    JOSupervisor = e.JOSupervisor,
                    ModifiedBy = e.ModifiedBy,
                    ModifiedOn = e.ModifiedOn,
                    CustomerNameEng = Customers.FirstOrDefault(c => c.CustCode == e.CustomerCode).CustName ?? "",
                    CustomerNameArb = Customers.FirstOrDefault(c => c.CustCode == e.CustomerCode).CustArbName ?? "",
                    ProjectNameEng = Sites.FirstOrDefault(c => c.SiteCode == e.SiteCode).SiteName ?? "",
                    ProjectNameArb = Sites.FirstOrDefault(c => c.SiteCode == e.SiteCode).SiteArbName ?? "",
                    DepNameEng = departments.FirstOrDefault(c => c.DeptCode == e.JODeptCode).NameEng ?? "-NA-",
                    DepNameArb = departments.FirstOrDefault(c => c.DeptCode == e.SiteCode).NameArabic ?? "-NA-",
                    StatusStr = ((MetadataJoStatusEnum)e.JOStatus).ToString(),
                    LogNotesCount = logNotes.Count(l => l.TicketNumber == e.TicketNumber && l.Type == "N")
                })
                 .Where(e => e.IsActive && !e.IsVoid &&
                                (e.CustomerCode.Contains(search) ||
                                e.SiteCode.Contains(search) ||
                                e.TicketNumber.Contains(search) ||
                                e.JOSupervisor.Contains(search) ||
                                e.CustRegEmail.Contains(search) ||
                                e.CustomerNameArb.Contains(search) ||
                                e.CustomerNameEng.Contains(search) ||
                                e.ProjectNameArb.Contains(search) ||
                                e.ProjectNameEng.Contains(search) ||
                                e.DepNameEng.Contains(search) ||
                                e.DepNameArb.Contains(search) ||
                                search == "" ||
                                search == null ||
                                string.IsNullOrEmpty(search))
                         //&& (contracts.Select(c => c.CustCode).Contains(e.CustomerCode) && contracts.Select(c => c.CustSiteCode).Contains(e.SiteCode))
                         );
                if (!string.IsNullOrEmpty(request.Input.CustomerCode))
                {
                    list = list.Where(e => e.CustomerCode == request.Input.CustomerCode);
                }
                if (!string.IsNullOrEmpty(request.Input.SiteCode))
                {
                    list = list.Where(e => e.SiteCode == request.Input.SiteCode);
                }
                if ((request.Input.Status is not null))
                {
                    list = list.Where(e => e.JOStatus == request.Input.Status);
                }



                if (!string.IsNullOrEmpty(request.Input.StatusStr))
                {
                    if (request.Input.StatusStr == "incomplete")
                    {
                        list = list.Where(e => (e.JOStatus == (short)MetadataJoStatusEnum.Open) || (e.JOStatus == (short)MetadataJoStatusEnum.Read));
                    }
                    else if (request.Input.StatusStr == "outofscope")
                    {
                        list = list.Where(e => (e.JOStatus == (short)MetadataJoStatusEnum.Open || e.JOStatus == (short)MetadataJoStatusEnum.Read || e.JOStatus == (short)MetadataJoStatusEnum.LateResponse));  //arrived
                        list = list.Where(e => e.IsApproved && !e.IsInScope && (!e.IsQuotationSubmitted || !e.IsPoRecieved));
                    }
                }





                if (request.Input.FromDate is not null && request.Input.ToDate is not null)
                {
                    list = list.Where(e => e.JODate < request.Input.ToDate && e.JODate >= request.Input.FromDate);
                }


                var nreports = await list.OrderBy(request.Input.OrderBy).PaginationListAsync(request.Input.Page, request.Input.PageCount, cancellationToken);

                return nreports;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


    }


    #endregion


    #region ViewTicket

    public class ViewTicketQuery : IRequest<ViewTicketDto>
    {
        public UserIdentityDto User { get; set; }
        public int Id { get; set; }
        public string TicketNumber { get; set; }
        public string WebRoot { get; set; }

    }

    public class ViewTicketQueryHandler : IRequestHandler<ViewTicketQuery, ViewTicketDto>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public ViewTicketQueryHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ViewTicketDto> Handle(ViewTicketQuery request, CancellationToken cancellationToken)
        {
            var obj = string.IsNullOrEmpty(request.TicketNumber) ?
                   await _context.FomMobJobTickets.AsNoTracking().ProjectTo<ViewTicketDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(e => e.Id == request.Id)
                : await _context.FomMobJobTickets.AsNoTracking().ProjectTo<ViewTicketDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(e => e.TicketNumber == request.TicketNumber);

            if (obj is not null)
            {
                obj.Image1WithFullPath = string.IsNullOrEmpty(obj.JOImg1) ? "" : request.WebRoot + obj.JOImg1;
                obj.Image2WithFullPath = string.IsNullOrEmpty(obj.JOImg2) ? "" : request.WebRoot + obj.JOImg2;
                obj.Image3WithFullPath = string.IsNullOrEmpty(obj.JOImg3) ? "" : request.WebRoot + obj.JOImg3;
                obj.Requester = _context.OprCustomers.FirstOrDefault(e => e.CustCode == obj.CustomerCode).CustName ?? "NA";
                obj.RequesterAr = _context.OprCustomers.FirstOrDefault(e => e.CustCode == obj.CustomerCode).CustArbName ?? "NA";
                obj.ProjectName = _context.OprSites.FirstOrDefault(e => e.SiteCode == obj.SiteCode).SiteName ?? "NA";
                obj.ProjectNameAr = _context.OprSites.FirstOrDefault(e => e.SiteCode == obj.SiteCode).SiteArbName ?? "NA";
                obj.RequestStatus = ((MetadataJoStatusEnum)obj.JOStatus).ToString();
                obj.MobileNumber = _context.OprCustomers.FirstOrDefault(e => e.CustCode == obj.CustomerCode).CustMobile1 ?? "NA";

                var service = await _context.ErpFomDepartments.FirstOrDefaultAsync(e => e.DeptCode == obj.JODeptCode);
                obj.ServiceType = service is null ? "-NA-" : service.NameEng;
                obj.ServiceTypeAr = service is null ? "-NA-" : service.NameArabic;

                var serviceCategory = service is null ? null : await _context.DepartmentTypes.FirstOrDefaultAsync(e => e.ServiceTypeCode == service.DeptServType);
                obj.ServiceCategory = serviceCategory is null ? "-NA-" : serviceCategory.ServiceTypeName;
                obj.ServiceCategoryAr = serviceCategory is null ? "-NA-" : serviceCategory.ServiceTypeName_Ar;
            }

            return obj;

        }
    }
    #endregion

    #region ViewWorkOrder

    public class ViewWorkOrderQuery : IRequest<ViewTicketDto>
    {
        public UserIdentityDto User { get; set; }
        public int Id { get; set; }
        public string TicketNumber { get; set; }
        public string WebRoot { get; set; }

    }

    public class ViewWorkOrderQueryHandler : IRequestHandler<ViewWorkOrderQuery, ViewTicketDto>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public ViewWorkOrderQueryHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ViewTicketDto> Handle(ViewWorkOrderQuery request, CancellationToken cancellationToken)
        {
            var workorderDetails = string.IsNullOrEmpty(request.TicketNumber) ?
                   await _context.FomJobWorkOrders.AsNoTracking().FirstOrDefaultAsync(e => e.Id == request.Id)
                : await _context.FomJobWorkOrders.AsNoTracking().FirstOrDefaultAsync(e => e.TicketNumber == request.TicketNumber);

            var obj = await _context.FomMobJobTickets.AsNoTracking().ProjectTo<ViewTicketDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(e => e.TicketNumber == workorderDetails.TicketNumber);

            if (obj is not null)
            {
                obj.Image1WithFullPath = string.IsNullOrEmpty(obj.JOImg1) ? "" : request.WebRoot + obj.JOImg1;
                obj.Image2WithFullPath = string.IsNullOrEmpty(obj.JOImg2) ? "" : request.WebRoot + obj.JOImg2;
                obj.Image3WithFullPath = string.IsNullOrEmpty(obj.JOImg3) ? "" : request.WebRoot + obj.JOImg3;
                obj.Requester = _context.OprCustomers.FirstOrDefault(e => e.CustCode == obj.CustomerCode).CustName ?? "NA";
                obj.RequesterAr = _context.OprCustomers.FirstOrDefault(e => e.CustCode == obj.CustomerCode).CustArbName ?? "NA";
                obj.ProjectName = _context.OprSites.FirstOrDefault(e => e.SiteCode == obj.SiteCode).SiteName ?? "NA";
                obj.ProjectNameAr = _context.OprSites.FirstOrDefault(e => e.SiteCode == obj.SiteCode).SiteArbName ?? "NA";
                obj.RequestStatus = ((MetadataJoStatusEnum)obj.JOStatus).ToString();
                obj.MobileNumber = _context.OprCustomers.FirstOrDefault(e => e.CustCode == obj.CustomerCode).CustMobile1 ?? "NA";

                var service = await _context.ErpFomDepartments.FirstOrDefaultAsync(e => e.DeptCode == obj.JODeptCode);
                obj.ServiceType = service is null ? "-NA-" : service.NameEng;
                obj.ServiceTypeAr = service is null ? "-NA-" : service.NameArabic;

                var serviceCategory = service is null ? null : await _context.DepartmentTypes.FirstOrDefaultAsync(e => e.ServiceTypeCode == service.DeptServType);
                obj.ServiceCategory = serviceCategory is null ? "-NA-" : serviceCategory.ServiceTypeName;
                obj.ServiceCategoryAr = serviceCategory is null ? "-NA-" : serviceCategory.ServiceTypeName_Ar;
            }

            return obj;

        }
    }
    #endregion



    #region UploadCustomerContractFiles       

    public class UploadCustomerContractFiles : IRequest<(bool, string)>
    {
        public UserIdentityDto User { get; set; }
        public InputImageFromCustomerDto Input { get; set; }
        public string WebRoot { get; set; }
    }

    public class UploadCustomerContractFilesHandler : IRequestHandler<UploadCustomerContractFiles, (bool, string)>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;

        public UploadCustomerContractFilesHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<(bool, string)> Handle(UploadCustomerContractFiles request, CancellationToken cancellationToken)
        {
            try
            {
                var obj = request.Input;
                TblErpFomCustomerContract CustomerContract = new();
                if (obj.Id > 0)
                    CustomerContract = await _context.FomCustomerContracts.AsNoTracking().FirstOrDefaultAsync(e => e.Id == obj.Id);

                if (request.Input.Image1IForm != null && request.Input.Image1IForm.Length > 0)
                {
                    var (res, fileName) = FileUploads.FileUploadWithIform(CustomerContract.ContractCode, request.WebRoot, request.Input.Image1IForm);
                    if (res)
                    {
                        CustomerContract.File1 = obj.WebRoot + fileName;
                    }
                }
                if (request.Input.Image2IForm != null && request.Input.Image2IForm.Length > 0)
                {
                    var (res, fileName) = FileUploads.FileUploadWithIform(CustomerContract.ContractCode, request.WebRoot, request.Input.Image2IForm);
                    if (res)
                    {
                        CustomerContract.File2 = obj.WebRoot + fileName;
                    }
                }
                if (request.Input.Image3IForm != null && request.Input.Image3IForm.Length > 0)
                {
                    var (res, fileName) = FileUploads.FileUploadWithIform(CustomerContract.ContractCode, request.WebRoot, request.Input.Image3IForm);
                    if (res)
                    {
                        CustomerContract.File3 = obj.WebRoot + fileName;
                    }
                }
                _context.FomCustomerContracts.Update(CustomerContract);
                await _context.SaveChangesAsync();
                return (true, "");
            }
            catch (Exception ex)
            {
                Log.Error("Error in Create Upda te Profm JobTicketHead Method");
                Log.Error("Error occured time : " + DateTime.UtcNow);
                Log.Error("Error message : " + ex.Message);
                Log.Error("Error StackTrace : " + ex.StackTrace);
                return (false, ex.Message);
            }

        }

    }



    #endregion
}
