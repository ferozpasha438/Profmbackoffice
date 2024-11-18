
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using CIN.Application;
using CIN.Application.FomMobDtos;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using CIN.Application.FomMobB2CQuery;

namespace LS.API.FomMobB2C.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FomMobB2CLoginController : ApiControllerBase
    {

        private IConfiguration _Config;
        //  public ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public FomMobB2CLoginController(IConfiguration config)
        {
            _Config = config;
        }

        private string GenerateJSONWebToken(OpLoginUserDto loginUserDTO)
        {
            var signingKey = Convert.FromBase64String(_Config["Jwt:Key"]);
            var expiryDuration = int.Parse(_Config["SessionTimeout:Expiry"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = null,              // Not required as no third-party is involved
                Audience = null,            // Not required as no third-party is involved
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(expiryDuration),
                Subject = new ClaimsIdentity(GetUserClaims(loginUserDTO)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(signingKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtTokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            var token = jwtTokenHandler.WriteToken(jwtToken);
            return token;
        }

        List<Claim> GetUserClaims(OpLoginUserDto loginUserDTO)
        {
            List<Claim> claims = new()
            {
                new("userid", loginUserDTO.UserId.ToString()),
                new("username", loginUserDTO.UserName.ToString()),
              //  new("customercode", loginUserDTO.CustomerCode),
                new("email", loginUserDTO.Email),
                new("mobile", loginUserDTO.Mobile),
                new("logintype",loginUserDTO.LoginType),
                new("role",loginUserDTO.Role),
                new("mapid",loginUserDTO.ClientUserMapId.ToString()),
              //  new(CustomClaimType.DbConnectionString, loginUserDTO.DBConnectionString),

            };
            return claims;

        }

        private void DeleteClaims()
        {

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] IpLoginB2CUserDto input)
        {
            Log.Info("ValidateUser() Method Begins");

            var (userId, userName, email, mobile, loginType, role, mapId) = await Mediator.Send(new CheckB2CMobileLogin() { Input = input });
            if (mapId > 0)      //Mobile Logs we using MapId as userId
            {
                var isLogUpdated = await Mediator.Send(new UpdateFomMobileLogs() { UserId = mapId, Token = input.Token });

                if (isLogUpdated)
                {

                    var token = GenerateJSONWebToken(new OpLoginUserDto { UserId = userId, /*DBConnectionString = GetConnectionString(),*/UserName = userName, Email = email, Mobile = mobile, LoginType = loginType, Role = role, ClientUserMapId = mapId });
                    return Ok(new { /*userSideMenuList,*/ token, /*company.LogoURL, */userId, userName, email, mobile, loginType, role, mapId });
                }
                else
                {
                    return BadRequest(new ApiMessageDto { Message = "Log Updation Failed" });
                }
            }
            else if (userId == 0)
            {
                return BadRequest(new ApiMessageDto { Message = "Invalid Username & Password" });
            }
            else
            {
                return BadRequest(new ApiMessageDto { Message = "Something went wrong" });
            }



        }

    }

}