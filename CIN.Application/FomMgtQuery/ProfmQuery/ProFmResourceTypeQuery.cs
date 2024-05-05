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
    public class GetResourceTypesPaginationListQuery : IRequest<PaginatedList<TblErpFomResourceTypeDto>>
    {
        public UserIdentityDto User { get; set; }
        public PaginationFilterDto Input { get; set; }

    }

    public class GetResourceTypesPaginationListQueryHandler : IRequestHandler<GetResourceTypesPaginationListQuery, PaginatedList<TblErpFomResourceTypeDto>>
    {
        private readonly CINDBOneContext _context;
     
        private readonly IMapper _mapper;
        public GetResourceTypesPaginationListQueryHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedList<TblErpFomResourceTypeDto>> Handle(GetResourceTypesPaginationListQuery request, CancellationToken cancellationToken)
        {


            var list = await _context.FomResourceType.AsNoTracking().ProjectTo<TblErpFomResourceTypeDto>(_mapper.ConfigurationProvider)
                                    .PaginationListAsync(request.Input.Page, request.Input.PageCount, cancellationToken);

            return list;
        }


    }


    #endregion

    #region GetById

    public class GetResourceTypeByIdQuery : IRequest<TblErpFomResourceTypeDto>
    {
        public UserIdentityDto User { get; set; }
        public int Id { get; set; }
    }

    public class GetResourceTypeByIdQueryHandler : IRequestHandler<GetResourceTypeByIdQuery, TblErpFomResourceTypeDto>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetResourceTypeByIdQueryHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TblErpFomResourceTypeDto> Handle(GetResourceTypeByIdQuery request, CancellationToken cancellationToken)
        {

            var obj = await _context.FomResourceType.AsNoTracking().ProjectTo<TblErpFomResourceTypeDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(e => e.Id == request.Id);
            return obj;
        }
    }
    #endregion

    #region GetByCode

    public class GetResourceTypeByCodeQuery : IRequest<TblErpFomResourceTypeDto>
    {
        public UserIdentityDto User { get; set; }
        public string Code { get; set; }
    }

    public class GetResourceTypeByCodeQueryHandler : IRequestHandler<GetResourceTypeByCodeQuery, TblErpFomResourceTypeDto>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetResourceTypeByCodeQueryHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TblErpFomResourceTypeDto> Handle(GetResourceTypeByCodeQuery request, CancellationToken cancellationToken)
        {

            var obj = await _context.FomResourceType.AsNoTracking().ProjectTo<TblErpFomResourceTypeDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(e => e.ResTypeCode == request.Code);
            return obj;
        }
    }
    #endregion

    #region GetSelectList

    public class GetSelectResourceTypesQuery : IRequest<List<LanCustomSelectListItem>>
    {
        public UserIdentityDto User { get; set; }
    }

    public class GetSelectResourceTypesQueryHandler : IRequestHandler<GetSelectResourceTypesQuery, List<LanCustomSelectListItem>>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetSelectResourceTypesQueryHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<LanCustomSelectListItem>> Handle(GetSelectResourceTypesQuery request, CancellationToken cancellationToken)
        {
            var list = await _context.FomResourceType
                .Where(e => e.IsActive)
              .AsNoTracking()
             .Select(e => new LanCustomSelectListItem { Text = e.ResTypeCode, Value = e.Id.ToString(), TextTwo = e.ResTypeName, TextAr = e.ResTypeNameAr })
                .ToListAsync(cancellationToken);
            return list;
        }
    }
    #endregion

    #region Create_And_Update

    public class CreateUpdateResourceTypeQuery : IRequest<int>
    {
        public UserIdentityDto User { get; set; }
        public TblErpFomResourceTypeDto Input { get; set; }
    }

    public class CreateUpdateResourceTypeQueryHandler : IRequestHandler<CreateUpdateResourceTypeQuery, int>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;

        public CreateUpdateResourceTypeQueryHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateUpdateResourceTypeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Log.Info("----Info Create Update Profm Resource Type method start----");

                var obj = request.Input;


                TblErpFomResourceType ResourceType = new();
                if (obj.Id > 0)
                    ResourceType = await _context.FomResourceType.AsNoTracking().FirstOrDefaultAsync(e => e.Id == obj.Id);


               
                ResourceType.ResTypeName = obj.ResTypeName;
                ResourceType.ResTypeNameAr = obj.ResTypeNameAr;
                ResourceType.IsActive = obj.IsActive;
                if (obj.Id > 0)
                {
                    _context.FomResourceType.Update(ResourceType);
                }
                else
                {
                    ResourceType.ResTypeCode = obj.ResTypeCode;
                    if (_context.FomResourceType.Any(e => e.ResTypeCode == obj.ResTypeCode))
                    {
                        return -1;
                    }

                    ResourceType.CreatedBy = request.User.UserName;
                    ResourceType.CreatedOn = DateTime.UtcNow;
                   
                    await _context.FomResourceType.AddAsync(ResourceType);
                }

                await _context.SaveChangesAsync();
                Log.Info("----Info Create Update Profm ResourceType method Exit----");
                return ResourceType.Id;
            }
            catch (Exception ex)
            {
                Log.Error("Error in Create Update Profm ResourceType Method");
                Log.Error("Error occured time : " + DateTime.UtcNow);
                Log.Error("Error message : " + ex.Message);
                Log.Error("Error StackTrace : " + ex.StackTrace);
                return 0;
            }
        }


    }



    #endregion

    #region Delete
    public class DeleteResourceTypeQuery : IRequest<int>
    {
        public UserIdentityDto User { get; set; }
        public int Id { get; set; }
    }

    public class DeleteResourceTypeQueryHandler : IRequestHandler<DeleteResourceTypeQuery, int>
    {

        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;

        public DeleteResourceTypeQueryHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(DeleteResourceTypeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Log.Info("----Info Delete Profm ResourceType Start----");

                if (request.Id > 0)
                {

                    var ResourceType = await _context.FomResourceType.AsNoTracking().FirstOrDefaultAsync(e => e.Id == request.Id);
                    ResourceType.IsActive = false;
                    _context.FomResourceType.Update(ResourceType);
                    await _context.SaveChangesAsync();

                    return request.Id;
                }
                return 0;
            }
            catch (Exception ex)
            {

                Log.Error("Error in Delete Profm ResourceType");
                Log.Error("Error occured time : " + DateTime.UtcNow);
                Log.Error("Error message : " + ex.Message);
                Log.Error("Error StackTrace : " + ex.StackTrace);
                return 0;
            }

        }
    }

    #endregion
}
