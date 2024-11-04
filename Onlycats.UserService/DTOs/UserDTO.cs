using System.ComponentModel.DataAnnotations;

namespace Onlycats.UserService.DTOs
{
    public class UserDTO
    {
        public required string Email { get; set; }

        public required string Password { get; set; }

        public required string DisplayName { get; set; }
        public required string UserName { get; set; }

        public UserDTO(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public UserDTO(string displayName, string userName, string email, string password)
        {
            DisplayName = displayName;
            UserName = userName;
            Email = email;
            Password = password;
        }
    }
}
