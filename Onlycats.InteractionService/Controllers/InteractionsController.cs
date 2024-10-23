using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlycatsTFG.repository.mongorepository;
using OnlycatsTFG.models;
using MongoDB.Bson;

namespace Onlycats.InteractionService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InteractionsController : ControllerBase
    {
        private readonly IMongoRepository<Activity, ObjectId> _mongoRepository;
        private readonly ILogger<InteractionsController> _logger;

        public InteractionsController(ILogger<InteractionsController> logger, IMongoRepository<Activity, ObjectId> mongoRepository)
        {
            _logger = logger;
            _mongoRepository = mongoRepository;
        }
        [HttpGet("interactions")]
        public async Task<List<Activity>> ReadAll()
        {
            return await _mongoRepository.ReadAll();
        }

        [HttpGet("interactions/{id}")]
        public async Task<Activity> ReadInteractionByIdAsync(ObjectId id)
        {
            return await _mongoRepository.ReadByIdAsync(id);
        }
        [HttpPost("/insert/{entity.ActivityId}")]
        [Authorize]
        public async Task AddInteractionAsync(Activity entity)
        {
            await _mongoRepository.CreateAsync(entity);
        }
        [HttpPut("/update/interactions/{entity.ActivityId}")]
        [Authorize]

        public async Task UpdateInteractionAsync(Activity entity)
        {
            await _mongoRepository.UpdateAsync(entity.ActivityId, entity);
        }
        [HttpDelete("/delete/interacions/{id}")]
        [Authorize]
        public async Task DeleteInteractionAsync(ObjectId id)
        {
            await _mongoRepository.DeleteAsync(id);
        }

        [HttpGet("/user/interactions/{userId}")]
        public async Task GetInteractionsByUserId(int userId)
        {
            await _mongoRepository.GetInteractionsByUserId(userId);
        }

        [HttpGet("/date/interactions/{postId}")]
        public async Task<List<Activity>> GetPostInteractionsOrderedByDateAsync(int postId)
        {
            return await _mongoRepository.GetPostInteractionsOrderedByDateAsync(postId);
        }

        [HttpGet("type/interactions/{type}")]
        public async Task<List<Activity>> GetByActivityType(int type)
        {
            return await _mongoRepository.GetByInteractionType(type);
        }
    }
}
