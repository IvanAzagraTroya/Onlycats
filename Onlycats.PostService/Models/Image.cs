using MongoDB.Bson;

namespace Onlycats.PostService.Models
{
    public class Image
    {
        public ObjectId Id { get; set; }
        public ObjectId Post_id { get; set; }
        public required string Content { get; set; }
    }
}
