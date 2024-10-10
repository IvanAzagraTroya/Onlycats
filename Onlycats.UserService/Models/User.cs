using System.Text.Json.Serialization;
using Onlycats.SharedUtilities.utils;

namespace OnlycatsTFG.models{
    public class User {
        [JsonPropertyName("id")]
        public int UserId {get;} = IdGenerator.GenerateId();
        [JsonPropertyName("display_name")]
        public string DisplayName { get; set;}
        [JsonPropertyName("username")]
        public string UserName { get; set;}
        [JsonPropertyName("profile_picture")]
        public string ProfilePicture { get; set; }
        [JsonPropertyName("follower_number")]
        public int FollowerNum {get; set;}
        [JsonPropertyName("following_number")]
        public int FollowingNum {get; set;}
        [JsonPropertyName("number_posts")]
        public int PostNum {get; set;}
        public string[]? PeopleFollowedId {get; set;} = null;

        public required string Email {get; set;}
        public required string Password {get; set;}
        public DateTime JoinedDate {get;} = DateTime.Now;
    }
}