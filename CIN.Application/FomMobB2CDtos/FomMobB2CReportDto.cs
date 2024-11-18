using CIN.Application.Common;
using CIN.Application.InvoiceDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIN.Application.FomMobB2CDtos
{
    public class B2CReportTicketSearchDto
    {
        public string CustCode { get; set; }
        public string ServiceType { get; set; }
        public string ResourceCode { get; set; } //assigned technician
        public short? TicketStatus { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

        public int ReportCount { get; set; }
        public int ReportIndex { get; set; }
    }
    public class B2CTickethistoryListDto
    {
        public string TicketNumber { get; set; }
        public string CustCode { get; set; }
        public string CustName { get; set; }
        public string Time { get; set; }
        public string StatusStr { get; set; }
        public string ResourceName { get; set; }
        public string ServiceType { get; set; }
        public DateTime Date { get; set; }
    }
    public class B2CReportTickethistoryListDto
    {
        //public string Code { get; set; }    
        //public string Name { get; set; }    
        //public decimal? C { get; set; }    

        //public List<B2CTickethistoryListDto> List { get; set; }
        public ReportListDto<B2CTickethistoryListDto> ListItems { get; set; }
    }

    public class B2CTicketSummarybycustListDto
    {
        public string CustCode { get; set; }
        public string CustName { get; set; }
        public string SchType { get; set; }
        public int Open { get; set; }
        public int Approved { get; set; }
        public int Closed { get; set; }
        public int Completed { get; set; }
        public int Void { get; set; }
        public int Total { get; set; }
    }
    public class B2CReportTicketSummarybycustListDto
    {
        public ReportListDto<B2CTicketSummarybycustListDto> ListItems { get; set; }

    }
    
}
