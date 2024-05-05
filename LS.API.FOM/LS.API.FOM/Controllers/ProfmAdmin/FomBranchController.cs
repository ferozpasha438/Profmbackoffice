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
//    public class FomBranchController : BaseController
//    {
//        private IConfiguration _Config;

//        public FomBranchController(IOptions<AppSettingsJson> appSettings, IConfiguration config) : base(appSettings)
//        {
//            _Config = config;
//        }


//        [HttpGet]
//        public async Task<IActionResult> Get([FromQuery] PaginationFilterDto filter)
//        {

//            var list = await Mediator.Send(new GetFomSysBranchList() { Input = filter, User = UserInfo() });
//            return Ok(list);
//        }


//        [HttpGet("{id}")]
//        public async Task<IActionResult> Get([FromRoute] int id)
//        {
//            var obj = await Mediator.Send(new GetFomSysBranchById() { Id = id, User = UserInfo() });
//            return obj is not null ? Ok(obj) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
//        }



//        [HttpPost]
//        public async Task<ActionResult> Create([FromBody] TblErpFomSysCompanyBranchDto dTO)
//        {
//            var id = await Mediator.Send(new CreateUpdateFomBranchCompany() { FomCompanyBranchDto = dTO, User = UserInfo() });
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
//            var companyId = await Mediator.Send(new DeleteFomCompanyBranch() { Id = id, User = UserInfo() });
//            if (companyId > 0)
//                return NoContent();
//            return BadRequest(new ApiMessageDto { Message = ApiMessageInfo.Failed });
//        }

//    }
//}
