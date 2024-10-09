using System.Text.Json.Serialization;
using OnlycatsTFG.utils;

namespace OnlycatsTFG.models{
    public class User {
        [JsonPropertyName("UserId")]
        public int UserId {get;} = IdGenerator.GenerateId();
        [JsonPropertyName("Followers")]
        public int FollowerNum {get; set;}
        [JsonPropertyName("Following")]
        public int FollowingNum {get; set;}
        [JsonPropertyName("Number of posts")]
        public int PostNum {get; set;}
        public string[]? PostsId {get; set;}
        public string[]? PeopleFollowedId {get; set;} = null;

        public required string Email {get; set;}
        public required string Password {get; set;}
        public DateTime JoinedDate {get;} = DateTime.Now;
    }
}