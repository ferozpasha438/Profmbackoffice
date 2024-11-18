using CIN.Application;
using CIN.Application.FomMobB2CQuery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.API.FomMobB2C.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicePeriodsController : BaseController
    {
        private readonly IOptions<AppSettingsJson> _appSettings;

        public ServicePeriodsController(IOptions<AppSettingsJson> appSettings) : base(appSettings)
        {
            _appSettings = appSettings;
        }

        [HttpGet("getAllActiveFomServicePeriodsForB2C")]
        public async Task<IActionResult> GetAllActiveFomServicePeriodsForB2C()
        {
            var departments = await Mediator.Send(new GetAllActiveFomServicePeriodsForB2C() { ImagesUrl = _appSettings.Value.ImagesUrl, User = UserInfo() });
            return Ok(departments);
        }

    }
}
