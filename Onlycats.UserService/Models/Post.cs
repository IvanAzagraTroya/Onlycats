
using System.Text.Json.Serialization;
using OnlycatsTFG.utils;

namespace OnlycatsTFG
{
    public class Post {
        [JsonPropertyName("id")]
        public int PostId {get; } = IdGenerator.GenerateId();
        [JsonPropertyName("owner_id")]
        public required string OwnerId {get; set;}
        [JsonPropertyName("image_url")]
        public required string ImageUrl {get; set;}
        public DateTime PostDate {get;} = DateTime.Now;
        [JsonPropertyName("likes")]
        public int LikeNumber {get; set;} = 0; //NoSQL para optimización
        [JsonPropertyName("dislikes")]
        public int DislikeNumber {get; set;} = 0; //NoSQL para optimización
        //public String[]? PostComments {get; set;} = null;
    }
}