using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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

            var user = _userRepository.GetByEmail(dto.Email);

            if (user == null) return BadRequest(user+" email is not found");

            if(_passwordHasher.VerifyHashedPassword(dto, user.Password, dto.Password).Equals(PasswordVerificationResult.Success))
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    issuer: _config["Jwt:Issuer"],
                    audience: _config["Jwt:Audience"],
                    claims: new List<Claim>() 
                    { 
                        new Claim("id", user.UserId.ToString()),
                        new Claim("username", user.UserName), 
                        new Claim("role", user.UserRole)
                    },
                    expires: DateTime.UtcNow.AddHours(2),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(tokenString);
            }
            return BadRequest("The password is not correct");
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserDTO user) 
        {
            if (user == null) return BadRequest("You have to fill the data");
            var exists = _userRepository.GetByEmail(user.Email);
            if (exists != null) return BadRequest("This email is already in use");
            user.Password = _passwordHasher.HashPassword(user, user.Password);
            User newUser = new User(user.DisplayName, user.UserName, user.Email, user.Password);
            var data = _userRepository.AddAsync(newUser);
            data.GetAwaiter();
            return Created();
        }
    }
}
