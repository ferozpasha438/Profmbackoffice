using CIN.Application;
using CIN.Application.Common;
using CIN.Application.FomMgtQuery.ProfmQuery;
using CIN.Application.GeneralLedgerQuery;
using CIN.Application.OperationsMgtQuery;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace LS.API.FomMobB2C.Controllers
{
    public class AssetMaintenanceReportController : BaseController
    {
        public AssetMaintenanceReportController(IOptions<AppSettingsJson> appSettings) : base(appSettings)
        {
        }

        [HttpGet("getSelectCustomerList")]
        public async Task<IActionResult> GetSelectCustomerList([FromQuery] bool? isPayment)
        {
            var obj = await Mediator.Send(new GetCustomersCustomList() { IsPayment = isPayment });
            return obj is not null ? Ok(obj) : NotFound(ApiMessageNotFound());
        }

        [HttpGet("getProjectCodesByCustomerCode")]
        public async Task<IActionResult> GetProjectCodesByCustomerCode([FromQuery] int customerId)
        {
            var list = await Mediator.Send(new GetProjectSelectListByCustomerCode() { CustomerId = customerId, User = UserInfo() });
            return Ok(list);
        }

        [HttpGet("cafmDayWiseDetails")]
        public async Task<IActionResult> CafmDayWiseDetails([FromQuery] string customerCode, [FromQuery] string projCode, [FromQuery] string deptCode, [FromQuery] DateTime? from, [FromQuery] DateTime? to, [FromQuery] string status)
        {
            var list = await Mediator.Send(new CafmDayWiseDetails() { CustomerCode = customerCode, ProjCode = projCode, DeptCode = deptCode, From = from, To = to, Status = status, User = UserInfo() });
            return Ok(list);
        }

        [HttpGet("cafmDayWiseSummary")]
        public async Task<IActionResult> CafmDayWiseSummary([FromQuery] string customerCode, [FromQuery] string projCode, [FromQuery] DateTime? from, [FromQuery] DateTime? to, [FromQuery] string projectwise)
        {
            var list = await Mediator.Send(new CafmDayWiseSummary() { CustomerCode = customerCode, ProjCode = projCode, From = from, To = to, Projectwise = projectwise, User = UserInfo() });
            return Ok(list);
        }

        [HttpGet("cafmassetdetails")]
        public async Task<IActionResult> Cafmassetdetails([FromQuery] string customerCode, [FromQuery] string projCode)
        {
            var list = await Mediator.Send(new Cafmassetdetails() { CustomerCode = customerCode, ProjCode = projCode, User = UserInfo() });
            return Ok(list);
        }

        [HttpGet("cafmassetcostanalysis")]
        public async Task<IActionResult> Cafmassetcostanalysis([FromQuery] string customerCode, [FromQuery] string projCode, [FromQuery] DateTime? from, [FromQuery] DateTime? to)
        {
            var list = await Mediator.Send(new Cafmassetcostanalysis() { CustomerCode = customerCode, ProjCode = projCode, From = from, To = to, User = UserInfo() });
            return Ok(list);
        }

    }
}
