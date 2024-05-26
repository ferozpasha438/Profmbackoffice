using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using CIN.Application;
using System.Threading.Tasks;
using CIN.Application.FomMgtQuery;
using System;
using CIN.Application.FomMgtDtos;

namespace LS.API.FOM.Controllers.ProfmAdmin
{
    public class FomWebDashboardController : BaseController
    {
        private IConfiguration _Config;

        public FomWebDashboardController(IOptions<AppSettingsJson> appSettings, IConfiguration config) : base(appSettings)
        {
            _Config = config;
        }



        [HttpGet("getWebDashboardData")]           //Web And CustomerPortal Dashboard are same
        public async Task<IActionResult> Get()
        {
            var list = await Mediator.Send(new GetWebDashboardDataQuery() { });

            return Ok(list);

        }
        [HttpPost("GetWebDashboardDataWithFilters")]
        public async Task<IActionResult> GetWebDashboardDataWithFilters([FromBody] RQDashBoardFiltersDto dto)
        {
            var obj = await Mediator.Send(new GetWebDashboardDataWithFilters() { ContractId = dto.ContractId, CustomerCode = dto.CustomerCode, SelectedDate = dto.UserDate });
            return obj is not null ? Ok(obj) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
        }




    }
}

