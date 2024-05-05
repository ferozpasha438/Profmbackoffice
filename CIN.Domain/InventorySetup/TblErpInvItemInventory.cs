

namespace CIN.Domain.InventorySetup
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using CIN.Domain.SystemSetup;

    [Table("tblErpInvItemInventory")]
    public class TblErpInvItemInventory : AutoActiveGenerateIdAuditableKey<int>
    {

        [ForeignKey(nameof(ItemCode))]
        public TblErpInvItemMaster InvItemMaster { get; set; }
        [StringLength(20)]
        public string ItemCode { get; set; }


        [ForeignKey(nameof(WHCode))]
        public TblInvDefWarehouse InvWarehouses { get; set; }
        [StringLength(10)]
        public string WHCode { get; set; }

        [Column(TypeName = "decimal(12,5)")]
       [Required]
        public decimal QtyOH { get; set; }


        [Column(TypeName = "decimal(12,5)")]
        [Required]
        public decimal QtyOnSalesOrder { get; set; }


        [Column(TypeName = "decimal(12,5)")]
        [Required]
        public decimal QtyOnPO { get; set; }

        [Column(TypeName = "decimal(12,5)")]
        [Required]
        public decimal QtyReserved { get; set; }

        [Column(TypeName = "numeric(11,5)")]
        [Required]
        public decimal ItemAvgCost { get; set; }


        [Column(TypeName = "numeric(11,5)")]
        [Required]
        public decimal ItemLastPOCost { get; set; }

        [Column(TypeName = "numeric(11,5)")]
        [Required]
        public decimal ItemLandedCost { get; set; }


        [Column(TypeName = "decimal(12,5)")]
        [Required]
        public decimal MinQty { get; set; }


        [Column(TypeName = "decimal(12,5)")]
        [Required]
        public decimal MaxQty { get; set; }

        [Column(TypeName = "decimal(12,5)")]
        [Required]
        public decimal EOQ { get; set; }

    }
}
