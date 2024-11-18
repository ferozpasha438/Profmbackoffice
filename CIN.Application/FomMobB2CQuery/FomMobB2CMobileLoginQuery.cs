using AutoMapper;

using MediatR;
using Microsoft.EntityFrameworkCore;
using CIN.Application.FomMobDtos;
using CIN.DB;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CIN.Application.FomMobB2CQuery
{

    #region FomMobB2CMobLogin
    public class CheckB2CMobileLogin : IRequest<(int, string, string, string, string, string, int)>       // userId, userName, email,mobile, loginType,role,mapid
    {
        public IpLoginB2CUserDto Input { get; set; }
    }

    public class CheckB2CMobileLoginHandler : IRequestHandler<CheckB2CMobileLogin, (int, string, string, string, string, string, int)>
    {
        private readonly CINDBOneContext _context;
        //  private readonly IMapper _mapper;

        public CheckB2CMobileLoginHandler(CINDBOneContext context)
        {
            _context = context;
        }
        public async Task<(int, string, string, string, string, string, int)> Handle(CheckB2CMobileLogin request, CancellationToken cancellationToken)
        {
            try
            {
                var userMap = await _context.FomB2CUserClientLoginMappings.FirstOrDefaultAsync(e => e.RegEmail == request.Input.UserName || e.RegMobile == request.Input.UserName || e.UserClientLoginCode == request.Input.UserName);
                int userId = 0;
                string role = "NA";
                if (userMap is not null)
                {

                    if (SecurePasswordHasher.IsPasswordDecoded(userMap.Password, request.Input.Password) && userMap.IsActive)
                    {
                        if (userMap.LoginType == "user")
                        {
                            var sysUser = _context.SystemLogins.FirstOrDefault(e => e.UserName == userMap.UserClientLoginCode);
                            if (sysUser is not null)
                            {
                                userId = sysUser.Id;
                                role = sysUser.UserType;
                            }
                        }

                    }
                    else
                    {
                        return (0, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 0);
                    }

                    return (userId, userMap.UserClientLoginCode, userMap.RegEmail, userMap.RegMobile, userMap.LoginType, role, userMap.Id);




                }
                return (0, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 0);
            }
            catch (Exception ex)
            {
                return (-1, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 0);
                throw;
            }

        }
    }
    #endregion


    #region UpdateFomMobileLogs
    public class UpdateFomMobileLogs : IRequest<bool>
    {
        public int UserId { get; set; }              //taking from mapId
        public string Token { get; set; }
    }

    public class UpdateFomMobileLogsHandler : IRequestHandler<UpdateFomMobileLogs, bool>
    {
        private readonly CINDBOneContext _context;
        //  private readonly IMapper _mapper;

        public UpdateFomMobileLogsHandler(CINDBOneContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(UpdateFomMobileLogs request, CancellationToken cancellationToken)
        {
            try
            {
                var UserMapLog = await _context.FomSysMobileLogs.FirstOrDefaultAsync(e => e.UserId == request.UserId);
                if (UserMapLog is not null)
                {
                    UserMapLog.Token = request.Token;
                    UserMapLog.Status = true;

                    _context.FomSysMobileLogs.Update(UserMapLog);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    UserMapLog = new()
                    {
                        UserId = request.UserId,
                        Status = true,
                        Token = request.Token,
                        CreatedOn = DateTime.UtcNow
                    };
                    await _context.FomSysMobileLogs.AddAsync(UserMapLog);
                    await _context.SaveChangesAsync();

                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }

        }
    }
    #endregion
}
