using System.ComponentModel.DataAnnotations;

namespace Inventarios.Server.AspNet.Models
{
    public class ChangePassword
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string OldPassword { get; set; }
        [Required]
      
        public string NewPassword { get; set; }
      
       
    }
}
