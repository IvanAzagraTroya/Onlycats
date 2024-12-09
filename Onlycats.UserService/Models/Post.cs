
using System.Text.Json.Serialization;
using OnlycatsTFG.utils;

namespace OnlycatsTFG
{
    public class Post {
        [JsonPropertyName("PostId")]
        public int PostId {get; } = IdGenerator.GenerateId();
        [JsonPropertyName("OwnerId")]
        public required string OwnerId {get; set;}
        [JsonPropertyName("ImageUrl")]
        public required string ImageUrl {get; set;}
        public DateTime PostDate {get;} = DateTime.Now;
        public int LikeNumber {get; set;} = 0; //NoSQL para optimización
        public int DislikeNumber {get; set;} = 0; //NoSQL para optimización
        //public String[]? PostComments {get; set;} = null;
    }
}