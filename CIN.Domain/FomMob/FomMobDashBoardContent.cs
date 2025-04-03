using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIN.Domain.FomMob
{
    [Table("tblFomMobContactUs")]
    public class TblFomMobContactUs
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string TitleEng { get; set; }
        public string TitleArb { get; set; }
        public string MessageEng { get; set; }
        public string MessageArb { get; set; }
        public string Contact1 { get; set; }
        [StringLength(100)]
        public string EmailId { get; set; }
        public string Contact2 { get; set; }
        public string Website { get; set; }
        public string UnifiedNumber { get; set; }
        public bool IsActive { get; set; }


    } 
    
    [Table("tblFomMobAboutUs")]
    public class TblFomMobAboutUs
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string TitleEng { get; set; }
        public string TitleArb { get; set; }
        public string MessageEng { get; set; }
        public string MessageArb { get; set; }
        public bool IsActive { get; set; }

    }
}
