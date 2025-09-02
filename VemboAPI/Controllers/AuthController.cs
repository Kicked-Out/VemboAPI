using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IConfiguration _configuration;

        public AuthController(VemboDbContext context, IJwtTokenGenerator jwtTokenGenerator, IPasswordHasher<User> passwordHasher, IConfiguration configuration)
        {
            _context = context;
            _jwtTokenGenerator = jwtTokenGenerator;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == dto.Email);

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
                UserName = dto.NickName,
                NickNameSlug = dto.NickName.ToLower().Replace(" ", "-"),
                NickName = dto.NickName,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsPremium = false,
                XP = 0,
                Rating = 0,
                Level = 1,
                Role = "User",
            };

            newUser.PasswordHash = _passwordHasher.HashPassword(newUser, dto.Password);

            _context.Users.Add(newUser);
            _context.SaveChanges();

            // ✅ Створення статистики одразу після юзера
            var stat = new UserStatistic
            {
                UserId = newUser.Id,
                Streak = 0,
                VBucks = 0,
                Hearts = 5,
                CurrentPeriodId = null // або null, якщо ще не прив'язано до курсу
            };

            _context.UserStatistics.Add(stat);
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

        [Authorize]
        [HttpGet("validate-token")]
        public IActionResult ValidateToken()
        {
            return Ok(new { valid = true });
        }
    }
}

