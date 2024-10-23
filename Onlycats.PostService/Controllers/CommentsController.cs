using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using OnlycatsTFG.models;
using OnlycatsTFG.PostService.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Onlycats.CommentService.Controllers
{
    [Route("api/[controller]")]
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

        [HttpGet("Comments")]
        public async Task<List<Comment>> ReadAllComments()
        {
            return await _mongoRepository.ReadAll();
        }

        [HttpGet("/Comments/{id}")]
        public async Task<Comment> ReadCommentByIdAsync(ObjectId id)
        {
            return await _mongoRepository.ReadByIdAsync(id);
        }
        [HttpPost("/insert/Comments/{entity.CommentId}")]
        [Authorize]
        public async Task AddCommentAsync(Comment entity)
        {
            await _mongoRepository.CreateAsync(entity);
        }
        [HttpDelete("/delete/Comments/{id}")]
        [Authorize]
        public async Task DeleteCommentAsync(ObjectId id)
        {
            await _mongoRepository.DeleteAsync(id);
        }
    }
}
