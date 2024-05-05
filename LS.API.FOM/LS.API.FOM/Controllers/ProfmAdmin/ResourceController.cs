using CIN.Application;
using CIN.Application.Common;
using CIN.Application.FomMgtDtos;
using CIN.Application.FomMgtQuery;
using CIN.Application.ProfmQuery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LS.API.FOM.Controllers.ProfmAdmin
{
    public class ResourceController : BaseController
    {
        private IConfiguration _Config;

        public ResourceController(IOptions<AppSettingsJson> appSettings, IConfiguration config) : base(appSettings)
        {
            _Config = config;
        }


        [HttpGet("getResourcesPaginationList")]
        public async Task<IActionResult> Get([FromQuery] PaginationFilterDto filter)
        {

            var list = await Mediator.Send(new GetResourcesPaginationListQuery() { Input = filter, User = UserInfo() });
            return Ok(list);
        }

        [HttpGet("getSelectResourcesList")]
        public async Task<IActionResult> Get()
        {

            var list = await Mediator.Send(new GetSelectResourcesQuery() { User = UserInfo() });
            return Ok(list);
        }


        [HttpGet("getResourceById/{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var obj = await Mediator.Send(new GetResourceByIdQuery() { Id = id, User = UserInfo() });
            return obj is not null ? Ok(obj) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
        }
        [HttpGet("getResourceByCode/{code}")]
        public async Task<IActionResult> Get([FromRoute] string code)
        {
            var obj = await Mediator.Send(new GetResourceByCodeQuery() { Code = code, User = UserInfo() });
            return obj is not null ? Ok(obj) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
        }



        [HttpPost("createUpdateResource")]
        public async Task<ActionResult> Create([FromBody] ErpFomResourcesDto dTO)
        {
            var id = await Mediator.Send(new CreateUpdateResourceQuery() { Input = dTO, User = UserInfo() });
            if (id > 0)
                return Created($"get/{id}", dTO);
            else if (id == -1)
            {
                return BadRequest(new ApiMessageDto { Message = ApiMessageInfo.Duplicate(nameof(dTO.Id)) });
            }
            return BadRequest(new ApiMessageDto { Message = ApiMessageInfo.Failed });
        }


        [HttpGet("getResourceLoginUsersList")]
        public async Task<IActionResult> GetResourceLoginUsersList()
        {
            var obj = await Mediator.Send(new GetAllUsersList() { User = UserInfo() });
            return obj is not null ? Ok(obj) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
        }




        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var resourceId = await Mediator.Send(new DeleteResourceQuery() { Id = id, User = UserInfo() });
            if (resourceId > 0)
                return NoContent();
            return BadRequest(new ApiMessageDto { Message = ApiMessageInfo.Failed });
        }
    }
}
