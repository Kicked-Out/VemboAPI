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
        private readonly IUserManager _userManager;
        private readonly IEmailSender _emailSender;

        public AuthController(
            VemboDbContext context,
            IJwtTokenGenerator jwtTokenGenerator,
            IConfiguration configuration,
            IUserManager userManager,
            IEmailSender emailSender)
        {
            _context = context;
            _jwtTokenGenerator = jwtTokenGenerator;
            _configuration = configuration;
            _userManager = userManager;
            _emailSender = emailSender;
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
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == dto.Email);
            if (user == null)
                return NotFound("User not found");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callback = $"https://example.com/reset-password?token={token}&email={user.Email}";
            await _emailSender.SendEmailAsync(user.Email, "Vembo Password reset token", callback);

            return Ok();
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == dto.Email);
            if (user == null)
                return NotFound("User not found");

            var result = await _userManager.ResetPasswordAsync(user, dto.Token!, dto.Password!);
            if (!result)
                return BadRequest("Invalid token or email");

            return Ok();
        }


    }
}

