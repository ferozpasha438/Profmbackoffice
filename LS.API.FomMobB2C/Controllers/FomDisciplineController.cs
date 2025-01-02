using CIN.Application;
using CIN.Application.FomMgtQuery;
using CIN.Application.FomMgtQuery.ProfmQuery;
using CIN.Application.ProfmQuery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace LS.API.FomMobB2C.Controllers
{
    public class FomDisciplineController : BaseController
    {
        public FomDisciplineController(IOptions<AppSettingsJson> appSettings) : base(appSettings)
        {
        }

        [HttpGet("getDepartmentSelectList")]
        public async Task<IActionResult> GetDepartmentSelectList()
        {
            var obj = await Mediator.Send(new GetDepartmentSelectList() { User = UserInfo() });
            return obj is not null ? Ok(obj) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
        }

        [HttpGet("getSelectResourceTypesQuery")]
        public async Task<IActionResult> GetSelectResourceTypesQuery()
        {
            var obj = await Mediator.Send(new GetSelectResourceTypesQuery() { User = UserInfo() });
            return obj is not null ? Ok(obj) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
        }


    }
}
