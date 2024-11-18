using CIN.Application;
using CIN.Application.Common;
using CIN.Application.FomMgtQuery;
using CIN.Application.FomMgtQuery.ProfmQuery;
using CIN.Application.FomMobB2CDtos;
using CIN.Application.FomMobB2CQuery;
using CIN.Application.FomMobDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Org.BouncyCastle.Ocsp;
using System;
using System.Threading.Tasks;

namespace LS.API.FomMobB2C.Controllers
{
    public class FomCustomerContractController : BaseController
    {
        private IConfiguration _Config;

        public FomCustomerContractController(IOptions<AppSettingsJson> appSettings, IConfiguration config) : base(appSettings)
        {
            _Config = config;
        }


        #region creating customercontract and Scheduling Tasks        

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ErpFomCustContractScheduleSummaryDto dTO)//ErpFomCustomerContractDto 
        {
            var res = await Mediator.Send(new CIN.Application.FomMobB2CQuery.CreateUpdateFomCustomerContract() { Input = dTO, User = UserInfo() });
            if (res.Id > 0)
                return Ok(res.Id); //Created($"get/{res.Id}", dTO);
            //else if (id == -1)
            //{
            //    return BadRequest(new ApiMessageDto { Message = ApiMessageInfo.Duplicate(nameof(dTO.Id)) });
            //}
            return BadRequest(new ApiMessageDto { Message = res.Message });
        }

        [HttpGet("getB2CFrontOfficeSchedulingList")]
        public async Task<IActionResult> GetB2CFrontOfficeSchedulingList([FromQuery] PaginationFilterDto filter)
        {
            var list = await Mediator.Send(new GetB2CFrontOfficeSchedulingList() { Input = filter.Values(), User = UserInfo() });
            return Ok(list);
        }

        [HttpGet("allB2CFomCalenderScheduleList")]
        public async Task<IActionResult> AllB2CFomCalenderScheduleList([FromQuery] PaginationFilterDto filter)
        {
            var list = await Mediator.Send(new AllB2CFomCalenderScheduleList() {User = UserInfo() });
            return Ok(list);
        }

        [HttpGet("getCustomerContractSelectList")]
        public async Task<IActionResult> GetCustomerContractSelectList()
        {
            var obj = await Mediator.Send(new CIN.Application.FomMobB2CQuery.GetCustomerContractSelectList() { User = UserInfo() });
            return obj is not null ? Ok(obj) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
        }
        
        [HttpGet("getLanCustomerContractSelectList")]
        public async Task<IActionResult> GetLanCustomerContractSelectList()
        {
            var obj = await Mediator.Send(new CIN.Application.FomMobB2CQuery.GetLanCustomerContractSelectList() { User = UserInfo() });
            return obj is not null ? Ok(obj) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
        }

        [HttpGet("getDefaultPaymentPrices")]
        public async Task<IActionResult> GetDefaultPaymentPrices([FromQuery] string type)
        {
            var list = await Mediator.Send(new GetB2CDefaultPaymentPricesQuery() { Type = type, User = UserInfo() });
            return Ok(list);
        }

        [HttpPost("createScheduleB2CJobTicketPayment")]   //only_customer from mobile
        public async Task<ActionResult> CreateScheduleB2CJobTicketPayment([FromBody] InputCreateUpdateB2CJobTicketDto input)
        {
            var (res, message) = await Mediator.Send(new CreateB2CJobTicketPaymentQuery() { Input = input, User = UserInfo() });
            if (res)
                return Ok(new ApiMessageDto { Message = ApiMessageInfo.Success });
            else
                return BadRequest(new ApiMessageDto { Message = message });
        }


        #endregion


        //[HttpGet]
        //public async Task<IActionResult> Get([FromQuery] PaginationFilterDto filter)
        //{

        //    var list = await Mediator.Send(new GetFomCustomerContractList() { Input = filter.Values(), User = UserInfo() });
        //    return Ok(list);
        //}


        //[HttpGet("{id}")]
        //public async Task<IActionResult> Get([FromRoute] int id)
        //{
        //    var obj = await Mediator.Send(new GetFomCustomerContractById() { Id = id, User = UserInfo() });
        //    return obj is not null ? Ok(obj) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
        //}



        //[HttpGet("getSelectAuthResourcesList")]
        //public async Task<IActionResult> GetSelectAuthResourcesList(string search)
        //{

        //    var list = await Mediator.Send(new GetSelectResourcesQuery() { Input = search, User = UserInfo() });
        //    return Ok(list);
        //}


        //[HttpPost("CreateScheduleSummary")]
        //public async Task<ActionResult> CreateScheduleSummary([FromBody] ErpFomScheduleSummaryDto dTO)
        //{
        //    var id = await Mediator.Send(new CreateUpdateSchedule() { ScheduleSummaryDto = dTO, User = UserInfo() });
        //    if (id > 0)
        //        return Created($"get/{id}", dTO);
        //    else if (id == -1)
        //    {
        //        return BadRequest(new ApiMessageDto { Message = ApiMessageInfo.Duplicate(nameof(dTO.Id)) });
        //    }
        //    return BadRequest(new ApiMessageDto { Message = ApiMessageInfo.Failed });
        //}


        //[HttpPost("GenerateScheduleSummary")]
        //public async Task<ActionResult> GenerateScheduleSummary([FromBody] GenerateScheduleDto dTO)
        //{
        //    var id = await Mediator.Send(new GenerateSchedule() { Input = dTO, User = UserInfo() });
        //    if (id > 0)
        //        return Created($"get/{id}", dTO);
        //    else if (id == -1)
        //    {
        //        return BadRequest(new ApiMessageDto { Message = ApiMessageInfo.Duplicate(nameof(dTO.Id)) });
        //    }
        //    return BadRequest(new ApiMessageDto { Message = ApiMessageInfo.Failed });
        //}



        //[HttpGet("GetFomCalenderScheduleList/{contractId}/{startDate}/{endDate}")]
        //public async Task<IActionResult> GetScheduleById([FromRoute] int contractId, [FromRoute] DateTime startDate, [FromRoute] DateTime endDate)
        //{
        //    var obj = await Mediator.Send(new GetFomCalenderScheduleList() { ContractId = contractId, StartDate = startDate, EndDate = endDate, User = UserInfo() });
        //    return obj is not null ? Ok(obj) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
        //}


        //[HttpGet("GetScheduleById/{deptCode}/{contractCode}")]
        //public async Task<IActionResult> GetScheduleById([FromRoute] string deptCode, [FromRoute] string contractCode)
        //{
        //    var obj = await Mediator.Send(new GetScheduleSummaryById() { DeptCode = deptCode, ContractCode = contractCode, User = UserInfo() });
        //    return obj is not null ? Ok(obj) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
        //}


        //[HttpGet("GetGeneratedSchedule/{deptCode}/{contractCode}")]
        //public async Task<IActionResult> GetGeneratedSchedule([FromRoute] string deptCode, [FromRoute] string contractCode)
        //{
        //    var obj = await Mediator.Send(new GetGeneratedSchedule() { DeptCode = deptCode, ContractCode = contractCode, User = UserInfo() });
        //    return obj is not null ? Ok(obj) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
        //}


        //[HttpGet("GetSelectCustomerSiteByCustCode")]
        //public async Task<IActionResult> GetSelectCustomerSiteByCustCode([FromQuery] string custCode)
        //{
        //    var obj = await Mediator.Send(new GetSelectCustomerSiteByCustCode() { Code = custCode, User = UserInfo() });
        //    return obj is not null ? Ok(obj) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
        //}


    }
}

