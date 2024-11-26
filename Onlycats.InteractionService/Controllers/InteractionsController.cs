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
        public async Task<ActionResult<List<Activity>>> ReadAll()
        {
            var interactions = await _mongoRepository.ReadAll();
            if(interactions.Count == 0) return NotFound();
            return Ok(interactions);
        }

        [HttpGet("interactions/{id}")]
        public async Task<ActionResult<Activity>> ReadInteractionByIdAsync(string id)
        {
            var objId = new ObjectId(id);
            var interaction = await _mongoRepository.ReadByIdAsync(objId);
            if(interaction == null) return NotFound();
            return Ok(interaction);
        }
        [HttpPost("interactions/insert")]
        [Authorize]
        public async Task<ActionResult> AddInteractionAsync([FromBody]Activity entity)
        {
            await _mongoRepository.CreateAsync(entity);
            return Created();
        }
        [HttpPut("/interactions/update/{entity.Id}")]
        [Authorize]
        public async Task<ActionResult> UpdateInteractionAsync([FromBody]Activity entity)
        {
            var interactionId = new ObjectId(entity.Id);
            await _mongoRepository.UpdateAsync(interactionId, entity);
            return NoContent();
        }
        [HttpDelete("interacions/delete/{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteInteractionAsyncs(string id)
        {
            var interactionId = new ObjectId(id);
            await _mongoRepository.DeleteAsync(interactionId);
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
        public async Task<ActionResult<List<Activity>>> GetPostInteractionsOrderedByDateAsync(string postId)
        {
            var objId = new ObjectId(postId);
            var activities = await _mongoRepository.GetPostInteractionsOrderedByDateAsync(objId);
            if (activities.Count == 0) return NoContent();
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
        public async Task<ActionResult<List<Activity>>> GetByPostIdAsync(string postId)
        {
            var id = new ObjectId(postId);
            var activities = await _mongoRepository.GetByOtherIdAsync(id);
            if (activities.Count() == 0) return NotFound();
            return Ok(activities);
        }
    }
}
