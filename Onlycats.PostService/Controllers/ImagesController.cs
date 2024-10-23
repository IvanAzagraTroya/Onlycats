using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Onlycats.PostService.Models;
using OnlycatsTFG.PostService.Repositories;

namespace OnlycatsTFG.PostService.Controllers
{
    [ApiController]
    [Route("api/images/[controller]")]
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
        public async Task<List<Image>> ReadAllImages()
        {
            return await _mongoRepository.ReadAll();
        }

        [HttpGet("/image/{id}")]
        public async Task<Image> ReadImageByIdAsync(ObjectId id)
        {
            return await _mongoRepository.ReadByIdAsync(id);
        }
        [HttpPost("/insert_image/{entity.ImageId}")]
        [Authorize]
        public async Task AddImageAsync(Image entity)
        {
            await _mongoRepository.CreateAsync(entity);
        }
        [HttpPut("/update_image/{entity.ImageId}")]
        [Authorize]

        public async Task UpdateImageAsync(Image entity)
        {
            await _mongoRepository.UpdateAsync(entity.Post_id, entity);
        }
        [HttpDelete("/delete_image/{id}")]
        [Authorize]
        public async Task DeleteImageAsync(ObjectId id)
        {
            await _mongoRepository.DeleteAsync(id);
        }
    }
}
