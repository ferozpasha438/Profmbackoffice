using CIN.Application.FomMobDtos;
using CIN.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using CIN.Application.FomMobB2CQuery;
using CIN.Application.Common;
using CIN.Application.FomMgtDtos;
using Microsoft.AspNetCore.Http;
using System;
using Newtonsoft.Json;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using CIN.Application.SystemQuery;

namespace LS.API.FomMobB2C.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FomMobB2CServiceController : BaseController
    {
        private readonly IWebHostEnvironment _env;
        public FomMobB2CServiceController(IOptions<AppSettingsJson> appSettings, IWebHostEnvironment env) : base(appSettings)
        {
            _env = env;
        }

        [HttpGet("getDepartmentList")]
        public async Task<IActionResult> GetDepartmentListQuery()
        {
            var departments = await Mediator.Send(new GetDepartmentListQuery() { User = UserInfo() });
            return Ok(departments);
        }

        [HttpGet("getActivitiesByDepartmentList")]
        public async Task<IActionResult> GetActivitiesByDepartmentListQuery([FromQuery] string deptCode)
        {
            var departments = await Mediator.Send(new GetActivitiesByDepartmentListQuery() { DeptCode = deptCode, User = UserInfo() });
            return Ok(departments);
        }

        [HttpGet("getServiceItemsByActDeptList")]
        public async Task<IActionResult> GetServiceItemsByActDeptListQuery([FromQuery] string actCode, [FromQuery] string deptCode)
        {
            var departments = await Mediator.Send(new GetServiceItemsByActDeptListQuery() { ActCode = actCode, DeptCode = deptCode, User = UserInfo() });
            return Ok(departments);
        }

        [HttpGet("getServiceDetailsByServiceCodeList")]
        public async Task<IActionResult> GetServiceDetailsByServiceCodeListQuery([FromQuery] string serviceCode)
        {
            var departments = await Mediator.Send(new GetServiceDetailsByServiceCodeListQuery() { ServiceCode = serviceCode, User = UserInfo() });
            return Ok(departments);
        }

        [HttpGet("getServiceItemsList")]
        public async Task<IActionResult> GetServiceItemsListQuery([FromQuery] PaginationFilterDto filter)
        {
            var serviceItems = await Mediator.Send(new GetServiceItemsListQuery() { Input = filter.Values(), User = UserInfo() });
            return Ok(serviceItems);
        }

        [HttpGet("getServiceItemById/{id:int}")]
        public async Task<IActionResult> GetServiceItemByIdQuery([FromRoute] int id)
        {
            var serviceItem = await Mediator.Send(new GetServiceItemByIdQuery() { Id = id, User = UserInfo() });
            return Ok(serviceItem);
        }

        [HttpGet("getAssignResourceSelectList")]
        public async Task<IActionResult> GetAssignResourceSelectList()
        {
            var serviceItem = await Mediator.Send(new GetAssignResourceSelectList() { User = UserInfo() });
            return Ok(serviceItem);
        }
        [HttpGet("getB2CTicketCountList")]
        public async Task<IActionResult> GetB2CTicketCountListQuery()
        {
            var serviceItem = await Mediator.Send(new GetB2CTicketCountListQuery() { User = UserInfo() });
            return Ok(serviceItem);
        }


        [HttpPost("createUpdateServiceItems")]
        public async Task<IActionResult> CreateUpdateServiceItems()// [FromQuery] TblErpFomServiceItemsDto filter)
        {
            var form = HttpContext.Request.Form;
            var files = form.Files;
            var serviceItem = JsonConvert.DeserializeObject<TblErpFomServiceItemsDto>(form["input"]);
            string filePath1 = string.Empty, filePath2 = string.Empty;

            foreach (var file in files)
            {
                if (file != null && file.Length > 0)
                {
                    var guid = Guid.NewGuid().ToString();
                    string name = file.FileName;

                    string imgPath = Convert.ToString(HttpContext.Request.Form[file.Name + "_"]);
                    guid = $"{guid}_{name}_{Path.GetExtension(file.FileName)}";

                    if (imgPath == "thumb")
                    {
                        filePath1 = guid;
                    }
                    else
                    {
                        filePath2 = guid;
                    }

                    var webRoot = $"{_env.ContentRootPath}/files";
                    var filePath = Path.Combine(webRoot, guid);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
            }

            if (filePath1.HasValue())
                serviceItem.ThumbNailImagePath = filePath1;

            if (filePath2.HasValue())
                serviceItem.FullImagePath = filePath2;

            var service = await Mediator.Send(new CreateUpdateServiceItemQuery() { Input = serviceItem, User = UserInfo() });
            if (service.Id > 0)
            {
                if (serviceItem.Id > 0)
                    return NoContent();
                else
                    return Created($"get/{service.Id}", serviceItem);
            }

            return BadRequest(new ApiMessageDto { Message = service.Message });

        }

        [HttpPost("createAssignresource")]
        public async Task<IActionResult> CreateAssignresource([FromBody] AssignTicketResourceDto input)
        {
            var appRes = await Mediator.Send(new CreateAssignresourceQuery() { Input = input, User = UserInfo() });
            if (appRes.Id > 0)
            {
                return Created($"get/{appRes.Id}", input);
            }
            return BadRequest(new ApiMessageDto { Message = appRes.Message });
        }


        //[HttpPost("addLogNoteForTicket")]
        //public async Task<IActionResult> AddLogNoteForTicket([FromBody] InputTblFomJobTicketLogNoteDto input)
        //{
        //    input.IsB2c = true;
        //    var appRes = await Mediator.Send(new AddLogNoteForTicketQuery() { Note = input, User = UserInfo() });
        //    if (appRes.Id > 0)
        //    {
        //        return Created($"get/{appRes.Id}", input);
        //    }
        //    return BadRequest(new ApiMessageDto { Message = "Failed" });
        //}

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var ServiceId = await Mediator.Send(new DeleteServiceItem() { Id = id, User = UserInfo() });
            if (ServiceId > 0)
                return NoContent();
            return BadRequest(new ApiMessageDto { Message = ApiMessageInfo.Failed });
        }

    }
}
