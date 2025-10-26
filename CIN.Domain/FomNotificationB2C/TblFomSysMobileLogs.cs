using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CIN.Domain.FomNotificationB2C
{
 
    [Table("tblFomSysMobileLogs")]
    public class TblFomSysMobileLogs
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedOn { get; set; }
    }

   



    
  


}
