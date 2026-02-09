using System.ComponentModel.DataAnnotations;

namespace ASPNETCoreWebAPI.Model
{
    public class LoginDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
