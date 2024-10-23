
//using System.Text.Json.Serialization;
//using MongoDB.Bson;
//using MongoDB.Bson.Serialization.Attributes;

//namespace OnlycatsTFG
//{
//    public class Post {
//        [BsonId]
//        [BsonRepresentation(BsonType.ObjectId)]
//        public ObjectId PostId { get; set; }
//        public required string OwnerId {get; set;}
//        public required string ImageUrl {get; set;}
//        public DateTime PostDate {get;} = DateTime.Now;
//        public int LikeNumber {get; set;} = 0;
//    }
//}