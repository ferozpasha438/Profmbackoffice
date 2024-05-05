using CIN.Application;
using CIN.Application.Common;
using CIN.Application.FomMgtDtos;
using CIN.Application.FomMgtQuery;
using Microsoft.AspNetCore.Hosting;
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
    public class FomCustomerController :BaseController
    {
        private IConfiguration _Config;
        private readonly IWebHostEnvironment _env;

        public FomCustomerController(IOptions<AppSettingsJson> appSettings, IConfiguration config, IWebHostEnvironment env) : base(appSettings)
        {
            _Config = config;
            _env = env;
        }


        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PaginationFilterDto filter)
        {

            var list = await Mediator.Send(new GetFomCustomerMasterList() { Input = filter, User = UserInfo() });
            return Ok(list);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var obj = await Mediator.Send(new GetFomCustomerMasterById() { Id = id, User = UserInfo() });
            return obj is not null ? Ok(obj) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
        }



        [HttpPost]
        public async Task<ActionResult> Create([FromBody] InputCreateUpdateCustomerDto dTO)
        {
            var id = await Mediator.Send(new CreateUpdateFomCustomer() { FomCustomerMasterDto = dTO, User = UserInfo() });
            if (id > 0)
                return Created($"get/{id}", dTO);
            else if (id == -1)
            {
                return BadRequest(new ApiMessageDto { Message = ApiMessageInfo.Duplicate(nameof(dTO.Id)) });
            }
            return BadRequest(new ApiMessageDto { Message = ApiMessageInfo.Failed });
        }

        [HttpPost("UploadCustomerFiles")]
        public async Task<ActionResult> UploadCustomerFiles([FromForm] InputImageFromCustomerDto dTO)
        {
            var webRoot = $"{_env.ContentRootPath}/Customerfiles";
            bool exists = System.IO.Directory.Exists(webRoot);
            if (!exists)
                System.IO.Directory.CreateDirectory(webRoot);
            var (res, message) = await Mediator.Send(new UploadCustomerFiles() { Input = dTO,WebRoot=webRoot, User = UserInfo() });
            if (res)
                return Ok(new ApiMessageDto { Message = ApiMessageInfo.Success });
            else
                return BadRequest(new ApiMessageDto { Message = message });
        }


        [HttpGet("getSelectList")]
        public async Task<IActionResult> GetSelectList()
        {
            var list = await Mediator.Send(new GetSelectList() { User = UserInfo() });
            return Ok(list);
        }


        [HttpGet("getCitiesSelectList")]
        public async Task<IActionResult> GetCitiesSelectList()
        {
            var list = await Mediator.Send(new GetSelectCityList() { User = UserInfo() });
            return Ok(list);
        }


        [HttpGet("getStateCountrybyCityCode/{cityCode}")]
        public async Task<IActionResult> GetStateCountrybyCityCode(string cityCode)
        {
            var list = await Mediator.Send(new GetStateCountrybyCityCode() { CityCode = cityCode, User = UserInfo() });
            return Ok(list);
        }



        [HttpGet("getSelectCustomerList")]
        public async Task<IActionResult> GetSelectCustomerList()
        {
            var obj = await Mediator.Send(new GetCustomersCustomList() { User = UserInfo() });
            return obj is not null ? Ok(obj) : NotFound(ApiMessageNotFound());
        }


        [HttpGet("getCustomerByCustomerCode/{custCode}")]
        public async Task<TblSndDefCustomerMasterDto> Get([FromRoute] string custCode)
        {
            var obj = await Mediator.Send(new GetCustomerByCustomerCode() { CustomerCode = custCode, User = UserInfo() });
            return obj;
        }

    }
}
