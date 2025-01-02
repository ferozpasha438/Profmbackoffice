using CIN.Application;
using CIN.Application.FomMgtQuery;
using CIN.Application.FomMgtQuery.ProfmQuery;
using CIN.Application.ProfmQuery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace LS.API.FomMobB2C.Controllers
{
    public class FomActivitiesController : BaseController
    {
        public FomActivitiesController(IOptions<AppSettingsJson> appSettings) : base(appSettings)
        {
        }

        [HttpGet("GetActivitiesByDeptCode/{code}")]
        public async Task<IActionResult> Get([FromRoute] string code)
        {
            var obj = await Mediator.Send(new GetActivitiesByDeptCode() { DeptCode = code, User = UserInfo() });
            return obj is not null ? Ok(obj) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
        }


    }
}
