using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Onlycats.UserService.DTOs;
using OnlycatsTFG.models;
using OnlycatsTFG.repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Onlycats.UserService.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IRepository<int, User> _userRepository;
        private readonly IConfiguration _config;

        public AuthController(IRepository<int, User> userRepository, IConfiguration config)
        {
            _userRepository = userRepository;
            _config = config;
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody]UserDTO dto)
        {
            if (dto == null) 
            { 
                return BadRequest();
            }
            var user = _userRepository.GetByEmailAsync(dto.Email);
            if (user == null)
            {
                return Unauthorized(user);
            }
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return Ok(tokenString);

        }
    }
}
