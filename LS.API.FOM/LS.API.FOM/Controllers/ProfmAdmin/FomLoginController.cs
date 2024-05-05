//using PROFM.Application;
//using PROFM.Application.Common.Email;
//using PROFM.Application.ProfmAdminDtos;
//using PROFM.Application.ProfmQuery;
//using PROFM.DB;
//using MediatR;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Options;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Security.Claims;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;

//namespace LS.API.PROFM.Controllers.ProfmAdmin
//{
//    public class FomLoginController : ApiControllerBase
//    {
//        private IConfiguration _Config;
//        private DBContext _context;
//        private IOptions<AppSettingsJson> _appSettings;
       

//        public FomLoginController(IConfiguration config, DBContext context, IOptions<AppSettingsJson> appSettings)
//        {
//            _Config = config;
//            _context = context;
//            _appSettings = appSettings;
//        }



//        [HttpPost("ValidateLogin")]
//        public async Task<IActionResult> ValidateLogin([FromBody] ProfmLoginMetaDataDto Input)
//        {


//            if (Input.UserName != null && Input.Password != null)
//            {
//                var user = await Mediator.Send(new CheckProfmLogin()
//                {

//                    Input = Input

//                });
//                if (user.Id == -1)
//                {
//                    return Ok(new LoginSuccessMessageDto
//                    {
//                        Message = "Successful Login",
//                        Status = true,
//                        UserId=user.UserId
//                    });
//                }

//                return BadRequest(new LoginSuccessMessageDto { Message = "Invalid Credential", Status = false });

//            }
//            return BadRequest(new LoginSuccessMessageDto { Message = "Empty Username & Password", Status = false });
//        }


//        //[HttpPost]
//        //public async Task<IActionResult> Post([FromBody] ProfmLoginMetaDataDto input)
//        //{
//        //    Log.Info("ValidateUser() Method Begins");
//        //    try
//        //    {
//        //        var (userId, branchCode, loginType) = await Mediator.Send(new CheckLogin() { Input = input });
//        //        if (userId > 0)
//        //        {
//        //            var metaData = await _cinDbContext.MetaDataList.FirstOrDefaultAsync(e => e.CINNumber == input.CINNumber);
//        //            short concurrentUsers = metaData.ConcurrentUsers;
//        //            short connectedUsers = metaData.ConnectedUsers;
//        //            var userSideMenuList = await Mediator.Send(new GetSideMenuOptionList() { User = new UserIdentityDto { UserId = userId } });
//        //            metaData.ConnectedUsers++;
//        //            _cinDbContext.MetaDataList.Update(metaData);
//        //            await _cinDbContext.SaveChangesAsync();
//        //            var branch = await Mediator.Send(new GetBranchInfoByBranchCode() { Input = branchCode });
//        //            var company = await Mediator.Send(new GetCompany() { Id = branch.CompanyId });
//        //            string teacherCode = string.Empty;
//        //            if (loginType == "Teacher")
//        //            {
//        //                teacherCode = await Mediator.Send(new GetTeacherCode() { Id = userId });
//        //            }
//        //            var token = GenerateJSONWebToken(new BaseCINLoginUserDTO { CompanyId = branch.CompanyId, BranchCode = branchCode, BranchId = branch.Id, UserId = userId, DBConnectionString = GetConnectionString(), DBHRMConnectionString = GetHRMConnectionString(), TeacherCode = teacherCode });

//        //            return Ok(new { userSideMenuList, token, company.LogoURL });


                   
//        //        }
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        throw;
//        //    }
//        //    return BadRequest(new ApiMessageDto { Message = "Invalid Username & Password" });
//        //}




//    }
//}
