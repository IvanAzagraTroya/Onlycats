using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using OnlycatsTFG.PostService.Repositories;

namespace OnlycatsTFG.PostService.Controllers
{
    [ApiController]
    [Route("api/")]
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
        public async Task<ActionResult<List<Post>>> ReadAllPosts()
        {
            var posts = await _mongoRepository.ReadAll();
            if (posts.Count == 0) { 
                return NotFound();
            }
            return Ok(posts);
        }

        [HttpGet("posts/{id}")]
        [Authorize]
        public async Task<ActionResult<Post>> ReadPostByIdAsync(string id)
        {
            var postId = new ObjectId(id);
            var post = await _mongoRepository.ReadByIdAsync(postId);
            if (post == null) {
                return NotFound();
            }
            return Ok(post);
        }
        [HttpPost("posts/insert")]
        [Authorize]
        public async Task<ActionResult> AddPostAsync(Post entity)
        {
            await _mongoRepository.CreateAsync(entity);
            return Created();
        }
        [HttpPut("posts/update/{entity.Id}")]
        [Authorize]
        public async Task<ActionResult> UpdatePostAsync(Post entity)
        {
            var postId = new ObjectId(entity.Id);
            await _mongoRepository.UpdateAsync(postId, entity);
            return NoContent();
        }
        [HttpDelete("posts/delete/{id}")]
        [Authorize]
        public async Task<ActionResult> DeletePostAsync(string id)
        {
            var postId = new ObjectId(id);
            await _mongoRepository.DeleteAsync(postId);
            return NoContent();
        }
        [HttpGet("posts/user/{id}")]
        [Authorize]
        public async Task<ActionResult> GetPostByUserIdAsync(int id)
        {
            var posts = await _mongoRepository.GetByOtherIdAsync(id);
            if(posts.Count() == 0) return NoContent();
            return Ok(posts);
        }
    }
}
