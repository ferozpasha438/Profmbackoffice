//using CIN.Application;
//using CIN.Application.Common;
//using CIN.Application.FomMgtDtos;
//using CIN.Application.FomMgtQuery;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Options;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace LS.API.FOM.Controllers.Admin
//{
//    public class FomCustomerCategoriesController : BaseController
//    {
//        private IConfiguration _Config;

//        public FomCustomerCategoriesController(IOptions<AppSettingsJson> appSettings, IConfiguration config) : base(appSettings)
//        {
//            _Config = config;
//        }


//        [HttpGet]
//        public async Task<IActionResult> Get([FromQuery] PaginationFilterDto filter)
//        {
//            var list = await Mediator.Send(new GetFomCustomerCategoriesList() { Input = filter, User = UserInfo() });
//            return Ok(list);
//        }
//    }
//}
