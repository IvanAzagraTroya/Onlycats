using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OnlycatsTFG.models{
    public class Activity {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id {get; set;}
        public required string PostId { get; set;}
        public required int UserId {get; set;}
        public ActivityType ActionType {get; set;}
        public string? Text { get; set; }
        public string ActivityDate { get; set; } = DateTime.Now.ToString();
    }

    public enum ActivityType{
        NoActivity = 0, Post = 1, Like = 2, Comment = 3, Delete = 4, Follow = 5, Unfollow = 6
    }
}