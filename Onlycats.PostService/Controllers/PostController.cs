using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using OnlycatsTFG.PostService.Repositories;

namespace OnlycatsTFG.PostService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IMongoRepository<Post, ObjectId> _mongoRepository;
        private readonly ILogger<PostsController> _logger;

        public PostsController(ILogger<PostsController> logger, IMongoRepository<Post, ObjectId> mongoRepository)
        {
            _logger = logger;
            _mongoRepository = mongoRepository;
        }

        [HttpGet("posts")]
        public async Task<List<Post>> ReadAllPosts()
        {
            return await _mongoRepository.ReadAll();
        }

        [HttpGet("/posts/{id}")]
        public async Task<Post> ReadPostByIdAsync(ObjectId id)
        {
            return await _mongoRepository.ReadByIdAsync(id);
        }
        [HttpPost("/insert/posts/{entity.PostId}")]
        [Authorize]
        public async Task AddPostAsync(Post entity)
        {
            await _mongoRepository.CreateAsync(entity);
        }
        [HttpPut("/update/posts/{entity.PostId}")]
        [Authorize]
        public async Task UpdatePostAsync(Post entity)
        {
            await _mongoRepository.UpdateAsync(entity.PostId, entity);
        }
        [HttpDelete("/delete/posts/{id}")]
        [Authorize]
        public async Task DeletePostAsync(ObjectId id)
        {
            await _mongoRepository.DeleteAsync(id);
        }
    }
}
