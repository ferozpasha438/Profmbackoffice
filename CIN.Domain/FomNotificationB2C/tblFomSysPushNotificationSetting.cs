using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CIN.Domain.FomNotificationB2C
{
   


    [Table("tblFomSysPushNotificationSetting")]
    public class TblFomSysPushNotificationSetting
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(100)]
        public string Title { get; set; }
        [StringLength(500)]
        public string Message { get; set; }
        [StringLength(20)]
        public string Type { get; set; }
        public bool Status { get; set; }
        public DateTime? CreatedOn { get; set; }
    }


  

}
