using CIN.Application;
using CIN.Application.Common;
using CIN.Application.FomMgtDtos;
using CIN.Application.FomMgtQuery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using CIN.Application.ProfmQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LS.API.FOM.Controllers.ProfmAdmin
{
    public class ResourceTypeController : BaseController
    {
        private IConfiguration _Config;

        public ResourceTypeController(IOptions<AppSettingsJson> appSettings, IConfiguration config) : base(appSettings)
        {
            _Config = config;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var obj = await Mediator.Send(new GetResourceTypeByIdQuery() { Id = id, User = UserInfo() });
            return obj is not null ? Ok(obj) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
        }

        [HttpGet("getResourceTypesPaginationList")]
        public async Task<IActionResult> Get([FromQuery] PaginationFilterDto filter)
        {

            var list = await Mediator.Send(new GetResourceTypesPaginationListQuery() { Input = filter, User = UserInfo() });
            return Ok(list);
        }

        [HttpGet("getSelectResourceTypesList")]
        public async Task<IActionResult> Get()
        {

            var list = await Mediator.Send(new GetSelectResourceTypesQuery() { User = UserInfo() });
            return Ok(list);
        }


       

        [HttpGet("getResourceTypeByCode/{code}")]
        public async Task<IActionResult> Get([FromRoute] string code)
        {
            var obj = await Mediator.Send(new GetResourceTypeByCodeQuery() { Code = code, User = UserInfo() });
            return obj is not null ? Ok(obj) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
        }



        [HttpPost("createUpdateResourceType")]
        public async Task<ActionResult> Create([FromBody] TblErpFomResourceTypeDto dTO)
        {
            var id = await Mediator.Send(new CreateUpdateResourceTypeQuery() { Input = dTO, User = UserInfo() });
            if (id > 0)
                return Created($"get/{id}", dTO);
            else if (id == -1)
            {
                return BadRequest(new ApiMessageDto { Message = ApiMessageInfo.Duplicate(nameof(dTO.Id)) });
            }
            return BadRequest(new ApiMessageDto { Message = ApiMessageInfo.Failed });
        }





        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var resourceId = await Mediator.Send(new DeleteResourceTypeQuery() { Id = id, User = UserInfo() });
            if (resourceId > 0)
                return NoContent();
            return BadRequest(new ApiMessageDto { Message = ApiMessageInfo.Failed });
        }
    }
}
