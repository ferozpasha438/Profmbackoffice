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
using System.Text.RegularExpressions;
using System.IO;
using CIN.Application.ProfmQuery;

namespace CIN.Application.FomMgtQuery
{
    #region GetAll
    public class GetFomCustomerMasterList : IRequest<PaginatedList<TblSndDefCustomerMasterDto>>
    {
        public UserIdentityDto User { get; set; }
        public PaginationFilterDto Input { get; set; }

    }

    public class GetFomCustomerMasterListHandler : IRequestHandler<GetFomCustomerMasterList, PaginatedList<TblSndDefCustomerMasterDto>>
    {
        private readonly CINDBOneContext _context;
        // private readonly DBContext _context;
        private readonly IMapper _mapper;
        public GetFomCustomerMasterListHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedList<TblSndDefCustomerMasterDto>> Handle(GetFomCustomerMasterList request, CancellationToken cancellationToken)
        {


            var list = await _context.OprCustomers.AsNoTracking().ProjectTo<TblSndDefCustomerMasterDto>(_mapper.ConfigurationProvider)
                                    .PaginationListAsync(request.Input.Page, request.Input.PageCount, cancellationToken);

            return list;
        }


    }


    #endregion

    #region GetById

    public class GetFomCustomerMasterById : IRequest<TblSndDefCustomerMaster>
    {
        public UserIdentityDto User { get; set; }
        public int Id { get; set; }
    }

    public class GetFomCustomerMasterByIdHandler : IRequestHandler<GetFomCustomerMasterById, TblSndDefCustomerMaster>
    {
        private readonly CINDBOneContext _context;
        // private readonly DBContext _context;
        private readonly IMapper _mapper;
        public GetFomCustomerMasterByIdHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TblSndDefCustomerMaster> Handle(GetFomCustomerMasterById request, CancellationToken cancellationToken)
        {
            try
            {
                TblSndDefCustomerMaster obj = new();
                var fomCustomer = await _context.OprCustomers.FirstOrDefaultAsync(e => e.Id == request.Id);
                return fomCustomer;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            // throw new NotImplementedException();
        }
    }
    #endregion

    #region Create_And_Update

    public class CreateUpdateFomCustomer : IRequest<int>
    {
        public UserIdentityDto User { get; set; }
        public InputCreateUpdateCustomerDto FomCustomerMasterDto { get; set; }
    }

    public class CreateUpdateFomCustomerHandler : IRequestHandler<CreateUpdateFomCustomer, int>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;

        public CreateUpdateFomCustomerHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateUpdateFomCustomer request, CancellationToken cancellationToken)
        {

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    Log.Info("----Info Create Update Fom Customer Master method start----");

                    var obj = request.FomCustomerMasterDto;


                    TblSndDefCustomerMaster customer = new();
                    if (obj.Id > 0)
                        customer = await _context.OprCustomers.AsNoTracking().FirstOrDefaultAsync(e => e.Id == obj.Id);



                    customer.CustCode = obj.CustCode;
                    customer.CustARAc = obj.CustARAc;
                    customer.CustOutStandBal = obj.CustOutStandBal ?? 0;
                    customer.CustAvailCrLimit = obj.CustAvailCrLimit ?? 0;
                    customer.CustArAcBranch = obj.CustArAcBranch;
                    customer.CustArAcCode = obj.CustArAcCode;
                    customer.CustARAdjAcCode = obj.CustARAdjAcCode;
                    customer.CustArbName = obj.CustArbName;
                    customer.CustARDiscAcCode = obj.CustARDiscAcCode;
                    customer.CustCatCode = obj.CustCatCode;
                    customer.CustCityCode1 = obj.CustCityCode1;
                    customer.CustCityCode2 = obj.CustCityCode2.HasValue() ? obj.CustCityCode2 : null;
                    customer.CustAlias = obj.CustAlias;
                    customer.CustContact1 = obj.CustContact1;
                    customer.CustContact2 = obj.CustContact2;
                    customer.CustCrLimit = obj.CustCrLimit;
                    customer.CustDefExpAcCode = obj.CustDefExpAcCode;
                    customer.CustDiscount = obj.CustDiscount;
                    customer.CustEmail1 = obj.CustEmail1;
                    customer.CustEmail2 = obj.CustEmail2;
                    customer.CustIsVendor = obj.CustIsVendor;
                    customer.CustLastPaidDate = obj.CustLastPaidDate;
                    customer.CustLastPayAmt = obj.CustLastPayAmt;
                    customer.CustLastSalesDate = obj.CustLastSalesDate;
                    customer.CustMobile1 = obj.CustMobile1;
                    customer.CustMobile2 = obj.CustMobile2;
                    customer.CustName = obj.CustName;
                    customer.CustOnHold = obj.CustOnHold;
                    customer.CustPhone1 = obj.CustPhone1;
                    customer.CustPhone2 = obj.CustPhone2;
                    customer.CustPriceLevel = obj.CustPriceLevel;                         //staticaly Assigned in angular
                    customer.CustRating = obj.CustRating;
                    customer.CustSalesArea = obj.CustSalesArea;
                    customer.CustSalesRep = obj.CustSalesRep;
                    customer.CustSetPriceLevel = obj.CustSetPriceLevel;
                    customer.CustType = obj.CustType;
                    customer.CustUDF1 = obj.CustUDF1;
                    customer.CustUDF2 = obj.CustUDF2;
                    customer.CustUDF3 = obj.CustUDF3;
                    customer.VATNumber = obj.VATNumber;
                    customer.SalesTermsCode = obj.SalesTermsCode;
                    customer.CustAddress1 = obj.CustAddress1;
                    customer.CustAddress2 = obj.CustAddress2;
                    customer.CustAlloChkPay = obj.CustAlloChkPay;
                    customer.CustAlloCrOverride = obj.CustAlloCrOverride;
                    customer.CustAllowCrsale = obj.CustAllowCrsale;
                    customer.CrNumber = obj.CrNumber;
                    customer.CustNameAliasEn = obj.CustNameAliasEn;
                    customer.CustNameAliasAr = obj.CustNameAliasAr;
                    customer.IsActive = obj.IsActive;
                    customer.CreatedOn = obj.CreatedOn;
                    customer.ModifiedOn = obj.ModifiedOn;


                    //if (request.FomCustomerMasterDto.IsFromWeb)
                    //{
                    //    if (request.FomCustomerMasterDto.Image1IForm != null && request.FomCustomerMasterDto.Image1IForm.Length > 0)
                    //    {
                    //        var (res, fileName) = FileUploads.FileUploadWithIform(customer.Id, request.Input.WebRoot, request.Input.Image1IForm);
                    //        if (res)
                    //        {
                    //            JobTicketHead.JOImg1 = fileName;
                    //        }
                    //    }
                    //    if (request.Input.Image2IForm != null && request.Input.Image2IForm.Length > 0)
                    //    {
                    //        var (res, fileName) = FileUploads.FileUploadWithIform(JobTicketHead.TicketNumber, request.Input.WebRoot, request.Input.Image2IForm);
                    //        if (res)
                    //        {
                    //            JobTicketHead.JOImg2 = fileName;
                    //        }
                    //    }
                    //    if (request.Input.Image3IForm != null && request.Input.Image3IForm.Length > 0)
                    //    {
                    //        var (res, fileName) = FileUploads.FileUploadWithIform(JobTicketHead.TicketNumber, request.Input.WebRoot, request.Input.Image3IForm);
                    //        if (res)
                    //        {
                    //            JobTicketHead.JOImg3 = fileName;
                    //        }
                    //    }
                    //}





                    if (obj.Id > 0)
                    {

                        _context.OprCustomers.Update(customer);
                    }
                    else
                    {

                        await _context.OprCustomers.AddAsync(customer);

                    }
                    await _context.SaveChangesAsync();



                    TblErpFomUserClientLoginMapping loginMapping = await _context.ErpFomUserClientLoginMapping.FirstOrDefaultAsync(e => e.RegEmail == customer.CustEmail1);

                    if (loginMapping is null)
                    {
                        loginMapping = new()
                        {
                            UserClientLoginCode = customer.CustCode,
                            RegEmail = customer.CustEmail1,
                            RegMobile = customer.CustMobile1,
                            Password = SecurePasswordHasher.EncodePassword(customer.CustCode),
                            LoginType = "client",
                            LastLoginDate = null,
                            IsActive = true,
                            CreatedOn = DateTime.Now,
                           
                        };
                        await _context.ErpFomUserClientLoginMapping.AddAsync(loginMapping);

                    }
                    else
                    {
                        loginMapping.Password = SecurePasswordHasher.EncodePassword(customer.CustCode);
                        loginMapping.RegEmail = customer.CustEmail1;
                        loginMapping.RegMobile = customer.CustMobile1;
                        loginMapping.CreatedOn=DateTime.Now;

                        // userBranch.BranchCode = obj.PrimaryBranch;
                        _context.ErpFomUserClientLoginMapping.Update(loginMapping);

                    }

                    await _context.SaveChangesAsync();






                    await transaction.CommitAsync();
                    Log.Info("----Info Create Update Fom Customer Master method Exit----");
                    return customer.Id;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Log.Error("Error in Create Update Fom Customer Master method");
                    Log.Error("Error occured time : " + DateTime.UtcNow);
                    Log.Error("Error message : " + ex.Message);
                    Log.Error("Error StackTrace : " + ex.StackTrace);
                    return 0;
                }
            }

        }
    }



    #endregion

    #region GetSelectList

    public class GetSelectList : IRequest<List<CustomSelectListItem>>
    {
        public UserIdentityDto User { get; set; }
        public string City { get; set; }
    }

    public class GetSelectListHandler : IRequestHandler<GetSelectList, List<CustomSelectListItem>>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetSelectListHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<CustomSelectListItem>> Handle(GetSelectList request, CancellationToken cancellationToken)
        {
            bool isArab = request.User.Culture.IsArab();
            var list = _context.CityCodes.AsNoTracking();

            if (request.City.HasValue())
                list = list.Where(e => e.CityName.Contains(request.City) || e.CityNameAr.Contains(request.City));
            //.Where(e => e.CompanyID == request.CompanyId || e.AccountId == request.AccountId)
            var cityList = await list.OrderByDescending(e => e.Id)
                           .Select(e => new CustomSelectListItem { Text = isArab ? e.CityNameAr : e.CityName, Value = e.CityCode })
                           .ToListAsync(cancellationToken);

            return cityList;
        }
    }

    #endregion

    #region GetSelectCityList

    public class GetSelectCityList : IRequest<List<CustomSelectListItem>>
    {
        public UserIdentityDto User { get; set; }
        public string CityCode { get; set; }
        public bool IsAutoComplete { get; set; }
        public string Search { get; set; }
    }

    public class GetSelectCityListHandler : IRequestHandler<GetSelectCityList, List<CustomSelectListItem>>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetSelectCityListHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<CustomSelectListItem>> Handle(GetSelectCityList request, CancellationToken cancellationToken)
        {
            bool isArab = request.User.Culture.IsArab();
            Log.Info("----Info GetSelectCityList method start----");
            var obj = _context.CityCodes.Where(e => e.IsActive).AsNoTracking();
            if (request.IsAutoComplete)
                obj = obj.Where(e => e.CityCode == request.CityCode
            && (e.CityCode.Contains(request.Search) || e.CityName.Contains(request.Search)));

            var newObj = await obj
               .OrderByDescending(e => e.Id)
               .Select(e => new CustomSelectListItem { Text = isArab ? e.CityNameAr : e.CityName, Value = e.CityCode, TextTwo = e.StateCode })
                  .ToListAsync(cancellationToken);

            Log.Info("----Info GetSelectCityList method Ends----");
            return newObj;
        }
    }

    #endregion

    #region GetStateCountrybyCityCode

    public class GetStateCountrybyCityCode : IRequest<CityStateCountryMappingDto>
    {
        public UserIdentityDto User { get; set; }
        public string CityCode { get; set; }
    }

    public class GetStateCountrybyCityCodeHandler : IRequestHandler<GetStateCountrybyCityCode, CityStateCountryMappingDto>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetStateCountrybyCityCodeHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CityStateCountryMappingDto> Handle(GetStateCountrybyCityCode request, CancellationToken cancellationToken)
        {
            Log.Info("----Info GetStateCountrybyCityCode method start----");


            CityStateCountryMappingDto obj = new();
            try
            {
                bool isArab = request.User.Culture.IsArab();
                var city = await _context.CityCodes.AsNoTracking().FirstOrDefaultAsync(e => e.CityCode == request.CityCode);
                if (city is not null)
                {
                    obj.Id = city.Id;
                    obj.CityName = city.CityName;
                    obj.CityNameAr = city.CityNameAr;
                    obj.CityCode = city.CityCode;
                    obj.IsActive = city.IsActive;
                    if (city is not null)
                    {
                        var stateCode = city.StateCode;
                        var state = await _context.StateCodes.AsNoTracking().FirstOrDefaultAsync(e => e.StateCode == stateCode);

                        obj.StateCode = state.StateCode;
                        obj.StateName = state.StateName;
                        if (state is not null)
                        {
                            var countryCode = state.CountryCode;
                            var country = await _context.CountryCodes.AsNoTracking().FirstOrDefaultAsync(e => e.CountryCode == countryCode);
                            obj.CountryCode = country.CountryCode;
                            obj.CountryName = country.CountryName;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            Log.Info("----Info GetStateCountrybyCityCode method Ends----");
            return obj;
        }
    }

    #endregion

    #region GetCustomersCustomList

    public class GetCustomersCustomList : IRequest<List<CustomSelectListItem>>
    {
        public UserIdentityDto User { get; set; }
        //   public bool? IsPayment { get; set; }
    }

    public class GetCustomersCustomListHandler : IRequestHandler<GetCustomersCustomList, List<CustomSelectListItem>>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetCustomersCustomListHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<CustomSelectListItem>> Handle(GetCustomersCustomList request, CancellationToken cancellationToken)
        {

            Log.Info("----Info GetCustomersCustomList method start----");
            var list = _context.OprCustomers.Where(e => e.IsActive);

            //if (request.IsPayment is null)
            //    list = list.Where(e => !e.CustOnHold);

            var newList = await list.AsNoTracking()
            .OrderBy(e => e.Id)
                 .Select(e => new CustomSelectListItem { Text = e.CustName, TextTwo = e.CustArbName, Value = e.CustCode.ToString() })
                  .ToListAsync(cancellationToken);
            Log.Info("----Info GetCustomersCustomList method Ends----");
            return newList;
        }
    }


    #endregion

    #region GetCustomerByCustomerCode
    public class GetCustomerByCustomerCode : IRequest<TblSndDefCustomerMasterDto>
    {
        public UserIdentityDto User { get; set; }
        public string CustomerCode { get; set; }
    }

    public class GetCustomerByCustomerCodeHandler : IRequestHandler<GetCustomerByCustomerCode, TblSndDefCustomerMasterDto>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetCustomerByCustomerCodeHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<TblSndDefCustomerMasterDto> Handle(GetCustomerByCustomerCode request, CancellationToken cancellationToken)
        {
            TblSndDefCustomerMasterDto obj = new();
            var Customer = await _context.OprCustomers.AsNoTracking().ProjectTo<TblSndDefCustomerMasterDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(e => e.CustCode == request.CustomerCode);

            return Customer;

        }
    }

    #endregion



    #region UploadCustomerFiles       

    public class UploadCustomerFiles : IRequest<(bool, string)>
    {
        public UserIdentityDto User { get; set; }
        public InputImageFromCustomerDto Input { get; set; }
        public string WebRoot { get; set; }
    }

    public class UploadCustomerFilesHandler : IRequestHandler<UploadCustomerFiles, (bool, string)>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;

        public UploadCustomerFilesHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<(bool, string)> Handle(UploadCustomerFiles request, CancellationToken cancellationToken)
        {
            try
            {
                var obj = request.Input;
                TblSndDefCustomerMaster customer = new();
                if (obj.Id > 0)
                    customer = await _context.OprCustomers.AsNoTracking().FirstOrDefaultAsync(e => e.Id == obj.Id);

                if (request.Input.Image1IForm != null && request.Input.Image1IForm.Length > 0)
                {
                    var (res, fileName) = FileUploads.FileUploadWithIform(customer.CustCode, request.WebRoot, request.Input.Image1IForm);
                    if (res)
                    {
                        customer.CustUDF1 = obj.WebRoot+fileName;
                    }
                }
                if (request.Input.Image2IForm != null && request.Input.Image2IForm.Length > 0)
                {
                    var (res, fileName) = FileUploads.FileUploadWithIform(customer.CustCode, request.WebRoot, request.Input.Image2IForm);
                    if (res)
                    {
                        customer.CustUDF2 = obj.WebRoot + fileName;
                    }
                }
                if (request.Input.Image3IForm != null && request.Input.Image3IForm.Length > 0)
                {
                    var (res, fileName) = FileUploads.FileUploadWithIform(customer.CustCode, request.WebRoot, request.Input.Image3IForm);
                    if (res)
                    {
                        customer.CustUDF3 = obj.WebRoot + fileName;
                    }
                }
                _context.OprCustomers.Update(customer);
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
