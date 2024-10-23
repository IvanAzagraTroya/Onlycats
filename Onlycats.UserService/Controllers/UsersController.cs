using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlycatsTFG.models;
using OnlycatsTFG.repository;

namespace Onlycats.UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IRepository<int, User> _userRepository;

        public UsersController(IRepository<int, User> userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("users/all")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            if (users.Count == 0) { 
                return NotFound();
            }
            return Ok(users);
        }

        [HttpPost("users/create_user")]
        public async Task<IActionResult> AddUserAsync([FromBody] User user)
        {
            await _userRepository.AddAsync(user);
            return Created();
        }

        [HttpPut("users/update_user")] 
        public async Task<IActionResult> UpdateUserAsync([FromBody] User user)
        {
            await _userRepository.UpdateAsync(user);
            return Ok(user);
        }
        [HttpGet("users/user_id")]
        public async Task<IActionResult> GetByIdAsync([FromBody] int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) 
            { 
                return NotFound(user);
            }
            return Ok(User);
        }

        [HttpGet("users/user_email")]
        public async Task<IActionResult> GetByEmailAsync([FromBody] string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if(user == null)
            {
                return NotFound(user);
            }
            return Ok(user);
        }
    }
}
