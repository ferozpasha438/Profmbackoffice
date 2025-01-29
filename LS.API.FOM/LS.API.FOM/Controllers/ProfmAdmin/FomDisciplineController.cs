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
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LS.API.FOM.Controllers.ProfmAdmin
{
    public class FomDisciplineController: BaseController
    {
        private IConfiguration _Config;

        private readonly IWebHostEnvironment _env;

        public FomDisciplineController(IOptions<AppSettingsJson> appSettings, IConfiguration config, IWebHostEnvironment env) : base(appSettings)
        {
            _Config = config;
            _env = env;
        }



        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PaginationFilterDto filter)
        {

            var list = await Mediator.Send(new GetFomDisciplinesList() { Input = filter, User = UserInfo() });
            return Ok(list);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var obj = await Mediator.Send(new GetFomDisciplinesById() { Id = id, User = UserInfo() });
            return obj is not null ? Ok(obj) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
        }



        [HttpGet("getSelectTimePeriodsList")]
        public async Task<IActionResult> Get()
        {

            var list = await Mediator.Send(new GetSelectTimePeriods() { User = UserInfo() });
            return Ok(list);
        }


        [HttpPost("UploadThumbnailFiles")]
        public async Task<ActionResult> UploadThumbnailFiles([FromForm] InputImageThumbnailDto dTO)
        {
            var webRoot = $"{_env.ContentRootPath}/DeptImages";
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
        public async Task<ActionResult> Create([FromBody] ErpFomDepartmentDto dTO)
        {

            //var webRoot = $"{_env.ContentRootPath}/Customerfiles";
            //bool exists = System.IO.Directory.Exists(webRoot);
            //if (!exists)
            //    System.IO.Directory.CreateDirectory(webRoot);
            //if (dTO.TImage != null && dTO.TImage.Length > 0)
            //{
            //    var guid = Guid.NewGuid().ToString();
            //    string name = string.Empty;
            //    name = Path.GetFileNameWithoutExtension(dTO.TImage.FileName);
            //    guid = $"{guid}_{name}_{ Path.GetExtension(dTO.TImage.FileName)}";
            //    dTO.ThumbNailImage += guid;
            //    var filePath = Path.Combine(webRoot, guid);
            //    using (var stream = new FileStream(filePath, FileMode.Create))
            //    {
            //        dTO.TImage.CopyTo(stream);
            //    }
            //}
            //else if (dTO.Id == 0)
            //{
            //    dTO.ThumbNailImage += "default_thumb.jpg";
            //}
            //else
            //{
            //    dTO.ThumbNailImage = string.Empty;
            //}



            var id = await Mediator.Send(new CreateUpdateFomDisciplines() { FomDisciplinesDto = dTO, User = UserInfo() });
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

        [HttpGet("getDepartmentSelectList")]
        public async Task<IActionResult> GetDepartmentSelectList()
        {
            var obj = await Mediator.Send(new GetDepartmentSelectList() { User = UserInfo() });
            return obj is not null ? Ok(obj) : NotFound(ApiMessageNotFound());
        }

    }
}
