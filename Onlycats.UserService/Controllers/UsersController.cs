﻿using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> AddUserAsync([FromBody] User user)
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
        [HttpGet("users/{id}")]
        [Authorize]
        public async Task<IActionResult> GetByIdAsync([FromQuery] int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) 
            { 
                return NotFound(user);
            }
            return Ok(User);
        }

        [HttpGet("users/email")]
        public async Task<IActionResult> GetByEmailAsync([FromQuery] string email)
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
