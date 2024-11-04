using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlycatsTFG.repository.mongorepository;
using OnlycatsTFG.models;
using MongoDB.Bson;

namespace Onlycats.InteractionService.Controllers
{
    [ApiController]
    [Route("api/")]
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
        [Authorize]
        public async Task<ActionResult<List<Activity>>> ReadAll()
        {
            var interactions = await _mongoRepository.ReadAll();
            if(interactions.Count == 0) return NotFound();
            return Ok(interactions);
        }

        [HttpGet("interactions/{id}")]
        public async Task<ActionResult<Activity>> ReadInteractionByIdAsync(ObjectId id)
        {
            var interaction = await _mongoRepository.ReadByIdAsync(id);
            if(interaction == null) return NotFound();
            return Ok(interaction);
        }
        [HttpPost("interactions/insert/{entity.ActivityId}")]
        [Authorize]
        public async Task<ActionResult> AddInteractionAsync(Activity entity)
        {
            await _mongoRepository.CreateAsync(entity);
            return Created();
        }
        [HttpPut("/interactions/update/{entity.ActivityId}")]
        [Authorize]

        public async Task<ActionResult> UpdateInteractionAsync(Activity entity)
        {
            await _mongoRepository.UpdateAsync(entity.ActivityId, entity);
            return NoContent();
        }
        [HttpDelete("interacions/delete/{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteInteractionAsync(ObjectId id)
        {
            await _mongoRepository.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("interactions/user/{userId}")]
        [Authorize]
        public async Task<ActionResult<List<Activity>>> GetInteractionsByUserId(int userId)
        {
            var interactions = await _mongoRepository.GetInteractionsByUserId(userId);
            if (interactions.Count == 0) return NotFound();
            return Ok(interactions);
        }

        [HttpGet("interactions/date/{postId}")]
        [Authorize]
        public async Task<ActionResult<List<Activity>>> GetPostInteractionsOrderedByDateAsync(int postId)
        {
            var activities = await _mongoRepository.GetPostInteractionsOrderedByDateAsync(postId);
            if (activities.Count == 0) return NotFound();
            return Ok(activities);
        }

        [HttpGet("interactions/type/{type}")]
        [Authorize]
        public async Task<ActionResult<List<Activity>>> GetByActivityType(int type)
        {
            var activities = await _mongoRepository.GetByInteractionType(type);
            if (activities.Count == 0) return NotFound();
            return Ok(activities);
        }

        [HttpGet("interactions/post/{id}")]
        [Authorize]
        public async Task<ActionResult<List<Activity>>> GetByPostIdAsync(ObjectId postId)
        {
            var activities = await _mongoRepository.GetByOtherIdAsync(postId);
            if (activities.Count() == 0) return NoContent();
            return Ok(activities);
        }
    }
}
