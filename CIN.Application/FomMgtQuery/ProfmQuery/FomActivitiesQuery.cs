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
    public class GetFomActivitiesList : IRequest<PaginatedList<TblErpFomActivitiesDto>>
    {
        public UserIdentityDto User { get; set; }
        public PaginationFilterDto Input { get; set; }

    }

    public class GetFomActivitiesListHandler : IRequestHandler<GetFomActivitiesList, PaginatedList<TblErpFomActivitiesDto>>
    {
        private readonly CINDBOneContext _context;
        // private readonly DBContext _context;
        private readonly IMapper _mapper;
        public GetFomActivitiesListHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedList<TblErpFomActivitiesDto>> Handle(GetFomActivitiesList request, CancellationToken cancellationToken)
        {


            var list = await _context.FomActivities.AsNoTracking().ProjectTo<TblErpFomActivitiesDto>(_mapper.ConfigurationProvider)
                                    .PaginationListAsync(request.Input.Page, request.Input.PageCount, cancellationToken);

            return list;
        }


    }


    #endregion

    #region GetActivitiesByDeptCode
    public class GetActivitiesByDeptCode : IRequest<List<TblErpFomActivitiesDto>>
    {
        public UserIdentityDto User { get; set; }
        public string DeptCode { get; set; }

    }

    public class GetActivitiesByDeptCodeHandler : IRequestHandler<GetActivitiesByDeptCode, List<TblErpFomActivitiesDto>>
    {
        private readonly CINDBOneContext _context;
        // private readonly DBContext _context;
        private readonly IMapper _mapper;
        public GetActivitiesByDeptCodeHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<TblErpFomActivitiesDto>> Handle(GetActivitiesByDeptCode request, CancellationToken cancellationToken)
        {


            var list = await _context.FomActivities.AsNoTracking().ProjectTo<TblErpFomActivitiesDto>(_mapper.ConfigurationProvider).Where(e => e.DeptCode == request.DeptCode).ToListAsync();

            return list;
        }


    }


    #endregion

    #region GetAllByDeptCode
    public class GetFomActivitiesListByDeptCode : IRequest<PaginatedList<TblErpFomActivitiesDto>>
    {
        public UserIdentityDto User { get; set; }
        public PaginationFilterDto Input { get; set; }

    }

    public class GetFomActivitiesListByDeptCodeHandler : IRequestHandler<GetFomActivitiesListByDeptCode, PaginatedList<TblErpFomActivitiesDto>>
    {
        private readonly CINDBOneContext _context;
        // private readonly DBContext _context;
        private readonly IMapper _mapper;
        public GetFomActivitiesListByDeptCodeHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedList<TblErpFomActivitiesDto>> Handle(GetFomActivitiesListByDeptCode request, CancellationToken cancellationToken)
        {


            var list = await _context.FomActivities.AsNoTracking().ProjectTo<TblErpFomActivitiesDto>(_mapper.ConfigurationProvider).Where(e => e.DeptCode == request.Input.Query)
                                    .PaginationListAsync(request.Input.Page, request.Input.PageCount, cancellationToken);

            return list;
        }


    }


    #endregion

    #region GetById

    public class GetFomActivitiesById : IRequest<TblErpFomActivitiesDto>
    {
        public UserIdentityDto User { get; set; }
        public int Id { get; set; }
    }

    public class GetFomActivitiesByIdHandler : IRequestHandler<GetFomActivitiesById, TblErpFomActivitiesDto>
    {
        private readonly CINDBOneContext _context;
        // private readonly DBContext _context;
        private readonly IMapper _mapper;
        public GetFomActivitiesByIdHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TblErpFomActivitiesDto> Handle(GetFomActivitiesById request, CancellationToken cancellationToken)
        {

            TblErpFomActivities obj = new();
            var department = await _context.FomActivities.AsNoTracking().ProjectTo<TblErpFomActivitiesDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(e => e.Id == request.Id);
            return department;
            // throw new NotImplementedException();
        }
    }
    #endregion

    #region Create_And_Update

    public class CreateUpdateFomActivities : IRequest<int>
    {
        public UserIdentityDto User { get; set; }
        public TblErpFomActivitiesDto FomActivitiesDto { get; set; }
    }

    public class CreateUpdateFomActivitiesHandler : IRequestHandler<CreateUpdateFomActivities, int>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;

        public CreateUpdateFomActivitiesHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateUpdateFomActivities request, CancellationToken cancellationToken)
        {
            try
            {
                Log.Info("----Info Create Update  Fom Activities method start----");

                var obj = request.FomActivitiesDto;


                TblErpFomActivities FomActivities = new();
                if (obj.Id > 0)
                    FomActivities = await _context.FomActivities.AsNoTracking().FirstOrDefaultAsync(e => e.Id == obj.Id);


                FomActivities.Id = obj.Id;
                FomActivities.ActCode = obj.ActCode;
                FomActivities.DeptCode = obj.DeptCode;
                FomActivities.ActName = obj.ActName;
                FomActivities.ActNameAr = obj.ActNameAr;
                FomActivities.ThumbNailImage = obj.ThumbNailImage;
                FomActivities.IsB2B = obj.IsB2B;
                FomActivities.IsB2C = obj.IsB2C;
                FomActivities.IsActive = obj.IsActive;




                if (obj.Id > 0)
                {

                    _context.FomActivities.Update(FomActivities);
                }
                else
                {
                    FomActivities.CreatedOn = obj.CreatedOn;
                    FomActivities.CreatedBy = obj.CreatedBy;
                    await _context.FomActivities.AddAsync(FomActivities);
                }
                await _context.SaveChangesAsync();
                Log.Info("----Info Create Update Fom Activities method Exit----");
                return FomActivities.Id;
            }
            catch (Exception ex)
            {
                Log.Error("Error in Create Update Fom Activities Method");
                Log.Error("Error occured time : " + DateTime.UtcNow);
                Log.Error("Error message : " + ex.Message);
                Log.Error("Error StackTrace : " + ex.StackTrace);
                return 0;
            }
        }


    }



    #endregion

    #region UploadThumbnailFiles       

    public class UploadThumbnailFiles : IRequest<(bool, string)>
    {
        public UserIdentityDto User { get; set; }
        public InputImageThumbnailDto Input { get; set; }
        public string WebRoot { get; set; }
    }

    public class UploadThumbnailFilesHandler : IRequestHandler<UploadThumbnailFiles, (bool, string)>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;

        public UploadThumbnailFilesHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<(bool, string)> Handle(UploadThumbnailFiles request, CancellationToken cancellationToken)
        {
            try
            {
                var obj = request.Input;
                TblErpFomActivities activity = new();
                if (obj.Id > 0)
                    activity = await _context.FomActivities.AsNoTracking().FirstOrDefaultAsync(e => e.Id == obj.Id);

                if (request.Input.Image1IForm != null && request.Input.Image1IForm.Length > 0)
                {
                    var (res, fileName) = FileUploads.FileUploadWithIform(activity.ActCode, request.WebRoot, request.Input.Image1IForm);
                    if (res)
                    {
                        activity.ThumbNailImage = obj.WebRoot + fileName;
                    }
                }
             
                _context.FomActivities.Update(activity);
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
