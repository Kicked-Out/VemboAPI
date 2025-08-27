using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VemboAPI.Domain.DTOs;
using VemboAPI.Domain.Entities;
using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;


namespace VemboAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly VemboDbContext _context;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AuthController(VemboDbContext context, IJwtTokenGenerator jwtTokenGenerator, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _jwtTokenGenerator = jwtTokenGenerator;
            _passwordHasher = passwordHasher;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == dto.Email);

            if (user == null)
                return Unauthorized("Invalid credentials");

            var token = _jwtTokenGenerator.GenerateToken(user);

            return Ok(new { token });
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDto dto)
        {
            if (_context.Users.Any(u => u.Email == dto.Email))
                return Conflict("Email already exists.");

            var newUser = new User
            {
                Email = dto.Email,
                UserName = dto.NickName,
                NickNameSlug = dto.NickName.ToLower().Replace(" ", "-"),
                NickName = dto.NickName,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsPremium = false,
                XP = 0,
                Rating = 0,
                Level = 1,
                Role = "User"
            };

            newUser.PasswordHash = _passwordHasher.HashPassword(newUser, dto.Password);

            _context.Users.Add(newUser);
            _context.SaveChanges();

            var token = _jwtTokenGenerator.GenerateToken(newUser);
            return Ok(new { token });
        }

        [Authorize]
        [HttpGet("validate-token")]
        public IActionResult ValidateToken()
        {
            return Ok(new { valid = true });
        }
    }
}
