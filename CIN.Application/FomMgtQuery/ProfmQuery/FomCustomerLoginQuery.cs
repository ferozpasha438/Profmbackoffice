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
    
//    public class CheckLogin : IRequest<(int, string, string)>
//    {
//        public ProfmLoginMetaDataDto Input { get; set; }
//    }

//    public class CheckLoginHandler : IRequestHandler<CheckLogin, (int, string, string)>
//    {
//        private readonly DBContext _context;
//        private readonly IMapper _mapper;

//        public CheckLoginHandler(DBContext context)
//        {
//            _context = context;
//        }
//        public async Task<(int, string, string)> Handle(CheckLogin request, CancellationToken cancellationToken)
//        {
//            try
//            {
//                var user = await _context.ProfmSysUser.FirstOrDefaultAsync(e => e.UserName == request.Input.UserName);

//                if (user is not null)
//                {
//                    //if (SecurePasswordHasher.Verify(request.Input.Password, user.Password))
//                    if (SecurePasswordHasher.IsPasswordDecoded(user.Password, request.Input.Password))
//                    {
//                        return (user.Id, user.PrimaryBranch, user.LoginType);
//                    }
//                    else
//                        return (0, string.Empty, string.Empty);
//                }
//            }
//            catch (Exception ex)
//            {
//                throw;
//            }
//            return (0, string.Empty, string.Empty);
//            //return await _context.Users.AnyAsync(e => e.UserName == request.Input.UserName && e.Password == request.Input.Password && e.CINNumber == request.Input.CINNumber);
//        }
//    }

//}
