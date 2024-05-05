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
//    public class GetFomSysUserList : IRequest<PaginatedList<TblErpFomSysUserDto>>
//    {
//        public UserIdentityDto User { get; set; }
//        public PaginationFilterDto Input { get; set; }

//    }

//    public class GetFomSysUserListHandler : IRequestHandler<GetFomSysUserList, PaginatedList<TblErpFomSysUserDto>>
//    {
//        private readonly DBContext _context;
//        // private readonly DBContext _context;
//        private readonly IMapper _mapper;
//        public GetFomSysUserListHandler(DBContext context, IMapper mapper)
//        {
//            _context = context;
//            _mapper = mapper;
//        }
//        public async Task<PaginatedList<TblErpFomSysUserDto>> Handle(GetFomSysUserList request, CancellationToken cancellationToken)
//        {


//            var list = await _context.ProfmSysUser.AsNoTracking().ProjectTo<TblErpFomSysUserDto>(_mapper.ConfigurationProvider)
//                                    .PaginationListAsync(request.Input.Page, request.Input.PageCount, cancellationToken);

//            return list;
//        }


//    }


//    #endregion

//    #region GetById

//    public class GetErpFomSysUserById : IRequest<TblErpFomSysUserDto>
//    {
//        public UserIdentityDto User { get; set; }
//        public int Id { get; set; }
//    }

//    public class GetErpFomSysUserByIdHandler : IRequestHandler<GetErpFomSysUserById, TblErpFomSysUserDto>
//    {
//        private readonly DBContext _context;
//        // private readonly DBContext _context;
//        private readonly IMapper _mapper;
//        public GetErpFomSysUserByIdHandler(DBContext context, IMapper mapper)
//        {
//            _context = context;
//            _mapper = mapper;
//        }

//        public async Task<TblErpFomSysUserDto> Handle(GetErpFomSysUserById request, CancellationToken cancellationToken)
//        {

//            TblErpFomSysUserDto obj = new();
//            var fomSysUser = await _context.ProfmSysUser.AsNoTracking().ProjectTo<TblErpFomSysUserDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(e => e.Id == request.Id);
//            return fomSysUser;
//            // throw new NotImplementedException();
//        }
//    }
//    #endregion

//    #region Create_And_Update

//    public class CreateUpdateFomSysUser : IRequest<int>
//    {
//        public UserIdentityDto User { get; set; }
//        public TblErpFomSysUserDto FomSysUserDto { get; set; }
//    }

//    public class CreateUpdateFomSysUserHandler : IRequestHandler<CreateUpdateFomSysUser, int>
//    {
//        private readonly DBContext _context;
//        private readonly IMapper _mapper;

//        public CreateUpdateFomSysUserHandler(DBContext context, IMapper mapper)
//        {
//            _context = context;
//            _mapper = mapper;
//        }

//        public async Task<int> Handle(CreateUpdateFomSysUser request, CancellationToken cancellationToken)
//        {
//            try
//            {
//                Log.Info("----Info Create Update Fom Sys User method start----");

//                var obj = request.FomSysUserDto;


//                TblErpFomSysUser ProfmSysUser = new();
//                if (obj.Id > 0)
//                    ProfmSysUser = await _context.ProfmSysUser.AsNoTracking().FirstOrDefaultAsync(e => e.Id == obj.Id);


//                ProfmSysUser.Id = obj.Id;
//                ProfmSysUser.UserCode = obj.UserCode;
//                ProfmSysUser.UserName = obj.UserName;
//                ProfmSysUser.Password = obj.Password;
//                ProfmSysUser.UserType = obj.UserType;
//                ProfmSysUser.UserEmail = obj.UserEmail;
//                ProfmSysUser.SwpireCardId = obj.SwpireCardId;
//                ProfmSysUser.PrimaryBranch = obj.PrimaryBranch;
//                ProfmSysUser.ImagePath = obj.ImagePath;
//                ProfmSysUser.IsActive = obj.IsActive;
//                ProfmSysUser.ModifiedOn = DateTime.Now;
//                ProfmSysUser.ModifiedBy = obj.ModifiedBy;
//                ProfmSysUser.IsLoginAllow = obj.IsLoginAllow;
//                ProfmSysUser.IsSigned = obj.IsSigned;
//                ProfmSysUser.SiteCode = obj.SiteCode;
//                ProfmSysUser.LoginType = obj.LoginType;

//                if (obj.Id > 0)
//                {

//                    _context.ProfmSysUser.Update(ProfmSysUser);
//                }
//                else
//                {

//                    await _context.ProfmSysUser.AddAsync(ProfmSysUser);
//                }
//                await _context.SaveChangesAsync();
//                Log.Info("----Info Create Update Fom Sys User method Exit----");
//                return ProfmSysUser.Id;
//            }
//            catch (Exception ex)
//            {
//                Log.Error("Error in Create Update Fom Sys User Method");
//                Log.Error("Error occured time : " + DateTime.UtcNow);
//                Log.Error("Error message : " + ex.Message);
//                Log.Error("Error StackTrace : " + ex.StackTrace);
//                return 0;
//            }
//        }


//    }



//    #endregion

//    #region Delete
//    public class DeleteFomSysUser : IRequest<int>
//    {
//        public UserIdentityDto User { get; set; }
//        public int Id { get; set; }
//    }

//    public class DeleteFomSysUserHandler : IRequestHandler<DeleteFomSysUser, int>
//    {

//        private readonly DBContext _context;
//        private readonly IMapper _mapper;

//        public DeleteFomSysUserHandler(DBContext context, IMapper mapper)
//        {
//            _context = context;
//            _mapper = mapper;
//        }

//        public async Task<int> Handle(DeleteFomSysUser request, CancellationToken cancellationToken)
//        {
//            try
//            {
//                Log.Info("----Info Delete Fom Sys User Start----");

//                if (request.Id > 0)
//                {
//                    var profmSysUser = await _context.ProfmSysUser.FirstOrDefaultAsync(e => e.Id == request.Id);
//                    _context.Remove(profmSysUser);

//                    await _context.SaveChangesAsync();

//                    return request.Id;
//                }
//                return 0;
//            }
//            catch (Exception ex)
//            {

//                Log.Error("Error in Delete Fom Sys User");
//                Log.Error("Error occured time : " + DateTime.UtcNow);
//                Log.Error("Error message : " + ex.Message);
//                Log.Error("Error StackTrace : " + ex.StackTrace);
//                return 0;
//            }

//        }
//    }

//    #endregion
//}
