using CIN.Application;
using CIN.Application.Common;
using CIN.Application.FomMobB2CDtos;
using CIN.Application.FomMobB2CQuery;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace LS.API.FomMobB2C.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FomMobB2CReportController : BaseController
    {
        private readonly IWebHostEnvironment _env;
        public FomMobB2CReportController(IOptions<AppSettingsJson> appSettings, IWebHostEnvironment env) : base(appSettings)
        {
            _env = env;
        }

        [HttpGet("getB2CTickethistory")]
        public async Task<IActionResult> GetB2CTickethistory([FromQuery] B2CReportTicketSearchDto filter)
        {
            var tickets = await Mediator.Send(new GetB2CTickethistoryQuery() { Input = filter, User = UserInfo() });
            return Ok(tickets);
        }

        [HttpGet("getB2CTicketSummarybycust")]
        public async Task<IActionResult> GetB2CTicketSummarybycust([FromQuery] B2CReportTicketSearchDto filter)
        {
            var tickets = await Mediator.Send(new GetB2CTicketSummarybycustQuery() { Input = filter, User = UserInfo() });
            return Ok(tickets);
        }
        [HttpGet("getB2CTicketSummarybyservicetype")]
        public async Task<IActionResult> GetB2CTicketSummarybyservicetype([FromQuery] B2CReportTicketSearchDto filter)
        {
            var tickets = await Mediator.Send(new GetB2CTicketSummarybyservicetypeQuery() { Input = filter, User = UserInfo() });
            return Ok(tickets);
        }

    }
}
