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

            // ✅ Створення статистики одразу після юзера
            var stat = new UserStatistic
            {
                UserId = newUser.Id,
                Streak = 0,
                Emeralds = 0,
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

        [HttpPost("forgot-password")]
        public IActionResult ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == dto.Email);
            if (user == null)
                return NotFound("User not found");

            var token = Guid.NewGuid().ToString();
            user.PasswordResetToken = token;
            user.PasswordResetTokenExpires = DateTime.UtcNow.AddHours(1);
            _context.SaveChanges();

            // Тут можна надіслати токен на email користувача
            return Ok(new { token });
        }

        [HttpPost("reset-password")]
        public IActionResult ResetPassword([FromBody] ResetPasswordDto dto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == dto.Email && u.PasswordResetToken == dto.Token);
            if (user == null || user.PasswordResetTokenExpires < DateTime.UtcNow)
                return BadRequest("Invalid token or email");

            user.Password = dto.NewPassword;
            user.PasswordResetToken = null;
            user.PasswordResetTokenExpires = null;
            _context.SaveChanges();

            return Ok();
        }


    }
}

