using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CIN.Domain.FomMob
{
    [Table("tblFomMobSequenceNumberGenerator")]
    public class TblFomMobSequenceNumberGenerator
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int?  SequenceNumber { get; set; }
        public int? CustomerContractSeq { get; set; }
        public bool? IsForJobTicket { get; set; } = false;
        public int Length { get; set; } = 6;
        public string   Prefix { get; set; }="";

    }
}
