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
    public class FomCustomerController : BaseController
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


        //[HttpPost("CreateUpdateMultiLoginCustomer")]
        //public async Task<ActionResult> CreateUpdateMultiLoginCustomer([FromBody] TblErpFomUserClientLoginMappingDto dTO)
        //{
        //    var id = await Mediator.Send(new CreateUpdateMultiLoginCustomer() { FomUserClientLoginMapping = dTO, User = UserInfo() });
        //    if (id > 0)
        //        return Created($"get/{id}", dTO);
        //    else if (id == -1)
        //    {
        //        return BadRequest(new ApiMessageDto { Message = ApiMessageInfo.Duplicate(nameof(dTO.Id)) });
        //    }
        //    return BadRequest(new ApiMessageDto { Message = ApiMessageInfo.Failed });
        //}

        //[HttpPost("CreateUpdateMultiLoginCustomer")]
        //public async Task<ActionResult> CreateUpdateMultiLoginCustomer([FromBody] List<TblErpFomUserClientLoginMappingDto> dtoList)
        //// Change to List
        //{
        //    foreach (var dTO in dtoList)
        //    {
        //        var id = await Mediator.Send(new CreateUpdateMultiLoginCustomer()
        //        {
        //            FomUserClientLoginMapping = dTO,
        //            User = UserInfo()
        //        });

        //        if (id <= 0)
        //        {
        //            return BadRequest(new ApiMessageDto { Message = ApiMessageInfo.Failed });
        //        }
        //    }

        //    return Ok(); // Return a general success response
        //}

        [HttpGet("checkLoginCodeExists")]
        public async Task<IActionResult> CheckLoginCodeExists([FromQuery] string loginCode, [FromQuery] int id)
        {
            var obj = await Mediator.Send(new CheckLoginCodeExists() { Id = id, LoginCode = loginCode, User = UserInfo() });
            return Ok(obj);
        }



        [HttpPost("CreateUpdateMultiLoginCustomer")]
        public async Task<ActionResult> CreateUpdateMultiLoginCustomer([FromBody] List<TblErpFomUserClientLoginMappingDto> dtoList)
        {
            var request = new CreateUpdateMultiLoginCustomer
            {
                FomUserClientLoginMappingList = dtoList,
                User = UserInfo()
            };

            var result = await Mediator.Send(request);

            if (result > 0)
                return Ok();
            else
                return BadRequest(new ApiMessageDto { Message = ApiMessageInfo.Failed });
        }

        [HttpPost("CreateMappingLoginCodesToSites")]
        public async Task<ActionResult> CreateMappingLoginCodesToSites([FromBody] TblErpFomUserClientLoginMappingDto dtoList)

        {
            var result = await Mediator.Send(new CreateMappingLoginCodesToSites { Input = dtoList, User = UserInfo() });
            if (result.Id > 0)
                return Ok();
            else
                return BadRequest(new ApiMessageDto { Message = result.Message });
        }


        [HttpDelete("DeleteMultiLoginCustomer/{id}")]
        public async Task<IActionResult> DeleteMultiLoginCustomer(int id)
        {
            var result = await Mediator.Send(new DeleteMultiLoginCustomer { Id = id });

            if (result)
            {
                return Ok(new ApiMessageDto { Message = ApiMessageInfo.Success });
            }
            else
            {
                return BadRequest(new ApiMessageDto { Message = ApiMessageInfo.Failed });
            }
        }

        [HttpGet("GetClientsByCustomerCode/{custCode}")]
        public async Task<List<TblErpFomUserClientLoginMappingDto>> GetClientsByCustomerCode([FromRoute] string custCode)
        {
            var obj = await Mediator.Send(new GetClientsByCustomerCode() { CustomerCode = custCode, User = UserInfo() });
            return obj;
        }




        [HttpPost("UploadCustomerFiles")]
        public async Task<ActionResult> UploadCustomerFiles([FromForm] InputImageFromCustomerDto dTO)
        {
            var webRoot = $"{_env.ContentRootPath}/Customerfiles";
            bool exists = System.IO.Directory.Exists(webRoot);
            if (!exists)
                System.IO.Directory.CreateDirectory(webRoot);
            var (res, message) = await Mediator.Send(new UploadCustomerFiles() { Input = dTO, WebRoot = webRoot, User = UserInfo() });
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

        [HttpGet("getSelectCustomerLoginList")]
        public async Task<IActionResult> GetSelectCustomerLoginList([FromQuery] string custCode)
        {
            var obj = await Mediator.Send(new GetSelectCustomerLoginList() { CustCode = custCode, User = UserInfo() });
            return obj is not null ? Ok(obj) : NotFound(ApiMessageNotFound());
        }

        [HttpGet("getSelectSitesByCustomerLoginCode")]
        public async Task<IActionResult> GetSelectSitesByCustomerLoginCode([FromQuery] string custCode, [FromQuery] string loginCode)
        {
            var obj = await Mediator.Send(new GetSelectSitesByCustomerLoginCode() { CustCode = custCode, LoginCode = loginCode, User = UserInfo() });
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
