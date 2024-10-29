using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OnlycatsTFG.models{
    public class Comment {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId CommentId { get; set; }
        public required string PostId {get; set;}
        public required string UserId {get; set;}
        public required string Content {get; set;}
        public string CommentDate {get;} = DateTime.Now.ToString();
        public int Likes {get; set;} = 0;
        public int Dislikes {get; set;} = 0;
    }
}