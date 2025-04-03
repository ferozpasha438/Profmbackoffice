using Microsoft.EntityFrameworkCore;
using CIN.Domain.FomMgt;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CIN.Domain.FomMob
{
    [Keyless]
    [Table("tblFomMobReasonCode")]
    public class TblFomMobReasonCode
    {
        public string Action { get; set; }
        public string ReasonCode { get; set; }
        public string ReasonCodeAr { get; set; }
       
    }
}
