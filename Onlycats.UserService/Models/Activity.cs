using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OnlycatsTFG.models{
    public class Activity {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ActivityId {get; set;}
        public required string UserId {get; set;}
        public ActivityType ActionType {get; set;}
        public DateTime ActivityDate {get;} = DateTime.Now;
    }

    public enum ActivityType{
        Post, Like, Dislike, Comment, Delete, Follow, Unfollow
    }
}