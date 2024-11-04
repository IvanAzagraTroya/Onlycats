using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Onlycats.PostService.Models
{
    public class Image
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public required string PostId { get; set; }
        public int UserId { get; set; }
        public required string Content { get; set; }
    }
}
