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
    [Table("tblFomMaterialForWo")]
    public class TblFomMaterialForWo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public int WOId { get; set; }

        [StringLength(20)]
        public string MaterialCode { get; set; }
        public string MaterialName { get; set; }     //since materials and tools data may come from external resources we need names of them
        public string MaterialNameAr { get; set; }

        [ForeignKey(nameof(WOId))]
        public TblFomJobWorkOrder SysWorkorder { get; set; }
        public int ReqQty { get; set; }
        public int AvailableQty { get; set; }
        public int BalQty { get; set; }
        public int ActivityMapId { get; set; }


    }
}
