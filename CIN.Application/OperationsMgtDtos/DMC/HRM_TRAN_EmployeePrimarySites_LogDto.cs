using AutoMapper;
using CIN.Domain.OpeartionsMgt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIN.Application.OperationsMgtDtos.DMC
{

    [AutoMap(typeof(HRM_TRAN_EmployeePrimarySites_Log))]

    public class HRM_TRAN_EmployeePrimarySites_LogDto
    {
        public long EmployeePrimarySitesLogID { get; set; }

        public string EmployeeNumber { get; set; }
        public string SiteCode { get; set; }
        public DateTime? TransferredDate { get; set; }
        public DateTime? CreatedDate { get; set; }

        public long? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedBy { get; set; }

    }


}
