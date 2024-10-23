using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using OnlycatsTFG.models;
using OnlycatsTFG.PostService.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Onlycats.CommentService.Controllers
{
    [Route("api/")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IMongoRepository<Comment, ObjectId> _mongoRepository;
        private readonly ILogger<CommentsController> _logger;

        public CommentsController(ILogger<CommentsController> logger, IMongoRepository<Comment, ObjectId> mongoRepository)
        {
            _logger = logger;
            _mongoRepository = mongoRepository;
        }

        [HttpGet("comments")]
        public async Task<ActionResult<List<Comment>>> ReadAllComments()
        {
            var comments = await _mongoRepository.ReadAll();
            if (comments.Count == 0) return NotFound();
            return Ok(comments);
        }

        [HttpGet("comments/{id}")]
        public async Task<ActionResult<Comment>> ReadCommentByIdAsync(ObjectId id)
        {
            var comment = await _mongoRepository.ReadByIdAsync(id);
            if (comment == null) return NotFound();
            return Ok(comment);
        }
        [HttpPost("comments/insert/{entity.CommentId}")]
        [Authorize]
        public async Task<ActionResult> AddCommentAsync(Comment entity)
        {
            await _mongoRepository.CreateAsync(entity);
            return Created();
        }
        [HttpDelete("comments/delete/{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteCommentAsync(ObjectId id)
        {
            await _mongoRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
