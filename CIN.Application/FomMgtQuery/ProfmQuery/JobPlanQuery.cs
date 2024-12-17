using AutoMapper;
using CIN.Application.Common;
using CIN.Application.FomMgtDtos;
using CIN.DB;
using CIN.Domain.FomMgt.AssetMaintenanceMgt;
using CIN.Domain.InvoiceSetup;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CIN.Application.FomMgtQuery.ProfmQuery
{
    #region GetAll
    public class GetFomJobPlanMasterList : IRequest<PaginatedList<TblErpFomJobPlanMasterDto>>
    {
        public UserIdentityDto User { get; set; }
        public PaginationFilterDto Input { get; set; }

    }

    public class GetFomJobPlanMasterListHandler : IRequestHandler<GetFomJobPlanMasterList, PaginatedList<TblErpFomJobPlanMasterDto>>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetFomJobPlanMasterListHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedList<TblErpFomJobPlanMasterDto>> Handle(GetFomJobPlanMasterList request, CancellationToken cancellationToken)
        {
            var search = request.Input;
            var list = _context.FomJobPlanMasters.Include(e => e.CustomerContract).AsNoTracking();

            if (search.Query.HasValue())
                list = list.Where(e => e.JobPlanCode.Contains(search.Query) || e.AssetCode.Contains(request.Input.Query));

            var filteredlist = await list
                .Select(e => new TblErpFomJobPlanMasterDto
                {
                    Id = e.Id,
                    JobPlanCode = e.JobPlanCode,
                    AssetCode = e.AssetCode,
                    DeptCode = e.DeptCode,
                    SectionCode = e.SectionCode,
                    ContractCode = e.ContractCode,
                    Customer = e.CustomerContract.CustCode,
                    Created = e.Created,
                    Approve = e.Approve,
                    IsClosed = e.IsClosed,
                    IsVoid = e.IsVoid,

                }).PaginationListAsync(request.Input.Page, request.Input.PageCount, cancellationToken);

            foreach (var item in filteredlist.Items)
            {
                var astMaster = await _context.FomAssetMasters.FirstOrDefaultAsync(e => e.AssetCode == item.AssetCode);
                var custMaster = await _context.OprCustomers.FirstOrDefaultAsync(e => e.CustCode == item.Customer);
                item.Location = astMaster?.Location ?? string.Empty;
                item.Customer = custMaster?.CustName ?? string.Empty;
            }
            return filteredlist;

        }


    }


    #endregion


    ////#region GenerateFomJobPlanCode

    ////public class GenerateFomJobPlanCode : IRequest<PaginatedList<TblErpFomJobPlanMasterDto>>
    ////{
    ////    public UserIdentityDto User { get; set; }
    ////    public PaginationFilterDto Input { get; set; }

    ////}

    ////public class GenerateFomJobPlanCodeHandler : IRequestHandler<GenerateFomJobPlanCode, PaginatedList<TblErpFomJobPlanMasterDto>>
    ////{
    ////    private readonly CINDBOneContext _context;
    ////    private readonly IMapper _mapper;
    ////    public GenerateFomJobPlanCodeHandler(CINDBOneContext context, IMapper mapper)
    ////    {
    ////        _context = context;
    ////        _mapper = mapper;
    ////    }
    ////    public async Task<PaginatedList<TblErpFomJobPlanMasterDto>> Handle(GenerateFomJobPlanCode request, CancellationToken cancellationToken)
    ////    {
    ////        var hasJobPlanCode = await _context.FomJobPlanMasters.AnyAsync(e => e.JobPlanCode == obj.JobPlanCode.Trim().Replace(" ", ""));
    ////        if (hasJobPlanCode)
    ////            return ApiMessageInfo.DuplicateInfo($"'{obj.JobPlanCode}'"); //{nameof(obj.SectionCode)}

    ////    }


    ////}


    ////#endregion




    #region GetFomJobPlanMasterById

    public class GetFomJobPlanMasterById : IRequest<TblErpFomJobPlanMasterDto>
    {
        public UserIdentityDto User { get; set; }
        public int Id { get; set; }

    }

    public class GetFomJobPlanMasterByIdHandler : IRequestHandler<GetFomJobPlanMasterById, TblErpFomJobPlanMasterDto>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetFomJobPlanMasterByIdHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<TblErpFomJobPlanMasterDto> Handle(GetFomJobPlanMasterById request, CancellationToken cancellationToken)
        {
            try
            {

                Log.Info("----Info GetFomJobPlanMasterById method start----");
                var obj = await _context.FomJobPlanMasters.AsNoTracking()
                    .Where(e => e.Id == request.Id)
                    .Select(e => new TblErpFomJobPlanMasterDto
                    {
                        JobPlanCode = e.JobPlanCode,
                        JobPlanDate = e.JobPlanDate,
                        AssetCode = e.AssetCode,
                        ContractCode = e.ContractCode,
                        ContStartDate = e.ContStartDate,
                        ContEndDate = e.ContEndDate,
                        Frequency = e.Frequency,
                        PreFixCode = e.PreFixCode,
                        DeptCode = e.DeptCode,
                        SectionCode = e.SectionCode,
                        PreparedBy = e.PreparedBy,
                        ApprovedBy = e.ApprovedBy,
                        PlanStartDate = e.PlanStartDate,
                        Remarks = e.Remarks,
                        NoJobPlanKpi = e.NoJobPlanKpi,
                        CanGenChildSch = e.CanGenChildSch,
                        ChildHasDiffFreq = e.ChildHasDiffFreq,
                    })
                    .FirstOrDefaultAsync();

                return obj;
            }
            catch (Exception ex)
            {

                throw;
            }
        }


    }


    #endregion


    #region GetFomJobPlanChildScheduleByJobCode

    public class GetFomJobPlanChildScheduleByJobCode : IRequest<List<CalculateDatesForFrequencySelectedDto>>
    {
        public UserIdentityDto User { get; set; }
        public int Id { get; set; }
        public string ChildCode { get; set; }

    }

    public class GetFomJobPlanChildScheduleByJobCodeHandler : IRequestHandler<GetFomJobPlanChildScheduleByJobCode, List<CalculateDatesForFrequencySelectedDto>>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetFomJobPlanChildScheduleByJobCodeHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<CalculateDatesForFrequencySelectedDto>> Handle(GetFomJobPlanChildScheduleByJobCode request, CancellationToken cancellationToken)
        {
            Log.Info("----Info GetFomJobPlanChildScheduleByJobCode method start----");
            var jobPlanCode = await _context.FomJobPlanMasters.AsNoTracking()
                    .Where(e => e.Id == request.Id)
                    .Select(e => e.JobPlanCode)
                    .FirstOrDefaultAsync();

            var schList = await _context.FomJobPlanChildSchedules.AsNoTracking()
                .Where(e => e.JobPlanCode == jobPlanCode && e.ChildCode == request.ChildCode).Select(e => new CalculateDatesForFrequencySelectedDto
                {
                    // Id = e.Id,
                    PlanStartDate = e.PlanStartDate,
                    Frequency = e.Frequency,
                    Remarks = e.Remarks,
                }).ToListAsync();

            return schList;
        }


    }


    #endregion


    #region GetFomJobPlanChildSchedulePrintByJobCode

    public class GetFomJobPlanChildSchedulePrintByJobCode : IRequest<List<TblErpFomJobPlanMasterDateScheduleDto>>
    {
        public UserIdentityDto User { get; set; }
        public string JobPlanCode { get; set; }

    }

    public class GetFomJobPlanChildSchedulePrintByJobCodeHandler : IRequestHandler<GetFomJobPlanChildSchedulePrintByJobCode, List<TblErpFomJobPlanMasterDateScheduleDto>>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetFomJobPlanChildSchedulePrintByJobCodeHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<TblErpFomJobPlanMasterDateScheduleDto>> Handle(GetFomJobPlanChildSchedulePrintByJobCode request, CancellationToken cancellationToken)
        {
            Log.Info("----Info GetFomJobPlanChildSchedulePrintByJobCode method start----");

            var schList = await _context.FomJobPlanChildSchedules.AsNoTracking()
                .Where(e => e.JobPlanCode == request.JobPlanCode).Select(e => new TblErpFomJobPlanMasterDateScheduleDto
                {
                    AssetCode = e.AssetCode,
                    ChildCode = e.ChildCode,
                    Frequency = e.Frequency,
                    Date = e.PlanStartDate,
                    Remarks = e.Remarks,
                }).ToListAsync();

            return schList;
        }


    }


    #endregion

    #region CreateUpdateFomJobPlanMaster For canGenChildSch = true

    public class CreateUpdateFomJobPlanMaster : IRequest<AppCtrollerDto>
    {
        public UserIdentityDto User { get; set; }
        public TblErpFomJobPlanMasterDto Input { get; set; }
    }

    public class CreateUpdateFomJobPlanMasterHandler : IRequestHandler<CreateUpdateFomJobPlanMaster, AppCtrollerDto>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;

        public CreateUpdateFomJobPlanMasterHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AppCtrollerDto> Handle(CreateUpdateFomJobPlanMaster request, CancellationToken cancellationToken)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    Log.Info("----Info CreateUpdateFomJobPlanMaster method start----");

                    var obj = request.Input;

                    TblErpFomJobPlanMaster jobPlanMst = await _context.FomJobPlanMasters.AsNoTracking().FirstOrDefaultAsync(e => e.Id == obj.Id) ?? new();

                    jobPlanMst.JobPlanDate = obj.JobPlanDate;
                    jobPlanMst.AssetCode = obj.AssetCode;
                    jobPlanMst.ContractCode = obj.ContractCode;
                    jobPlanMst.ContStartDate = obj.ContStartDate;
                    jobPlanMst.ContEndDate = obj.ContEndDate;
                    jobPlanMst.Frequency = obj.Frequency;
                    jobPlanMst.PreFixCode = obj.PreFixCode;
                    jobPlanMst.DeptCode = obj.DeptCode;
                    jobPlanMst.SectionCode = obj.SectionCode;
                    jobPlanMst.PreparedBy = obj.PreparedBy;
                    jobPlanMst.ApprovedBy = obj.ApprovedBy;
                    jobPlanMst.PlanStartDate = obj.PlanStartDate;
                    jobPlanMst.Remarks = obj.Remarks;
                    jobPlanMst.NoJobPlanKpi = obj.NoJobPlanKpi;
                    jobPlanMst.CanGenChildSch = obj.CanGenChildSch;
                    jobPlanMst.ChildHasDiffFreq = obj.ChildHasDiffFreq;
                    //jobPlanMst.Approve = obj.Approve;
                    jobPlanMst.IsActive = true;


                    var schedules = obj.JobPlanSchedules;

                    //For Updating Records
                    if (obj.Id > 0)
                    {
                        _context.FomJobPlanMasters.Update(jobPlanMst);
                        await _context.SaveChangesAsync();


                        if (schedules != null && schedules.Count > 0)
                        {
                            var groupedChilds = schedules.GroupBy(e => e.ChildCode).Select(e => e.Key).ToList();

                            var currentChilds = _context.FomJobPlanChildSchedules.Where(e => e.JobPlanCode == jobPlanMst.JobPlanCode && groupedChilds.Any(ch => ch == e.ChildCode));
                            if (currentChilds.Any())
                            {
                                _context.FomJobPlanChildSchedules.RemoveRange(currentChilds);
                                await _context.SaveChangesAsync();
                            }

                            List<TblErpFomJobPlanChildSchedule> ChildSchList = new();
                            foreach (var childSch in schedules)
                            {
                                ChildSchList.Add(new()
                                {
                                    JobPlanCode = jobPlanMst.JobPlanCode,
                                    AssetCode = jobPlanMst.AssetCode,
                                    ChildCode = childSch.ChildCode,
                                    Frequency = childSch.Frequency,
                                    PlanStartDate = childSch.Date,
                                    Remarks = childSch.Remarks,
                                });
                            }

                            if (ChildSchList.Count > 0)
                            {
                                await _context.FomJobPlanChildSchedules.AddRangeAsync(ChildSchList);
                                await _context.SaveChangesAsync();
                            }
                        }


                    }
                    else
                    {
                        int jobPlanSeq = 0;
                        var jobSeq = await _context.Sequences.FirstOrDefaultAsync();
                        if (jobSeq is null)
                        {
                            jobPlanSeq = 1;
                            TblSequenceNumberSetting setting = new();
                            setting.JobPlanNumber = jobPlanSeq;
                            await _context.Sequences.AddAsync(setting);
                        }
                        else
                        {
                            jobPlanSeq = jobSeq.JobPlanNumber + 1;
                            jobSeq.JobPlanNumber = jobPlanSeq;
                            _context.Sequences.Update(jobSeq);
                        }
                        await _context.SaveChangesAsync();

                        obj.JobPlanCode = $"J{jobPlanSeq}";

                        //var hasJobPlanCode = await _context.FomJobPlanMasters.AnyAsync(e => e.JobPlanCode == obj.JobPlanCode.Trim().Replace(" ", ""));
                        //if (hasJobPlanCode)
                        //    return ApiMessageInfo.DuplicateInfo($"'{obj.JobPlanCode}'"); //{nameof(obj.SectionCode)}

                        jobPlanMst.JobPlanCode = obj.JobPlanCode.Trim().Replace(" ", "").ToUpper();
                        jobPlanMst.Created = DateTime.Now;
                        jobPlanMst.CreatedBy = request.User.UserId;
                        await _context.FomJobPlanMasters.AddAsync(jobPlanMst);

                        await _context.SaveChangesAsync();

                        if (schedules != null && schedules.Count > 0)
                        {
                            List<TblErpFomJobPlanChildSchedule> ChildSchList = new();
                            foreach (var childSch in schedules)
                            {
                                ChildSchList.Add(new()
                                {
                                    JobPlanCode = jobPlanMst.JobPlanCode,
                                    AssetCode = jobPlanMst.AssetCode,
                                    ChildCode = childSch.ChildCode,
                                    Frequency = childSch.Frequency,
                                    PlanStartDate = childSch.Date,
                                    Remarks = childSch.Remarks,
                                });
                            }

                            if (ChildSchList.Count > 0)
                            {
                                await _context.FomJobPlanChildSchedules.AddRangeAsync(ChildSchList);
                                await _context.SaveChangesAsync();
                            }
                        }

                    }

                    Log.Info("----Info CreateUpdateFomJobPlanMaster method Exit----");
                    await transaction.CommitAsync();
                    return ApiMessageInfo.Status(1, jobPlanMst.Id);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Log.Error("Error in CreateUpdateFomJobPlanMaster Method");
                    Log.Error("Error occured time : " + DateTime.UtcNow);
                    Log.Error("Error message : " + ex.Message);
                    Log.Error("Error StackTrace : " + ex.StackTrace);
                    return ApiMessageInfo.Status(0);
                    //return ApiMessageInfo.Status(ex.Message + " " + ex.InnerException?.Message);
                }
            }
        }

    }



    #endregion



    #region CreateUpdateFomJobPlanMasterNoSchedules For canGenChildSch = false

    public class CreateUpdateFomJobPlanMasterNoSchedules : IRequest<AppCtrollerDto>
    {
        public UserIdentityDto User { get; set; }
        public TblErpFomJobPlanMasterDto Input { get; set; }
    }

    public class CreateUpdateFomJobPlanMasterNoSchedulesHandler : IRequestHandler<CreateUpdateFomJobPlanMasterNoSchedules, AppCtrollerDto>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;

        public CreateUpdateFomJobPlanMasterNoSchedulesHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AppCtrollerDto> Handle(CreateUpdateFomJobPlanMasterNoSchedules request, CancellationToken cancellationToken)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    Log.Info("----Info CreateUpdateFomJobPlanMasterNoSchedules method start----");

                    var obj = request.Input;

                    TblErpFomJobPlanMaster jobPlanMst = await _context.FomJobPlanMasters.AsNoTracking().FirstOrDefaultAsync(e => e.Id == obj.Id) ?? new();

                    jobPlanMst.JobPlanDate = obj.JobPlanDate;
                    jobPlanMst.AssetCode = obj.AssetCode;
                    jobPlanMst.ContractCode = obj.ContractCode;
                    jobPlanMst.ContStartDate = obj.ContStartDate;
                    jobPlanMst.ContEndDate = obj.ContEndDate;
                    jobPlanMst.Frequency = obj.Frequency;
                    jobPlanMst.PreFixCode = obj.PreFixCode;
                    jobPlanMst.DeptCode = obj.DeptCode;
                    jobPlanMst.SectionCode = obj.SectionCode;
                    jobPlanMst.PreparedBy = obj.PreparedBy;
                    jobPlanMst.ApprovedBy = obj.ApprovedBy;
                    jobPlanMst.PlanStartDate = obj.PlanStartDate;
                    jobPlanMst.Remarks = obj.Remarks;
                    jobPlanMst.NoJobPlanKpi = obj.NoJobPlanKpi;
                    jobPlanMst.CanGenChildSch = obj.CanGenChildSch;
                    jobPlanMst.ChildHasDiffFreq = obj.ChildHasDiffFreq;
                    //jobPlanMst.Approve = obj.Approve;
                    jobPlanMst.IsActive = true;


                    if (obj.Id > 0)
                    {
                        _context.FomJobPlanMasters.Update(jobPlanMst);
                    }
                    else
                    {
                        int jobPlanSeq = 0;
                        var jobSeq = await _context.Sequences.FirstOrDefaultAsync();
                        if (jobSeq is null)
                        {
                            jobPlanSeq = 1;
                            TblSequenceNumberSetting setting = new();
                            setting.JobPlanNumber = jobPlanSeq;
                            await _context.Sequences.AddAsync(setting);
                        }
                        else
                        {
                            jobPlanSeq = jobSeq.JobPlanNumber + 1;
                            jobSeq.JobPlanNumber = jobPlanSeq;
                            _context.Sequences.Update(jobSeq);
                        }
                        await _context.SaveChangesAsync();

                        obj.JobPlanCode = $"J{jobPlanSeq}";

                        //var hasJobPlanCode = await _context.FomJobPlanMasters.AnyAsync(e => e.JobPlanCode == obj.JobPlanCode.Trim().Replace(" ", ""));
                        //if (hasJobPlanCode)
                        //    return ApiMessageInfo.DuplicateInfo($"'{obj.JobPlanCode}'"); //{nameof(obj.SectionCode)}

                        jobPlanMst.JobPlanCode = obj.JobPlanCode.Trim().Replace(" ", "").ToUpper();
                        jobPlanMst.Created = DateTime.Now;
                        jobPlanMst.CreatedBy = request.User.UserId;
                        await _context.FomJobPlanMasters.AddAsync(jobPlanMst);
                    }

                    await _context.SaveChangesAsync();


                    var schedules = obj.JobPlanSchedules;

                    if (schedules != null && schedules.Count > 0)
                    {
                        var currentChilds = _context.FomJobPlanChildSchedules.Where(e => e.JobPlanCode == jobPlanMst.JobPlanCode);
                        if (currentChilds.Any())
                        {
                            _context.FomJobPlanChildSchedules.RemoveRange(currentChilds);
                            await _context.SaveChangesAsync();
                        }

                        List<TblErpFomJobPlanChildSchedule> ChildSchList = new();
                        foreach (var childSch in schedules)
                        {
                            ChildSchList.Add(new()
                            {
                                JobPlanCode = jobPlanMst.JobPlanCode,
                                AssetCode = jobPlanMst.AssetCode,
                                ChildCode = childSch.ChildCode,
                                Frequency = childSch.Frequency,
                                PlanStartDate = childSch.Date,
                                Remarks = childSch.Remarks,
                            });
                        }

                        if (ChildSchList.Count > 0)
                        {
                            await _context.FomJobPlanChildSchedules.AddRangeAsync(ChildSchList);
                            await _context.SaveChangesAsync();
                        }
                    }

                    Log.Info("----Info CreateUpdateFomJobPlanMasterNoSchedules method Exit----");
                    await transaction.CommitAsync();
                    return ApiMessageInfo.Status(1, jobPlanMst.Id);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Log.Error("Error in CreateUpdateFomJobPlanMasterNoSchedules Method");
                    Log.Error("Error occured time : " + DateTime.UtcNow);
                    Log.Error("Error message : " + ex.Message);
                    Log.Error("Error StackTrace : " + ex.StackTrace);
                    return ApiMessageInfo.Status(0);
                    //return ApiMessageInfo.Status(ex.Message + " " + ex.InnerException?.Message);
                }
            }
        }

    }



    #endregion


    #region ApproveJobMaster

    public class ApproveJobMaster : IRequest<AppCtrollerDto>
    {
        public UserIdentityDto User { get; set; }
        public TblErpFomJobPlanMasterApprovalDto Input { get; set; }
    }

    public class ApproveJobMasterQueryHandler : IRequestHandler<ApproveJobMaster, AppCtrollerDto>
    {
        //private readonly ICurrentUserService _currentUserService;
        //protected string UserId => _currentUserService.UserId;
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;

        public ApproveJobMasterQueryHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AppCtrollerDto> Handle(ApproveJobMaster request, CancellationToken cancellationToken)
        {
            try
            {
                Log.Info("----Info ApproveJobMaster method start----");
                var obj = request.Input;

                var jobPlan = await _context.FomJobPlanMasters.FirstOrDefaultAsync(e => e.Id == obj.Id);
                if (jobPlan is null)
                    return ApiMessageInfo.Status(2);

                if (obj.Type == "approve")
                    jobPlan.Approve = obj.Approve;
                else if (obj.Type == "closed")
                    jobPlan.IsClosed = obj.Approve;
                else if (obj.Type == "void")
                    jobPlan.IsVoid = obj.Approve;

                await _context.SaveChangesAsync();
                return ApiMessageInfo.Status(1, obj.Id);
            }
            catch (Exception ex)
            {
                Log.Error("Error in ApproveJobMaster Method");
                Log.Error("Error occured time : " + DateTime.UtcNow);
                Log.Error("Error message : " + ex.Message);
                Log.Error("Error StackTrace : " + ex.StackTrace);
                return ApiMessageInfo.Status(0);
            }

        }
    }

    #endregion


    #region AddJobPlanNotes

    public class AddJobPlanNotes : IRequest<AppCtrollerDto>
    {
        public UserIdentityDto User { get; set; }
        public TblErpFomJobPlanMessageLogDto Input { get; set; }
    }

    public class AddJobPlanNotesQueryHandler : IRequestHandler<AddJobPlanNotes, AppCtrollerDto>
    {
        //private readonly ICurrentUserService _currentUserService;
        //protected string UserId => _currentUserService.UserId;
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;

        public AddJobPlanNotesQueryHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AppCtrollerDto> Handle(AddJobPlanNotes request, CancellationToken cancellationToken)
        {
            try
            {
                Log.Info("----Info AddJobPlanNotes method start----");
                var obj = request.Input;
                TblErpFomJobPlanMessageLog MessageLog = new()
                {
                    JobPlanCode = obj.JobPlanCode,
                    Message = obj.Message,
                    MessageDate = obj.MessageDate,
                };

                await _context.FomJobPlanMessageLogs.AddAsync(MessageLog);
                await _context.SaveChangesAsync();

                return ApiMessageInfo.Status(1, MessageLog.Id);
            }
            catch (Exception ex)
            {
                Log.Error("Error in AddJobPlanNotes Method");
                Log.Error("Error occured time : " + DateTime.UtcNow);
                Log.Error("Error message : " + ex.Message);
                Log.Error("Error StackTrace : " + ex.StackTrace);
                return ApiMessageInfo.Status(0);
            }

        }
    }

    #endregion


    #region GetJobPlanNotesByJobCode


    public class GetJobPlanNotesByJobCode : IRequest<List<TblErpFomJobPlanMessageLogDto>>
    {
        public UserIdentityDto User { get; set; }
        public string JobPlanCode { get; set; }
    }

    public class GetJobPlanNotesByJobCodeQueryHandler : IRequestHandler<GetJobPlanNotesByJobCode, List<TblErpFomJobPlanMessageLogDto>>
    {
        //private readonly ICurrentUserService _currentUserService;
        //protected string UserId => _currentUserService.UserId;
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;

        public GetJobPlanNotesByJobCodeQueryHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TblErpFomJobPlanMessageLogDto>> Handle(GetJobPlanNotesByJobCode request, CancellationToken cancellationToken)
        {
            Log.Info("----Info GetJobPlanNotesByJobCode method start----");
            return await _context.FomJobPlanMessageLogs.Where(e => e.JobPlanCode == request.JobPlanCode).Select(e => new TblErpFomJobPlanMessageLogDto
            {
                Message = e.Message,
                MessageDate = e.MessageDate
            }).ToListAsync();
        }
    }

    #endregion

    #region AllJobPlanFomCalenderScheduleList
    public class AllJobPlanFomCalenderScheduleList : IRequest<List<CIN.Application.FomMobB2CDtos.FomScheduleDetailsDto>>
    {
        public UserIdentityDto User { get; set; }
        public string JobPlanCode { get; set; }
    }

    public class AllJobPlanFomCalenderScheduleListHandler : IRequestHandler<AllJobPlanFomCalenderScheduleList, List<CIN.Application.FomMobB2CDtos.FomScheduleDetailsDto>>
    {
        private readonly CINDBOneContext _context;
        // private readonly DBContext _context;
        private readonly IMapper _mapper;
        public AllJobPlanFomCalenderScheduleListHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<CIN.Application.FomMobB2CDtos.FomScheduleDetailsDto>> Handle(AllJobPlanFomCalenderScheduleList request, CancellationToken cancellationToken)
        {
            try
            {
                var startDate = DateTime.Now.AddMonths(-8);
                var endDate = DateTime.Now.AddDays(365);
                var schList = _context.FomJobPlanChildSchedules.AsQueryable();
                if (request.JobPlanCode.HasValue())
                    schList = schList.Where(e => e.JobPlanCode == request.JobPlanCode);

                var list = await schList.Include(e => e.JobPlanMaster)
                                                      .AsNoTracking()
                                                      .Where(e => e.PlanStartDate >= startDate && e.PlanStartDate <= endDate)
                                                      .Select(e => new CIN.Application.FomMobB2CDtos.FomScheduleDetailsDto
                                                      {

                                                          ContractCode = e.JobPlanMaster.ContractCode,
                                                          SchDate = e.PlanStartDate,
                                                          Department = e.JobPlanMaster.DeptCode,
                                                          Frequency = e.Frequency,
                                                          TranNumber = e.JobPlanCode,
                                                          ServiceItem = e.AssetCode,
                                                          SerType = e.ChildCode,
                                                          Remarks = e.Remarks,
                                                          //Time = e.Time.ToString(@"hh\:mm"),
                                                          //IsReschedule = e.IsReschedule,
                                                          //IsActive = e.IsActive

                                                      }).OrderBy(e => e.SchDate).ToListAsync(cancellationToken);

                var grpSchedules = from sch in list
                                   group sch by sch.ContractCode into g
                                   select g.Key;

                foreach (var schContractCode in grpSchedules)
                {
                    var contractDetails = await _context.FomCustomerContracts.AsNoTracking()
                                                  .Select(e => new { e.Id, e.CustCode, e.ContractCode })
                                                    .FirstOrDefaultAsync(e => e.ContractCode == schContractCode);

                    var custDetails = await _context.OprCustomers.AsNoTracking()
                                                    .Select(e => new { e.CustCode, e.CustName, e.CustArbName })
                                                    .FirstOrDefaultAsync(e => e.CustCode == contractDetails.CustCode);

                    list.Where(e => e.ContractCode == schContractCode).ToList().ForEach(e =>
                    {
                        e.ContractCode = contractDetails.ContractCode;
                        e.CustomerName = custDetails.CustName;
                        e.CustomerNameAr = custDetails.CustArbName;
                    });
                }

                return list;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }


    #endregion


    #region GetFomAssetJobPlanOrdersList

    public class GetFomAssetJobPlanOrdersList : IRequest<PaginatedList<TblErpFomJobPlanMasterDto>>
    {
        public UserIdentityDto User { get; set; }
        public PaginationFilterDto Input { get; set; }

    }

    public class GetFomAssetJobPlanOrdersListHandler : IRequestHandler<GetFomAssetJobPlanOrdersList, PaginatedList<TblErpFomJobPlanMasterDto>>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetFomAssetJobPlanOrdersListHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedList<TblErpFomJobPlanMasterDto>> Handle(GetFomAssetJobPlanOrdersList request, CancellationToken cancellationToken)
        {
            var search = request.Input;
            var list = _context.FomJobPlanMasters.Include(e => e.CustomerContract).AsNoTracking()
                .Where(e => e.Approve == true);

            if (search.Query.HasValue())
                list = list.Where(e => e.JobPlanCode.Contains(search.Query) || e.AssetCode.Contains(request.Input.Query));

            var filteredlist = await list
                .Select(e => new TblErpFomJobPlanMasterDto
                {
                    Id = e.Id,
                    JobPlanCode = e.JobPlanCode,
                    AssetCode = e.AssetCode,
                    DeptCode = e.DeptCode,
                    SectionCode = e.SectionCode,
                    ContractCode = e.ContractCode,
                    Customer = e.CustomerContract.CustCode,
                    Created = e.Created,
                    Approve = e.Approve,
                    IsClosed = e.IsClosed,
                    IsVoid = e.IsVoid,

                }).PaginationListAsync(request.Input.Page, request.Input.PageCount, cancellationToken);

            foreach (var item in filteredlist.Items)
            {
                var astMaster = await _context.FomAssetMasters.FirstOrDefaultAsync(e => e.AssetCode == item.AssetCode);
                var custMaster = await _context.OprCustomers.FirstOrDefaultAsync(e => e.CustCode == item.Customer);
                item.Location = astMaster?.Location ?? string.Empty;
                item.Customer = custMaster?.CustName ?? string.Empty;
            }
            return filteredlist;

        }


    }


    #endregion

    #region GetAssetjoborderchilditemsByJob

    public class GetAssetjoborderchilditemsByJob : IRequest<List<GetAssetjoborderchilditemsByJobDto>>
    {
        public UserIdentityDto User { get; set; }
        public string JobPlanCode { get; set; }
        public string Status { get; set; }
    }

    public class GetAssetjoborderchilditemsByJobHandler : IRequestHandler<GetAssetjoborderchilditemsByJob, List<GetAssetjoborderchilditemsByJobDto>>
    {
        private readonly CINDBOneContext _context;
        // private readonly DBContext _context;
        private readonly IMapper _mapper;
        public GetAssetjoborderchilditemsByJobHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<GetAssetjoborderchilditemsByJobDto>> Handle(GetAssetjoborderchilditemsByJob request, CancellationToken cancellationToken)
        {
            try
            {

                var list = await _context.FomJobPlanChildSchedules.Include(e => e.JobPlanMaster)
                    .AsNoTracking().Where(e => e.JobPlanCode == request.JobPlanCode).ToListAsync();

                //var groupedChildList = list.GroupBy(e => e.ChildCode).ToList().Select(e => new GetAssetjoborderchilditemsByJobDto
                //{
                //    ChildCode = e.Key,
                //    ChildItems = e.Select(item => new TblErpFomJobPlanMasterDateScheduleDto
                //    {
                //        AssetCode = item.AssetCode,
                //        ChildCode = item.ChildCode,
                //        Date = item.PlanStartDate,
                //        Frequency = item.Frequency,
                //        Remarks = item.Remarks,
                //        Id = item.Id
                //    }).ToList()
                //}).ToList();

                var groupedChildList = new List<GetAssetjoborderchilditemsByJobDto>();
                IEnumerable<TblErpFomJobPlanChildSchedule> cItems = null;

                foreach (var cItem in list.GroupBy(e => e.ChildCode).ToList())
                {
                    cItems = cItem;
                    if (request.Status == "P")
                        cItems = cItem.Where(e => e.IsClosed == false);
                    else if (request.Status == "C")
                        cItems = cItem.Where(e => e.IsClosed == true);

                    groupedChildList.Add(new()
                    {
                        ChildCode = cItem.Key,
                        ChildItems = cItems.Select(item => new TblErpFomJobPlanMasterDateScheduleDto
                        {
                            AssetCode = item.AssetCode,
                            ChildCode = item.ChildCode,
                            Date = item.PlanStartDate,
                            Frequency = item.Frequency,
                            Remarks = item.Remarks,
                            IsClosed = item.IsClosed,
                            Id = item.Id
                        }).ToList()
                    });
                }

                return groupedChildList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }


    }


    #endregion


    #region CreateAssetClosingInfo

    public class CreateAssetClosingInfo : IRequest<AppCtrollerDto>
    {
        public UserIdentityDto User { get; set; }
        public TblErpFomJobPlanScheduleClosureDto Input { get; set; }
    }

    public class CreateAssetClosingInfoQueryHandler : IRequestHandler<CreateAssetClosingInfo, AppCtrollerDto>
    {
        //private readonly ICurrentUserService _currentUserService;
        //protected string UserId => _currentUserService.UserId;
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;

        public CreateAssetClosingInfoQueryHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AppCtrollerDto> Handle(CreateAssetClosingInfo request, CancellationToken cancellationToken)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    Log.Info("----Info CreateAssetClosingInfo method start----");
                    var obj = request.Input;

                    if (await _context.FomJobPlanChildSchedules.AnyAsync(e => e.Id == obj.ChildScheduleId && e.IsClosed == true))
                        return ApiMessageInfo.Status($"Already Closed");

                    var closingDate = DateTime.Now;
                    TblErpFomJobPlanScheduleClosure Closure = new()
                    {
                        ChildScheduleId = obj.ChildScheduleId,
                        AssetCode = obj.AssetCode,
                        JobPlanCode = obj.JobPlanCode,
                        ChildCode = obj.ChildCode,
                        Frequency = obj.Frequency,
                        ClosedBy = obj.ClosedBy,
                        Remarks = obj.Remarks,
                        ClosingDate = closingDate,
                    };

                    await _context.FomJobPlanScheduleClosures.AddAsync(Closure);
                    await _context.SaveChangesAsync();

                    List<TblErpFomJobPlanScheduleClosureItem> ClosureItems = new();

                    void InserItemsIntoClosures(List<TblErpFomJobPlanScheduleClosureItemDto> items)
                    {
                        foreach (var item in items)
                        {
                            ClosureItems.Add(new() { Description = item.Description, Quantity = item.Quantity, Hours = item.Hours, Source = item.Source, CreatedDate = DateTime.Now, ScheduleClosureId = Closure.Id });
                        }
                    }

                    var materials = obj.Materials;
                    var tools = obj.Tools;
                    var laborHours = obj.LaborHours;

                    InserItemsIntoClosures(materials);
                    InserItemsIntoClosures(tools);
                    InserItemsIntoClosures(laborHours);

                    if (ClosureItems.Count > 0)
                    {
                        await _context.FomJobPlanScheduleClosureItems.AddRangeAsync(ClosureItems);
                        await _context.SaveChangesAsync();
                    }

                    var childSchedule = await _context.FomJobPlanChildSchedules.FirstOrDefaultAsync(e => e.Id == obj.ChildScheduleId);
                    childSchedule.IsClosed = true;
                    childSchedule.ClosedBy = obj.ClosedBy;
                    childSchedule.ClosedDate = closingDate;

                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();
                    return ApiMessageInfo.Status(1, Closure.Id);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Log.Error("Error in CreateAssetClosingInfo Method");
                    Log.Error("Error occured time : " + DateTime.UtcNow);
                    Log.Error("Error message : " + ex.Message);
                    Log.Error("Error StackTrace : " + ex.StackTrace);
                    return ApiMessageInfo.Status(0);
                }
            }
        }
    }

    #endregion

    #region Delete
    public class DeleteJobMaster : IRequest<int>
    {
        public UserIdentityDto User { get; set; }
        public int Id { get; set; }
    }

    public class DeleteJobMasterQueryHandler : IRequestHandler<DeleteJobMaster, int>
    {
        //private readonly ICurrentUserService _currentUserService;
        //protected string UserId => _currentUserService.UserId;
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;

        public DeleteJobMasterQueryHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(DeleteJobMaster request, CancellationToken cancellationToken)
        {
            try
            {
                Log.Info("----Info delte method start----");
                Log.Info("----Info delete method end----");

                if (request.Id > 0)
                {
                    var jobPlan = await _context.FomJobPlanMasters.FirstOrDefaultAsync(e => e.Id == request.Id);
                    var jobMasterSches = _context.FomJobPlanChildSchedules.Where(e => e.JobPlanCode == jobPlan.JobPlanCode);
                    if (await jobMasterSches.AnyAsync())
                        _context.RemoveRange(jobMasterSches);

                    _context.Remove(jobPlan);

                    await _context.SaveChangesAsync();
                    return request.Id;
                }
                return 0;
            }
            catch (Exception ex)
            {
                Log.Error("Error in delete Method");
                Log.Error("Error occured time : " + DateTime.UtcNow);
                Log.Error("Error message : " + ex.Message);
                Log.Error("Error StackTrace : " + ex.StackTrace);
                return 0;
            }

        }
    }

    #endregion


}
