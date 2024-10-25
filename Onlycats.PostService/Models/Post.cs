
using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace OnlycatsTFG
{
    public class Post {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId PostId { get; set; }
        public required int UserId {get; set;}
        [JsonPropertyName("image_url")]
        public required string ImageUrl {get; set;}
        public string PostDate { get; set; } = DateTime.Now.ToString();
        public int LikeNumber {get; set;} = 0;
    }
}