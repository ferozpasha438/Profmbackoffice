using CIN.Application;
using CIN.Application.Common;
using CIN.Application.FomMgtDtos;
using CIN.Application.FomMgtQuery.ProfmQuery;
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
    public class FomActivitiesController :BaseController
    {
        private IConfiguration _Config;
      
        private readonly IWebHostEnvironment _env;
        public FomActivitiesController(IOptions<AppSettingsJson> appSettings, IConfiguration config, IWebHostEnvironment env) : base(appSettings)
        {
            _Config = config;
            _env = env;
        }


        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PaginationFilterDto filter)
        {

            var list = await Mediator.Send(new GetFomActivitiesList() { Input = filter, User = UserInfo() });
            return Ok(list);
        }

        [HttpGet("GetActivitiesByDeptCode/{code}")]
        public async Task<IActionResult> Get([FromRoute] string code)
        {
            var obj = await Mediator.Send(new GetActivitiesByDeptCode() { DeptCode = code, User = UserInfo() });
            return obj is not null ? Ok(obj) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
        }

        [HttpGet("GetGetActivitityListByDeptCode")]
        public async Task<IActionResult> GetGetActivitityListByDeptCode([FromQuery] PaginationFilterDto filter)
        {

            var list = await Mediator.Send(new GetFomActivitiesListByDeptCode() { Input = filter, User = UserInfo() });
            return Ok(list);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var obj = await Mediator.Send(new GetFomActivitiesById() { Id = id, User = UserInfo() });
            return obj is not null ? Ok(obj) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
        }

        [HttpPost("UploadThumbnailFiles")]
        public async Task<ActionResult> UploadThumbnailFiles([FromForm] InputImageThumbnailDto dTO)
        {
            var webRoot = $"{_env.ContentRootPath}/ActImages";
            bool exists = System.IO.Directory.Exists(webRoot);
            if (!exists)
                System.IO.Directory.CreateDirectory(webRoot);
            var (res, message) = await Mediator.Send(new UploadThumbnailFiles() { Input = dTO, WebRoot = webRoot, User = UserInfo() });
            if (res)
                return Ok(new ApiMessageDto { Message = ApiMessageInfo.Success });
            else
                return BadRequest(new ApiMessageDto { Message = message });
        }


        [HttpPost]
        public async Task<ActionResult> Create([FromBody] TblErpFomActivitiesDto dTO)
        {
            var id = await Mediator.Send(new CreateUpdateFomActivities() { FomActivitiesDto = dTO, User = UserInfo() });
            if (id > 0)
            {
                dTO.Id = id;
                return Created($"get/{id}", dTO);
            }                
            else if (id == -1)
            {
                return BadRequest(new ApiMessageDto { Message = ApiMessageInfo.Duplicate(nameof(dTO.Id)) });
            }
            return BadRequest(new ApiMessageDto { Message = ApiMessageInfo.Failed });
        }

    }
}
