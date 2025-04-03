using CIN.Domain.FomMgt;
using CIN.Domain.InventorySetup;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIN.Domain.FomMob
{
    [Table("tblFomToolsForWo")]
    public class TblFomToolsForWo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public int WOId { get; set; }

        [StringLength(20)]
        public string ToolCode { get; set; }
        [ForeignKey(nameof(ToolCode))]
        public TblErpInvItemMaster SysItem { get; set; }

        public string ToolName { get; set; }
        public string ToolNameAr { get; set; }
        public int Qty { get; set; } = 1;

        [ForeignKey(nameof(WOId))]
        public TblFomJobWorkOrder SysWorkorder { get; set; }
         [ForeignKey(nameof(ActivityMapId))]
        public TblFomDepActivitiesForWo SysActivtyToWoMap { get; set; }

        public int ActivityMapId { get; set; } = 0;
        public string ToolType { get; set; }        //From Item category
    }
}
