using AutoMapper;
using CIN.Application.Common;
using CIN.Application.OperationsMgtDtos;
using CIN.DB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using System.Linq.Dynamic.Core;
using CIN.Domain.OpeartionsMgt;
using System.Globalization;

namespace CIN.Application.OperationsMgtQuery
{

    #region EnterEmployeeTransResign

    public class EnterEmployeeTransResign : IRequest<long>
    {
        public UserIdentityDto User { get; set; }
        public TblOpEmployeeTransResignDto Input { get; set; }
    }

    public class EnterEmployeeTransResignHandler : IRequestHandler<EnterEmployeeTransResign, long>
    {
        private readonly CINDBOneContext _context;
        
        private readonly IMapper _mapper;

        public EnterEmployeeTransResignHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            
            _mapper = mapper;
        }

        public async Task<long> Handle(EnterEmployeeTransResign request, CancellationToken cancellationToken)
        {

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {

                    var OldAttList = _context.EmployeeAttendance.AsNoTracking().Where(e => e.EmployeeNumber == request.Input.EmployeeNumber && e.AttnDate >= request.Input.AttnDate&&e.isDefaultEmployee /*&& (e.Attendance=="P" || e.Attendance == "A"|| e.Attendance == "OT")*/).ToList();
                   
                    TblOpEmployeeTransResign obj = _context.EmployeeTransResign.AsNoTracking().FirstOrDefault(e => e.EmployeeNumber == request.Input.EmployeeNumber && e.AttnDate == request.Input.AttnDate);

                    if (obj == null)
                    {
                        obj = new TblOpEmployeeTransResign();
                        obj.Id = 0;
                    }
                        obj.EmployeeNumber = request.Input.EmployeeNumber;
                        obj.AttnDate = Convert.ToDateTime(request.Input.AttnDate, CultureInfo.InvariantCulture);
                        obj.ProjectCode = request.Input.ProjectCode;
                        obj.SiteCode = request.Input.SiteCode;
                    obj.TR = request.Input.TR;
                    obj.R = request.Input.R;
                    obj.Remarks = request.Input.Remarks;

                 
                    if (obj.Id > 0)
                    {

                       
                        

                        obj.ModifiedBy = request.User.UserId;
                        obj.Modified = DateTime.UtcNow;


                        _context.EmployeeTransResign.Update(obj);
                        _context.SaveChanges();

                    }
                    else
                    {
                        obj.Id = 0;
                        obj.CreatedBy = request.User.UserId;
                        obj.Created = DateTime.UtcNow;


                        _context.EmployeeTransResign.Add(obj);
                        _context.SaveChanges();

                    }
                    if (OldAttList.Count!=0)
                    {
                        if (OldAttList.Any(e=>e.SiteCode== request.Input.SiteCode && request.Input.TR) ||(request.Input.R)||(!request.Input.R&&!request.Input.TR)) 
                        {
                            transaction.Rollback();
                            return -1;
                        }
                                                                   
                    }
                    transaction.Commit();
                    return obj.Id;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Log.Error("Error in EnterEmployeeTransResign Method");
                    Log.Error("Error occured time : " + DateTime.UtcNow);
                    Log.Error("Error message : " + ex.Message);
                    Log.Error("Error StackTrace : " + ex.StackTrace);
                    return 0;

                }

            }
        }
    }

    #endregion

    


    


    



    

    

}


