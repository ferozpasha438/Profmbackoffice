//using AutoMapper;
//using PROFM.Application;
//using PROFM.Application.Common;
//using PROFM.Application.ProfmAdminDtos;
//using PROFM.DB;
//using MediatR;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using AutoMapper.QueryableExtensions;
//using System.Linq.Dynamic.Core;
//using PROFM.Domain.ProfmMgt;

//namespace PROFM.Application.ProfmQuery
//{
//    #region GetAll
//    public class GetFomSysCompanyList : IRequest<PaginatedList<TblErpFomSysCompanyDto>>
//    {
//        public UserIdentityDto User { get; set; }
//        public PaginationFilterDto Input { get; set; }

//    }

//    public class GetFomSysCompanyListHandler : IRequestHandler<GetFomSysCompanyList, PaginatedList<TblErpFomSysCompanyDto>>
//    {
//        private readonly DBContext _context;
//        // private readonly DBContext _context;
//        private readonly IMapper _mapper;
//        public GetFomSysCompanyListHandler(DBContext context, IMapper mapper)
//        {
//            _context = context;
//            _mapper = mapper;
//        }
//        public async Task<PaginatedList<TblErpFomSysCompanyDto>> Handle(GetFomSysCompanyList request, CancellationToken cancellationToken)
//        {


//            var list = await _context.ErpFomSysCompany.AsNoTracking().ProjectTo<TblErpFomSysCompanyDto>(_mapper.ConfigurationProvider)
//                                    .PaginationListAsync(request.Input.Page, request.Input.PageCount, cancellationToken);

//            return list;
//        }


//    }


//    #endregion

//    #region GetById

//    public class GetFomSysCompanyById : IRequest<TblErpFomSysCompanyDto>
//    {
//        public UserIdentityDto User { get; set; }
//        public int Id { get; set; }
//    }

//    public class GetFomSysCompanyByIdHandler : IRequestHandler<GetFomSysCompanyById, TblErpFomSysCompanyDto>
//    {
//        private readonly DBContext _context;
//        // private readonly DBContext _context;
//        private readonly IMapper _mapper;
//        public GetFomSysCompanyByIdHandler(DBContext context, IMapper mapper)
//        {
//            _context = context;
//            _mapper = mapper;
//        }

//        public async Task<TblErpFomSysCompanyDto> Handle(GetFomSysCompanyById request, CancellationToken cancellationToken)
//        {

//            TblErpFomSysCompany obj = new();
//            var profmComapany = await _context.ErpFomSysCompany.AsNoTracking().ProjectTo<TblErpFomSysCompanyDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(e => e.Id == request.Id);
//            return profmComapany;
//            // throw new NotImplementedException();
//        }
//    }
//    #endregion

//    #region Create_And_Update
    
//    public class CreateUpdateFomCompany : IRequest<int>
//    {
//        public UserIdentityDto User { get; set; }
//        public TblErpFomSysCompanyDto FomSysCompanyDto { get; set; }
//    }

//    public class CreateUpdateFomCompanyHandler : IRequestHandler<CreateUpdateFomCompany, int>
//    {
//        private readonly DBContext _context;
//        private readonly IMapper _mapper;

//        public CreateUpdateFomCompanyHandler(DBContext context, IMapper mapper)
//        {
//            _context = context;
//            _mapper = mapper;
//        }

//        public async Task<int> Handle(CreateUpdateFomCompany request, CancellationToken cancellationToken)
//        {
//            try
//            {
//                Log.Info("----Info Create Update Fom Company method start----");

//                var obj = request.FomSysCompanyDto;


//                TblErpFomSysCompany FomCompany = new();
//                if (obj.Id > 0)
//                    FomCompany = await _context.ErpFomSysCompany.AsNoTracking().FirstOrDefaultAsync(e => e.Id == obj.Id);


//                FomCompany.Id = obj.Id;
//                FomCompany.CompanyName = obj.CompanyName;
//                FomCompany.CompanyNameAr = obj.CompanyNameAr;
//                FomCompany.CompanyAddress = obj.CompanyAddress;
//                FomCompany.CompanyAddressAr = obj.CompanyAddressAr;
//                FomCompany.Phone = obj.Phone;
//                FomCompany.Email = obj.Email;
//                FomCompany.VATNumber = obj.VATNumber;
//                FomCompany.DateFormat = obj.DateFormat;
//                FomCompany.GeoLocLatitude = obj.GeoLocLatitude;
//                FomCompany.GeoLocLongitude = obj.GeoLocLongitude;
//                FomCompany.LogoURL = obj.LogoURL;
//                //ProfmCompany.PriceDecimal = obj.PriceDecimal;
//                // ProfmCompany.QuantityDecimal = obj.QuantityDecimal;
//                FomCompany.City = obj.City;
//                FomCompany.State = obj.State;
//                FomCompany.Country = obj.Country;
//                FomCompany.Mobile = obj.Mobile;
//                FomCompany.Website = obj.Website;
//                FomCompany.LogoImagePath = obj.LogoImagePath;
//                FomCompany.CrNumber = obj.CrNumber;
//                FomCompany.CcNumber = obj.CcNumber;
//                FomCompany.IsActive = obj.IsActive;
//                FomCompany.CreatedOn = DateTime.Now;
//                FomCompany.CreatedBy = request.User.UserName;


//                if (obj.Id > 0)
//                {

//                    _context.ErpFomSysCompany.Update(FomCompany);
//                }
//                else
//                {

//                    await _context.ErpFomSysCompany.AddAsync(FomCompany);
//                }
//                await _context.SaveChangesAsync();
//                Log.Info("----Info Create Update Profm Company method Exit----");
//                return FomCompany.Id;
//            }
//            catch (Exception ex)
//            {
//                Log.Error("Error in Create Update Profm Company Method");
//                Log.Error("Error occured time : " + DateTime.UtcNow);
//                Log.Error("Error message : " + ex.Message);
//                Log.Error("Error StackTrace : " + ex.StackTrace);
//                return 0;
//            }
//        }


//    }



//    #endregion

//    #region SingleItem

//    public class GetCompany : IRequest<TblErpFomSysCompanyDto>
//    {
//        public UserIdentityDto User { get; set; }
//        public int Id { get; set; }
//        public string Code { get; set; }
//    }

//    public class GetCompanyHandler : IRequestHandler<GetCompany, TblErpFomSysCompanyDto>
//    {
//        private readonly DBContext _context;
//        private readonly IMapper _mapper;
//        public GetCompanyHandler(DBContext context, IMapper mapper)
//        {
//            _context = context;
//            _mapper = mapper;
//        }
//        public async Task<TblErpFomSysCompanyDto> Handle(GetCompany request, CancellationToken cancellationToken)
//        {
//            TblErpFomSysCompanyDto obj = new();
//            try
//            {
//                obj = await _context.ErpfomSysCompanyBranch.AsNoTracking()
//                            .Where(e => e.Id == request.Id)
//                       .ProjectTo<TblErpFomSysCompanyDto>(_mapper.ConfigurationProvider)
//                          .FirstOrDefaultAsync(cancellationToken);

                
//            }
//            catch (Exception ex)
//            {
//            }
//            return obj;
//        }
//    }

//    #endregion


//    #region Delete
//    public class DeleteFomComapny : IRequest<int>
//    {
//        public UserIdentityDto User { get; set; }
//        public int Id { get; set; }
//    }

//    public class DeleteFomComapnyHandler : IRequestHandler<DeleteFomComapny, int>
//    {

//        private readonly DBContext _context;
//        private readonly IMapper _mapper;

//        public DeleteFomComapnyHandler(DBContext context, IMapper mapper)
//        {
//            _context = context;
//            _mapper = mapper;
//        }

//        public async Task<int> Handle(DeleteFomComapny request, CancellationToken cancellationToken)
//        {
//            try
//            {
//                Log.Info("----Info Delete Profm Resource Start----");

//                if (request.Id > 0)
//                {

//                    var Company = await _context.ErpFomSysCompany.AsNoTracking().FirstOrDefaultAsync(e => e.Id == request.Id);
//                    Company.IsActive = false;
//                    _context.ErpFomSysCompany.Update(Company);
//                    await _context.SaveChangesAsync();

//                    return request.Id;
//                }
//                return 0;
//            }
//            catch (Exception ex)
//            {

//                Log.Error("Error in Delete Profm Resource");
//                Log.Error("Error occured time : " + DateTime.UtcNow);
//                Log.Error("Error message : " + ex.Message);
//                Log.Error("Error StackTrace : " + ex.StackTrace);
//                return 0;
//            }

//        }
//    }

//    #endregion
//}
