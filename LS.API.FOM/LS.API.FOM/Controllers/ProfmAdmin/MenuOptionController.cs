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
//    public class MenuOptionController :BaseController
//    {
//        private IConfiguration _Config;

//        public MenuOptionController(IOptions<AppSettingsJson> appSettings, IConfiguration config) : base(appSettings)
//        {
//            _Config = config;
//        }



//        [HttpGet("getUserMenuSubLink/{id:int}")]
//        public async Task<IActionResult> GetUserMenuSubLink(int id)
//        {
//            var obj = await Mediator.Send(new GetUserMenuSubLink() { UserId = id });
//            return obj is not null ? Ok(obj) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
//        }







//        [HttpGet("getUserWiseMenuCodes/{id:int}")]
//        public async Task<IActionResult> GetUserWiseMenuCodes(int id)
//        {
//            var obj = await Mediator.Send(new GetUserWiseMenuCodes() { UserId = id, User = UserInfo() });
//            return Ok(obj);
//        }

//        [HttpGet("getSideMenuOptionList/{id:int}")]
//        public async Task<IActionResult> GetSideMenuOptionList(int id)
//        {
//            var obj = await Mediator.Send(new GetSideMenuOptionList() { UserId = id, User = UserInfo() });
//            return obj is not null ? Ok(obj) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
//        }




//        [HttpPost]
//        public async Task<ActionResult> Create([FromBody] MenuItemFlatNodeListDto input)
//        {
//            //await Task.Delay(3000);

//            var userId = await Mediator.Send(new CreateMenuOption() { MenuNodeList = input });
//            if (userId > 0)
//            {
//                return Ok(new ApiMessageDto { Message = ApiMessageInfo.Success });
//            }
//            return BadRequest(new ApiMessageDto { Message = ApiMessageInfo.Failed });
//        }


//    }
//}
