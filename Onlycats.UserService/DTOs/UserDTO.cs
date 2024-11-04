using System.ComponentModel.DataAnnotations;

namespace Onlycats.UserService.DTOs
{
    public class UserDTO
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
