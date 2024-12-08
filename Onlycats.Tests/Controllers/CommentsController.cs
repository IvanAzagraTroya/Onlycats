using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using Onlycats.PostService.Models;
using OnlycatsTFG.models;
using OnlycatsTFG.PostService.Repositories;

namespace OnlycatsTFG.PostService.Controllers
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
        public async Task<ActionResult<Comment>> ReadCommentByIdAsync(string id)
        {
            var comment = await _mongoRepository.ReadByIdAsync(id);
            if (comment == null) return NotFound();
            return Ok(comment);
        }
        [HttpPost("comments/insert")]
        [Authorize]
        public async Task<ActionResult> AddCommentAsync([FromForm] Comment entity)
        {
            await _mongoRepository.CreateAsync(entity);
            return Created();
        }
        [HttpDelete("comments/delete/{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteCommentAsync(string id)
        {
            await _mongoRepository.DeleteAsync(id);
            return NoContent();
        }
        [HttpGet("comments/post/{id}")]
        public async Task<ActionResult<List<Comment>>> GetCommentByPostIdAsync(string id)
        {
            var postId = new ObjectId(id);
            var comments = await _mongoRepository.GetByOtherIdAsync(postId);
            if (comments.Count() == 0) return NoContent();
            return Ok(comments);
        }

        [HttpPut("comment/update_likes/{id}")]
        [Authorize]
        public async Task<ActionResult> UpdateLikes(string id, [FromBody] Request request)
        {
            var comment = await _mongoRepository.ReadByIdAsync(id);
            if (comment == null) return NotFound("There's no comment with that id");

            if (request.IsLiked) comment.Likes++;
            else comment.Likes--;

            await _mongoRepository.UpdateAsync(id, comment);
            return Ok(comment);
        }
    }
}
