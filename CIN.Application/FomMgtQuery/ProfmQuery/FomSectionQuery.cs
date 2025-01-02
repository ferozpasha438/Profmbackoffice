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
using static System.Collections.Specialized.BitVector32;

namespace CIN.Application.FomMgtQuery.ProfmQuery
{
    #region GetAll
    public class GetFomSectionList : IRequest<PaginatedList<TblErpFomSectionDto>>
    {
        public UserIdentityDto User { get; set; }
        public PaginationFilterDto Input { get; set; }

    }

    public class GetFomSectionListHandler : IRequestHandler<GetFomSectionList, PaginatedList<TblErpFomSectionDto>>
    {
        private readonly CINDBOneContext _context;
        // private readonly DBContext _context;
        private readonly IMapper _mapper;
        public GetFomSectionListHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedList<TblErpFomSectionDto>> Handle(GetFomSectionList request, CancellationToken cancellationToken)
        {
            var search = request.Input;
            var list = _context.FomSections.AsNoTracking();

            if (search.Query.HasValue())
                list = list.Where(e => e.Name.Contains(search.Query) || e.NameAr.Contains(request.Input.Query));

            var filteredlist = await list.ProjectTo<TblErpFomSectionDto>(_mapper.ConfigurationProvider)
                                    .PaginationListAsync(request.Input.Page, request.Input.PageCount, cancellationToken);
            return filteredlist;

            //return new(new List<TblErpFomSectionDto>() { new() {  SectionCode = "code", Name = "tet", NameAr = "tet_ar", Description = "desc",
            // Created = DateTime.Now} }, 1, 1, 1);
        }


    }


    #endregion


    #region GetSelectList
    public class GetSectiomSelectList : IRequest<List<LanCustomSelectListItem>>
    {
        public UserIdentityDto User { get; set; }
        public bool? IsForAssetMgt { get; set; }
    }

    public class GetSectiomSelectListHandler : IRequestHandler<GetSectiomSelectList, List<LanCustomSelectListItem>>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetSectiomSelectListHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<LanCustomSelectListItem>> Handle(GetSectiomSelectList request, CancellationToken cancellationToken)
        {
            var list = _context.FomSections
                .Where(e => e.IsActive)
              .AsNoTracking();
            if (request.IsForAssetMgt is not null && request.IsForAssetMgt == true)
                list = list.Where(e => e.ForAssetMgt == true);

            var newList = await list.Select(e => new LanCustomSelectListItem { Text = e.Name, TextAr = e.NameAr, Value = e.SectionCode })
                .ToListAsync(cancellationToken);
            return newList;
        }
    }
    #endregion

    #region GetFomSectionById
    public class GetFomSectionById : IRequest<TblErpFomSectionDto>
    {
        public UserIdentityDto User { get; set; }
        public int Id { get; set; }

    }

    public class GetFomSectionByIdHandler : IRequestHandler<GetFomSectionById, TblErpFomSectionDto>
    {
        private readonly CINDBOneContext _context;
        // private readonly DBContext _context;
        private readonly IMapper _mapper;
        public GetFomSectionByIdHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<TblErpFomSectionDto> Handle(GetFomSectionById request, CancellationToken cancellationToken)
        {
            return await _context.FomSections.AsNoTracking().Where(e => e.Id == request.Id)
                                     .ProjectTo<TblErpFomSectionDto>(_mapper.ConfigurationProvider)
                                    .FirstOrDefaultAsync();
        }
    }


    #endregion

    #region CreateUpdateFomSection

    public class CreateUpdateFomSection : IRequest<AppCtrollerDto>
    {
        public UserIdentityDto User { get; set; }
        public TblErpFomSectionDto Input { get; set; }
    }

    public class CreateUpdateFomSectionHandler : IRequestHandler<CreateUpdateFomSection, AppCtrollerDto>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;

        public CreateUpdateFomSectionHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AppCtrollerDto> Handle(CreateUpdateFomSection request, CancellationToken cancellationToken)
        {
            try
            {
                Log.Info("----Info CreateUpdateFomSection method start----");

                var obj = request.Input;

                TblErpFomSection section = await _context.FomSections.AsNoTracking().FirstOrDefaultAsync(e => e.Id == obj.Id) ?? new();

                section.Name = obj.Name;
                section.NameAr = obj.NameAr;
                section.Description = obj.Description;
                section.ForAssetMgt = obj.ForAssetMgt;
                section.IsActive = obj.IsActive;

                if (obj.Id > 0)
                {
                    _context.FomSections.Update(section);
                }
                else
                {
                    var hasSectionCode = await _context.FomSections.AnyAsync(e => e.SectionCode == obj.SectionCode.Trim());
                    if (hasSectionCode)
                        return ApiMessageInfo.DuplicateInfo($"'{obj.SectionCode}'"); //{nameof(obj.SectionCode)}

                    section.SectionCode = obj.SectionCode.Trim().Replace(" ", "").ToUpper();
                    section.Created = DateTime.Now;
                    section.CreatedBy = request.User.UserId;
                    await _context.FomSections.AddAsync(section);
                }
                await _context.SaveChangesAsync();
                Log.Info("----Info CreateUpdateFomSection method Exit----");
                return ApiMessageInfo.Status(1, section.Id);
            }
            catch (Exception ex)
            {
                Log.Error("Error in CreateUpdateFomSection Method");
                Log.Error("Error occured time : " + DateTime.UtcNow);
                Log.Error("Error message : " + ex.Message);
                Log.Error("Error StackTrace : " + ex.StackTrace);
                return ApiMessageInfo.Status(0);
            }
        }

    }



    #endregion

    #region Delete
    public class DeleteSection : IRequest<int>
    {
        public UserIdentityDto User { get; set; }
        public int Id { get; set; }
    }

    public class DeleteSectionQueryHandler : IRequestHandler<DeleteSection, int>
    {
        //private readonly ICurrentUserService _currentUserService;
        //protected string UserId => _currentUserService.UserId;
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;

        public DeleteSectionQueryHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(DeleteSection request, CancellationToken cancellationToken)
        {
            try
            {
                Log.Info("----Info delte method start----");
                Log.Info("----Info delete method end----");

                if (request.Id > 0)
                {
                    var Section = await _context.FomSections.FirstOrDefaultAsync(e => e.Id == request.Id);
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
