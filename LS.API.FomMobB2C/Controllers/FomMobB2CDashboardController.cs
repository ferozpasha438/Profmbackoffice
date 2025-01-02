using AutoMapper.Configuration;
using CIN.Application;
using CIN.Application.Common;
using CIN.Application.FomMgtQuery;
using CIN.Application.FomMobB2CDtos;
using CIN.Application.FomMobB2CQuery;
using CIN.Application.FomMobQuery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace LS.API.FomMobB2C.Controllers
{
    public class FomMobB2CDashboardController : BaseController
    {
       

        public FomMobB2CDashboardController(IOptions<AppSettingsJson> appSettings) : base(appSettings)
        {
            
        }

        [HttpGet("getCustomers")]
        public async Task<IActionResult> Get([FromQuery] PaginationFilterDto filter)
        {

            var list = await Mediator.Send(new GetFomCustomerMasterList() { Input = filter, User = UserInfo() });
            return Ok(list);
        }

        [HttpGet("getWebDashboardData")]           //Web And CustomerPortal Dashboard are same
        public async Task<IActionResult> Get()
        {
            var list = await Mediator.Send(new GetWebDashboardData1Query() { });

            return Ok(list);

        }

        [HttpPost("GetWebDashboardDataWithFilters")]
        public async Task<IActionResult> GetWebDashboardDataWithFilters([FromBody] RQDashBoardFiltersDto dto)
        {
            var obj = await Mediator.Send(new GetWebDashboardData1WithFilters() { ContractId = dto.ContractId, CustomerCode = dto.CustomerCode, SelectedDate = dto.UserDate });
            return obj is not null ? Ok(obj) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
        }

        //[HttpGet("GetCustomerContractSelectList")]
        //public async Task<IActionResult> GetCustomerContractSelectList()
        //{
        //    var obj = await Mediator.Send(new GetCustomerContractSelectList() { User = UserInfo() });
        //    return obj is not null ? Ok(obj) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
        //}


    }
}
