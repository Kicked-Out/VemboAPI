using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _configuration;

        public AuthController(VemboDbContext context, IJwtTokenGenerator jwtTokenGenerator, IConfiguration configuration)
        {
            _context = context;
            _jwtTokenGenerator = jwtTokenGenerator;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == dto.Email && u.Password == dto.Password);
            if (user == null)
                return Unauthorized("Invalid credentials");

            var token = _jwtTokenGenerator.GenerateToken(user);
            var jwtSettings = _configuration.GetSection("Jwt");

            Response.Cookies.Append("token", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpireMinutes"]))
            });

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
                Password = dto.Password, // для безпеки — хешуй пізніше
                NickName = dto.NickName,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsPremium = false,
                XP = 0,
                Rating = 0,
                Level = 1,
                Role = "User",
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            var token = _jwtTokenGenerator.GenerateToken(newUser);
            var jwtSettings = _configuration.GetSection("Jwt");

            Response.Cookies.Append("token", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpireMinutes"]))
            });

            return Ok(new { token });
        }
    }
}

