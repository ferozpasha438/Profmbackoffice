using System.ComponentModel.DataAnnotations;

namespace CIN.Application.FomMobDtos
{
    public class OpLoginUserDto
    {
        public int UserId { get; set; }
        public string ApiToken { get; set; }
        public int? CompanyId { get; set; } = 0;
        public int? BranchId { get; set; } = 0;
        public string BranchCode { get; set; }
        public string DBConnectionString { get; set; }

        public string UserName { get; set; }
        public string CustomerCode { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string LoginType { get; set; }
        public string Role { get; set; }
        public int? ClientUserMapId { get; set; } = 0;

    }


    public class IpLoginUserDto
    {
        [Required(ErrorMessage = "*")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "*")]
        public string Password { get; set; }
    }

    public class IpLoginB2CUserDto : IpLoginUserDto
    {
        public string Token { get; set; }
    }
}