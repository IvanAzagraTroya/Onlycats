using OnlycatsTFG.utils;
namespace OnlycatsTFG.models {
    public class Reaction{
        public int ReactionId {get;} = IdGenerator.GenerateId();
        public required string UserId {get; set;}
        public required string PostId {get; set;}
        public required string ReactionType {get; set;}
    }

    public enum ReactionType {
        Like,
        Dislike
    }
}