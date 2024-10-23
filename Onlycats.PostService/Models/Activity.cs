using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OnlycatsTFG.models{
    public class Activity {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId ActivityId {get; set;}
        public required int PostId { get; set;}
        public required int UserId {get; set;}
        public ActivityType ActionType {get; set;}
        public DateTime ActivityDate {get;} = DateTime.Now;
    }

    public enum ActivityType{
        NoActivity = 0, Post = 1, Like = 2, Comment = 3, Delete = 4, Follow = 5, Unfollow = 6
    }
}