
using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace OnlycatsTFG
{
    public class Post {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string PostId { get; set; }
        public required string UserId {get; set;}
        [JsonPropertyName("image_url")]
        public required string ImageUrl {get; set;}
        public string PostDate {get;} = DateTime.Now.ToString();
        [JsonPropertyName("likes")]
        public int LikeNumber {get; set;} = 0; //NoSQL para optimización
        [JsonPropertyName("dislikes")]
        public int DislikeNumber {get; set;} = 0; //NoSQL para optimización
        //public String[]? PostComments {get; set;} = null;
    }
}