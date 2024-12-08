using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlycatsTFG.models;
using OnlycatsTFG.repository;

namespace Onlycats.UserService.Controllers
{
    [Route("api/")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IRepository<int, User> _userRepository;

        public UsersController(IRepository<int, User> userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            if (users.Count == 0) { 
                return NotFound();
            }
            return Ok(users);
        }

        [HttpPost("users/create")]
        public async Task<IActionResult> AddUserAsync([FromForm] User user, [FromForm]IFormFile file)
        {
            await _userRepository.AddAsync(user);
            return Created();
        }

        [HttpPut("users/update")]
        [Authorize]
        public async Task<IActionResult> UpdateUserAsync([FromBody] User user)
        {
            await _userRepository.UpdateAsync(user);
            return Ok(user);
        }
        [HttpGet("users/user/{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) 
            { 
                return NotFound(user);
            }
            return Ok(user);
        }

        [HttpGet("users/email/{email}")]
        public IActionResult GetByEmail(string email)
        {
            var user = _userRepository.GetByEmail(email);
            if(user == null)
            {
                return NotFound(user);
            }
            return Ok(user);
        }

        [HttpGet("users/profile_picture")]
        public IActionResult GetPostImage([FromQuery] User user)
        {
            var url = user.ProfilePicture;
            if (string.IsNullOrEmpty(url)) return BadRequest("The post has no image url");
            if (!System.IO.File.Exists(url)) return NotFound("Image not found");
            var image = System.IO.File.OpenRead(url);
            var filename = Path.GetFileName(url);
            var fileImage = File(image, "application/octet-stream", filename);
            return fileImage;
        }
    }
}
