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
//using PROFM.Application.ProFmAllDtos;

//namespace LS.API.PROFM.Controllers.ProfmAdmin
//{
//    public class ClientCategoryController :BaseController
//    {
//        private IConfiguration _Config;

//        public ClientCategoryController(IOptions<AppSettingsJson> appSettings, IConfiguration config) : base(appSettings)
//        {
//            _Config = config;
//        }


//        [HttpGet("getClientCategorysPaginationList")]
//        public async Task<IActionResult> Get([FromQuery] PaginationFilterDto filter)
//        {

//            var list = await Mediator.Send(new GetClientCategorysPaginationListQuery() { Input = filter, User = UserInfo() });
//            return Ok(list);
//        }

//        [HttpGet("getSelectClientCategorysList")]
//        public async Task<IActionResult> Get()
//        {

//            var list = await Mediator.Send(new GetSelectClientCategorysQuery() { User = UserInfo() });
//            return Ok(list);
//        }


//        [HttpGet("getClientCategoryById/{id}")]
//        public async Task<IActionResult> Get([FromRoute] int id)
//        {
//            var obj = await Mediator.Send(new GetClientCategoryByIdQuery() { Id = id, User = UserInfo() });
//            return obj is not null ? Ok(obj) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
//        }
//         [HttpGet("getClientCategoryByCode/{code}")]
//        public async Task<IActionResult> Get([FromRoute] string code)
//        {
//            var obj = await Mediator.Send(new GetClientCategoryByCodeQuery() { Code = code, User = UserInfo() });
//            return obj is not null ? Ok(obj) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
//        }



//        [HttpPost("createUpdateClientCategory")]
//        public async Task<ActionResult> Create([FromBody] TblProFmClientCategoryDto dTO)
//        {
//            var id = await Mediator.Send(new CreateUpdateClientCategoryQuery() { Input = dTO, User = UserInfo() });
//            if (id > 0)
//                return Created($"get/{id}", dTO);
//            else if (id == -1)
//            {
//                return BadRequest(new ApiMessageDto { Message = ApiMessageInfo.Duplicate(nameof(dTO.Id)) });
//            }
//            return BadRequest(new ApiMessageDto { Message = ApiMessageInfo.Failed });
//        }





//        [HttpDelete("{id}")]
//        public async Task<ActionResult> Delete([FromRoute] int id)
//        {
//            var academicId = await Mediator.Send(new DeleteClientCategoryQuery() { Id = id, User = UserInfo() });
//            if (academicId > 0)
//                return NoContent();
//            return BadRequest(new ApiMessageDto { Message = ApiMessageInfo.Failed });
//        }
//    }
//}
