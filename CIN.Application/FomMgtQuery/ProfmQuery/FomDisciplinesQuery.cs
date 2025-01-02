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
using CIN.Domain.OpeartionsMgt;
using CIN.Application.ProfmQuery;

namespace CIN.Application.FomMgtQuery
{
    #region GetAll
    public class GetFomDisciplinesList : IRequest<PaginatedList<TblErpFomDepartmentDto>>
    {
        public UserIdentityDto User { get; set; }
        public PaginationFilterDto Input { get; set; }

    }

    public class GetFomDisciplinesListHandler : IRequestHandler<GetFomDisciplinesList, PaginatedList<TblErpFomDepartmentDto>>
    {
        private readonly CINDBOneContext _context;
        // private readonly DBContext _context;
        private readonly IMapper _mapper;
        public GetFomDisciplinesListHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedList<TblErpFomDepartmentDto>> Handle(GetFomDisciplinesList request, CancellationToken cancellationToken)
        {


            var list = await _context.ErpFomDepartments.AsNoTracking().ProjectTo<TblErpFomDepartmentDto>(_mapper.ConfigurationProvider)
                                    .PaginationListAsync(request.Input.Page, request.Input.PageCount, cancellationToken);

            return list;
        }


    }


    #endregion

    #region GetById

    public class GetFomDisciplinesById : IRequest<TblErpFomDepartmentDto>
    {
        public UserIdentityDto User { get; set; }
        public int Id { get; set; }
    }

    public class GetFomDisciplinesByIdHandler : IRequestHandler<GetFomDisciplinesById, TblErpFomDepartmentDto>
    {
        private readonly CINDBOneContext _context;
        // private readonly DBContext _context;
        private readonly IMapper _mapper;
        public GetFomDisciplinesByIdHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TblErpFomDepartmentDto> Handle(GetFomDisciplinesById request, CancellationToken cancellationToken)
        {

            TblErpFomDepartment obj = new();
            var department = await _context.ErpFomDepartments.AsNoTracking().ProjectTo<TblErpFomDepartmentDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(e => e.Id == request.Id);
            return department;
            // throw new NotImplementedException();
        }
    }
    #endregion

    #region Create_And_Update

    public class CreateUpdateFomDisciplines : IRequest<int>
    {
        public UserIdentityDto User { get; set; }
        public ErpFomDepartmentDto FomDisciplinesDto { get; set; }
    }

    public class CreateUpdateFomDisciplinesHandler : IRequestHandler<CreateUpdateFomDisciplines, int>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;

        public CreateUpdateFomDisciplinesHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateUpdateFomDisciplines request, CancellationToken cancellationToken)
        {
            try
            {
                Log.Info("----Info Create Update  Fom Disciplines method start----");



                var obj = request.FomDisciplinesDto;


                TblErpFomDepartment FomDisciplines = new();
                if (obj.Id > 0)
                    FomDisciplines = await _context.ErpFomDepartments.AsNoTracking().FirstOrDefaultAsync(e => e.Id == obj.Id);


                FomDisciplines.Id = obj.Id;
                FomDisciplines.DeptCode = obj.DeptCode;
                FomDisciplines.DeptServType = obj.DeptServType;
                FomDisciplines.NameEng = obj.NameEng;
                FomDisciplines.NameArabic = obj.NameArabic;
                FomDisciplines.IsSheduleRequired1 = obj.IsSheduleRequired1;
                FomDisciplines.IsSheduleRequired2 = obj.IsSheduleRequired2;
                FomDisciplines.IsActive = obj.IsActive;
                FomDisciplines.ServiceTimePeriods = string.Join(",", obj.ServiceTimePeriods);
                if (!string.IsNullOrEmpty(obj.ThumbNailImage))
                    FomDisciplines.ThumbNailImage = obj.ThumbNailImage;


                if (obj.Id > 0)
                {

                    _context.ErpFomDepartments.Update(FomDisciplines);
                }
                else
                {
                    FomDisciplines.CreatedBy = obj.CreatedBy;
                    FomDisciplines.CreatedDate = DateTime.Now;
                    await _context.ErpFomDepartments.AddAsync(FomDisciplines);
                }
                await _context.SaveChangesAsync();
                Log.Info("----Info Create Update Fom Disciplines method Exit----");
                return FomDisciplines.Id;
            }
            catch (Exception ex)
            {
                Log.Error("Error in Create Update Fom Disciplines Method");
                Log.Error("Error occured time : " + DateTime.UtcNow);
                Log.Error("Error message : " + ex.Message);
                Log.Error("Error StackTrace : " + ex.StackTrace);
                return 0;
            }
        }


    }



    #endregion

    #region GetSelectList

    public class GetSelectTimePeriods : IRequest<List<LanCustomSelectListItem>>
    {
        public UserIdentityDto User { get; set; }
    }

    public class GetSelectTimePeriodsHandler : IRequestHandler<GetSelectTimePeriods, List<LanCustomSelectListItem>>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetSelectTimePeriodsHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<LanCustomSelectListItem>> Handle(GetSelectTimePeriods request, CancellationToken cancellationToken)
        {
            var list = await _context.FomPeriod
                .Where(e => e.IsActive)
              .AsNoTracking()
             .Select(e => new LanCustomSelectListItem { Text = e.Title, Value = e.Id.ToString(), TextTwo = e.TitleAr, TextAr = e.TitleAr })
                .ToListAsync(cancellationToken);
            return list;
        }
    }
    #endregion

    #region GetDepartmentSelectList


    public class GetDepartmentSelectList : IRequest<List<LanCustomSelectListItem>>
    {
        public UserIdentityDto User { get; set; }
    }

    public class GetDepartmentSelectListHandler : IRequestHandler<GetDepartmentSelectList, List<LanCustomSelectListItem>>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetDepartmentSelectListHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<LanCustomSelectListItem>> Handle(GetDepartmentSelectList request, CancellationToken cancellationToken)
        {
            var list = await _context.ErpFomDepartments
                .Where(e => e.IsActive)
              .AsNoTracking()
             .Select(e => new LanCustomSelectListItem { Text = e.NameEng, Value = e.DeptCode, TextAr = e.NameArabic })
                .ToListAsync(cancellationToken);
            return list;
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
                TblErpFomDepartment department = new();
                if (obj.Id > 0)
                    department = await _context.ErpFomDepartments.AsNoTracking().FirstOrDefaultAsync(e => e.Id == obj.Id);

                if (request.Input.Image1IForm != null && request.Input.Image1IForm.Length > 0)
                {
                    var (res, fileName) = FileUploads.FileUploadWithIform(department.DeptCode, request.WebRoot, request.Input.Image1IForm);
                    if (res)
                    {
                        department.ThumbNailImage = obj.WebRoot + fileName;
                    }
                }
                //if (request.Input.Image2IForm != null && request.Input.Image2IForm.Length > 0)
                //{
                //    var (res, fileName) = FileUploads.FileUploadWithIform(customer.CustCode, request.WebRoot, request.Input.Image2IForm);
                //    if (res)
                //    {
                //        customer.CustUDF2 = obj.WebRoot + fileName;
                //    }
                //}
                //if (request.Input.Image3IForm != null && request.Input.Image3IForm.Length > 0)
                //{
                //    var (res, fileName) = FileUploads.FileUploadWithIform(customer.CustCode, request.WebRoot, request.Input.Image3IForm);
                //    if (res)
                //    {
                //        customer.CustUDF3 = obj.WebRoot + fileName;
                //    }
                //}
                _context.ErpFomDepartments.Update(department);
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
