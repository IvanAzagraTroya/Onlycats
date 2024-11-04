using Microsoft.AspNetCore.Identity;
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
        private readonly IPasswordHasher<UserDTO> _passwordHasher;

        public AuthController(IRepository<int, User> userRepository, IConfiguration config)
        {
            _userRepository = userRepository;
            _config = config;
            _passwordHasher = new PasswordHasher<UserDTO>();
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody]UserDTO dto)
        {
            if (dto == null) return BadRequest();

            var user = _userRepository.GetByEmailAsync(dto.Email);

            if (user == null) return BadRequest(user+" email is not found");

            var result = _passwordHasher.VerifyHashedPassword(dto, user.Result.Password, dto.Password);

            if(result == 0) return BadRequest(user +" password is not correct");

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: new List<Claim>(),
                expires: DateTime.Now.AddHours(2),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return Ok(tokenString);

        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserDTO user) 
        {
            if (user == null) return BadRequest();
            var exists = _userRepository.GetByEmailAsync(user.Email);
            if(exists != null) return BadRequest("This email is already in use");
            _passwordHasher.HashPassword(user, user.Password);
            User newUser = new User(displayName: user.DisplayName, user.UserName, user.Email, user.Password);
            _userRepository.AddAsync(newUser);
            return Created();
        }
    }
}
