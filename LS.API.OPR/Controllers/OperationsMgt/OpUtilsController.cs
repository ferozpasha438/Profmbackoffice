using CIN.Application;
using CIN.Application.Common;
using CIN.Application.OperationsMgtDtos;
using CIN.Application.OperationsMgtQuery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LS.API.OPR.Controllers.OperationsMgt
{
    public class OpUtilsController : BaseController
    {

        public OpUtilsController(IOptions<AppSettingsJson> appSettings) : base(appSettings)
        {
        }

     
    




        [HttpPost("RefreshOffs")]
        public async Task<ActionResult> RefreshOffs([FromBody] RefreshOffsDto Dto)
        {
            var id = await Mediator.Send(new RefreshOffs() { User = UserInfo(),dto=Dto });
            if (id > 0)
                return Ok(new ApiMessageDto { Message = ApiMessageInfo.Success });
            
            return BadRequest(new ApiMessageDto { Message = "Offs Updation Failed" });
        }


    }
}
