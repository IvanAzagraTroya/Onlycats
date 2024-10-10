using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlycatsTFG.PostService.Repositories;
using OnlycatsTFG.models;

namespace Onlycats.InteractionService.Controllers
{
    [ApiController]
    [Route("interactions")]
    public class InteractionsController : ControllerBase
    {
        private readonly IMongoRepository<Activity, string> _mongoRepository;
        private readonly ILogger<InteractionsController> _logger;

        public InteractionsController(ILogger<InteractionsController> logger, IMongoRepository<Activity, string> mongoRepository)
        {
            _logger = logger;
            _mongoRepository = mongoRepository;
        }

        [HttpGet("/{id}")]
        public async Task<Activity> ReadInteractionByIdAsync(string id)
        {
            return await _mongoRepository.ReadByIdAsync(id);
        }
        [HttpPost("/insert/{entity.ActivityId}")]
        [Authorize]
        public async Task AddInteractionAsync(Activity entity)
        {
            await _mongoRepository.CreateAsync(entity);
        }
        [HttpPut("/update/{entity.ActivityId}")]
        [Authorize]

        public async Task UpdateInteractionAsync(Activity entity)
        {
            await _mongoRepository.UpdateAsync(entity.ActivityId, entity);
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
