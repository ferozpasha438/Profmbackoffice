using CIN.Application;
using CIN.Application.Common;
using CIN.Application.FomMgtDtos;
using CIN.Application.FomMgtQuery;
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
    public class FomItemMasterController : BaseController
    {
        private IConfiguration _Config;

        public FomItemMasterController(IOptions<AppSettingsJson> appSettings, IConfiguration config) : base(appSettings)
        {
            _Config = config;
        }


        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PaginationFilterDto filter)
        {

            var list = await Mediator.Send(new GetFomItemMasterList() { Input = filter.Values(), User = UserInfo() });
            return Ok(list);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var obj = await Mediator.Send(new GetFomItemMasterById() { Id = id, User = UserInfo() });
            return obj is not null ? Ok(obj) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
        }

        [HttpGet("GetUOMSelectList")]
        public async Task<IActionResult> GetUOMSelectList()
        {
            var obj = await Mediator.Send(new GetUOMList() {User = UserInfo() });
            return obj is not null ? Ok(obj) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
        }

        [HttpGet("GetSelectClass")]
        public async Task<IActionResult> GetSelectClass()
        {
            var obj = await Mediator.Send(new GetSelectClass() { User = UserInfo() });
            return obj is not null ? Ok(obj) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
        }
        [HttpGet("GetTaxSelectList")]
        public async Task<IActionResult> GetTaxSelectList()
        {
            var obj = await Mediator.Send(new GetTaxList() {User = UserInfo() });
            return obj is not null ? Ok(obj) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
        }

        [HttpGet("GetSelectSubClassList")]
        public async Task<IActionResult> getSelectSubClassList()
        {
            var obj = await Mediator.Send(new getSelectSubClassList() { User = UserInfo() });
            return obj is not null ? Ok(obj) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
        }


        [HttpGet("GetSelectSubCategoryList")]
        public async Task<IActionResult> GetSelectSubCategoryList([FromQuery] string itemCat)
        {
            var obj = await Mediator.Send(new GetSelectSubCategoryList() {Code= itemCat,  User = UserInfo() });
            return obj is not null ? Ok(obj) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
        }


        [HttpGet("GetSelectCategoryList")]
        public async Task<IActionResult> GetSelectCategoryList()
        {
            var obj = await Mediator.Send(new GetSelectCategoryList() { User = UserInfo() });
            return obj is not null ? Ok(obj) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
        }


        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ErpInvItemMasterDto dTO)
        {
            var id = await Mediator.Send(new CreateUpdateFomItemMaster() { FomItemMasterDto = dTO, User = UserInfo() });
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
            var itemId = await Mediator.Send(new DeleteFomItemMaster() { Id = id, User = UserInfo() });
            if (itemId > 0)
                return NoContent();
            return BadRequest(new ApiMessageDto { Message = ApiMessageInfo.Failed });
        }

    }
}
