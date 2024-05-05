﻿using AutoMapper;
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


namespace CIN.Application.OperationsMgtQuery
{



  

    #region RefreshOffs
    public class RefreshOffs : IRequest<int>
    {
        public UserIdentityDto User { get; set; }
        public RefreshOffsDto dto { get; set; }
    }

    public class RefreshOffsHandler : IRequestHandler<RefreshOffs, int>
    {
        private readonly CINDBOneContext _context;
        private readonly DMCContext _contextDMC;
        private readonly IMapper _mapper;

        public RefreshOffsHandler(CINDBOneContext context, DMCContext contextDMC, IMapper mapper)
        {
            _context = context;
            _contextDMC = contextDMC;
            _mapper = mapper;
        }

        public async Task<int> Handle(RefreshOffs request, CancellationToken cancellationToken)
        {
            using (var transaction = await _contextDMC.Database.BeginTransactionAsync())
            {
                using (var transaction2 = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {

                        Log.Info("----Info RefreshOffs method start----");


                        List<TblOpMonthlyRoasterForSite> Roasters = new();


                        if (request.dto.FromDate != null && request.dto.ToDate != null)                 //within periode
                        {
                            if (request.dto.SiteCode!=null)                                             //within sites
                            {
                                Roasters = await _context.TblOpMonthlyRoasterForSites.Where(e =>

                               (e.MonthEndDate >= request.dto.FromDate ||
                                e.MonthEndDate >= request.dto.ToDate) &&
                              e.SiteCode == request.dto.SiteCode
                              
                               
                               ).ToListAsync();
                            }
                            else {                                                                        //all sites
                                Roasters = await _context.TblOpMonthlyRoasterForSites.Where(e =>

                                    e.MonthEndDate >= request.dto.FromDate &&
                                    e.MonthEndDate >= request.dto.ToDate &&
                                    e.SiteCode != null

                                   ).ToListAsync();
                            }

                        }
                        else if (request.dto.FromDate != null && request.dto.ToDate == null) {                  //from date only

                            if (request.dto.SiteCode != null)                                             //within sites
                            {
                                Roasters = await _context.TblOpMonthlyRoasterForSites.Where(e =>

                                e.MonthEndDate >= request.dto.FromDate &&
                              e.SiteCode == request.dto.SiteCode


                               ).ToListAsync();
                            }
                            else
                            {                                                                        //all sites
                                Roasters = await _context.TblOpMonthlyRoasterForSites.Where(e =>

                                    e.MonthEndDate >= request.dto.FromDate &&
                                    e.SiteCode != null

                                   ).ToListAsync();
                            }

                        }
                        else if (request.dto.FromDate==null && request.dto.ToDate==null) {                      //specific month

                            if (request.dto.SiteCode != null)                                             //within sites
                            {
                                Roasters = await _context.TblOpMonthlyRoasterForSites.Where(e =>

                               e.Month==request.dto.Month &&
                               e.Year==request.dto.Year &&
                              e.SiteCode == request.dto.SiteCode


                               ).ToListAsync();
                            }
                            else
                            {                                                                        //all sites
                                Roasters = await _context.TblOpMonthlyRoasterForSites.Where(e =>

                                    e.Month == request.dto.Month &&
                               e.Year == request.dto.Year &&
                                    e.SiteCode != null

                                   ).ToListAsync();
                            }

                        }

                        if (Roasters.Count == 0)
                        {

                            return -1;

                        }


                        List<HRM_DEF_EmployeeOff> offsInHRM = new();
                        List<HRM_DEF_EmployeeOff> offsInOp = new();

                        foreach (var roaster in Roasters)
                        {

                            long empId =  _contextDMC.HRM_TRAN_Employees.FirstOrDefault(emp => emp.EmployeeNumber == roaster.EmployeeNumber).EmployeeID ;
                            DateTime sDate = new DateTime(roaster.Year,roaster.Month,1);


                            if (roaster.S1 == "O")
                            {
                                offsInOp.Add(
                                    new()
                                    {
                                        Date = sDate.AddDays(0),
                                        EmployeeId = empId,
                                        SiteCode = roaster.SiteCode
                                        
                                    });
                            }

                            if (roaster.S2 == "O")
                            {
                                offsInOp.Add(
                                    new()
                                    {
                                        Date = sDate.AddDays(1),
                                        EmployeeId = empId,
                                        SiteCode = roaster.SiteCode
                                       
                                    });
                            }
                            if (roaster.S3 == "O")
                            {
                                offsInOp.Add(
                                    new()
                                    {
                                        Date = sDate.AddDays(2),
                                        EmployeeId = empId,
                                        SiteCode = roaster.SiteCode
                                       
                                    });
                            }
                            if (roaster.S4 == "O")
                            {
                                offsInOp.Add(
                                    new()
                                    {
                                        Date = sDate.AddDays(3),
                                        EmployeeId = empId,
                                        SiteCode = roaster.SiteCode                                      
                                    });
                            }
                            if (roaster.S5 == "O")
                            {
                                offsInOp.Add(
                                    new()
                                    {
                                        Date = sDate.AddDays(4),
                                        EmployeeId = empId,
                                        SiteCode = roaster.SiteCode
                                       
                                    });
                            }
                            if (roaster.S6 == "O")
                            {
                                offsInOp.Add(
                                    new()
                                    {
                                        Date = sDate.AddDays(5),
                                        EmployeeId = empId,
                                        SiteCode = roaster.SiteCode
                                        
                                    });
                            }
                            if (roaster.S7 == "O")
                            {
                                offsInOp.Add(
                                    new()
                                    {
                                        Date = sDate.AddDays(6),
                                        EmployeeId = empId,
                                        SiteCode = roaster.SiteCode
                                        
                                    });
                            }
                            if (roaster.S8 == "O")
                            {
                                offsInOp.Add(
                                    new()
                                    {
                                        Date = sDate.AddDays(7),
                                        EmployeeId = empId,
                                        SiteCode = roaster.SiteCode
                                        
                                    });
                            }
                            if (roaster.S9 == "O")
                            {
                                offsInOp.Add(
                                    new()
                                    {
                                        Date = sDate.AddDays(8),
                                        EmployeeId = empId,
                                        SiteCode = roaster.SiteCode
                                        
                                    });
                            }
                            if (roaster.S10 == "O")
                            {
                                offsInOp.Add(
                                    new()
                                    {
                                        Date = sDate.AddDays(9),
                                        EmployeeId = empId,
                                        SiteCode = roaster.SiteCode
                                        
                                    });
                            }
                            if (roaster.S11 == "O")
                            {
                                offsInOp.Add(
                                    new()
                                    {
                                        Date = sDate.AddDays(10),
                                        EmployeeId = empId,
                                        SiteCode = roaster.SiteCode
                                        
                                    });
                            }
                            if (roaster.S12 == "O")
                            {
                                offsInOp.Add(
                                    new()
                                    {
                                        Date = sDate.AddDays(11),
                                        EmployeeId = empId,
                                        SiteCode = roaster.SiteCode
                                        
                                    });
                            }
                            if (roaster.S13 == "O")
                            {
                                offsInOp.Add(
                                    new()
                                    {
                                        Date = sDate.AddDays(12),
                                        EmployeeId = empId,
                                        SiteCode = roaster.SiteCode
                                        
                                    });
                            }
                            if (roaster.S14 == "O")
                            {
                                offsInOp.Add(
                                    new()
                                    {
                                        Date = sDate.AddDays(13),
                                        EmployeeId = empId,
                                        SiteCode = roaster.SiteCode
                                        
                                    });
                            }
                            if (roaster.S15 == "O")
                            {
                                offsInOp.Add(
                                    new()
                                    {
                                        Date = sDate.AddDays(14),
                                        EmployeeId = empId,
                                        SiteCode = roaster.SiteCode
                                        
                                    });
                            }
                            if (roaster.S16 == "O")
                            {
                                offsInOp.Add(
                                    new()
                                    {
                                        Date = sDate.AddDays(15),
                                        EmployeeId = empId,
                                        SiteCode = roaster.SiteCode
                                        
                                    });
                            }
                            if (roaster.S17 == "O")
                            {
                                offsInOp.Add(
                                    new()
                                    {
                                        Date = sDate.AddDays(16),
                                        EmployeeId = empId,
                                        SiteCode = roaster.SiteCode
                                        
                                    });
                            }
                            if (roaster.S18 == "O")
                            {
                                offsInOp.Add(
                                    new()
                                    {
                                        Date = sDate.AddDays(17),
                                        EmployeeId = empId,
                                        SiteCode = roaster.SiteCode
                                        
                                    });
                            }
                            if (roaster.S19 == "O")
                            {
                                offsInOp.Add(
                                    new()
                                    {
                                        Date = sDate.AddDays(18),
                                        EmployeeId = empId,
                                        SiteCode = roaster.SiteCode
                                        
                                    });
                            }
                            if (roaster.S20 == "O")
                            {
                                offsInOp.Add(
                                    new()
                                    {
                                        Date = sDate.AddDays(19),
                                        EmployeeId = empId,
                                        SiteCode = roaster.SiteCode
                                        
                                    });
                            }
                            if (roaster.S21 == "O")
                            {
                                offsInOp.Add(
                                    new()
                                    {
                                        Date = sDate.AddDays(20),
                                        EmployeeId = empId,
                                        SiteCode = roaster.SiteCode
                                        
                                    });
                            }
                            if (roaster.S22 == "O")
                            {
                                offsInOp.Add(
                                    new()
                                    {
                                        Date = sDate.AddDays(21),
                                        EmployeeId = empId,
                                        SiteCode = roaster.SiteCode
                                        
                                    });
                            }
                            if (roaster.S23 == "O")
                            {
                                offsInOp.Add(
                                    new()
                                    {
                                        Date = sDate.AddDays(22),
                                        EmployeeId = empId,
                                        SiteCode = roaster.SiteCode
                                        
                                    });
                            }
                            if (roaster.S24 == "O")
                            {
                                offsInOp.Add(
                                    new()
                                    {
                                        Date = sDate.AddDays(23),
                                        EmployeeId = empId,
                                        SiteCode = roaster.SiteCode
                                        
                                    });
                            }
                            if (roaster.S25 == "O")
                            {
                                offsInOp.Add(
                                    new()
                                    {
                                        Date = sDate.AddDays(24),
                                        EmployeeId = empId,
                                        SiteCode = roaster.SiteCode
                                        
                                    });
                            }
                            if (roaster.S26 == "O")
                            {
                                offsInOp.Add(
                                    new()
                                    {
                                        Date = sDate.AddDays(25),
                                        EmployeeId = empId,
                                        SiteCode = roaster.SiteCode
                                        
                                    });
                            }
                            if (roaster.S27 == "O")
                            {
                                offsInOp.Add(
                                    new()
                                    {
                                        Date = sDate.AddDays(26),
                                        EmployeeId = empId,
                                        SiteCode = roaster.SiteCode
                                        
                                    });
                            }
                            if (roaster.S28 == "O")
                            {
                                offsInOp.Add(
                                    new()
                                    {
                                        Date = sDate.AddDays(27),
                                        EmployeeId = empId,
                                        SiteCode = roaster.SiteCode
                                        
                                    });
                            }
                            if (roaster.S29 == "O")
                            {
                                offsInOp.Add(
                                    new()
                                    {
                                        Date = sDate.AddDays(28),
                                        EmployeeId = empId,
                                        SiteCode = roaster.SiteCode
                                        
                                    });
                            }
                            if (roaster.S30 == "O")
                            {
                                offsInOp.Add(
                                    new()
                                    {
                                        Date = sDate.AddDays(29),
                                        EmployeeId = empId,
                                        SiteCode = roaster.SiteCode
                                    
                                    });
                            }
                            if (roaster.S31 == "O")
                            {
                                offsInOp.Add(
                                    new()
                                    {
                                        Date = sDate.AddDays(30),
                                        EmployeeId = empId,
                                        SiteCode = roaster.SiteCode
                                      
                                    });
                            }





                            var offsInHrm =  _contextDMC.HRM_DEF_EmployeeOffs.Where(e => e.EmployeeId == empId && e.Date <= roaster.MonthEndDate && e.Date >= roaster.MonthStartDate && e.SiteCode==roaster.SiteCode).ToList();

                            if (offsInHrm.Count > 0)
                            {
                                offsInHRM.AddRange(offsInHrm);
                            }







                        }

                        if (offsInHRM.Count > 0 && offsInOp.Count == 0)
                        {
                            _contextDMC.HRM_DEF_EmployeeOffs.RemoveRange(offsInHRM);
                           await _contextDMC.SaveChangesAsync();
                        }
                        else if (offsInHRM.Count ==0  && offsInOp.Count > 0)
                        {
                           await _contextDMC.HRM_DEF_EmployeeOffs.AddRangeAsync(offsInOp);
                          await  _contextDMC.SaveChangesAsync();

                        }
                        else if(offsInHRM.Count > 0 && offsInOp.Count > 0)
                        {

                            List<HRM_DEF_EmployeeOff> OffsToBeAdd = new();
                            List<HRM_DEF_EmployeeOff> OffsToBeDelete = new();
                            foreach (var offHrm in offsInHRM)
                            {

                                if (!offsInOp.Any(e=>e.EmployeeId==offHrm.EmployeeId&&e.Date==offHrm.Date&&e.SiteCode==offHrm.SiteCode))
                                {
                                    OffsToBeDelete.Add(offHrm);
                                }
                            } 
                            foreach (var offOp in offsInOp)
                            {

                                if (!offsInHRM.Any(e=>e.EmployeeId== offOp.EmployeeId&&e.Date== offOp.Date&&e.SiteCode== offOp.SiteCode))
                                {
                                    OffsToBeAdd.Add(offOp);
                                }
                            }

                            if (OffsToBeDelete.Count>0)
                            {
                                _contextDMC.RemoveRange(OffsToBeDelete);
                              await  _contextDMC.SaveChangesAsync();
                            
                            }  
                            if (OffsToBeAdd.Count>0)
                            {

                               await _contextDMC.AddRangeAsync(OffsToBeAdd);
                               await _contextDMC.SaveChangesAsync();

                            }
                            //


                        }

                      

                        await transaction.CommitAsync();
                        await transaction2.CommitAsync();
                        Log.Info("Exit From RefreshOffs Method");
                        return 1;

                    }
                    catch (Exception ex)
                    {
                        Log.Error("Error in RefreshOffs Method");
                        Log.Error("Error occured time : " + DateTime.UtcNow);
                        Log.Error("Error message : " + ex.Message);
                        Log.Error("Error StackTrace : " + ex.StackTrace);
                       
                       await transaction.RollbackAsync();
                       await transaction2.RollbackAsync();
                        return 0;
                    }
                }
            }
        }
    }

    #endregion
}
