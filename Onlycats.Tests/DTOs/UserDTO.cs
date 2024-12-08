using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Onlycats.UserService.DTOs
{
    public class UserDTO
    {
        public required string Email { get; set; }

        public required string Password { get; set; }

        public string? DisplayName { get; set; }
        public string? UserName { get; set; }

        public UserDTO() { }

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

        public UserDTO FromJson(string json)
        {
            return JsonSerializer.Deserialize<UserDTO>(json);
        }
    }
}
