using CIN.Domain.FomMgt;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CIN.Domain.FomMob
{
    [Table("tblFomMobAuthorities")]
    public class TblFomMobAuthorities
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public bool CanVoidTicket { get; set; }
        public bool CanSurvey { get; set; }
        public bool CanApproveTicket { get; set; }
        public bool CanCovertToWorkOrder { get; set; }
        public bool CanForecloseTicket { get; set; }
        public bool CanCompleteTicket { get; set; }
        public bool CanReconsileTicket { get; set; }
        public bool CanCloseTicket { get; set; }
        public bool IsActive { get; set; }
    }
}
