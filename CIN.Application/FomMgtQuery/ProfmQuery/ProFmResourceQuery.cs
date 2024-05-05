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
using CIN.Domain.FomMgt;
using CIN.Domain.InventorySetup;

namespace CIN.Application.ProfmQuery
{
    #region GetPaginationList
    public class GetResourcesPaginationListQuery : IRequest<PaginatedList<TblErpFomResourcesDto>>
    {
        public UserIdentityDto User { get; set; }
        public PaginationFilterDto Input { get; set; }

    }

    public class GetResourcesPaginationListQueryHandler : IRequestHandler<GetResourcesPaginationListQuery, PaginatedList<TblErpFomResourcesDto>>
    {
        private readonly CINDBOneContext _context;
        // private readonly DBContext _context;
        private readonly IMapper _mapper;
        public GetResourcesPaginationListQueryHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedList<TblErpFomResourcesDto>> Handle(GetResourcesPaginationListQuery request, CancellationToken cancellationToken)
        {


            var list = await _context.FomResources.AsNoTracking().ProjectTo<TblErpFomResourcesDto>(_mapper.ConfigurationProvider)
                                    .PaginationListAsync(request.Input.Page, request.Input.PageCount, cancellationToken);

            return list;
        }


    }


    #endregion

    #region GetById

    public class GetResourceByIdQuery : IRequest<TblErpFomResourcesDto>
    {
        public UserIdentityDto User { get; set; }
        public int Id { get; set; }
    }

    public class GetResourceByIdQueryHandler : IRequestHandler<GetResourceByIdQuery, TblErpFomResourcesDto>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetResourceByIdQueryHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TblErpFomResourcesDto> Handle(GetResourceByIdQuery request, CancellationToken cancellationToken)
        {

            var obj = await _context.FomResources.AsNoTracking().ProjectTo<TblErpFomResourcesDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(e => e.Id == request.Id);
            return obj;
        }
    }
    #endregion

    #region GetByCode

    public class GetResourceByCodeQuery : IRequest<TblErpFomResourcesDto>
    {
        public UserIdentityDto User { get; set; }
        public string Code { get; set; }
    }

    public class GetResourceByCodeQueryHandler : IRequestHandler<GetResourceByCodeQuery, TblErpFomResourcesDto>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetResourceByCodeQueryHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TblErpFomResourcesDto> Handle(GetResourceByCodeQuery request, CancellationToken cancellationToken)
        {

            var obj = await _context.FomResources.AsNoTracking().ProjectTo<TblErpFomResourcesDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(e => e.ResCode == request.Code);
            return obj;
        }
    }
    #endregion

    #region GetSelectList

    public class GetSelectResourcesQuery : IRequest<List<LanCustomSelectListItem>>
    {
        public UserIdentityDto User { get; set; }
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
            var list = await _context.FomResources
                .Where(e => e.IsActive)
              .AsNoTracking()
             .Select(e => new LanCustomSelectListItem { Text = e.ResCode, Value = e.Id.ToString(), TextTwo = e.NameEng, TextAr = e.NameAr })
                .ToListAsync(cancellationToken);
            return list;
        }
    }
    #endregion


    #region Get All Login Users List

    public class GetAllUsersList : IRequest<List<CustomSelectListItem>>
    {
        public UserIdentityDto User { get; set; }
    }

    public class GetAllUsersListHandler : IRequestHandler<GetAllUsersList, List<CustomSelectListItem>>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetAllUsersListHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<CustomSelectListItem>> Handle(GetAllUsersList request, CancellationToken cancellationToken)
        {

            //var branchId = request.User.BranchId;
            //var branchCode = await _context.CompanyBranches.Where(e => e.Id == branchId).Select(e => e.BranchCode).FirstOrDefaultAsync();
            return await _context.SystemLogins.AsNoTracking()//.Where(e => e.PrimaryBranch == branchCode)
                .Select(e => new CustomSelectListItem { Text = e.LoginId, Value = e.UserName })
                .ToListAsync(cancellationToken);
        }
    }

    #endregion




    #region Create_And_Update

    public class CreateUpdateResourceQuery : IRequest<int>
    {
        public UserIdentityDto User { get; set; }
        public ErpFomResourcesDto Input { get; set; }
    }

    public class CreateUpdateResourceQueryHandler : IRequestHandler<CreateUpdateResourceQuery, int>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;

        public CreateUpdateResourceQueryHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateUpdateResourceQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Log.Info("----Info Create Update Profm Company method start----");


                var obj = request.Input;


                TblErpFomResources Resource = new();
                if (obj.Id > 0)
                    Resource = await _context.FomResources.AsNoTracking().FirstOrDefaultAsync(e => e.Id == obj.Id);


               
                Resource.NameAr = obj.NameAr;
                Resource.NameEng = obj.NameEng;
                Resource.ResCode = obj.ResCode;
                Resource.ResTypeCode = obj.ResTypeCode;
                Resource.LoginUser = obj.LoginUser;

              
                //foreach (var item in DeptCodes)
                //{
                //    DeptCodes.Add(item);
                //}

                //for (int i = 0; i < obj.DeptCodes.Length; i++)
                //{
                //    Resource.DeptCodes = obj.DeptCodes[i];


                //}

                //for(int i = 0; i < obj.DeptCodes.Length; i++)
                //{
                //    stringArray = obj.DeptCodes[i] + ','  ;
                //}

                Resource.DeptCodes = string.Join(",", obj.DeptCodes);

               

                if (obj.Id > 0)
                {
                    Resource.CreatedBy = request.User.UserName;
                    Resource.CreatedOn = DateTime.UtcNow;
                    Resource.ApprovalAuth = obj.ApprovalAuth;
                    Resource.IsActive = obj.IsActive;
                    _context.FomResources.Update(Resource);
                }
                else
                {
                    if (_context.FomResources.Any(e => e.ResCode == obj.ResCode))
                    {
                        return -1;
                    }

                    Resource.CreatedBy = request.User.UserName;
                    Resource.CreatedOn = DateTime.UtcNow;
                    Resource.ApprovalAuth = obj.ApprovalAuth;
                    Resource.IsActive = obj.IsActive;
                    await _context.FomResources.AddAsync(Resource);
                }
                await _context.SaveChangesAsync();
                Log.Info("----Info Create Update Profm Resource method Exit----");
                return Resource.Id;
            }
            catch (Exception ex)
            {
                Log.Error("Error in Create Update Profm Resource Method");
                Log.Error("Error occured time : " + DateTime.UtcNow);
                Log.Error("Error message : " + ex.Message);
                Log.Error("Error StackTrace : " + ex.StackTrace);
                return 0;
            }
        }


    }



    #endregion

    #region Delete
    public class DeleteResourceQuery : IRequest<int>
    {
        public UserIdentityDto User { get; set; }
        public int Id { get; set; }
    }

    public class DeleteResourceQueryHandler : IRequestHandler<DeleteResourceQuery, int>
    {

        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;

        public DeleteResourceQueryHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(DeleteResourceQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Log.Info("----Info Delete Profm Resource Start----");

                if (request.Id > 0)
                {

                    var Resource = await _context.FomResources.AsNoTracking().FirstOrDefaultAsync(e => e.Id == request.Id);
                    Resource.IsActive = false;
                    _context.FomResources.Update(Resource);
                    await _context.SaveChangesAsync();

                    return request.Id;
                }
                return 0;
            }
            catch (Exception ex)
            {

                Log.Error("Error in Delete Profm Resource");
                Log.Error("Error occured time : " + DateTime.UtcNow);
                Log.Error("Error message : " + ex.Message);
                Log.Error("Error StackTrace : " + ex.StackTrace);
                return 0;
            }

        }
    }

    #endregion
}
