using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace OnlycatsTFG
{
    public class Post {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public int UserId {get; set; }
        public string ImageUrl { get; set; } = "";
        public string PostDate { get; } = DateTime.UtcNow.ToShortDateString() + " "+DateTime.UtcNow.Hour.ToString() +":"+ DateTime.UtcNow.Minute.ToString();
        public int LikeNumber {get; set; } = 0;
        public string? Text { get; set; }

        public Post(int userId, string imageUrl, string? text)
        {
            UserId = userId;
            ImageUrl = imageUrl;
            Text = text;
        }

        public Post() { }
    }
}