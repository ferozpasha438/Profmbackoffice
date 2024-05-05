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
    public class OperationsDashboardController : BaseController
    {

        public OperationsDashboardController(IOptions<AppSettingsJson> appSettings) : base(appSettings)
        {
        }

     

        [HttpGet("getOpeartionsDashboard")]
        public async Task<IActionResult> GetOpeartionsDashboard()
        {
            var obj = await Mediator.Send(new GetOpeartionsDashboard() { User = UserInfo() });
            return obj is not null ? Ok(obj) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
        }

          [HttpPost("getOpeartionsDashboardByFilter")]
        public async Task<IActionResult> GetOpeartionsDashboardByFilter([FromBody] OperationsDashboardIpDto input)
        {
           
            if (input.DashBoardSubType == "operations")
            {
              var  DashBoard = await Mediator.Send(new GetOpeartionsDashboardByFilter() { Input = input, User = UserInfo() });
                return Ok(DashBoard);
            }
            else if (input.DashBoardSubType == "management")
            {
               var DashBoard = await Mediator.Send(new GetOpeartionsManagementDashboardByFilter() { Input = input, User = UserInfo() });
                return Ok(DashBoard);
            }
            return BadRequest();
        }
        
      
        [HttpGet("getFilterOptions")]
        public List<OperationsDashboardFilterOptionsDto> FilterOptions()
        {
                List<OperationsDashboardFilterOptionsDto> filterOptions = new();
                filterOptions.Add(new()
                {
                    Key = "late",
                    IsSelected = false,
                });
                filterOptions.Add(new()
                {
                    Key = "arrive",
                    IsSelected = false,
                });
                filterOptions.Add(new()
                {
                    Key = "break",
                    IsSelected = false,
                });
                filterOptions.Add(new()
                {
                    Key = "out Of geofence",
                    IsSelected = false,
                });

            filterOptions.Add(new()
            {
                Key = "out of geofence count>0",
                IsSelected = false,
            });

            filterOptions.Add(new()
                {
                    Key = "overtime",
                    IsSelected = false,
                });

                filterOptions.Add(new()
                {
                    Key = "logged out",
                    IsSelected = false,
                });
                filterOptions.Add(new()
                {
                    Key = "on duty",
                    IsSelected = false,
                });
             filterOptions.Add(new()
                {
                    Key = "not reported",
                    IsSelected = false,
                });
             filterOptions.Add(new()
                {
                    Key = "reported",
                    IsSelected = false,
                });
            filterOptions.Add(new()
                {
                    Key = "shift not assigned",
                    IsSelected = false,
                });
             filterOptions.Add(new()
                {
                    Key = "employee on leave",
                    IsSelected = false,
                });

               
                return filterOptions;
            }

        


    }
}
