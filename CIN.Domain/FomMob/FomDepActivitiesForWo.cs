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
    [Table("tblFomDepActivitiesForWo")]
    public class TblFomDepActivitiesForWo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(WOId))]
        public TblFomJobWorkOrder SysWorkorder { get; set; }
        public int WOId { get; set; }
        [ForeignKey(nameof(ActivityCode))]
        public TblErpFomActivities SysFomActivity { get; set; }
        public string ActivityCode { get; set; }
        public string ActivityName { get; set; }
        public string ActivityNameAr { get; set; }
        public string SLADuration { get; set; }
        public string SLAType { get; set; } //Hours,Days
        [StringLength(500)]
        public string ActivityLocation { get; set; }
        
       public string SLADurationNote { get; set; }
       public string ActivityDescription { get; set; }
    }
}
