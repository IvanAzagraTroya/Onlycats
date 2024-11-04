using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OnlycatsTFG.models
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        public string DisplayName { get; set; }
        [Required]
        public string UserName { get; set; }
        public string? ProfilePicture { get; set; }
        public int FollowerNum { get; set; } = 0;
        public int FollowingNum { get; set; } = 0;
        public int PostNum { get; set; } = 0;
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string JoinedDate { get; } = DateTime.Now.ToString();

        public User(string displayName, string userName, string email, string password) 
        { 
            DisplayName = displayName; 
            UserName = userName; 
            Email = email; 
            Password = password; 
        }
    }
}