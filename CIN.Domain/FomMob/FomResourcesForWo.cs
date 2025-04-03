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
    [Table("tblFomResourcesForWo")]
    public class TblFomResourcesForWo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public int WOId { get; set; }
        [StringLength(20)]
        public string ResourceCode { get; set; }


        [ForeignKey(nameof(WOId))]
        public TblFomJobWorkOrder SysWorkorder { get; set; }
        [ForeignKey(nameof(ResourceCode))]
        public TblErpFomResources SysResource { get; set; }
        public int ActivityMapId { get; set; } = 0;
    }
}
