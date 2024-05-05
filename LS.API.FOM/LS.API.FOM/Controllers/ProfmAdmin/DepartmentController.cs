//using AutoMapper;
//using CIN.Application;
//using CIN.Application.Common;
//using CIN.Application.FomMgtDtos;
////using CIN.Application.SalesSetupDtos;
//using CIN.DB;
//using MediatR;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using AutoMapper.QueryableExtensions;
//using System.Linq.Dynamic.Core;
//using CIN.Domain.FleetMgt;

//namespace LS.API.PROFM.Controllers.ProfmAdmin
//{
//    public class DepartmentController :BaseController
//    {
//        private IConfiguration _Config;

//        public DepartmentController(IOptions<AppSettingsJson> appSettings, IConfiguration config) : base(appSettings)
//        {
//            _Config = config;
//        }


//        [HttpGet("getDepartmentsPaginationList")]
//        public async Task<IActionResult> Get([FromQuery] PaginationFilterDto filter)
//        {

//            var list = await Mediator.Send(new GetDepartmentsPaginationListQuery() { Input = filter, User = UserInfo() });
//            return Ok(list);
//        }

//        [HttpGet("getSelectDepartmentsList")]
//        public async Task<IActionResult> Get()
//        {

//            var list = await Mediator.Send(new GetSelectDepartmentsQuery() { User = UserInfo() });
//            return Ok(list);
//        }


//        [HttpGet("getDepartmentById/{id}")]
//        public async Task<IActionResult> Get([FromRoute] int id)
//        {
//            var obj = await Mediator.Send(new GetDepartmentByIdQuery() { Id = id, User = UserInfo() });
//            return obj is not null ? Ok(obj) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
//        }
//         [HttpGet("getDepartmentByCode/{code}")]
//        public async Task<IActionResult> Get([FromRoute] string code)
//        {
//            var obj = await Mediator.Send(new GetDepartmentByCodeQuery() { Code = code, User = UserInfo() });
//            return obj is not null ? Ok(obj) : NotFound(new ApiMessageDto { Message = ApiMessageInfo.NotFound });
//        }



//        [HttpPost("createUpdateDepartment")]
//        public async Task<ActionResult> Create([FromBody] TblProFmDepartmentDto dTO)
//        {
//            var id = await Mediator.Send(new CreateUpdateDepartmentQuery() { Input = dTO, User = UserInfo() });
//            if (id > 0)
//                return Created($"get/{id}", dTO);
//            else if (id == -1)
//            {
//                return BadRequest(new ApiMessageDto { Message = ApiMessageInfo.Duplicate(nameof(dTO.Id)) });
//            }
//            return BadRequest(new ApiMessageDto { Message = ApiMessageInfo.Failed });
//        }





//        [HttpDelete("{id}")]
//        public async Task<ActionResult> Delete([FromRoute] int id)
//        {
//            var academicId = await Mediator.Send(new DeleteDepartmentQuery() { Id = id, User = UserInfo() });
//            if (academicId > 0)
//                return NoContent();
//            return BadRequest(new ApiMessageDto { Message = ApiMessageInfo.Failed });
//        }



//        #region DepartmentTypes--->metadata

//        [HttpGet("getSelectDepartmentTypes")]
//        public async Task<IActionResult> GetDepartmentTypes()
//        {

//            var list = await Mediator.Send(new GetSelectDepartmentTypeQuery { User = UserInfo() });
//            return Ok(list);
//        }

//        #endregion
//    }
//}
