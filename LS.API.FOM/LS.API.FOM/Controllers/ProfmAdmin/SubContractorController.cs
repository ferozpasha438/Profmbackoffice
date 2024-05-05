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
//    public class SubContractorController :BaseController
//    {
//        private IConfiguration _Config;

//        public SubContractorController(IOptions<AppSettingsJson> appSettings, IConfiguration config) : base(appSettings)
//        {
//            _Config = config;
//        }


//        [HttpGet("getSubContractorsPaginationList")]
//        public async Task<IActionResult> Get([FromQuery] PaginationFilterDto filter)
//        {

//            var list = await Mediator.Send(new GetSubContractorsPaginationListQuery() { Input = filter, User = UserInfo() });
//            return Ok(list);
//        }

//        [HttpGet("getSelectSubContractorsList")]
//        public async Task<IActionResult> Get()
//        {

//            var list = await Mediator.Send(new GetSelectSubContractorsQuery() { User = UserInfo() });
//            return Ok(list);
//        }


//        [HttpGet("getSubContractorById/{id}")]
//        public async Task<IActionResult> Get([FromRoute] int id)
//        {
//            var obj = await Mediator.Send(new GetSubContractorByIdQuery() { Id = id, User = UserInfo() });
//            return obj is not null ? Ok(obj) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
//        }
//         [HttpGet("getSubContractorByCode/{code}")]
//        public async Task<IActionResult> Get([FromRoute] string code)
//        {
//            var obj = await Mediator.Send(new GetSubContractorByCodeQuery() { Code = code, User = UserInfo() });
//            return obj is not null ? Ok(obj) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
//        }



//        [HttpPost("createUpdateSubContractor")]
//        public async Task<ActionResult> Create([FromBody] TblProFmSubContractorDto dTO)
//        {
//            var id = await Mediator.Send(new CreateUpdateSubContractorQuery() { Input = dTO, User = UserInfo() });
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
//            var academicId = await Mediator.Send(new DeleteSubContractorQuery() { Id = id, User = UserInfo() });
//            if (academicId > 0)
//                return NoContent();
//            return BadRequest(new ApiMessageDto { Message = ApiMessageInfo.Failed });
//        }
//    }
//}
