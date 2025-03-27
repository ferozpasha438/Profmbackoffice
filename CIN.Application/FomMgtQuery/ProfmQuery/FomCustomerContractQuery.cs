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
using Microsoft.Data.SqlClient;

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
                       .OrderByDescending(x => x.Id)
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





    #region GetGeneratedScheduleFilter
    public class GetGeneratedScheduleFilter : IRequest<PaginatedList<GeneratedScheduleFilterDto>>
    {
        public UserIdentityDto User { get; set; }
        public PaginationFilterContractDto Input { get; set; }
    }

    public class GetGeneratedScheduleFilterHandler : IRequestHandler<GetGeneratedScheduleFilter, PaginatedList<GeneratedScheduleFilterDto>>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;

        public GetGeneratedScheduleFilterHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<GeneratedScheduleFilterDto>> Handle(GetGeneratedScheduleFilter request, CancellationToken cancellationToken)
        {
            var contractDetails = await _context.FomCustomerContracts
                .FirstOrDefaultAsync(x => x.ContractCode == request.Input.ContractCode);

            if (contractDetails == null)
            {
                //  return PaginatedList<GeneratedScheduleDetailsDto>.Empty(request.Input.Page, request.Input.PageCount);
                return new PaginatedList<GeneratedScheduleFilterDto>(new List<GeneratedScheduleFilterDto>(), 0, request.Input.Page, request.Input.PageCount);



            }

            DateTime? startDate = null;
            DateTime? endDate = null;
            if (!string.IsNullOrEmpty(request.Input.ContractCode) && !string.IsNullOrEmpty(request.Input.StartDate) && !string.IsNullOrEmpty(request.Input.EndDate))
            {


                startDate = new DateTime(Convert.ToInt32(request.Input.StartDate.Split('-')[0]), Convert.ToInt32(request.Input.StartDate.Split('-')[1]), Convert.ToInt32(request.Input.StartDate.Split('-')[2]));
                endDate = new DateTime(Convert.ToInt32(request.Input.EndDate.Split('-')[0]), Convert.ToInt32(request.Input.EndDate.Split('-')[1]), Convert.ToInt32(request.Input.EndDate.Split('-')[2]));

            }




            var result = await _context.FomScheduleSummary.AsNoTracking()
                .Where(x => x.ContractId == contractDetails.Id && x.DeptCode == request.Input.DeptCode).FirstOrDefaultAsync();
            //.Select(x => new GeneratedScheduleDetailsDto
            //{
            //    Id = x.Id,
            //    IsApproved = x.IsApproved,
            //    DeptCode = x.DeptCode,
            //    ApproveDate = x.ApproveDate,
            //    IsSchGenerated = x.IsSchGenerated
            //})
            //

            if (result == null)
            {
                return new PaginatedList<GeneratedScheduleFilterDto>(new List<GeneratedScheduleFilterDto>(), 0, request.Input.Page, request.Input.PageCount);

                // return PaginatedList<GeneratedScheduleDetailsDto>.Empty(request.Input.Page, request.Input.PageCount);
            }


            var list = await _context.FomScheduleDetails.AsNoTracking()
                .Where(e => e.SchId == result.Id && e.ContractId == contractDetails.Id && e.Department == request.Input.DeptCode && (e.SchDate >= startDate && e.SchDate <= endDate))
                .OrderBy(e => e.SchDate)
                .Select(e => new GeneratedScheduleFilterDto
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
                })
                .PaginationListAsync(request.Input.Page, request.Input.PageCount, cancellationToken);

            if (list.TotalCount > 0)
            {
                foreach (var item in list.Items)
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

            return list;

            //result.DetailRows = list.Items;
            //return new PaginatedList<GeneratedScheduleFilterDto>(
            //    new List<GeneratedScheduleFilterDto> { result },
            //    list.TotalCount, request.Input.Page, request.Input.PageCount);


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
            if (list.TotalCount > 0)
            {
                foreach (var item in list.Items)
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
                                search == null ||
                                string.IsNullOrEmpty(search))
                         //&& (contracts.Select(c => c.CustCode).Contains(e.CustomerCode) /*&& contracts.Select(c => c.CustSiteCode).Contains(e.SiteCode))*/
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


    #region GetJobTicketsReport
    public class GetJobTicketsReportList : IRequest<PaginatedList<RecentTicketsDto>>
    {
        public UserIdentityDto User { get; set; }
        public InputTicketsPaginationFilterDto Input { get; set; }

    }

    public class GetJobTicketsReportListHandler : IRequestHandler<GetJobTicketsReportList, PaginatedList<RecentTicketsDto>>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetJobTicketsReportListHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedList<RecentTicketsDto>> Handle(GetJobTicketsReportList request, CancellationToken cancellationToken)
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
                                search == null ||
                                string.IsNullOrEmpty(search))
                         // && (contracts.Select(c => c.CustCode).Contains(e.CustomerCode) )
                         );
                if (!string.IsNullOrEmpty(request.Input.CustomerCode))
                {
                    list = list.Where(e => e.CustomerCode == request.Input.CustomerCode);
                }
                if (!string.IsNullOrEmpty(request.Input.SiteCode))
                {
                    list = list.Where(e => e.SiteCode == request.Input.SiteCode);
                }
                //if (!string.IsNullOrEmpty(request.Input.ContractCode))
                //{
                //    list = list.Where(e => e.ContractCode == request.Input.ContractCode);
                //}
                //list = list.Where(e => e.IsActive && !e.IsVoid);
                if (request.Input.Status is not null)
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

    // Method to get Status text based on the MetadataJoStatusEnum




    #region GetSummaryJobTicketsReport
    public class GetSummaryJobTicketsReport : IRequest<PaginatedList<AggregatedReportDto>>
    {
        public UserIdentityDto User { get; set; }
        public InputTicketsReportPaginationFilterDto Input { get; set; }

    }
    public class GetSummaryJobTicketsReportHandler : IRequestHandler<GetSummaryJobTicketsReport, PaginatedList<AggregatedReportDto>>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;

        public GetSummaryJobTicketsReportHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<AggregatedReportDto>> Handle(GetSummaryJobTicketsReport request, CancellationToken cancellationToken)
        {
            try
            {

                // Temporary Fixed  Update IsClosed status based on IsCompleted status
                await _context.Database.ExecuteSqlRawAsync(
                    @"UPDATE TblFomJobTicket 
              SET IsClosed = 'false' 
              WHERE IsClosed = 'true' AND IsCompleted = 'true'");
                //Temporary Fixed Ends Here


                // Adjust ToDate to include the full day
                if (request.Input.ToDate is not null)
                {
                    request.Input.ToDate = request.Input.ToDate.Value.AddDays(1);
                }

                // Base query for job tickets
                var jobTicketsQuery = _context.FomMobJobTickets.AsNoTracking()
                    .Where(e => e.IsActive && !e.IsVoid);

                // Apply filters dynamically
                if (request.Input.FromDate is not null && request.Input.ToDate is not null)
                {
                    jobTicketsQuery = jobTicketsQuery.Where(e => e.JODate >= request.Input.FromDate && e.JODate < request.Input.ToDate);
                }

                if (!string.IsNullOrEmpty(request.Input.DeptCode))
                {
                    jobTicketsQuery = jobTicketsQuery.Where(e => e.JODeptCode == request.Input.DeptCode);
                }

                if (!string.IsNullOrEmpty(request.Input.CustomerCode))
                {
                    jobTicketsQuery = jobTicketsQuery.Where(e => e.CustomerCode == request.Input.CustomerCode);
                }

                if (!string.IsNullOrEmpty(request.Input.SiteCode))
                {
                    jobTicketsQuery = jobTicketsQuery.Where(e => e.SiteCode == request.Input.SiteCode);
                }

                if (!string.IsNullOrEmpty(request.Input.StatusStr))
                {
                    jobTicketsQuery = jobTicketsQuery.Where(e => e.JobMaintenanceType == request.Input.StatusStr);
                }

                // Compute Opening Jobs Before Aggregation
                var jobCounts = await _context.FomMobJobTickets.AsNoTracking()
                    .Where(j => j.JODate < request.Input.FromDate && !j.IsVoid)
                    .GroupBy(j => 1) // Single row result
                    .Select(g => new
                    {
                        TotJobs = g.Count(),
                        Closed = g.Count(j => j.IsClosed),
                        ForeClosed = g.Count(j => j.IsForeClosed),
                        Completed = g.Count(j => j.IsCompleted)
                    })
                    .FirstOrDefaultAsync(cancellationToken);

                int previousClosing = jobCounts != null ? jobCounts.TotJobs - (jobCounts.Closed + jobCounts.ForeClosed + jobCounts.Completed) : 0;

                // Order before processing
                var sortedJobTickets = await jobTicketsQuery
                    .OrderBy(j => j.JODate)
                    .ToListAsync(cancellationToken);

                List<AggregatedReportDto> aggregatedDataList = new();

                foreach (var group in sortedJobTickets.GroupBy(e => e.JODate.Date))
                {
                    int received = group.Count(); // Jobs received that day
                    int totalJobs = previousClosing + received;

                    var aggregatedData = new AggregatedReportDto
                    {
                        Date = group.Key,
                        Opening = previousClosing,
                        Received = received,
                        TotJobs = totalJobs,
                        ForeClosed = group.Count(e => e.IsForeClosed),
                        Closed = group.Count(e => e.IsClosed),
                        Completed = group.Count(e => e.IsCompleted),
                        Closing = totalJobs - (group.Count(e => e.IsClosed) + group.Count(e => e.IsForeClosed) + group.Count(e => e.IsCompleted)),
                        Percentage = group.Count(e => e.IsCompleted) * 100.0 / (totalJobs == 0 ? 1 : totalJobs) // Compute %Per
                    };

                    aggregatedDataList.Add(aggregatedData);

                    // Update previousClosing for the next iteration
                    previousClosing = aggregatedData.Closing;
                }

                // Main method
                var chartDataList = new List<ChartDataDto>();

                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync(cancellationToken);

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = @"
                                                SELECT JOStatus, COUNT(*) AS Count
                                                FROM TblFomJobTicket
                                                WHERE IsVoid = 'false' AND JOStatus NOT IN (8, 9, 11)
                                                AND JODate >= (SELECT MIN(JODate) FROM TblFomJobTicket)
                                                AND JODate <= @ToDate
                                                GROUP BY JOStatus
                                                ORDER BY JOStatus";

                        var toDateParam = command.CreateParameter();
                        toDateParam.ParameterName = "@ToDate";
                        toDateParam.Value = request.Input.ToDate;
                        command.Parameters.Add(toDateParam);

                        using (var reader = await command.ExecuteReaderAsync(cancellationToken))
                        {
                            while (await reader.ReadAsync(cancellationToken))
                            {
                                var status = reader.GetInt16(0); // JOStatus is an Int16
                                var count = reader.GetInt32(1);  // COUNT(*) is an Int32

                                chartDataList.Add(new ChartDataDto
                                {
                                    Status = status,
                                    Count = count,
                                    Name = GetStatusText(status) // Map the status text
                                });
                            }
                        }
                    }
                }

                var totalJobsRecive = aggregatedDataList.Sum(x => x.Received);
                var totalCompleted = aggregatedDataList.Sum(x => x.Completed);
                var totalBalance = totalJobsRecive - totalCompleted;


                var performanceStats = new
                {
                    OpeningJobs = aggregatedDataList.Sum(x => x.Opening),
                    TotalReceived = aggregatedDataList.Sum(x => x.Received),
                    TotalJobs = aggregatedDataList.Sum(x => x.TotJobs),
                    Completed = aggregatedDataList.Sum(x => x.Completed),
                    Balance = aggregatedDataList.Sum(x => x.Received) - aggregatedDataList.Sum(x => x.Completed),
                    CompletedPercentage = (totalJobsRecive == 0) ? 0 : (totalCompleted * 100.0 / totalJobsRecive),
                    BalancePercentage = (totalJobsRecive == 0) ? 0 : (totalBalance * 100.0 / totalJobsRecive),
                    Percentage = aggregatedDataList.Sum(x => x.Completed) * 100.0 / (aggregatedDataList.Sum(x => x.TotJobs) == 0 ? 1 : aggregatedDataList.Sum(x => x.TotJobs))
                };


                // Apply Pagination
                int pageNumber = request.Input.Page > 0 ? request.Input.Page : 1;
                int pageSize = request.Input.PageCount > 0 ? request.Input.PageCount : 10;
                int totalCount = aggregatedDataList.Count;

                var paginatedList = new PaginatedList<AggregatedReportDto>(
                    aggregatedDataList.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList(),
                    totalCount,
                    pageNumber,
                    pageSize
                );

                // Add chart data to the response (if applicable)
                paginatedList.ChartData = chartDataList;

                paginatedList.PerformanceStatistics = performanceStats;

                return paginatedList;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Handle method: {ex.Message}");
                return null;
            }
        }



        // Method to get Status text based on MetadataJoStatusEnum
        private string GetStatusText(int status)
        {
            if (Enum.IsDefined(typeof(MetadataJoStatusEnum), status))
            {
                return ((MetadataJoStatusEnum)status).ToString(); // Converts the enum value to its name
            }
            return "Unknown"; // Default text for undefined status
        }


    }






    #endregion



    #region GetDeptWiseSummaryJobTicketsReport
    public class GetDeptWiseSummaryJobTicketsReport : IRequest<PaginatedList<AggregatedReportDto>>
    {
        public UserIdentityDto User { get; set; }
        public InputTicketsReportPaginationFilterDto Input { get; set; }

    }
    public class GetDeptWiseSummaryJobTicketsReportHandler : IRequestHandler<GetDeptWiseSummaryJobTicketsReport, PaginatedList<AggregatedReportDto>>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;

        public GetDeptWiseSummaryJobTicketsReportHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<PaginatedList<AggregatedReportDto>> Handle(GetDeptWiseSummaryJobTicketsReport request, CancellationToken cancellationToken)
        {
            try
            {

                // Temporary Fixed  Update IsClosed status based on IsCompleted status
                await _context.Database.ExecuteSqlRawAsync(
                    @"UPDATE TblFomJobTicket 
              SET IsClosed = 'false' 
              WHERE IsClosed = 'true' AND IsCompleted = 'true'");
                //Temporary Fixed Ends Here


                // Adjust ToDate to include the full day
                if (request.Input.ToDate is not null)
                {
                    request.Input.ToDate = request.Input.ToDate.Value.AddDays(1);
                }

                // Base query for job tickets
                var jobTicketsQuery = _context.FomMobJobTickets.AsNoTracking()
                    .Where(e => e.IsActive && !e.IsVoid);

                // Apply filters dynamically
                if (request.Input.FromDate is not null && request.Input.ToDate is not null)
                {
                    jobTicketsQuery = jobTicketsQuery.Where(e => e.JODate >= request.Input.FromDate && e.JODate < request.Input.ToDate);
                }

                if (!string.IsNullOrEmpty(request.Input.CustomerCode))
                {
                    jobTicketsQuery = jobTicketsQuery.Where(e => e.CustomerCode == request.Input.CustomerCode);
                }

                if (!string.IsNullOrEmpty(request.Input.SiteCode))
                {
                    jobTicketsQuery = jobTicketsQuery.Where(e => e.SiteCode == request.Input.SiteCode);
                }

                if (!string.IsNullOrEmpty(request.Input.StatusStr))
                {
                    jobTicketsQuery = jobTicketsQuery.Where(e => e.JobMaintenanceType == request.Input.StatusStr);
                }

                // Compute Opening Jobs Before Aggregation
                var jobCounts = await _context.FomMobJobTickets.AsNoTracking()
                    .Where(j => j.JODate < request.Input.FromDate && !j.IsVoid)
                    .GroupBy(j => j.JODeptCode) // Group by Discipline
                    .Select(g => new
                    {
                        Discipline = g.Key,
                        TotJobs = g.Count(),
                        Closed = g.Count(j => j.IsClosed),
                        ForeClosed = g.Count(j => j.IsForeClosed),
                        Completed = g.Count(j => j.IsCompleted)
                    })
                    .ToListAsync(cancellationToken);

                // Convert to Dictionary for quick access
                var previousClosingDict = jobCounts.ToDictionary(
                    j => j.Discipline,
                    j => j.TotJobs - (j.Closed + j.ForeClosed + j.Completed)
                );

                // Group by Discipline
                var groupedJobTickets = await jobTicketsQuery
                    .GroupBy(e => e.JODeptCode)
                    .Select(group => new AggregatedReportDto
                    {
                        DeptName = group.Key,
                        Opening = previousClosingDict.ContainsKey(group.Key) ? previousClosingDict[group.Key] : 0,
                        Received = group.Count(), // Jobs received that day
                        ForeClosed = group.Count(e => e.IsForeClosed),
                        Closed = group.Count(e => e.IsClosed),
                        Completed = group.Count(e => e.IsCompleted),
                        Closing = (previousClosingDict.ContainsKey(group.Key) ? previousClosingDict[group.Key] : 0) + group.Count() - (group.Count(e => e.IsClosed) + group.Count(e => e.IsForeClosed) + group.Count(e => e.IsCompleted)),
                        // Calculate Completion Percentage
                        Percentage = group.Count() > 0 ? ((double)group.Count(e => e.IsCompleted) / group.Count()) * 100 : 0
                    })
                    //.OrderBy(g => g.) // Order alphabetically
                    .ToListAsync(cancellationToken);


                var totalJobsRecive = groupedJobTickets.Sum(x => x.Received);
                var totalCompleted = groupedJobTickets.Sum(x => x.Completed);
                var totalBalance = totalJobsRecive - totalCompleted;


                var performanceStats = new
                {
                    OpeningJobs = groupedJobTickets.Sum(x => x.Opening),
                    TotalReceived = groupedJobTickets.Sum(x => x.Received),
                    TotalJobs = groupedJobTickets.Sum(x => x.TotJobs),
                    Completed = groupedJobTickets.Sum(x => x.Completed),
                    Balance = groupedJobTickets.Sum(x => x.Received) - groupedJobTickets.Sum(x => x.Completed),
                    CompletedPercentage = (totalJobsRecive == 0) ? 0 : (totalCompleted * 100.0 / totalJobsRecive),
                    BalancePercentage = (totalJobsRecive == 0) ? 0 : (totalBalance * 100.0 / totalJobsRecive),
                    Percentage = groupedJobTickets.Sum(x => x.Completed) * 100.0 / (groupedJobTickets.Sum(x => x.TotJobs) == 0 ? 1 : groupedJobTickets.Sum(x => x.TotJobs))
                };


                // Apply Pagination
                int pageNumber = request.Input.Page > 0 ? request.Input.Page : 1;
                int pageSize = request.Input.PageCount > 0 ? request.Input.PageCount : 10;
                int totalCount = groupedJobTickets.Count;

                var paginatedList = new PaginatedList<AggregatedReportDto>(
                    groupedJobTickets.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList(),
                    totalCount,
                    pageNumber,
                    pageSize
                );


                paginatedList.PerformanceStatistics = performanceStats;
                return paginatedList;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Handle method: {ex.Message}");
                return null;
            }
        }

    }

    #endregion



    #region GetProjectWiseSummaryJobTicketsReport
    public class GetProjectWiseSummaryJobTicketsReport : IRequest<PaginatedList<AggregatedReportDto>>
    {
        public UserIdentityDto User { get; set; }
        public InputTicketsReportPaginationFilterDto Input { get; set; }

    }
    public class GetProjectWiseSummaryJobTicketsReportHandler : IRequestHandler<GetProjectWiseSummaryJobTicketsReport, PaginatedList<AggregatedReportDto>>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;

        public GetProjectWiseSummaryJobTicketsReportHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<PaginatedList<AggregatedReportDto>> Handle(GetProjectWiseSummaryJobTicketsReport request, CancellationToken cancellationToken)
        {
            try
            {


                // Temporary Fixed  Update IsClosed status based on IsCompleted status
                await _context.Database.ExecuteSqlRawAsync(
                    @"UPDATE TblFomJobTicket 
              SET IsClosed = 'false' 
              WHERE IsClosed = 'true' AND IsCompleted = 'true'");
                //Temporary Fixed Ends Here



                // Adjust ToDate to include the full day
                if (request.Input.ToDate is not null)
                {
                    request.Input.ToDate = request.Input.ToDate.Value.AddDays(1);
                }

                // Base query for job tickets
                var jobTicketsQuery = _context.FomMobJobTickets.AsNoTracking()
                    .Where(e => e.IsActive && !e.IsVoid);

                // Apply filters dynamically
                if (request.Input.FromDate is not null && request.Input.ToDate is not null)
                {
                    jobTicketsQuery = jobTicketsQuery.Where(e => e.JODate >= request.Input.FromDate && e.JODate < request.Input.ToDate);
                }

                if (!string.IsNullOrEmpty(request.Input.CustomerCode))
                {
                    jobTicketsQuery = jobTicketsQuery.Where(e => e.CustomerCode == request.Input.CustomerCode);
                }

                if (!string.IsNullOrEmpty(request.Input.SiteCode))
                {
                    jobTicketsQuery = jobTicketsQuery.Where(e => e.SiteCode == request.Input.SiteCode);
                }

                if (!string.IsNullOrEmpty(request.Input.StatusStr))
                {
                    jobTicketsQuery = jobTicketsQuery.Where(e => e.JobMaintenanceType == request.Input.StatusStr);
                }

                // Compute Opening Jobs Before Aggregation
                var jobCounts = await _context.FomMobJobTickets.AsNoTracking()
                    .Where(j => j.JODate < request.Input.FromDate && !j.IsVoid)
                    .GroupBy(j => j.SiteCode) // Group by Discipline
                    .Select(g => new
                    {
                        SiteName = g.Key,
                        TotJobs = g.Count(),
                        Closed = g.Count(j => j.IsClosed),
                        ForeClosed = g.Count(j => j.IsForeClosed),
                        Completed = g.Count(j => j.IsCompleted)
                    })
                    .ToListAsync(cancellationToken);

                // Convert to Dictionary for quick access
                var previousClosingDict = jobCounts.ToDictionary(
                    j => j.SiteName,
                    j => j.TotJobs - (j.Closed + j.ForeClosed + j.Completed)
                );



                var siteDict = await _context.ProfmDefSiteMaster
                    .ToDictionaryAsync(s => s.SiteCode, s => s.SiteName);

                var groupedJobTickets = await jobTicketsQuery
                                    .GroupBy(e => e.SiteCode)
                                    .Select(group => new AggregatedReportDto
                                    {
                                        ProjectName = siteDict.ContainsKey(group.Key) ? siteDict[group.Key] : "Unknown",
                                        Opening = previousClosingDict.ContainsKey(group.Key) ? previousClosingDict[group.Key] : 0,
                                        Received = group.Count(),
                                        ForeClosed = group.Count(e => e.IsForeClosed),
                                        Closed = group.Count(e => e.IsClosed),
                                        Completed = group.Count(e => e.IsCompleted),
                                        Closing = (previousClosingDict.ContainsKey(group.Key) ? previousClosingDict[group.Key] : 0) +
                                                  group.Count() - (group.Count(e => e.IsClosed) + group.Count(e => e.IsForeClosed) + group.Count(e => e.IsCompleted)),
                                        Percentage = group.Count() > 0 ? ((double)group.Count(e => e.IsCompleted) / group.Count()) * 100 : 0
                                    })
                                    .ToListAsync(cancellationToken);


                var totalJobsRecive = groupedJobTickets.Sum(x => x.Received);
                var totalCompleted = groupedJobTickets.Sum(x => x.Completed);
                var totalBalance = totalJobsRecive - totalCompleted;


                var performanceStats = new
                {
                    OpeningJobs = groupedJobTickets.Sum(x => x.Opening),
                    TotalReceived = groupedJobTickets.Sum(x => x.Received),
                    TotalJobs = groupedJobTickets.Sum(x => x.TotJobs),
                    Completed = groupedJobTickets.Sum(x => x.Completed),
                    Balance = groupedJobTickets.Sum(x => x.Received) - groupedJobTickets.Sum(x => x.Completed),
                    CompletedPercentage = (totalJobsRecive == 0) ? 0 : (totalCompleted * 100.0 / totalJobsRecive),
                    BalancePercentage = (totalJobsRecive == 0) ? 0 : (totalBalance * 100.0 / totalJobsRecive),
                    Percentage = groupedJobTickets.Sum(x => x.Completed) * 100.0 / (groupedJobTickets.Sum(x => x.TotJobs) == 0 ? 1 : groupedJobTickets.Sum(x => x.TotJobs))
                };

                // Apply Pagination
                int pageNumber = request.Input.Page > 0 ? request.Input.Page : 1;
                int pageSize = request.Input.PageCount > 0 ? request.Input.PageCount : 10;
                int totalCount = groupedJobTickets.Count;

                var paginatedList = new PaginatedList<AggregatedReportDto>(
                    groupedJobTickets.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList(),
                    totalCount,
                    pageNumber,
                    pageSize
                );

                paginatedList.PerformanceStatistics = performanceStats;
                return paginatedList;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Handle method: {ex.Message}");
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


    //#region GetActivitiesByDeptCode
    //public class GetDeptActivitiesByDeptCodes : IRequest<List<TblErpFomActivitiesDto>>
    //{
    //    public UserIdentityDto User { get; set; }
    //    public string [] DeptCodes { get; set; } // Change from string to List<string>
    //}

    //public class GetDeptActivitiesByDeptCodesHandler : IRequestHandler<GetDeptActivitiesByDeptCodes, List<TblErpFomActivitiesDto>>
    //{
    //    private readonly CINDBOneContext _context;
    //    private readonly IMapper _mapper;
    //    public GetDeptActivitiesByDeptCodesHandler(CINDBOneContext context, IMapper mapper)
    //    {
    //        _context = context;
    //        _mapper = mapper;
    //    }
    //    public async Task<List<TblErpFomActivitiesDto>> Handle(GetDeptActivitiesByDeptCodes request, CancellationToken cancellationToken)
    //    {
    //        var list = await _context.FomActivities.AsNoTracking()
    //            .ProjectTo<TblErpFomActivitiesDto>(_mapper.ConfigurationProvider)
    //            .Where(e => request.DeptCodes.Contains(e.DeptCode)) // Filtering by multiple department codes
    //            .ToListAsync();

    //        return list;
    //    }
    //}



    //#endregion

    #region GetActivitiesByDeptCode
    public class GetDeptActivitiesByDeptCodes : IRequest<List<DisciplineDto>>
    {
        public UserIdentityDto User { get; set; }
        public string[] DeptCodes { get; set; } // Change from string to List<string>
        public string ContractCode { get; set; }
    }

    public class GetDeptActivitiesByDeptCodesHandler : IRequestHandler<GetDeptActivitiesByDeptCodes, List<DisciplineDto>>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;

        public GetDeptActivitiesByDeptCodesHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<DisciplineDto>> Handle(GetDeptActivitiesByDeptCodes request, CancellationToken cancellationToken)
        {
            List<DisciplineDto> disciplines;

            if (!string.IsNullOrWhiteSpace(request.ContractCode) && request.ContractCode != "null")
            {
                // Fetch activities from ContractDeptActivity with ContractCode
                //var contractActivities = await _context.ContractDeptActivity
                //    .AsNoTracking()
                //    .Where(e => request.DeptCodes.Contains(e.DeptCode) && e.ContractCode == request.ContractCode)
                //    .ToListAsync(cancellationToken);

                //// Fetch activities from FomActivities
                //var fomActivities = await _context.FomActivities
                //    .AsNoTracking()
                //    .ProjectTo<TblErpFomActivitiesDto>(_mapper.ConfigurationProvider)
                //    .Where(e => request.DeptCodes.Contains(e.DeptCode))
                //    .ToListAsync(cancellationToken);

                //// Get unique ActCodes from contractActivities to determine common activities
                //var contractActivityCodes = new HashSet<string>(contractActivities.Select(a => a.ActCode));

                //// Combine activities with the SelectCheckBox flag and include DeptCode
                //var combinedActivities = fomActivities.Select(fomActivity => new ActivityDto
                //{
                //    DeptCode = fomActivity.DeptCode, // Ensure DeptCode is included
                //    ActivityName = fomActivity.ActName,
                //    SelectCheckBox = contractActivityCodes.Contains(fomActivity.ActCode)
                //}).ToList();

                var contractActivities = await _context.ContractDeptActivity
                                        .AsNoTracking()
                                        .Where(e => request.DeptCodes.Contains(e.DeptCode) && e.ContractCode == request.ContractCode)
                                        .ToListAsync(cancellationToken);

                // Fetch activities from FomActivities
                var fomActivities = await _context.FomActivities
                                    .AsNoTracking()
                                    .ProjectTo<TblErpFomActivitiesDto>(_mapper.ConfigurationProvider)
                                    .Where(e => request.DeptCodes.Contains(e.DeptCode))
                                    .ToListAsync(cancellationToken);

                //// Get unique ActCodes from contractActivities to determine common activities
                //var contractActivityCodes = new HashSet<string>(contractActivities.Select(a => a.ActCode));

                //// Combine activities with the SelectCheckBox flag and include DeptCode
                //var combinedActivities = fomActivities.Select(fomActivity => new ActivityDto
                //{
                //    DeptCode = fomActivity.DeptCode, // Ensure DeptCode is included
                //    ActCode = fomActivity.ActCode,
                //    SelectCheckBox = true // true if in contractActivities
                //}).ToList();

                var contractActivityCodes = new HashSet<string>(contractActivities
                                         .Where(a => !string.IsNullOrWhiteSpace(a.ActCode))
                                         .Select(a => a.ActCode.Trim().ToLower())

                                 );

                // Combine activities with the SelectCheckBox flag and include DeptCode
                var combinedActivities = fomActivities.Select(fomActivity =>
                {
                    // Ensure ActCode is not null and trim/convert to lower case for comparison
                    var normalizedActCode = fomActivity.ActCode?.Trim().ToLower();

                    return new ActivityDto
                    {
                        DeptCode = fomActivity.DeptCode,
                        ActCode = fomActivity.ActCode,
                        SelectCheckBox = normalizedActCode != null && contractActivityCodes.Contains(normalizedActCode)
                    };
                }).ToList();



                disciplines = combinedActivities
                                    .GroupBy(a => a.DeptCode)
                                    .Select(group => new DisciplineDto
                                    {
                                        DisciplineName = group.Key, // DeptCode as the DisciplineName
                                        Activities = group.ToList() // List of ActivityDto
                                    })
                                    .ToList();


                //    // Group combined activities by DeptCode and create DisciplineDto
                //    disciplines = combinedActivities
                //        .GroupBy(a => a.DeptCode)
                //        .Select(group => new DisciplineDto
                //        {
                //            DisciplineName = group.Key, // DeptCode as the DisciplineName
                //    Activities = group.ToList() // List of ActivityDto
                //}).ToList();

            }
            else
            {
                // Fetch activities from FomActivities without a specific ContractCode
                var activities = await _context.FomActivities
                    .AsNoTracking()
                    .ProjectTo<TblErpFomActivitiesDto>(_mapper.ConfigurationProvider)
                    .Where(e => request.DeptCodes.Contains(e.DeptCode))
                    .ToListAsync(cancellationToken);

                // Group activities by department with SelectCheckBox = false for all
                disciplines = activities
                    .GroupBy(a => a.DeptCode)
                    .Select(group => new DisciplineDto
                    {
                        DisciplineName = group.Key,
                        Activities = group.Select(a => new ActivityDto
                        {
                            DeptCode = a.DeptCode,
                            ActCode = a.ActCode,
                            SelectCheckBox = false
                        }).ToList()
                    }).ToList();
            }

            return disciplines;
        }


        //public async Task<List<DisciplineDto>> Handle(GetDeptActivitiesByDeptCodes request, CancellationToken cancellationToken)
        //{
        //    List<DisciplineDto> disciplines;

        //    var ContractCode = request.ContractCode;

        //    if (ContractCode !="null")
        //    {
        //        // Fetch activities without a contract code
        //        var activities = await _context.ContractDeptActivity
        //                        .AsNoTracking()
        //                        .Where(e => request.DeptCodes.Contains(e.DeptCode) && e.ContractCode == request.ContractCode)
        //                        .ToListAsync(cancellationToken);

        //        // Group activities by department to build disciplines
        //        disciplines = activities
        //            .GroupBy(a => new { a.DeptCode })
        //            .Select(group => new DisciplineDto
        //            {
        //                DisciplineName = group.Key.DeptCode, // Map to a more descriptive field if needed
        //                Activities = group.Select(a => new ActivityDto
        //                {
        //                    ActivityName = a.ActCode,
        //                    SelectCheckBox = true
        //                }).ToList()
        //            }).ToList();
        //    }
        //    else
        //    {

        //        // Fetch activities with a contract code
        //        var activities = await _context.FomActivities.AsNoTracking()
        //            .ProjectTo<TblErpFomActivitiesDto>(_mapper.ConfigurationProvider)
        //            .Where(e => request.DeptCodes.Contains(e.DeptCode))
        //            .ToListAsync(cancellationToken);

        //        // Group activities by department to build disciplines
        //        disciplines = activities
        //            .GroupBy(a => new { a.DeptCode }) // Ensure DeptName is available in TblErpFomActivitiesDto
        //            .Select(group => new DisciplineDto
        //            {
        //                DisciplineName = group.Key.DeptCode, // Map to a more descriptive field if needed
        //                Activities = group.Select(a => new ActivityDto
        //                {
        //                    ActivityName = a.ActName,
        //                    SelectCheckBox = false
        //                }).ToList()
        //            }).ToList();



        //    }

        //    return disciplines;
        //}
    }

    //public class GetDeptActivitiesByDeptCodesHandler : IRequestHandler<GetDeptActivitiesByDeptCodes, List<DisciplineDto>>
    //{
    //    private readonly CINDBOneContext _context;
    //    private readonly IMapper _mapper;

    //    public GetDeptActivitiesByDeptCodesHandler(CINDBOneContext context, IMapper mapper)
    //    {
    //        _context = context;
    //        _mapper = mapper;
    //    }

    //    public async Task<List<DisciplineDto>> Handle(GetDeptActivitiesByDeptCodes request, CancellationToken cancellationToken)
    //    {

    //        if(request.ContractCode != null) { 
    //        // Fetch and map the activities from the database
    //        var activities = await _context.FomActivities.AsNoTracking()
    //            .ProjectTo<TblErpFomActivitiesDto>(_mapper.ConfigurationProvider)
    //            .Where(e => request.DeptCodes.Contains(e.DeptCode))
    //            .ToListAsync(cancellationToken);

    //        // Group activities by department to build disciplines
    //        var disciplines = activities
    //            .GroupBy(a => new { a.DeptCode}) // Ensure DeptName is available in TblErpFomActivitiesDto
    //            .Select(group => new DisciplineDto
    //            {
    //                DisciplineName = group.Key.DeptCode,
    //                Activities = group.Select(a => new ActivityDto
    //                {
    //                    ActivityName = a.ActName,
    //                    SelectCheckBox = false
    //                }).ToList()
    //            }).ToList();

    //    //    return disciplines;

    //        }
    //        else
    //        {
    //            var activities = await _context.ContractDeptActivity.AsNoTracking()
    //            .Where(e => request.DeptCodes.Contains(e.DeptCode))
    //            .ToListAsync(cancellationToken);

    //            // Group activities by department to build disciplines
    //            var disciplines = activities
    //                .GroupBy(a => new { a.DeptCode }) // Ensure DeptName is available in TblErpFomActivitiesDto
    //                .Select(group => new DisciplineDto
    //                {
    //                    DisciplineName = group.Key.DeptCode,
    //                    Activities = group.Select(a => new ActivityDto
    //                    {
    //                        ActivityName = a.ActCode,
    //                        SelectCheckBox = false
    //                    }).ToList()
    //                }).ToList();

    //        }
    //        return disciplines;
    //    }
    //}
    #endregion

    // DTOs for returning the structured result


    public class CreateChkUnChkContDeptActivity : IRequest<int>
    {
        public UserIdentityDto User { get; set; }
        public List<TblErpFomContractDeptActDto> InputList { get; set; }
    }

    public class CreateChkUnChkContDeptActivityHandler : IRequestHandler<CreateChkUnChkContDeptActivity, int>
    {
        private readonly CINDBOneContext _context;

        public CreateChkUnChkContDeptActivityHandler(CINDBOneContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateChkUnChkContDeptActivity request, CancellationToken cancellationToken)
        {
            try
            {
                Log.Info("----Info Create/Update Check UnCheck Contract Discipline Activities method start----");

                foreach (var discipline in request.InputList)
                {
                    if (string.IsNullOrEmpty(discipline.DeptCode) || string.IsNullOrEmpty(discipline.ContractCode))
                        continue; // Skip invalid entries

                    // Remove existing activities for the same ContractCode and DeptCode
                    var existingActivities = await _context.ContractDeptActivity
                        .Where(e => e.ContractCode == discipline.ContractCode && e.DeptCode == discipline.DeptCode)
                        .ToListAsync(cancellationToken);

                    if (existingActivities.Any())
                    {
                        _context.ContractDeptActivity.RemoveRange(existingActivities);
                        await _context.SaveChangesAsync(cancellationToken);
                    }

                    // Add new activities
                    foreach (var activity in discipline.Activities)
                    {
                        var newActivity = new TblErpFomContractDeptAct
                        {
                            ContractId = discipline.ContractId,
                            ActivityId = activity.ActivityId,
                            ActCode = activity.ActCode,
                            DeptCode = discipline.DeptCode,
                            ContractCode = discipline.ContractCode
                        };

                        await _context.ContractDeptActivity.AddAsync(newActivity, cancellationToken);
                    }
                }

                await _context.SaveChangesAsync(cancellationToken);
                Log.Info("----Info Check UnCheck Contract Discipline Activities method exit----");

                return 1; // Return a success code
            }
            catch (Exception ex)
            {
                Log.Error("Error in Create/Update Check UnCheck Contract Discipline Activities method");
                Log.Error($"Error occurred at: {DateTime.UtcNow}");
                Log.Error($"Error message: {ex.Message}");
                Log.Error($"Error stack trace: {ex.StackTrace}");
                return 0; // Return a failure code
            }
        }
    }








    //#region  Create Chk UnChk ContDeptActivity

    //public class CreateChkUnChkContDeptActivity : IRequest<int>
    //{
    //    public UserIdentityDto User { get; set; }
    //    public TblErpFomContractDeptActDto Input { get; set; }
    //}

    ////public class CreateChkUnChkContDeptActivityHandler : IRequestHandler<CreateChkUnChkContDeptActivity, int>
    ////{
    ////    private readonly CINDBOneContext _context;
    ////    private readonly IMapper _mapper;

    ////    public CreateChkUnChkContDeptActivityHandler(CINDBOneContext context, IMapper mapper)
    ////    {
    ////        _context = context;
    ////        _mapper = mapper;
    ////    }

    ////    public async Task<int> Handle(CreateChkUnChkContDeptActivity request, CancellationToken cancellationToken)
    ////    {
    ////        try
    ////        {
    ////            Log.Info("----Info Create Update Check UnCheck Contract Descipline Activity  method start----");

    ////            var obj = request.Input;


    ////            TblErpFomContractDeptAct ChkUnchkActivity = new();
    ////            if (obj.Id > 0)
    ////                ChkUnchkActivity = await _context.ContractDeptActivity.AsNoTracking().FirstOrDefaultAsync(e => e.Id == obj.Id);



    ////            ChkUnchkActivity.ContractId = 0;
    ////            ChkUnchkActivity.ActivityId = 0;
    ////            ChkUnchkActivity.ActCode = obj.ActCode;
    ////            ChkUnchkActivity.DeptCode = obj.DeptCode;
    ////            ChkUnchkActivity.ContractCode = obj.ContractCode;
    ////            if (obj.Id > 0)
    ////            {
    ////                _context.ContractDeptActivity.Update(ChkUnchkActivity);
    ////            }
    ////            else
    ////            {


    ////                ChkUnchkActivity.ContractId = 0;
    ////                ChkUnchkActivity.ActivityId = 0;
    ////                ChkUnchkActivity.ActCode = obj.ActCode;
    ////                ChkUnchkActivity.DeptCode = obj.DeptCode;
    ////                ChkUnchkActivity.ContractCode = obj.ContractCode;

    ////                await _context.ContractDeptActivity.AddAsync(ChkUnchkActivity);
    ////            }

    ////            await _context.SaveChangesAsync();
    ////            Log.Info("----Info  Check UnCheck Contract Descipline Activity  method Exit----");
    ////            return ChkUnchkActivity.Id;
    ////        }
    ////        catch (Exception ex)
    ////        {
    ////            Log.Error("Error in Create Update Check UnCheck Contract Descipline Activity  Method");
    ////            Log.Error("Error occured time : " + DateTime.UtcNow);
    ////            Log.Error("Error message : " + ex.Message);
    ////            Log.Error("Error StackTrace : " + ex.StackTrace);
    ////            return 0;
    ////        }
    ////    }


    ////}
    //public class CreateChkUnChkContDeptActivityHandler : IRequestHandler<CreateChkUnChkContDeptActivity, int>
    //{
    //    private readonly CINDBOneContext _context;
    //    private readonly IMapper _mapper;

    //    public CreateChkUnChkContDeptActivityHandler(CINDBOneContext context, IMapper mapper)
    //    {
    //        _context = context;
    //        _mapper = mapper;
    //    }
    //    public async Task<int> Handle(CreateChkUnChkContDeptActivity request, CancellationToken cancellationToken)
    //    {
    //        try
    //        {
    //            Log.Info("----Info Create Update Check UnCheck Contract Discipline Activity method start----");

    //            var obj = request.Input;

    //            // Check if the record exists based on ContractCode, DeptCode, and ActCode
    //                var existingActivities = await _context.ContractDeptActivity
    //                                               .Where(e => e.ContractCode == obj.ContractCode && e.DeptCode == obj.DeptCode && obj.ActCode==obj.ActCode)
    //                                               .ToListAsync(cancellationToken);

    //            if (existingActivities.Any())
    //            {
    //                // Remove each matching record
    //                _context.ContractDeptActivity.RemoveRange(existingActivities);
    //                await _context.SaveChangesAsync(); // Save changes after deletion
    //            }

    //            // Insert the new record
    //            var newActivity = new TblErpFomContractDeptAct
    //            {
    //                ContractId = obj.ContractId,
    //                ActivityId = obj.ActivityId,
    //                ActCode = obj.ActCode,
    //                DeptCode = obj.DeptCode,
    //                ContractCode = obj.ContractCode
    //            };

    //            await _context.ContractDeptActivity.AddAsync(newActivity);
    //            await _context.SaveChangesAsync(); // Save changes after adding the new record

    //            Log.Info("----Info Check UnCheck Contract Discipline Activity method Exit----");
    //            return newActivity.Id;
    //        }
    //        catch (Exception ex)
    //        {
    //            Log.Error("Error in Create Update Check UnCheck Contract Discipline Activity Method");
    //            Log.Error("Error occurred time : " + DateTime.UtcNow);
    //            Log.Error("Error message : " + ex.Message);
    //            Log.Error("Error StackTrace : " + ex.StackTrace);
    //            return 0;
    //        }
    //    }

    //    //public async Task<int> Handle(CreateChkUnChkContDeptActivity request, CancellationToken cancellationToken)
    //    //{
    //    //    try
    //    //    {
    //    //        Log.Info("----Info Create Update Check UnCheck Contract Discipline Activity method start----");

    //    //        var obj = request.Input;

    //    //        // Check if the record exists based on ContractCode, DeptCode, and ActCode
    //    //        var ChkUnchkActivity = await _context.ContractDeptActivity
    //    //            .AsNoTracking()
    //    //            .FirstOrDefaultAsync(e => e.ContractCode == obj.ContractCode && e.DeptCode == obj.DeptCode && e.ActCode == obj.ActCode);

    //    //        if (ChkUnchkActivity != null)
    //    //        {
    //    //            // Update existing record
    //    //            ChkUnchkActivity.ContractId = obj.ContractId;
    //    //            ChkUnchkActivity.ActivityId = obj.ActivityId;
    //    //            ChkUnchkActivity.ActCode = obj.ActCode;
    //    //            ChkUnchkActivity.DeptCode = obj.DeptCode;
    //    //            ChkUnchkActivity.ContractCode = obj.ContractCode;

    //    //            _context.ContractDeptActivity.Update(ChkUnchkActivity);
    //    //        }
    //    //        else
    //    //        {
    //    //            // Insert new record
    //    //            ChkUnchkActivity = new TblErpFomContractDeptAct
    //    //            {
    //    //                ContractId = obj.ContractId,
    //    //                ActivityId = obj.ActivityId,
    //    //                ActCode = obj.ActCode,
    //    //                DeptCode = obj.DeptCode,
    //    //                ContractCode = obj.ContractCode
    //    //            };

    //    //            await _context.ContractDeptActivity.AddAsync(ChkUnchkActivity);
    //    //        }

    //    //        await _context.SaveChangesAsync();
    //    //        Log.Info("----Info Check UnCheck Contract Discipline Activity method Exit----");
    //    //        return ChkUnchkActivity.Id;
    //    //    }
    //    //    catch (Exception ex)
    //    //    {
    //    //        Log.Error("Error in Create Update Check UnCheck Contract Discipline Activity Method");
    //    //        Log.Error("Error occurred time : " + DateTime.UtcNow);
    //    //        Log.Error("Error message : " + ex.Message);
    //    //        Log.Error("Error StackTrace : " + ex.StackTrace);
    //    //        return 0;
    //    //    }
    //    //}
    //}



    //#endregion


}
