using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CIN.Application.FomMobDtos
{
 
     public class Out_FomWebDashBoardTicketsData 
    {
        public int TotalTickets { get; set; } = 0;
        public int ClosedTickets { get; set; } = 0;
        public int PendingTickets { get; set; } = 0;
        public Out_FomWebDashBoardLast30DaysData Last30DaysData { get; set; } = new();
        public List<Out_FomWebDashBoardDepWiseTicketsData> DepWiseData { get; set; } = new();
        public List<Out_FomWebDashBoardMonthWiseTicketsData> MonthWiseData { get; set; } = new();
        public List<Out_FomWebDashBoardDepAndStatusWiseTicketsData> DepAndStatusWiseData { get; set; } = new();

        public List<string> MonthsNames { get; set; } = new();
        public List<int> MonthlyTotalTickets{ get; set; } = new();
        public List<Out_FomWebDashBoardRecenetTickets> RecentTickets { get; set; } = new();
    }

     public class Out_FomWebDashBoardLast30DaysData 
    {
        public int TotalTickets { get; set; } = 0;
        public int ClosedTickets { get; set; } = 0;
        public int PendingTickets { get; set; } = 0;
       

    }




    public class Out_FomWebDashBoardDepWiseTicketsData
    {
        public int TotalTickets { get; set; } = 0;
        public int ClosedTickets { get; set; } = 0;
        public int PendingTickets { get; set; } = 0;
        public string Department { get; set; }
        public string DepartmentNameEng { get; set; }
        public string DepartmentNameArb { get; set; }
    }
    public class Out_FomWebDashBoardDepAndStatusWiseTicketsData
    {
        public List<Out_FomWebDashBoardDepStatusWiseTicketsCount> DepsDataList { get; set; } = new();
        public string StatusStr { get; set; }
    }

     public class Out_FomWebDashBoardDepStatusWiseTicketsCount
    {
        public string Department { get; set; }
        public string DepartmentNameEng { get; set; }
        public string DepartmentNameArb { get; set; }
        public int Count { get; set; }
    }




    public class Out_FomWebDashBoardMonthWiseTicketsData
    {
        public int TotalTickets { get; set; } = 0;
        public int ClosedTickets { get; set; } = 0;
        public int PendingTickets { get; set; } = 0;
        public string Month { get; set; }
    }
    public class Out_FomWebDashBoardRecenetTickets
    {
        public string ProjectNameEng { get; set; }
        public string ProjectNameArb { get; set; }
        public DateTime  Date{ get; set; }
        public string TicketNumber { get; set; }
        public string MaintananceType { get; set; }
        public string StatusStr { get; set; }
    }


}
