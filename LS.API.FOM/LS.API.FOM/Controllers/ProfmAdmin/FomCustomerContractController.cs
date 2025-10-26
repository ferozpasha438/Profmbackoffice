using CIN.Application;
using CIN.Application.Common;
using CIN.Application.FomMgtDtos;
using CIN.Application.FomMgtQuery;
using CIN.Application.FomMgtQuery.ProfmQuery;
using CIN.Application.ProfmQuery;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace LS.API.FOM.Controllers.ProfmAdmin
{
    public class FomCustomerContractController : BaseController
    {
        private IConfiguration _Config;
        private readonly IWebHostEnvironment _env;

        public FomCustomerContractController(IOptions<AppSettingsJson> appSettings, IConfiguration config, IWebHostEnvironment env) : base(appSettings)
        {
            _Config = config;
            _env = env;
        }


        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PaginationFilterDto filter)
        {

            var list = await Mediator.Send(new GetFomCustomerContractList() { Input = filter.Values(), User = UserInfo() });
            return Ok(list);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var obj = await Mediator.Send(new GetFomCustomerContractById() { Id = id, User = UserInfo() });
            return obj is not null ? Ok(obj) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
        }



        [HttpGet("getSelectAuthResourcesList")]
        public async Task<IActionResult> GetSelectAuthResourcesList(string search)
        {

            var list = await Mediator.Send(new CIN.Application.FomMgtQuery.ProfmQuery.GetSelectResourcesQuery() { Input = search, User = UserInfo() });
            return Ok(list);
        }



        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ErpFomCustomerContractDto dTO)
        {
            var id = await Mediator.Send(new CreateUpdateFomCustomerContract() { CustomerContractDto = dTO, User = UserInfo() });
            if (id > 0)
            {
                dTO.Id = id;
                return Created($"get/{id}", dTO);
            }
            else if (id == -1)
            {
                return BadRequest(new ApiMessageDto { Message = ApiMessageInfo.Duplicate(nameof(dTO.Id)) });
            }
            return BadRequest(new ApiMessageDto { Message = ApiMessageInfo.Failed });
        }



        [HttpPost("CreateScheduleSummary")]
        public async Task<ActionResult> CreateScheduleSummary([FromBody] ErpFomScheduleSummaryDto dTO)
        {
            var id = await Mediator.Send(new CreateUpdateSchedule() { ScheduleSummaryDto = dTO, User = UserInfo() });
            if (id > 0)
                return Created($"get/{id}", dTO);
            else if (id == -1)
            {
                return BadRequest(new ApiMessageDto { Message = ApiMessageInfo.Duplicate(nameof(dTO.Id)) });
            }
            return BadRequest(new ApiMessageDto { Message = ApiMessageInfo.Failed });
        }


        [HttpPost("GenerateScheduleSummary")]
        public async Task<ActionResult> GenerateScheduleSummary([FromBody] GenerateScheduleDto dTO)
        {
            var id = await Mediator.Send(new GenerateSchedule() { Input = dTO, User = UserInfo() });
            if (id > 0)
                return Created($"get/{id}", dTO);
            else if (id == -1)
            {
                return BadRequest(new ApiMessageDto { Message = ApiMessageInfo.Duplicate(nameof(dTO.Id)) });
            }
            return BadRequest(new ApiMessageDto { Message = ApiMessageInfo.Failed });
        }



        [HttpGet("GetFomCalenderScheduleList/{contractId}/{startDate}/{endDate}")]
        public async Task<IActionResult> GetScheduleById([FromRoute] int contractId, [FromRoute] DateTime startDate, [FromRoute] DateTime endDate)
        {
            var obj = await Mediator.Send(new GetFomCalenderScheduleList() { ContractId = contractId, StartDate = startDate, EndDate = endDate, User = UserInfo() });
            return obj is not null ? Ok(obj) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
        }
        [HttpGet("GetFomCalenderScheduleList/{contractId}/{deptCode}")]
        public async Task<IActionResult> GetScheduleById([FromRoute] int contractId, [FromRoute] string deptCode)
        {
            var obj = await Mediator.Send(new GetFomCalenderScheduleListByDeptCode() { ContractId = contractId, DeptCode = deptCode, User = UserInfo() });
            return obj is not null ? Ok(obj) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
        }
        [HttpGet("AllFomCalenderScheduleList")]
        public async Task<IActionResult> AllFomCalenderScheduleList()
        {
            var obj = await Mediator.Send(new AllFomCalenderScheduleList() { User = UserInfo() });
            return obj is not null ? Ok(obj) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
        }
        [HttpPost("CalenderScheduleList")]
        public async Task<ActionResult> CalenderScheduleList([FromBody] RQCalenderScheduleListDto dTO)
        {
            var obj = await Mediator.Send(new CalenderScheduleList() { Input = dTO, User = UserInfo() });
            return obj is not null ? Ok(obj) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
        }


        [HttpGet("GetScheduleById/{deptCode}/{contractCode}")]
        public async Task<IActionResult> GetScheduleById([FromRoute] string deptCode, [FromRoute] string contractCode)
        {
            var obj = await Mediator.Send(new GetScheduleSummaryById() { DeptCode = deptCode, ContractCode = contractCode, User = UserInfo() });
            return obj is not null ? Ok(obj) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
        }


        [HttpGet("GetGeneratedSchedule/{deptCode}/{contractCode}")]
        public async Task<IActionResult> GetGeneratedSchedule([FromRoute] string deptCode, [FromRoute] string contractCode)
        {
            var obj = await Mediator.Send(new GetGeneratedSchedule() { DeptCode = deptCode, ContractCode = contractCode, User = UserInfo() });
            return obj is not null ? Ok(obj) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
        }


        [HttpGet("GetSelectCustomerSiteByCustCode")]
        public async Task<IActionResult> GetSelectCustomerSiteByCustCode([FromQuery] string custCode)
        {
            var obj = await Mediator.Send(new GetSelectCustomerSiteByCustCode() { Code = custCode, User = UserInfo() });
            return obj is not null ? Ok(obj) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
        }

        [HttpGet("GetSelectCustomerSiteList")]
        public async Task<IActionResult> GetSelectCustomerSiteList()
        {
            var obj = await Mediator.Send(new GetSelectCustomerSiteList() { User = UserInfo() });
            return obj is not null ? Ok(obj) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
        }

        [HttpGet("GetAllSchedulingList")]
        public async Task<IActionResult> GetAllSchedulingList([FromQuery] PaginationFilterDto filter)
        {
            var list = await Mediator.Send(new GetAllSchedulingList() { Input = filter.Values(), User = UserInfo() });
            return Ok(list);
        }


        [HttpGet("GetGeneratedScheduleFilter")]
        public async Task<IActionResult> GetGeneratedScheduleFilter([FromQuery] PaginationFilterContractDto filters)
        {
            var list = await Mediator.Send(new GetGeneratedScheduleFilter() { Input = filters.Values(), User = UserInfo() });
            return Ok(list);
        }


        [HttpPost("getJobTicketReport")]
        public async Task<IActionResult> GetJobTicketReport([FromBody] InputTicketsPaginationFilterDto input)
        {
            var res = await Mediator.Send(new GetJobTicketsReportList() { Input = input, User = UserInfo() });
            return Ok(res);
        }

        [HttpPost("getCustomerAnalyticsInScope")]
        public async Task<IActionResult> GetCustomerAnalytics([FromQuery] string outscope, [FromBody] InputTicketsPaginationFilterDto input)
        {
            var res = await Mediator.Send(new GetCustomerAnalytics() { Input = input, IsInScope = outscope.HasValue() ? false : true, User = UserInfo() });
            return Ok(res);
        }


        [HttpPost("getSummaryJobTicketsReport")]
        public async Task<IActionResult> GetSummaryJobTicketsReport([FromBody] InputTicketsReportPaginationFilterDto input)
        {
            var res = await Mediator.Send(new GetSummaryJobTicketsReport() { Input = input, User = UserInfo() });
            return Ok(res);
        }

        [HttpPost("getDeptWiseSummaryJobTicketsReport")]
        public async Task<IActionResult> GetDeptWiseSummaryJobTicketsReport([FromBody] InputTicketsReportPaginationFilterDto input)
        {
            var res = await Mediator.Send(new GetDeptWiseSummaryJobTicketsReport() { Input = input, User = UserInfo() });
            return Ok(res);
        }

        [HttpPost("getProjectWiseSummaryJobTicketsReport")]
        public async Task<IActionResult> GetProjectWiseSummaryJobTicketsReport([FromBody] InputTicketsReportPaginationFilterDto input)
        {
            var res = await Mediator.Send(new GetProjectWiseSummaryJobTicketsReport() { Input = input, User = UserInfo() });
            return Ok(res);
        }



        [HttpGet("GetCustomerContractSelectList")]
        public async Task<IActionResult> GetCustomerContractSelectList()
        {
            var obj = await Mediator.Send(new GetCustomerContractSelectList() { User = UserInfo() });
            return obj is not null ? Ok(obj) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
        }
        [HttpPost("getTicketsListPaginationWithFilter")]
        public async Task<IActionResult> GetTicketsListPaginationWithFilter([FromBody] InputTicketsPaginationFilterDto input)
        {
            var res = await Mediator.Send(new GetTicketsListPaginationWithFilterQuery() { Input = input, User = UserInfo() });
            return Ok(res);
        }
        [HttpGet("getSelectJobStatusesEnum")]
        public List<CustomSelectListItem> GetSelectJobStatusesEnum()
        {

            return Enum.GetValues(typeof(MetadataJoStatusEnum))
                 .Cast<MetadataJoStatusEnum>()
                 .Select(v => new CustomSelectListItem { Text = v.ToString(), Value = ((int)v).ToString() })
                 .ToList();
        }

        [HttpGet("getSelectJobStatusesEnumForTicketList")]
        public List<CustomSelectListItem> GetSelectJobStatusesEnumForTicketList()
        {
            return Enum.GetValues(typeof(MetadataJoStatusEnum))
                 .Cast<MetadataJoStatusEnum>()
                 .Select(v => new CustomSelectListItem { Text = v.ToString(), Value = ((int)v).ToString() })
                 .Where(v => v.Text == MetadataJoStatusEnum.Approved.ToString() || v.Text == MetadataJoStatusEnum.WorkInProgress.ToString() ||
                            v.Text == MetadataJoStatusEnum.Closed.ToString() || v.Text == MetadataJoStatusEnum.Void.ToString() ||
                            v.Text == MetadataJoStatusEnum.Completed.ToString())
                 .ToList();
        }

        [HttpGet("getSelectPPTMgmtStatusEnumForPPTList")]
        public List<CustomSelectListItem> GetSelectPPTMgmtStatusEnumForPPTList()
        {
            return Enum.GetValues(typeof(PPTMgmtStatusEnum))
                 .Cast<PPTMgmtStatusEnum>()
                 .Select(v => new CustomSelectListItem { Text = v.ToString(), Value = ((int)v).ToString() })
                 .ToList();
        }


        [HttpPost("changeJobStatusForTicket")]
        public async Task<IActionResult> ChangeJobStatusForTicket([FromBody] ChangeJobStatusForTicketDto input)
        {
            //return BadRequest(new ApiMessageDto { Message = "tckt.Message" });
            var tckt = await Mediator.Send(new ChangeJobStatusForTicket() { Input = input, User = UserInfo() });
            return tckt.Id > 0 ? Ok(tckt) : BadRequest(new ApiMessageDto { Message = tckt.Message });
        }

        [HttpPost("changePptMgmtStatusForPpt")]
        public async Task<IActionResult> ChangePptMgmtStatusForPpt()
        {
            //return BadRequest(new ApiMessageDto { Message = "tckt.Message" });
            //ChangePptMgmtStatusWithFileForPptDto input
            var formData = Request.Form["input"];
            ChangePptMgmtStatusForPptDto input = JsonConvert.DeserializeObject<ChangePptMgmtStatusForPptDto>(formData);

            var files = Request.Form.Files;
            //var file = fileData.Count > 0 ? fileData[0] : null;
            foreach (var file in files)
            {
                if (file != null && file.Length > 0)
                {
                    var guid = Guid.NewGuid().ToString();
                    string fileName = file.FileName;
                    string name = file.Name;
                    string description = Convert.ToString(HttpContext.Request.Form[name]);
                    description = string.IsNullOrEmpty(description) ? file.FileName : description;

                    guid = $"{guid}_{Path.GetExtension(file.FileName)}";
                    var webRoot = $"{_env.ContentRootPath}/CustomerContractfiles";
                    var filePath = Path.Combine(webRoot, guid);

                    if (name == "fileone")
                    {
                        input.ImageName = fileName;
                        input.ImageUrl = guid;
                    }
                    else if (name == "filetwo")
                    {
                        input.Image1Name = fileName;
                        input.Image1Url = guid;
                    }
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
            }
            var tckt = await Mediator.Send(new ChangePptMgmtStatusForPpt() { Input = input, User = UserInfo() });
            return tckt.Id > 0 ? Ok(tckt) : BadRequest(new ApiMessageDto { Message = tckt.Message });
        }


        [HttpPost("getWorkOrderListPaginationWithFilter")]
        public async Task<IActionResult> getWorkOrderListPaginationWithFilter([FromBody] InputTicketsPaginationFilterDto input)
        {
            var res = await Mediator.Send(new getWorkOrderListPaginationWithFilterQuery() { Input = input, User = UserInfo() });
            return Ok(res);
        }
        [HttpGet("viewTicketByTicketNumber/{ticketNumber}")]
        public async Task<IActionResult> ViewTicketById([FromRoute] string ticketNumber)
        {
            string ImageUrl = _Config["AppSettings:ImagesUrl"].Replace('\\', '/');
            var tckt = await Mediator.Send(new ViewTicketQuery() { Id = 0, WebRoot = ImageUrl, TicketNumber = ticketNumber });

            //if (tckt is not null)
            //{
            //    if (!tckt.IsRead && UserInfo().LoginType == "user")
            //    {
            //        int minResponseTime = int.Parse(_Config["AppSettings:MinResponseTime"]);
            //        var (obj, message) = await Mediator.Send(new ReadJobTicketQuery() { TicketNumber = ticketNumber, MinResposeTime = minResponseTime, User = UserInfo() });
            //        if (obj is not null)
            //        {
            //            tckt.IsRead = true;
            //        }
            //    }
            //}
            return tckt is not null ? Ok(tckt) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
        }

        [HttpGet("viewScheduleById/{id}")]
        public async Task<IActionResult> ViewScheduleById([FromRoute] int id)
        {
            var tckt = await Mediator.Send(new ViewScheduleById() { Id = id });
            return tckt is not null ? Ok(tckt) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
        }

        [HttpGet("viewWorkOrderByTicketNumber/{ticketNumber}")]
        public async Task<IActionResult> ViewWorkOrderById([FromRoute] string ticketNumber)
        {
            string ImageUrl = _Config["AppSettings:ImagesUrl"].Replace('\\', '/');
            var tckt = await Mediator.Send(new ViewWorkOrderQuery() { Id = 0, WebRoot = ImageUrl, TicketNumber = ticketNumber });

            //if (tckt is not null)
            //{
            //    if (!tckt.IsRead && UserInfo().LoginType == "user")
            //    {
            //        int minResponseTime = int.Parse(_Config["AppSettings:MinResponseTime"]);
            //        var (obj, message) = await Mediator.Send(new ReadJobTicketQuery() { TicketNumber = ticketNumber, MinResposeTime = minResponseTime, User = UserInfo() });
            //        if (obj is not null)
            //        {
            //            tckt.IsRead = true;
            //        }
            //    }
            //}
            return tckt is not null ? Ok(tckt) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
        }


        [HttpPost("UploadCustomerContractFiles")]
        public async Task<ActionResult> UploadCustomerContractFiles([FromForm] InputImageFromCustomerDto dTO)
        {
            var webRoot = $"{_env.ContentRootPath}/CustomerContractfiles";
            bool exists = System.IO.Directory.Exists(webRoot);
            if (!exists)
                System.IO.Directory.CreateDirectory(webRoot);
            var (res, message) = await Mediator.Send(new UploadCustomerContractFiles() { Input = dTO, WebRoot = webRoot, User = UserInfo() });
            if (res)
                return Ok(new ApiMessageDto { Message = ApiMessageInfo.Success });
            else
                return BadRequest(new ApiMessageDto { Message = message });
        }


        [HttpGet("GetActivitiesByDeptCodes/{codes}/{contarctCode}")]
        public async Task<IActionResult> GetActivitiesByDeptCodes([FromRoute] string codes, string contarctCode)
        {
            var deptCodes = codes.Split(','); // Splitting the codes by comma
            var obj = await Mediator.Send(new GetDeptActivitiesByDeptCodes() { DeptCodes = deptCodes, ContractCode = contarctCode, User = UserInfo() });
            return obj is not null ? Ok(obj) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
        }

        //[HttpGet("GetActivitiesByDeptCodes/{codes}")]
        //public async Task<IActionResult> Get([FromRoute] string codes)
        //{
        //    if (string.IsNullOrEmpty(codes))
        //    {
        //        return BadRequest(new ApiMessageDto { Message = "Department codes are required." });
        //    }

        //    // Splitting the comma-separated string into an array of department codes


        //    var obj = await Mediator.Send(new GetDeptActivitiesByDeptCodes
        //    {
        //        DeptCodes = codes.Split(','),  // Pass the array of department codes
        //        User = UserInfo()       // Assuming you're getting user info from a helper method
        //    });

        //    return obj is not null ? Ok(obj) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
        //}


        [HttpPost("createChkUnChkContDeptActivity")]
        public async Task<ActionResult> Create([FromBody] List<TblErpFomContractDeptActDto> dtoList)
        {
            if (dtoList == null || !dtoList.Any())
                return BadRequest(new ApiMessageDto { Message = "No data provided." });

            var result = await Mediator.Send(new CreateChkUnChkContDeptActivity { InputList = dtoList, User = UserInfo() });

            if (result > 0)
                return Created("createChkUnChkContDeptActivity", dtoList);
            else
                return BadRequest(new ApiMessageDto { Message = ApiMessageInfo.Failed });
        }








        //[HttpPost("createChkUnChkContDeptActivity")]
        //public async Task<ActionResult> Create([FromBody] TblErpFomContractDeptActDto dTO)
        //{
        //    var id = await Mediator.Send(new CreateChkUnChkContDeptActivity() { Input = dTO, User = UserInfo() });
        //    if (id > 0)
        //        return Created($"get/{id}", dTO);
        //    else if (id == -1)
        //    {
        //        return BadRequest(new ApiMessageDto { Message = ApiMessageInfo.Duplicate(nameof(dTO.Id)) });
        //    }
        //    return BadRequest(new ApiMessageDto { Message = ApiMessageInfo.Failed });
        //}
    }
}

