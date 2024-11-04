
using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace OnlycatsTFG
{
    public class Post {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }// = ObjectId.GenerateNewId().ToString();
        public required int UserId {get; set;}
        public required string ImageUrl {get; set;}
        public string PostDate { get; } = DateTime.Now.ToString();
        public int LikeNumber {get; set;} = 0;
    }
}