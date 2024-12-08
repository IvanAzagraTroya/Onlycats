using OnlycatsTFG;
using OnlycatsTFG.PostService.Repositories;

namespace Onlycats.PostService.Repositories
{
    public interface IPostsRepository : IMongoRepository<Post, string>
    {
    }
}
