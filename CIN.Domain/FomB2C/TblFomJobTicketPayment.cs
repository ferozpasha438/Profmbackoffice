using CIN.Domain.FomMob;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIN.Domain.FomB2C
{
    [Table("tblFomJobTicketPayment")]
    public class TblFomJobTicketPayment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string TicketNumber { get; set; }
        [StringLength(250)]
        public string TokenNumber { get; set; }
        public string Response { get; set; }
        public bool IsDayService { get; set; }
        public DateTime Date { get; set; }
    }

}
