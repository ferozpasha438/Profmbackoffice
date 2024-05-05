using CIN.Application;
using CIN.Application.Common;
using CIN.Application.FomMgtDtos;
using CIN.Application.FomMgtQuery;
//using CIN.Application.ProfmQuery;
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
    public class FomItemSubCategoryController : BaseController
    {
        private IConfiguration _Config;

        public FomItemSubCategoryController(IOptions<AppSettingsJson> appSettings, IConfiguration config) : base(appSettings)
        {
            _Config = config;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PaginationFilterDto filter)
        {

            var list = await Mediator.Send(new GetFomItemSubCategoriesList() { Input = filter, User = UserInfo() });
            return Ok(list);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var obj = await Mediator.Send(new GetFomItemSubCategoryById() { Id = id, User = UserInfo() });
            return obj is not null ? Ok(obj) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
        }



        [HttpPost]
        public async Task<ActionResult> Create([FromBody] InvDefSubCategoryDto dTO)
        {
            var id = await Mediator.Send(new CreateUpdateFomItemSubCategory() { FomItemSubCategoryDto = dTO, User = UserInfo() });
            if (id > 0)
                return Created($"get/{id}", dTO);
            else if (id == -1)
            {
                return BadRequest(new ApiMessageDto { Message = ApiMessageInfo.Duplicate(nameof(dTO.Id)) });
            }
            return BadRequest(new ApiMessageDto { Message = ApiMessageInfo.Failed });
        }
    }
}
