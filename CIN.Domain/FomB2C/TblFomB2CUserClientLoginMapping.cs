using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIN.Domain.FomB2C
{
    [Table("tblFomB2CUserClientLoginMapping")]
    public class TblFomB2CUserClientLoginMapping
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [StringLength(20)]
        public string UserClientLoginCode { get; set; }
        [StringLength(256)]
        public string RegEmail { get; set; }
        [StringLength(256)]
        public string RegMobile { get; set; }

        [StringLength(128)]
        public string Password { get; set; }
        [StringLength(15)]
        public string LoginType { get; set; } //user,client
        public string LastLoginDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }

    }
}
