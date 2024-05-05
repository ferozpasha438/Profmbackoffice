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

namespace CIN.Application.FomMgtQuery.ProfmQuery
{
    #region GetAll
    public class GetFomSysAuthoritiesList : IRequest<List<TblErpFomSysLoginAuthorityDto>>
    {
        public UserIdentityDto User { get; set; }
       // public PaginationFilterDto Input { get; set; }

    }

    public class GetFomSysAuthoritiesListHandler : IRequestHandler<GetFomSysAuthoritiesList, List<TblErpFomSysLoginAuthorityDto>>
    {
        private readonly CINDBOneContext _context;
        // private readonly DBContext _context;
        private readonly IMapper _mapper;
        public GetFomSysAuthoritiesListHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<TblErpFomSysLoginAuthorityDto>> Handle(GetFomSysAuthoritiesList request, CancellationToken cancellationToken)
        {


            var list = await _context.LoginAuthority.AsNoTracking().ProjectTo<TblErpFomSysLoginAuthorityDto>(_mapper.ConfigurationProvider).ToListAsync();
                       

            return list;
        }


    }


    #endregion

    #region GetById

    public class GetFomSysAuthoritiesListById : IRequest<TblErpFomSysLoginAuthorityDto>
    {
        public UserIdentityDto User { get; set; }
        public int Id { get; set; }
    }

    public class GetFomSysAuthoritiesListByIdHandler : IRequestHandler<GetFomSysAuthoritiesListById, TblErpFomSysLoginAuthorityDto>
    {
        private readonly CINDBOneContext _context;
        // private readonly DBContext _context;
        private readonly IMapper _mapper;
        public GetFomSysAuthoritiesListByIdHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TblErpFomSysLoginAuthorityDto> Handle(GetFomSysAuthoritiesListById request, CancellationToken cancellationToken)
        {

            TblErpFomSysLoginAuthority obj = new();
            var authority = await _context.LoginAuthority.AsNoTracking().ProjectTo<TblErpFomSysLoginAuthorityDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(e => e.LoginID == request.Id);
            return authority;
            // throw new NotImplementedException();
        }
    }
    #endregion

    #region Create_And_Update

    public class CreateUpdateFomSysAuthorities : IRequest<int>
    {
        public UserIdentityDto User { get; set; }
        public TblErpFomSysLoginAuthorityDto SysLoginAuthority { get; set; }
    }

    public class CreateUpdateFomSysAuthoritiesHandler : IRequestHandler<CreateUpdateFomSysAuthorities, int>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;

        public CreateUpdateFomSysAuthoritiesHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateUpdateFomSysAuthorities request, CancellationToken cancellationToken)
        {
            try
            {
                Log.Info("----Info Create Update  Fom Sys Authorities method start----");

                var obj = request.SysLoginAuthority;


                TblErpFomSysLoginAuthority LoginAuthority = new();
                if (obj.Id > 0)
                    LoginAuthority = await _context.LoginAuthority.AsNoTracking().FirstOrDefaultAsync(e => e.Id == obj.Id);


                LoginAuthority.Id = obj.Id;
                LoginAuthority.LoginID = obj.LoginID;
                LoginAuthority.RaiseTicket = obj.RaiseTicket;
                LoginAuthority.VoidTicket = obj.VoidTicket;
                LoginAuthority.ForeCloseWO = obj.ForeCloseWO;
                LoginAuthority.ApproveTicket = obj.ApproveTicket;
                LoginAuthority.CloseWO = obj.CloseWO;
                LoginAuthority.ManageWO = obj.ManageWO;
                LoginAuthority.ModifyTicket = obj.ModifyTicket;
                LoginAuthority.VoidAfterApproval = obj.VoidAfterApproval;




                if (obj.Id > 0)
                {

                    _context.LoginAuthority.Update(LoginAuthority);
                    
                }
                else
                {

                    await _context.LoginAuthority.AddAsync(LoginAuthority);
                    
                }
                await _context.SaveChangesAsync();
                Log.Info("----Info Create Update  Fom Sys Authorities method Exit----");
                return LoginAuthority.Id;
            }
            catch (Exception ex)
            {
                Log.Error("Error in Create Update  Fom Sys Authorities Method");
                Log.Error("Error occured time : " + DateTime.UtcNow);
                Log.Error("Error message : " + ex.Message);
                Log.Error("Error StackTrace : " + ex.StackTrace);
                return 0;
            }
        }


    }



    #endregion
}
