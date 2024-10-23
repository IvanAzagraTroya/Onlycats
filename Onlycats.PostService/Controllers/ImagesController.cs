using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Onlycats.PostService.Models;
using OnlycatsTFG.PostService.Repositories;

namespace OnlycatsTFG.PostService.Controllers
{
    [ApiController]
    [Route("api/")]
    public class ImagesController : ControllerBase
    {
        private readonly IMongoRepository<Image, ObjectId> _mongoRepository;
        private readonly ILogger<ImagesController> _logger;

        public ImagesController(ILogger<ImagesController> logger, IMongoRepository<Image, ObjectId> mongoRepository)
        {
            _logger = logger;
            _mongoRepository = mongoRepository;
        }

        [HttpGet("images")]
        public async Task<ActionResult<List<Image>>> ReadAllImages()
        {
            var images = await _mongoRepository.ReadAll();
            if(images.Count == 0) return NotFound();
            return Ok(images);
        }

        [HttpGet("/images/{id}")]
        public async Task<ActionResult<Image>> ReadImageByIdAsync(ObjectId id)
        {
            var image = await _mongoRepository.ReadByIdAsync(id);
            if (image == null) return NotFound();
            return Ok(image);
        }
        [HttpPost("images/insert/{entity.ImageId}")]
        [Authorize]
        public async Task<ActionResult> AddImageAsync(Image entity)
        {
            await _mongoRepository.CreateAsync(entity);
            return Created();
        }
        [HttpPut("images/update/{entity.ImageId}")]
        [Authorize]
        public async Task<ActionResult> UpdateImageAsync(Image entity)
        {
            await _mongoRepository.UpdateAsync(entity.Post_id, entity);
            return NoContent();
        }
        [HttpDelete("images/delete/{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteImageAsync(ObjectId id)
        {
            await _mongoRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
