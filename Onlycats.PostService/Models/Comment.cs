using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OnlycatsTFG.models{
    public class Comment {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public required string PostId {get; set;}
        public required int UserId {get; set;}
        public required string Content {get; set;}
        public required string Displayname {  get; set; }
        public required string Username { get; set; }
        public string CommentDate { get; set; } = DateTime.Now.ToString();
        public int Likes {get; set;} = 0;
    }
}