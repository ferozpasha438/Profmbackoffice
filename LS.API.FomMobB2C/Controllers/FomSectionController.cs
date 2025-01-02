using CIN.Application.Common;
using CIN.Application.FomMgtDtos;
using CIN.Application.FomMgtQuery.ProfmQuery;
using CIN.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using CIN.Application.FomMgtQuery;

namespace LS.API.FomMobB2C.Controllers
{
    public class FomSectionController : BaseController
    {
        public FomSectionController(IOptions<AppSettingsJson> appSettings) : base(appSettings)
        {

        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PaginationFilterDto filter)
        {
            var list = await Mediator.Send(new GetFomSectionList() { Input = filter.Values(), User = UserInfo() });
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var list = await Mediator.Send(new GetFomSectionById() { Id = id, User = UserInfo() });
            return Ok(list);
        }

        [HttpGet("getSectiomSelectList")]
        public async Task<IActionResult> GetSectiomSelectList([FromQuery] bool? isForAssetMgt)
        {
            var obj = await Mediator.Send(new GetSectiomSelectList() { IsForAssetMgt = isForAssetMgt, User = UserInfo() });
            return obj is not null ? Ok(obj) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
        }

        [HttpPost("createUpdateFomSection")]
        public async Task<ActionResult> CreateUpdateFomSection([FromBody] TblErpFomSectionDto input)
        {
            var res = await Mediator.Send(new CreateUpdateFomSection() { Input = input, User = UserInfo() });
            var msg = new ApiMessageDto { Message = res.Message };

            if (res.Id > 0)
                return input.Id > 0 ? NoContent() : Created($"get/{input.Id}", input);
            else
                return BadRequest(msg);
        }

        [HttpDelete("deletesection/{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var BranchId = await Mediator.Send(new DeleteSection() { Id = id, User = UserInfo() });
            if (BranchId > 0)
                return NoContent();
            return BadRequest(new ApiMessageDto { Message = ApiMessageInfo.Failed });
        }
    }
}
