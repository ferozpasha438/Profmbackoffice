using CIN.Application;
using CIN.Application.Common;
using CIN.Application.OperationsMgtDtos;
using CIN.Application.OperationsMgtQuery;
using CIN.Domain.OpeartionsMgt;
using CIN.Domain.SystemSetup;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LS.API.OPR.Controllers.OperationsMgt
{
    public class EmployeeTransResignController : BaseController
    {

        public EmployeeTransResignController(IOptions<AppSettingsJson> appSettings) : base(appSettings)
        {
        }






        [HttpPost("enterEmployeeTransResign")]
        public async Task<ActionResult> EnterEmployeeTransResign([FromBody] TblOpEmployeeTransResignDto dto)
        {
            var id = await Mediator.Send(new EnterEmployeeTransResign() { Input = dto, User = UserInfo() });
            if (id > 0)
            {
                return Created($"get/{id}", dto);
            }
            else if (id == -1)
            {
                return BadRequest(new ApiMessageDto { Message = "Employee_Attendance_Already_Exist_On_Selected_Day" });

            }

            return BadRequest(new ApiMessageDto { Message = ApiMessageInfo.Failed });

        }


         




       

        

    



    



    }



}
