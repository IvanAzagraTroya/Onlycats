using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlycatsTFG.PostService.Repositories;

namespace OnlycatsTFG.PostService.Controllers
{
    [ApiController]
    [Route("interactions")]
    public class InteractionsController : ControllerBase
    {
        private readonly IMongoRepository<Post, string> _mongoRepository;
        private readonly ILogger<InteractionsController> _logger;

        public InteractionsController(ILogger<InteractionsController> logger, IMongoRepository<Post, string> mongoRepository)
        {
            _logger = logger;
            _mongoRepository = mongoRepository;
        }

        [HttpGet("/{id}")]
        public async Task<Post> ReadInteractionByIdAsync(string id)
        {
            return await _mongoRepository.ReadByIdAsync(id);
        }
        [HttpPost("/insert/{entity.PostId}")]
        [Authorize]
        public async Task AddInteractionAsync(Post entity)
        {
            await _mongoRepository.CreateAsync(entity);
        }
        [HttpPut("/update/{entity.PostId}")]
        [Authorize]

        public async Task UpdateInteractionAsync(Post entity)
        {
            await _mongoRepository.UpdateAsync(entity.PostId, entity);
        }
        [HttpDelete("/delete/{id}")]
        [Authorize]
        public async Task DeleteInteractionAsync(string id)
        {
            await _mongoRepository.DeleteAsync(id);
        }

        [HttpGet("/user/{userId}")]
        public async Task GetInteractionsByUserId(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
