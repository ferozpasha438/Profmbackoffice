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
//    public class ProfmClientLoginController : ApiControllerBase
//    {
//        private IConfiguration _Config;
//        private DBContext _context;
//        private IOptions<AppSettingsJson> _appSettings;

//        public ProfmClientLoginController(IConfiguration config, DBContext context, IOptions<AppSettingsJson> appSettings)
//        {
//            _Config = config;
//            _context = context;
//            _appSettings = appSettings;

//        }

//        [HttpPost("ClientLogin")]
//        public async Task<IActionResult> ValidateClientLogin([FromBody] ProfmLoginMetaDataDto Input)
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
//                        Status = true
//                    });
//                }

//                return BadRequest(new LoginSuccessMessageDto { Message = "Invalid Credential", Status = false });

//            }
//            return BadRequest(new LoginSuccessMessageDto { Message = "Empty Username & Password", Status = false });
//        }

//        private string GenerateJSONWebToken(BaseLoginUserDTO loginUserDTO)
//        {
//            var signingKey = Convert.FromBase64String(_Config["Jwt:Key"]);
//            var expiryDuration = int.Parse(_Config["SessionTimeout:Expiry"]);

//            var tokenDescriptor = new SecurityTokenDescriptor
//            {
//                Issuer = null,              // Not required as no third-party is involved
//                Audience = null,            // Not required as no third-party is involved
//                IssuedAt = DateTime.UtcNow,
//                NotBefore = DateTime.UtcNow,
//                Expires = DateTime.UtcNow.AddMinutes(expiryDuration),
//                Subject = null,
//                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(signingKey), SecurityAlgorithms.HmacSha256Signature)
//            };
//            var jwtTokenHandler = new JwtSecurityTokenHandler();
//            var jwtToken = jwtTokenHandler.CreateJwtSecurityToken(tokenDescriptor);
//            var token = jwtTokenHandler.WriteToken(jwtToken);
//            return token;
//        }


//        [HttpPost("Registraion")]
//        public async Task<IActionResult> Registraion([FromBody] TblProfmClientMasterDto clientRegistrations)
//        {
//            var user = await Mediator.Send(new ClientRegistraion() { clientsRegistration = clientRegistrations });
            
//            if (user == -2)
//            {
//                return BadRequest(new LoginSuccessMessageDto { Message = "Username Already Exists ", Status = false });
//            }
//            if (user != null && user != -1)
//            {
//                return Ok(new LoginSuccessMessageDto { Message = "Successfully Registration Done", Status = true });
//            }
//            return BadRequest(new LoginSuccessMessageDto { Message = "Invalid Registration ", Status = false });

//        }


//        [HttpGet("ForgotPassword")]
//        public async Task<IActionResult> ForgotPassword([FromQuery] string EmailId)
//        {
//            if (EmailId is not null)
//            {
//                var user = await Mediator.Send(new ForgotPassword()
//                {

//                    EmailId = EmailId

//                });
//                if (user is not null)
//                {
//                    // SentEmailNotification obj = new();
//                    SentEmailNotification.Send(new EmailDtos()
//                    {
//                        To = "alilogicsystems@gmail.com",
//                        Subject = "Good Morning",
//                        MessageBody = "This is test Email from Shamsher"
//                    }, _Config);
//                    return Ok(new ForgotPasswordDto { Password = user.Password, Status = true });
//                }

//                return BadRequest(new LoginSuccessMessageDto { Message = "Invalid Email", Status = false });

//            }
//            return BadRequest(new LoginSuccessMessageDto { Message = "Empty Email ", Status = false });
//        }



       








//    }
//}
