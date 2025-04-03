using CIN.Domain.FomMgt;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIN.Domain.FomMob
{
    [Table("tblFomMobSurveyForm")]
    public class TblFomMobSurveyForm
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string TicketNumber { get; set; }
        [ForeignKey(nameof(TicketNumber))]
        public TblFomJobTicket SysTicket { get; set; }
        public string Surveyor { get; set; }

        [Column(TypeName = "date")]
        public DateTime SurveyDate { get; set; } 

        [Column(TypeName = "date")]
        public DateTime ExCloseDate { get; set; }
        public bool IsSurveyCompleted { get; set; } = false;
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
