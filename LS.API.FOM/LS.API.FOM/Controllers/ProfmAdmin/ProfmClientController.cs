//using PROFM.Application;
//using PROFM.Application.Common;
//using PROFM.Application.ProfmAdminDtos;
//using PROFM.Application.ProfmQuery;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Options;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace LS.API.PROFM.Controllers.ProfmAdmin
//{
//    public class ProfmClientController :BaseController
//    {
//        private IConfiguration _Config;

//        public ProfmClientController(IOptions<AppSettingsJson> appSettings, IConfiguration config) : base(appSettings)
//        {
//            _Config = config;
//        }



//        [HttpGet]
//        public async Task<IActionResult> Get([FromQuery] PaginationFilterDto filter)
//        {
//            var list = await Mediator.Send(new GetClientList() { Input = filter, User = UserInfo() });
//            return Ok(list);
//        }


//        [HttpGet("{id}")]
//        public async Task<IActionResult> Get([FromRoute] int id)
//        {
//            var obj = await Mediator.Send(new GetClientById() { Id = id, User = UserInfo() });
//            return obj is not null ? Ok(obj) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
//        }

//    }
//}
