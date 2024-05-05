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
//    #region  Login

//    public class CheckProfmLogin : IRequest<TblFomLoginMasterDto>
//    {
//        public UserIdentityDto User { get; set; }
//        public ProfmLoginMetaDataDto Input { get; set; }

//    }
//    public class CheckProfmLoginHandler : IRequestHandler<CheckProfmLogin, TblFomLoginMasterDto>
//    {
//        private readonly DBContext _context;
//        private readonly IMapper _mapper;
//        public CheckProfmLoginHandler(DBContext context, IMapper mapper)
//        {
//            _context = context;
//            _mapper = mapper;
//        }
//        public async Task<TblFomLoginMasterDto> Handle(CheckProfmLogin request, CancellationToken cancellationToken)
//        {
//            try
//            {
//                var user = await _context.ProfmSysUser.FirstOrDefaultAsync(e => (e.UserName == request.Input.UserName ) && e.Password == request.Input.Password);

//                if (user is not null)
//                    return new() { Id = -1 ,UserId=user.Id};
//                else
//                    return new() { Id = 1 ,UserId=0}; ;
//            }
//            catch (Exception ex)
//            {
//                Log.Error("Error in ProfmClientlogin");
//                Log.Error("Error occured time : " + DateTime.UtcNow);
//                Log.Error("Error message : " + ex.Message);
//                Log.Error("Error StackTrace : " + ex.StackTrace);
//                return new() { Id = 0 };

//            }
//        }
//    }

//    #endregion

//    //#region Registraion

//    //public class ClientRegistraion : IRequest<int>
//    //{
//    //    public UserIdentityDto User { get; set; }
//    //    public TblProfmClientMasterDto clientsRegistration { get; set; }

//    //}

//    //public class ClientRegistraionHandler : IRequestHandler<ClientRegistraion, int>
//    //{
//    //    private readonly DBContext _context;
//    //    private readonly IMapper _mapper;
//    //    public ClientRegistraionHandler(DBContext context, IMapper mapper)
//    //    {
//    //        _context = context;
//    //        _mapper = mapper;
//    //    }

//    //    public async Task<int> Handle(ClientRegistraion request, CancellationToken cancellationToken)
//    //    {
//    //        Log.Info("----Info CreateClientRegistration method start----");

//    //        try
//    //        {
//    //            var obj = request.clientsRegistration;
//    //            TblProfmClientMaster TblProfmClientLogin = new();
//    //            //var wardnumber = await _context.DefSchoolStudentMaster.AsNoTracking().FirstOrDefaultAsync(e => e.StuAdmNum == obj.WardNumber);
//    //            //if (wardnumber is null)
//    //            //{
//    //            //    return 2;
//    //            //}
//    //            if (obj.Id > 0)
//    //                TblProfmClientLogin = await _context.ProfmClientMaster.AsNoTracking().FirstOrDefaultAsync(e => e.Id == obj.Id);

//    //            //var regMobile = await _context.ProfmClientMaster.AsNoTracking().FirstOrDefaultAsync(e => e.RegisteredMobile == obj.RegisteredMobile);
//    //            //if (regMobile is null)
//    //            //{
//    //            //    return 3;
//    //            //}


//    //            var ParentsLogin = await _context.ProfmClientMaster.AsNoTracking().FirstOrDefaultAsync(e => (e.RegisteredEmail == request.clientsRegistration.RegisteredEmail || e.RegisteredMobile == request.clientsRegistration.RegisteredMobile) && e.LoginPassword == request.clientsRegistration.LoginPassword);
//    //            if (ParentsLogin is not null)
//    //            {
//    //                return -2;
//    //            }
//    //            //string Name = obj.Name;
//    //            //string SchoolId = obj.SchoolId;

//    //            TblProfmClientLogin.Id = obj.Id;
//    //            TblProfmClientLogin.ClientName = obj.ClientName;
//    //            TblProfmClientLogin.ClientName_Ar = obj.ClientName_Ar;
//    //            TblProfmClientLogin.ClientShortName = obj.ClientShortName;
//    //            TblProfmClientLogin.Country = obj.Country;
//    //            TblProfmClientLogin.City = obj.City;
//    //            TblProfmClientLogin.Area = obj.Area;
//    //            TblProfmClientLogin.PrimaryAddress = obj.PrimaryAddress;
//    //            TblProfmClientLogin.SecondaryAddress = obj.SecondaryAddress;
//    //            TblProfmClientLogin.RegisteredMobile = obj.RegisteredMobile;
//    //            TblProfmClientLogin.RegisteredEmail = obj.RegisteredEmail;
//    //            TblProfmClientLogin.AlternameMobile = obj.AlternameMobile;
//    //            TblProfmClientLogin.AlternateEmail = obj.AlternateEmail;
//    //            TblProfmClientLogin.LandLine1 = obj.LandLine1;
//    //            TblProfmClientLogin.LandLine2 = obj.LandLine2;
//    //            TblProfmClientLogin.ContactPerson1 = obj.ContactPerson1;
//    //            TblProfmClientLogin.Designation1 = obj.Designation1;
//    //            TblProfmClientLogin.ContactPerson2 = obj.ContactPerson2;
//    //            TblProfmClientLogin.Designation2 = obj.Designation2;
//    //            TblProfmClientLogin.VATNum = obj.VATNum;
//    //            TblProfmClientLogin.CRNum = obj.CRNum;
//    //            TblProfmClientLogin.TypeOfBusiness = obj.TypeOfBusiness;
//    //            TblProfmClientLogin.NumOfEmp = obj.NumOfEmp;
//    //            TblProfmClientLogin.LogoPath = obj.LogoPath;
//    //            TblProfmClientLogin.GeoLocLat = obj.GeoLocLat;
//    //            TblProfmClientLogin.GeoLocLan = obj.GeoLocLan;
//    //            TblProfmClientLogin.GeoLocGain = obj.GeoLocGain;
//    //            TblProfmClientLogin.InActiveDate = obj.InActiveDate;
//    //            TblProfmClientLogin.LoginPassword = obj.LoginPassword;
//    //            TblProfmClientLogin.IsActive = obj.IsActive;
//    //            TblProfmClientLogin.CreatedBy = obj.CreatedBy;
                

//    //            if (obj.Id > 0)
//    //            {
//    //                _context.ProfmClientMaster.Update(TblProfmClientLogin);

//    //            }
//    //            else
//    //            {
//    //                TblProfmClientLogin.CreatedOn = obj.CreatedOn;
//    //                await _context.ProfmClientMaster.AddAsync(TblProfmClientLogin);
//    //            }
//    //            await _context.SaveChangesAsync();
//    //            Log.Info("----Info Create Client Registration method Exit----");
//    //            return TblProfmClientLogin.Id;



//    //        }
//    //        catch (Exception ex)
//    //        {
//    //            Log.Error("Error in Create Client Registration Method");
//    //            Log.Error("Error occured time : " + DateTime.UtcNow);
//    //            Log.Error("Error message : " + ex.Message);
//    //            Log.Error("Error StackTrace : " + ex.StackTrace);
//    //            return 0;
//    //        }
//    //        // throw new NotImplementedException();
//    //    }
//    //}
//    //#endregion

//    //#region GetAllClients  
    
//    //public class GetClientList : IRequest<PaginatedList<TblProfmClientMasterDto>>
//    //{
//    //    public UserIdentityDto User { get; set; }
//    //    public PaginationFilterDto Input { get; set; }

//    //}

//    //public class GetAssignDriverListHandler : IRequestHandler<GetClientList, PaginatedList<TblProfmClientMasterDto>>
//    //{
//    //    private readonly DBContext _context;
//    //    private readonly IMapper _mapper;
//    //    public GetAssignDriverListHandler(DBContext context, IMapper mapper)
//    //    {
//    //        _context = context;
//    //        _mapper = mapper;
//    //    }
//    //    public async Task<PaginatedList<TblProfmClientMasterDto>> Handle(GetClientList request, CancellationToken cancellationToken)
//    //    {


//    //        var list = await _context.ProfmClientMaster.AsNoTracking().ProjectTo<TblProfmClientMasterDto>(_mapper.ConfigurationProvider)
//    //                                .PaginationListAsync(request.Input.Page, request.Input.PageCount, cancellationToken);

//    //        return list;
//    //    }


//    //}

//    //#endregion

//    //#region GetClientId

//    //public class GetClientById : IRequest<TblProfmClientMasterDto>
//    //{
//    //    public UserIdentityDto User { get; set; }
//    //    public int Id { get; set; }
//    //}

//    //public class GetClientByIdHandler : IRequestHandler<GetClientById, TblProfmClientMasterDto>
//    //{
//    //    private readonly DBContext _context;
//    //    private readonly IMapper _mapper;
//    //    public GetClientByIdHandler(DBContext context, IMapper mapper)
//    //    {
//    //        _context = context;
//    //        _mapper = mapper;
//    //    }

//    //    public async Task<TblProfmClientMasterDto> Handle(GetClientById request, CancellationToken cancellationToken)
//    //    {

//    //        TblProfmClientMasterDto obj = new();
//    //        var profmClient = await _context.ProfmClientMaster.AsNoTracking().ProjectTo<TblProfmClientMasterDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(e => e.Id == request.Id);
//    //        return profmClient;
          
//    //    }
//    //}
//    //#endregion

//    //#region ForgotPassword

   
//    //public class ForgotPassword : IRequest<ForgotPasswordDto>
//    //{
//    //    public UserIdentityDto User { get; set; }
//    //    public string EmailId { get; set; }

//    //}
//    //public class ForgotPasswordHandler : IRequestHandler<ForgotPassword, ForgotPasswordDto>
//    //{
//    //    private readonly DBContext _context;
//    //    private readonly IMapper _mapper;
//    //    public ForgotPasswordHandler(DBContext context, IMapper mapper)
//    //    {
//    //        _context = context;
//    //        _mapper = mapper;
//    //    }
//    //    public async Task<ForgotPasswordDto> Handle(ForgotPassword request, CancellationToken cancellationToken)
//    //    {
//    //        try
//    //        {

//    //            var password = await _context.ProfmClientMaster.AsNoTracking().Where(e => e.RegisteredEmail == request.EmailId).
//    //                       Select(e => new ForgotPasswordDto
//    //                       {
//    //                           Password = e.LoginPassword

//    //                       }).FirstOrDefaultAsync(cancellationToken);

//    //            return password;



//    //        }
//    //        catch (Exception ex)
//    //        {
//    //            Log.Error("Error in Forgot Password");
//    //            Log.Error("Error occured time : " + DateTime.UtcNow);
//    //            Log.Error("Error message : " + ex.Message);
//    //            Log.Error("Error StackTrace : " + ex.StackTrace);
//    //            return null;

//    //        }
//    //    }
//    //}
//    //#endregion


//}
