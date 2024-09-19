using OnlycatsTFG.utils;
namespace OnlycatsTFG.models{
    public class Comment {
        public int CommentId {get;} = IdGenerator.GenerateId();
        public required string PostId {get; set;}
        public required string UserId {get; set;}
        public required string Content {get; set;}
        public DateTime CommentDate {get;} = DateTime.Now;
        public int Likes {get; set;} = 0;
        public int Dislikes {get; set;} = 0;
    }
}