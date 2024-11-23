using System.ComponentModel.DataAnnotations;

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
        public bool IsVerified { get; set; } = false;
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string JoinedDate { get; } = DateTime.UtcNow.ToString();
        public string UserRole { get; set; } = Role.User.ToString();
        public enum Role { User, Admin }

        public User(string displayName, string userName, string email, string password) 
        { 
            DisplayName = displayName; 
            UserName = userName; 
            Email = email; 
            Password = password; 
        }
    }
}