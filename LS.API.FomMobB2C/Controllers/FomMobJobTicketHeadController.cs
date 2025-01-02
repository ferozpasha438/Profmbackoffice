using CIN.Application;
using CIN.Application.FomMobB2CQuery;
using CIN.Application.FomMobDtos;
using CIN.Application.FomMobQuery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace LS.API.FomMobB2C.Controllers
{
    public class FomMobJobTicketHeadController : BaseController
    {
        public FomMobJobTicketHeadController(IOptions<AppSettingsJson> appSettings) : base(appSettings)
        {

        }

        [HttpPost("getB2CTicketsListPaginationWithFilter")]
        public async Task<IActionResult> GetB2CTicketsListPaginationWithFilterQuery([FromBody] InputUserTicketsPaginationFilterDto input)
        {
            var res = await Mediator.Send(new GetB2CTicketsListPaginationWithFilterQuery() { Input = input, User = UserInfo() });
            return Ok(res);
        }


        [HttpPost("getFrontOfficeB2CTicketsListPaginationWithFilter")]
        public async Task<IActionResult> GetFrontOfficeB2CTicketsListPaginationWithFilterQuery([FromBody] InputUserTicketsPaginationFilterDto input)
        {
            var res = await Mediator.Send(new GetFrontOfficeB2CTicketsListPaginationWithFilterQuery() { Input = input, User = UserInfo() });
            return Ok(res);
        }



        #region For Day Service

        [HttpPost("createUpdateB2CJobTicket")]   //only_customer from mobile
        public async Task<ActionResult> CreateUpdateB2CJobTicket([FromBody] InputCreateUpdateB2CJobTicketDto input)
        {
            input.IsFromMobile = true;
            input.IsFromWeb = false;

            var (res, message) = await Mediator.Send(new CreateUpdateB2CJobTicketHeadQuery() { Input = input, User = UserInfo() });
            if (res)
                return Ok(new ApiMessageDto { Message = message });
            else
                return BadRequest(new ApiMessageDto { Message = message });
        }

        [HttpPost("createB2CJobTicketPayment")]   //only_customer from mobile
        public async Task<ActionResult> CreateB2CJobTicketPayment([FromBody] InputCreateUpdateB2CJobTicketDto input)
        {
            var (res, message) = await Mediator.Send(new CreateB2CJobTicketPaymentQuery() { Input = input, User = UserInfo() });
            if (res)
                return Ok(new ApiMessageDto { Message = ApiMessageInfo.Success });
            else
                return BadRequest(new ApiMessageDto { Message = message });
        }

        [HttpPost("frontOfficeB2CTicketAction")]   //only_customer from mobile
        public async Task<ActionResult> FrontOfficeB2CTicketAction([FromBody] FrontOfficeB2CTicketDto input)
        {
            var res = await Mediator.Send(new FrontOfficeB2CTicketAction() { Input = input, User = UserInfo() });
            if (res.Id > 0)
                return Ok(new ApiMessageDto { Message = ApiMessageInfo.Success });
            else
                return BadRequest(new ApiMessageDto { Message = res.Message });
        }



        #endregion


        //#region For Monthly & Yearly Service


        //[HttpPost("createUpdateB2CMonthlyYearlyJobTicket")]   //only_customer from mobile
        //public async Task<ActionResult> CreateUpdateB2CMonthlyYearlyJobTicket([FromBody] InputCreateUpdateB2CJobTicketDto input)
        //{
        //    input.IsFromMobile = true;
        //    input.IsFromWeb = false;

        //    var (res, message) = await Mediator.Send(new CreateUpdateB2CJobTicketHeadQuery() { Input = input, User = UserInfo() });
        //    if (res)
        //        return Ok(new ApiMessageDto { Message = message });
        //    else
        //        return BadRequest(new ApiMessageDto { Message = message });
        //}

        //#endregion



    }
}
