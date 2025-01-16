using AutoMapper;
using AutoMapper.QueryableExtensions;
using CIN.Application.Common;
using CIN.Application.FomMgtDtos;
using CIN.DB;
using CIN.Domain.FomMgt;
using CIN.Domain.FomMgt.AssetMaintenanceMgt;
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
    public class GetFomAssetMasterList : IRequest<PaginatedList<TblErpFomAssetMasterDto>>
    {
        public UserIdentityDto User { get; set; }
        public PaginationFilterDto Input { get; set; }

    }

    public class GetFomAssetMasterListHandler : IRequestHandler<GetFomAssetMasterList, PaginatedList<TblErpFomAssetMasterDto>>
    {
        private readonly CINDBOneContext _context;
        // private readonly DBContext _context;
        private readonly IMapper _mapper;
        public GetFomAssetMasterListHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedList<TblErpFomAssetMasterDto>> Handle(GetFomAssetMasterList request, CancellationToken cancellationToken)
        {
            var search = request.Input;
            var list = _context.FomAssetMasters.AsNoTracking();

            if (search.Query.HasValue())
                list = list.Where(e => e.AssetCode.Contains(search.Query) || e.Name.Contains(search.Query) || e.NameAr.Contains(request.Input.Query));

            var filteredlist = await list
                .Select(e => new TblErpFomAssetMasterDto
                {
                    Id = e.Id,
                    AssetCode = e.AssetCode,
                    Name = e.Name,
                    NameAr = e.NameAr,
                    ContractCode = e.ContractCode,
                    DeptCode = e.DeptCode,
                    SectionCode = e.SectionCode,
                    Created = e.Created,
                    IsActive = e.IsActive,

                }).PaginationListAsync(request.Input.Page, request.Input.PageCount, cancellationToken);
            return filteredlist;

        }


    }


    #endregion


    #region GetFomAssetMasterById
    public class GetFomAssetMasterById : IRequest<TblErpFomAssetMasterDto>
    {
        public UserIdentityDto User { get; set; }
        public int Id { get; set; }

    }

    public class GetFomAssetMasterByIdHandler : IRequestHandler<GetFomAssetMasterById, TblErpFomAssetMasterDto>
    {
        private readonly CINDBOneContext _context;
        // private readonly DBContext _context;
        private readonly IMapper _mapper;
        public GetFomAssetMasterByIdHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<TblErpFomAssetMasterDto> Handle(GetFomAssetMasterById request, CancellationToken cancellationToken)
        {
            var assetMaster = await _context.FomAssetMasters.AsNoTracking().Where(e => e.Id == request.Id)
                                    .Select(e => new TblErpFomAssetMasterDto
                                    {
                                        Id = e.Id,
                                        AssetCode = e.AssetCode,
                                        Name = e.Name,
                                        NameAr = e.NameAr,
                                        Description = e.Description,
                                        SectionCode = e.SectionCode,
                                        DeptCode = e.DeptCode,
                                        ContractCode = e.ContractCode,
                                        Location = e.Location,
                                        Classification = e.Classification,
                                        RouteGroup = e.RouteGroup,
                                        JobPlan = e.JobPlan,
                                        HasChild = e.HasChild,
                                        IsActive = e.IsActive,
                                        IsWrittenOff = e.IsWrittenOff ?? false,
                                        InstallDate = e.InstallDate,
                                        ReplacementDate = e.ReplacementDate,
                                        AssetScale = e.AssetScale ?? 0,
                                    })
                                    .FirstOrDefaultAsync();

            assetMaster.AssetTasks = await _context.FomAssetMasterTasks.Where(e => e.AssetCode == assetMaster.AssetCode)
                .Select(e => new TblErpFomAssetMasterTaskDto
                {
                    ActCode = e.ActCode,
                    ResTypeCode = e.ResTypeCode,
                })
                .ToListAsync();

            assetMaster.AssetChilds = await _context.FomAssetMasterChilds.Where(e => e.AssetCode == assetMaster.AssetCode)
               .Select(e => new TblErpFomAssetMasterChildDto
               {
                   ChildCode = e.ChildCode,
                   Name = e.Name,
               })
               .ToListAsync();

            return assetMaster;
        }
    }


    #endregion


    #region GetFomAssetMasterTaskByAsset

    public class GetFomAssetMasterTaskByAsset : IRequest<TblErpFomAssetMasterDto>
    {
        public UserIdentityDto User { get; set; }
        public string AssetCode { get; set; }

    }

    public class GetFomAssetMasterTaskByAssetHandler : IRequestHandler<GetFomAssetMasterTaskByAsset, TblErpFomAssetMasterDto>
    {
        private readonly CINDBOneContext _context;
        // private readonly DBContext _context;
        private readonly IMapper _mapper;
        public GetFomAssetMasterTaskByAssetHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<TblErpFomAssetMasterDto> Handle(GetFomAssetMasterTaskByAsset request, CancellationToken cancellationToken)
        {
            var assetMaster = new TblErpFomAssetMasterDto();
            assetMaster.AssetTasks = await _context.FomAssetMasterTasks.Where(e => e.AssetCode == request.AssetCode)
                .Select(e => new TblErpFomAssetMasterTaskDto
                {
                    ActCode = e.ActCode,
                    ResTypeCode = e.ResTypeCode,
                })
                .ToListAsync();
            return assetMaster;
        }
    }


    #endregion

    #region GetFomAssetMasterByAssetCode

    public class GetFomAssetMasterByAssetCode : IRequest<GetFomAssetMasterByAssetCodeDto>
    {
        public UserIdentityDto User { get; set; }
        public string AssetCode { get; set; }
    }

    public class GetFomAssetMasterByAssetCodeHandler : IRequestHandler<GetFomAssetMasterByAssetCode, GetFomAssetMasterByAssetCodeDto>
    {
        private readonly CINDBOneContext _context;
        // private readonly DBContext _context;
        private readonly IMapper _mapper;
        public GetFomAssetMasterByAssetCodeHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<GetFomAssetMasterByAssetCodeDto> Handle(GetFomAssetMasterByAssetCode request, CancellationToken cancellationToken)
        {
            var assetMaster = await _context.FomAssetMasters.AsNoTracking().Where(e => e.AssetCode == request.AssetCode)
                                    .Select(e => new GetFomAssetMasterByAssetCodeDto
                                    {
                                        SectionCode = e.SectionCode,
                                        DeptCode = e.DeptCode,
                                        ContractCode = e.ContractCode,
                                    })
                                    .FirstOrDefaultAsync();

            assetMaster.HasChild = await _context.FomAssetMasterChilds.AsNoTracking().AnyAsync(e => e.AssetCode == request.AssetCode);


            var contract = await _context.FomCustomerContracts.AsNoTracking().Where(c => c.ContractCode == assetMaster.ContractCode)
                .Select(e => new { e.ContStartDate, e.ContEndDate })
                .FirstOrDefaultAsync();

            assetMaster.ContStartDate = contract.ContStartDate;
            assetMaster.ContEndDate = contract.ContEndDate;

            return assetMaster;
        }
    }

    #endregion


    #region GetFomAssetMasterChildsByAssetCode

    public class GetFomAssetMasterChildsByAssetCode : IRequest<List<CustomSelectListItem>>
    {
        public UserIdentityDto User { get; set; }
        public string AssetCode { get; set; }
        public int Id { get; set; }
    }

    public class GetFomAssetMasterChildsByAssetCodeHandler : IRequestHandler<GetFomAssetMasterChildsByAssetCode, List<CustomSelectListItem>>
    {
        private readonly CINDBOneContext _context;
        // private readonly DBContext _context;
        private readonly IMapper _mapper;
        public GetFomAssetMasterChildsByAssetCodeHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<CustomSelectListItem>> Handle(GetFomAssetMasterChildsByAssetCode request, CancellationToken cancellationToken)
        {
            var assetMaster = await _context.FomAssetMasterChilds.AsNoTracking().Where(e => e.AssetCode == request.AssetCode)
                                    .Select(e => new CustomSelectListItem
                                    {
                                        Text = e.AssetCode,
                                        TextTwo = e.ChildCode,
                                    })
                                    .ToListAsync();



            if (request.Id > 0)
            {
                var jobPlanCode = await _context.FomJobPlanMasters.Where(e => e.Id == request.Id).Select(e => e.JobPlanCode).FirstOrDefaultAsync();
                foreach (var item in assetMaster)
                {

                    item.Value = await _context.FomJobPlanChildSchedules.Where(e => e.JobPlanCode == jobPlanCode && e.ChildCode == item.TextTwo).Select(e => e.Frequency).FirstOrDefaultAsync();
                }
            }
            return assetMaster;
        }
    }


    #endregion

    #region GetFomAssetMasterSelectList
    public class GetFomAssetMasterSelectList : IRequest<List<LanCustomSelectListItem>>
    {
        public UserIdentityDto User { get; set; }
    }

    public class GetFomAssetMasterSelectListHandler : IRequestHandler<GetFomAssetMasterSelectList, List<LanCustomSelectListItem>>
    {
        private readonly CINDBOneContext _context;
        // private readonly DBContext _context;
        private readonly IMapper _mapper;
        public GetFomAssetMasterSelectListHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<LanCustomSelectListItem>> Handle(GetFomAssetMasterSelectList request, CancellationToken cancellationToken)
        {
            var assetMasters = await _context.FomAssetMasters.AsNoTracking()
                                    .Where(e => e.IsActive)
                                    .Select(e => new LanCustomSelectListItem
                                    {
                                        Text = e.Name,
                                        TextAr = e.NameAr,
                                        Value = e.AssetCode,
                                    })
                                    .ToListAsync();
            return assetMasters;
        }
    }


    #endregion

    #region CreateUpdateFomAssetMaster

    public class CreateUpdateFomAssetMaster : IRequest<AppCtrollerDto>
    {
        public UserIdentityDto User { get; set; }
        public TblErpFomAssetMasterDto Input { get; set; }
    }

    public class CreateUpdateFomAssetMasterHandler : IRequestHandler<CreateUpdateFomAssetMaster, AppCtrollerDto>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;

        public CreateUpdateFomAssetMasterHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AppCtrollerDto> Handle(CreateUpdateFomAssetMaster request, CancellationToken cancellationToken)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    Log.Info("----Info CreateUpdateFomAssetMaster method start----");

                    var obj = request.Input;

                    TblErpFomAssetMaster assetMst = await _context.FomAssetMasters.AsNoTracking().FirstOrDefaultAsync(e => e.Id == obj.Id) ?? new();

                    assetMst.Name = obj.Name;
                    assetMst.NameAr = obj.NameAr;
                    assetMst.Description = obj.Description;
                    assetMst.SectionCode = obj.SectionCode;
                    assetMst.DeptCode = obj.DeptCode;
                    assetMst.ContractCode = obj.ContractCode;
                    assetMst.Location = obj.Location;
                    assetMst.Classification = obj.Classification;
                    assetMst.RouteGroup = obj.RouteGroup;
                    assetMst.JobPlan = obj.JobPlan;
                    assetMst.HasChild = obj.HasChild;
                    assetMst.IsActive = obj.IsActive;
                    assetMst.InstallDate = obj.InstallDate;
                    assetMst.ReplacementDate = obj.ReplacementDate;
                    assetMst.IsWrittenOff = obj.IsWrittenOff;
                    assetMst.AssetScale = obj.AssetScale;

                    if (obj.Id > 0)
                    {
                        _context.FomAssetMasters.Update(assetMst);
                    }
                    else
                    {
                        var hasAssetCode = await _context.FomAssetMasters.AnyAsync(e => e.AssetCode == obj.AssetCode.Trim().Replace(" ", ""));
                        if (hasAssetCode)
                            return ApiMessageInfo.DuplicateInfo($"'{obj.AssetCode}'"); //{nameof(obj.SectionCode)}

                        assetMst.AssetCode = obj.AssetCode.Trim().Replace(" ", "").ToUpper();
                        assetMst.Created = DateTime.Now;
                        assetMst.CreatedBy = request.User.UserId;
                        await _context.FomAssetMasters.AddAsync(assetMst);
                    }
                    await _context.SaveChangesAsync();


                    var childs = obj.AssetChilds;
                    var tasks = obj.AssetTasks;

                    if (obj.HasChild && childs != null && childs.Count > 0)
                    {
                        var currentChilds = _context.FomAssetMasterChilds.Where(e => e.AssetCode == assetMst.AssetCode);
                        if (currentChilds.Any())
                        {
                            _context.FomAssetMasterChilds.RemoveRange(currentChilds);
                            await _context.SaveChangesAsync();
                        }

                        List<TblErpFomAssetMasterChild> ChildList = new();
                        foreach (var child in childs)
                        {
                            ChildList.Add(new()
                            {
                                AssetCode = assetMst.AssetCode,
                                ChildCode = child.ChildCode,
                                Name = child.Name,
                            });
                        }

                        if (ChildList.Count > 0)
                        {
                            await _context.FomAssetMasterChilds.AddRangeAsync(ChildList);
                            await _context.SaveChangesAsync();
                        }
                    }

                    if (tasks != null && tasks.Count > 0)
                    {
                        var currentTasks = _context.FomAssetMasterTasks.Where(e => e.AssetCode == assetMst.AssetCode);
                        if (currentTasks.Any())
                        {
                            _context.FomAssetMasterTasks.RemoveRange(currentTasks);
                            await _context.SaveChangesAsync();
                        }

                        List<TblErpFomAssetMasterTask> TaskList = new();
                        foreach (var task in tasks)
                        {
                            TaskList.Add(new()
                            {
                                AssetCode = assetMst.AssetCode,
                                ActCode = task.ActCode,
                                ResTypeCode = task.ResTypeCode,
                            });
                        }

                        if (TaskList.Count > 0)
                        {
                            await _context.FomAssetMasterTasks.AddRangeAsync(TaskList);
                            await _context.SaveChangesAsync();
                        }
                    }

                    Log.Info("----Info CreateUpdateFomAssetMaster method Exit----");
                    await transaction.CommitAsync();
                    return ApiMessageInfo.Status(1, assetMst.Id);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Log.Error("Error in CreateUpdateFomAssetMaster Method");
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


    #region ImportExcelFomAssetMaster

    public class ImportExcelFomAssetMaster : IRequest<AppCtrollerDto>
    {
        public UserIdentityDto User { get; set; }
        public List<TblErpFomAssetMasterDto> Input { get; set; }
    }

    public class ImportExcelFomAssetMasterHandler : IRequestHandler<ImportExcelFomAssetMaster, AppCtrollerDto>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;

        public ImportExcelFomAssetMasterHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AppCtrollerDto> Handle(ImportExcelFomAssetMaster request, CancellationToken cancellationToken)
        {
            int savedCount = 0, duplicateCount = 0;

            var sectionCodes = request.Input.Select(e => e.SectionCode).Distinct().ToList();
            var departmentCodes = request.Input.Select(e => e.DeptCode).Distinct().ToList();
            var custContractCodes = request.Input.Select(e => e.ContractCode).Distinct().ToList();

            var sectionCodeList = sectionCodes.Except(_context.FomSections.AsNoTracking().Select(e => e.SectionCode).AsEnumerable());
            var departmentList = departmentCodes.Except(_context.ErpFomDepartments.AsNoTracking().Select(e => e.DeptCode).AsEnumerable());
            var custContractList = custContractCodes.Except(_context.FomCustomerContracts.AsNoTracking().Select(e => e.ContractCode).AsEnumerable());

            if (sectionCodeList != null && sectionCodeList.Count() > 0)
                return ApiMessageInfo.Status($"Wrong SectionCodes:  {string.Join(", ", sectionCodeList)}");
            if (departmentList != null && departmentList.Count() > 0)
                return ApiMessageInfo.Status($"Wrong DeptCodes: {string.Join(", ", departmentList)}");
            if (custContractList != null && custContractList.Count() > 0)
                return ApiMessageInfo.Status($"Wrong ContractCodes: {string.Join(", ", custContractList)}");

            foreach (var obj in request.Input)
            {
                var hasAssetCode = await _context.FomAssetMasters.AnyAsync(e => e.AssetCode == obj.AssetCode.Trim().Replace(" ", ""));
                if (!hasAssetCode)
                {
                    using (var transaction = await _context.Database.BeginTransactionAsync())
                    {
                        try
                        {
                            Log.Info("----Info ImportExcelFomAssetMaster method start----");

                            TblErpFomAssetMaster assetMst = new()
                            {
                                AssetCode = obj.AssetCode.Trim().Replace(" ", "").ToUpper(),
                                Name = obj.Name,
                                NameAr = obj.NameAr,
                                Description = obj.Description,
                                SectionCode = obj.SectionCode,
                                DeptCode = obj.DeptCode,
                                ContractCode = obj.ContractCode,
                                Location = obj.Location,
                                Classification = obj.Classification,
                                RouteGroup = obj.RouteGroup,
                                JobPlan = obj.JobPlan,
                                HasChild = obj.HasChild,
                                IsActive = true,
                                IsWrittenOff = obj.IsWrittenOff,
                                AssetScale = obj.AssetScale,
                                Created = DateTime.Now,
                                CreatedBy = request.User.UserId
                            };


                            await _context.FomAssetMasters.AddAsync(assetMst);
                            await _context.SaveChangesAsync();


                            var childs = obj.AssetChilds;

                            if (obj.HasChild && childs != null && childs.Count > 0)
                            {
                                var currentChilds = _context.FomAssetMasterChilds.Where(e => e.AssetCode == assetMst.AssetCode);
                                if (currentChilds.Any())
                                {
                                    _context.FomAssetMasterChilds.RemoveRange(currentChilds);
                                    await _context.SaveChangesAsync();
                                }

                                List<TblErpFomAssetMasterChild> ChildList = new();
                                foreach (var child in childs)
                                {
                                    ChildList.Add(new()
                                    {
                                        AssetCode = assetMst.AssetCode,
                                        ChildCode = child.ChildCode,
                                        Name = child.Name,
                                    });
                                }

                                if (ChildList.Count > 0)
                                {
                                    await _context.FomAssetMasterChilds.AddRangeAsync(ChildList);
                                    await _context.SaveChangesAsync();
                                }
                            }

                            Log.Info("----Info ImportExcelFomAssetMaster method Exit----");
                            await transaction.CommitAsync();
                            savedCount = savedCount + 1;
                        }
                        catch (Exception ex)
                        {
                            await transaction.RollbackAsync();
                            Log.Error("Error in ImportExcelFomAssetMaster Method");
                            Log.Error("Error occured time : " + DateTime.UtcNow);
                            Log.Error("Error message : " + ex.Message);
                            Log.Error("Error StackTrace : " + ex.StackTrace);
                            //return ApiMessageInfo.Status(0);
                            //return ApiMessageInfo.Status(ex.Message + " " + ex.InnerException?.Message);
                        }
                    }
                }
                else
                    duplicateCount++;
            }

            if (savedCount > 0)
                return ApiMessageInfo.Status(1, savedCount);
            if (duplicateCount == request.Input.Count)
                return ApiMessageInfo.Status(ApiMessageInfo.Duplicate("Data"));
            else
                return ApiMessageInfo.Status(0);

        }

    }



    #endregion


    #region BulkImportExcelFomAssetMaster

    public class BulkImportExcelFomAssetMaster : IRequest<AppCtrollerDto>
    {
        public UserIdentityDto User { get; set; }
        public List<TblErpFomAssetMasterDto> Input { get; set; }
    }

    public class BulkImportExcelFomAssetMasterHandler : IRequestHandler<BulkImportExcelFomAssetMaster, AppCtrollerDto>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;

        public BulkImportExcelFomAssetMasterHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AppCtrollerDto> Handle(BulkImportExcelFomAssetMaster request, CancellationToken cancellationToken)
        {
            int savedCount = 0;
            bool hasduplicateCount = false;
            var assetList = request.Input;

            var fomAssetCodes = _context.FomAssetMasters.Select(e => e.AssetCode);
            var assetMasters = assetList.Where(ast => !fomAssetCodes.Any(assetCode => assetCode == ast.AssetCode.Trim().Replace(" ", "")));
            int assetMastersCount = assetMasters.Count();
            if (assetMastersCount > 0)
            {
                using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        Log.Info("----Info BulkImportExcelFomAssetMaster method start----");

                        List<TblErpFomAssetMaster> FomAssetMasters = assetMasters.Select(obj => new TblErpFomAssetMaster()
                        {
                            AssetCode = obj.AssetCode.Trim().Replace(" ", "").ToUpper(),
                            Name = obj.Name,
                            NameAr = obj.NameAr,
                            Description = obj.Description,
                            SectionCode = obj.SectionCode,
                            DeptCode = obj.DeptCode,
                            ContractCode = obj.ContractCode,
                            Location = obj.Location,
                            Classification = obj.Classification,
                            RouteGroup = obj.RouteGroup,
                            JobPlan = obj.JobPlan,
                            HasChild = obj.HasChild,
                            IsActive = true,
                            IsWrittenOff = obj.IsWrittenOff,
                            AssetScale = obj.AssetScale,
                            Created = DateTime.Now,
                            CreatedBy = request.User.UserId
                        }).ToList();


                        await _context.FomAssetMasters.AddRangeAsync(FomAssetMasters);
                        await _context.SaveChangesAsync();


                        var childs = assetMasters.Where(e => e.HasChild).SelectMany(e => e.AssetChilds);

                        if (childs != null && childs.Count() > 0)
                        {
                            List<TblErpFomAssetMasterChild> ChildList = childs.Select(child => new TblErpFomAssetMasterChild()
                            {
                                AssetCode = child.AssetCode,
                                ChildCode = child.ChildCode,
                                Name = child.Name,
                            }).ToList();

                            if (ChildList.Count > 0)
                            {
                                await _context.FomAssetMasterChilds.AddRangeAsync(ChildList);
                                await _context.SaveChangesAsync();
                            }
                        }

                        Log.Info("----Info BulkImportExcelFomAssetMaster method Exit----");
                        await transaction.CommitAsync();
                        savedCount = assetMastersCount;
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        Log.Error("Error in BulkImportExcelFomAssetMaster Method");
                        Log.Error("Error occured time : " + DateTime.UtcNow);
                        Log.Error("Error message : " + ex.Message);
                        Log.Error("Error StackTrace : " + ex.StackTrace);
                        return ApiMessageInfo.Status(0);
                        //return ApiMessageInfo.Status(ex.Message + " " + ex.InnerException?.Message);
                    }
                }
            }
            else
                hasduplicateCount = true;

            if (savedCount > 0)
                return ApiMessageInfo.Status(1, savedCount);
            if (hasduplicateCount)
                return ApiMessageInfo.Status(ApiMessageInfo.Duplicate("Data"));
            else
                return ApiMessageInfo.Status(0);

        }

    }



    #endregion


    #region Delete
    public class DeleteAssetMaster : IRequest<int>
    {
        public UserIdentityDto User { get; set; }
        public int Id { get; set; }
    }

    public class DeleteAssetMasterQueryHandler : IRequestHandler<DeleteAssetMaster, int>
    {
        //private readonly ICurrentUserService _currentUserService;
        //protected string UserId => _currentUserService.UserId;
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;

        public DeleteAssetMasterQueryHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(DeleteAssetMaster request, CancellationToken cancellationToken)
        {
            try
            {
                Log.Info("----Info delte method start----");
                Log.Info("----Info delete method end----");

                if (request.Id > 0)
                {
                    var Section = await _context.FomAssetMasters.FirstOrDefaultAsync(e => e.Id == request.Id);
                    _context.Remove(Section);
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
