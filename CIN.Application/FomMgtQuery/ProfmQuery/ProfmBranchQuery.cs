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
//    public class GetFomSysBranchList : IRequest<PaginatedList<TblErpFomSysCompanyBranchDto>>
//    {
//        public UserIdentityDto User { get; set; }
//        public PaginationFilterDto Input { get; set; }

//    }

//    public class GetFomSysBranchListHandler : IRequestHandler<GetFomSysBranchList, PaginatedList<TblErpFomSysCompanyBranchDto>>
//    {
//        private readonly DBContext _context;
//        // private readonly DBContext _context;
//        private readonly IMapper _mapper;
//        public GetFomSysBranchListHandler(DBContext context, IMapper mapper)
//        {
//            _context = context;
//            _mapper = mapper;
//        }
//        public async Task<PaginatedList<TblErpFomSysCompanyBranchDto>> Handle(GetFomSysBranchList request, CancellationToken cancellationToken)
//        {


//            var list = await _context.ErpfomSysCompanyBranch.AsNoTracking().ProjectTo<TblErpFomSysCompanyBranchDto>(_mapper.ConfigurationProvider)
//                                    .PaginationListAsync(request.Input.Page, request.Input.PageCount, cancellationToken);

//            return list;
//        }


//    }


//    #endregion

//    #region GetById

//    public class GetFomSysBranchById : IRequest<TblErpFomSysCompanyBranchDto>
//    {
//        public UserIdentityDto User { get; set; }
//        public int Id { get; set; }
//    }

//    public class GetProfmSysBranchByIdHandler : IRequestHandler<GetFomSysBranchById, TblErpFomSysCompanyBranchDto>
//    {
//        private readonly DBContext _context;
//        // private readonly DBContext _context;
//        private readonly IMapper _mapper;
//        public GetProfmSysBranchByIdHandler(DBContext context, IMapper mapper)
//        {
//            _context = context;
//            _mapper = mapper;
//        }

//        public async Task<TblErpFomSysCompanyBranchDto> Handle(GetFomSysBranchById request, CancellationToken cancellationToken)
//        {

//            TblErpFomSysCompanyBranch obj = new();
//            var fomBranch = await _context.ErpfomSysCompanyBranch.AsNoTracking().ProjectTo<TblErpFomSysCompanyBranchDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(e => e.Id == request.Id);
//            return fomBranch;
//            // throw new NotImplementedException();
//        }
//    }
//    #endregion

//    #region Create_And_Update

//    public class CreateUpdateFomBranchCompany : IRequest<int>
//    {
//        public UserIdentityDto User { get; set; }
//        public TblErpFomSysCompanyBranchDto FomCompanyBranchDto { get; set; }
//    }

//    public class CreateUpdateFomBranchHandler : IRequestHandler<CreateUpdateFomBranchCompany, int>
//    {
//        private readonly DBContext _context;
//        private readonly IMapper _mapper;

//        public CreateUpdateFomBranchHandler(DBContext context, IMapper mapper)
//        {
//            _context = context;
//            _mapper = mapper;
//        }

//        public async Task<int> Handle(CreateUpdateFomBranchCompany request, CancellationToken cancellationToken)
//        {
//            try
//            {
//                Log.Info("----Info Create Update Fom Branch method start----");

//                var obj = request.FomCompanyBranchDto;


//                TblErpFomSysCompanyBranch FomCompanyBranch = new();
//                if (obj.Id > 0)
//                    FomCompanyBranch = await _context.ErpfomSysCompanyBranch.AsNoTracking().FirstOrDefaultAsync(e => e.Id == obj.Id);


//                FomCompanyBranch.Id = obj.Id;
//                FomCompanyBranch.CompanyId = obj.CompanyId;
//                FomCompanyBranch.BranchCode = obj.BranchCode;
//                FomCompanyBranch.BranchName = obj.BranchName;
//                FomCompanyBranch.BankName = obj.BankName;
//                FomCompanyBranch.BankNameAr = obj.BankNameAr;
//                FomCompanyBranch.BranchAddress = obj.BranchAddress;
//                FomCompanyBranch.BranchAddressAr = obj.BranchAddressAr;
//                FomCompanyBranch.AccountNumber = obj.AccountNumber;
//                FomCompanyBranch.Phone = obj.Phone;
//                FomCompanyBranch.Mobile = obj.Mobile;
//                FomCompanyBranch.City = obj.City;
//                FomCompanyBranch.State = obj.State;
//                FomCompanyBranch.AuthorityName = obj.AuthorityName;
//                FomCompanyBranch.GeoLocLatitude = obj.GeoLocLatitude;
//                FomCompanyBranch.GeoLocLongitude = obj.GeoLocLongitude;
//                FomCompanyBranch.Remarks = obj.Remarks;
//                FomCompanyBranch.Iban = obj.Iban;
//                FomCompanyBranch.IsActive = obj.IsActive;
//                FomCompanyBranch.CreatedOn = DateTime.Now;
//                FomCompanyBranch.CreatedBy = request.User.UserName;


//                if (obj.Id > 0)
//                {

//                    _context.ErpfomSysCompanyBranch.Update(FomCompanyBranch);
//                }
//                else
//                {

//                    await _context.ErpfomSysCompanyBranch.AddAsync(FomCompanyBranch);
//                }
//                await _context.SaveChangesAsync();
//                Log.Info("----Info Create Update Fom Company Branch method Exit----");
//                return FomCompanyBranch.Id;
//            }
//            catch (Exception ex)
//            {
//                Log.Error("Error in Create Update Fom Company Branch Method");
//                Log.Error("Error occured time : " + DateTime.UtcNow);
//                Log.Error("Error message : " + ex.Message);
//                Log.Error("Error StackTrace : " + ex.StackTrace);
//                return 0;
//            }
//        }


//    }



//    #endregion

//    #region GetBranchInfoByBranchCode

//    public class GetBranchInfoByBranchCode : IRequest<TblErpFomSysCompanyBranchDto>
//    {
//        public UserIdentityDto User { get; set; }
//        public string Input { get; set; }
//    }

//    public class GetBranchInfoByBranchCodeHandler : IRequestHandler<GetBranchInfoByBranchCode, TblErpFomSysCompanyBranchDto>
//    {
//        private readonly DBContext _context;
//        private readonly IMapper _mapper;
//        public GetBranchInfoByBranchCodeHandler(DBContext context, IMapper mapper)
//        {
//            _context = context;
//            _mapper = mapper;
//        }
//        public async Task<TblErpFomSysCompanyBranchDto> Handle(GetBranchInfoByBranchCode request, CancellationToken cancellationToken)
//        {
//            Log.Info("----Info GetBranchInfoByBranchCode method start----");
//            try
//            {
//                var item = await _context.ErpfomSysCompanyBranch.AsNoTracking()
//                   .Where(e => e.BranchCode == request.Input)
//                   .OrderByDescending(e => e.Id)
//                  .ProjectTo<TblErpFomSysCompanyBranchDto>(_mapper.ConfigurationProvider)
//                     .FirstOrDefaultAsync(cancellationToken);
//                Log.Info("----Info GetBranchInfoByBranchCode method Ends----");
//                return item;

//            }
//            catch (Exception ex)
//            {
//                Log.Info(ex.Message);
//                return null;
//            }
//        }
//    }

//    #endregion

//    #region Delete
//    public class DeleteFomCompanyBranch : IRequest<int>
//    {
//        public UserIdentityDto User { get; set; }
//        public int Id { get; set; }
//    }

//    public class DeleteFomBranchHandler : IRequestHandler<DeleteFomCompanyBranch, int>
//    {

//        private readonly DBContext _context;
//        private readonly IMapper _mapper;

//        public DeleteFomBranchHandler(DBContext context, IMapper mapper)
//        {
//            _context = context;
//            _mapper = mapper;
//        }

//        public async Task<int> Handle(DeleteFomCompanyBranch request, CancellationToken cancellationToken)
//        {
//            try
//            {
//                Log.Info("----Info Delete Fom Branch Start----");

//                if (request.Id > 0)
//                {
//                    var fomBranch = await _context.ErpfomSysCompanyBranch.FirstOrDefaultAsync(e => e.Id == request.Id);
//                    _context.Remove(fomBranch);

//                    await _context.SaveChangesAsync();

//                    return request.Id;
//                }
//                return 0;
//            }
//            catch (Exception ex)
//            {

//                Log.Error("Error in Delete fom Company Branch");
//                Log.Error("Error occured time : " + DateTime.UtcNow);
//                Log.Error("Error message : " + ex.Message);
//                Log.Error("Error StackTrace : " + ex.StackTrace);
//                return 0;
//            }

//        }
//    }

//    #endregion

//}
