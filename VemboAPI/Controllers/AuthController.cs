using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VemboAPI.Domain.DTOs;
using VemboAPI.Domain.Entities;
using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Infrastructure.Services;

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
        private readonly IUserManager _userManager;
        private readonly IEmailService _emailService;
        private readonly IAchievementService _achievementService;
        private readonly IQuestService _questService;

        public AuthController(VemboDbContext context, IJwtTokenGenerator jwtTokenGenerator, IPasswordHasher<User> passwordHasher, IConfiguration configuration, IUserManager userManager, IEmailService emailService, IAchievementService achievementService, IUserAchievementService userAchievementService, IQuestService questService, IUserQuestProgressService userQuestProgressService)
        {
            _context = context;
            _jwtTokenGenerator = jwtTokenGenerator;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
            _userManager = userManager;
            _emailService = emailService;
            _achievementService = achievementService;
            _questService = questService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == dto.Email);


            if (user == null)
                return Unauthorized("Invalid credentials");

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                return Unauthorized("Invalid password");
            }

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
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
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
                CurrentPeriodId = 1, // або null, якщо ще не прив'язано до курсу
            };

            _context.UserStatistics.Add(stat);
            _context.SaveChanges();

            var userPeriodProgress = new UserPeriodProgress
            {
                UserId = newUser.Id,
                PeriodId = 1,
                XP = 0,
                CompletedCount = 0
            };

            _context.UserPeriodProgresses.Add(userPeriodProgress);
            _context.SaveChanges();

            var userTopicProgress = new UserTopicProgress
            {
                UserId = newUser.Id,
                TopicId = 1,
                CompletedCount = 0
            };

            _context.UserTopicProgresses.Add(userTopicProgress);
            _context.SaveChanges();

            var userUnitProgress = new UserUnitProgress
            {
                UserId = newUser.Id,
                UnitId = 1,
                CompletedCount = 0
            };

            _context.UserUnitProgresses.Add(userUnitProgress);
            _context.SaveChanges();

            var userLevelProgress = new UserLevelProgress
            {
                UserId = newUser.Id,
                LevelId = 1,
                CompletedCount = 0

            };

            _context.UserLevelProgresses.Add(userLevelProgress);
            _context.SaveChanges();

            var userLessonProgress1 = new UserLessonProgress
            {
                UserId = newUser.Id,
                LessonId = 1,
                CompletedCount = 0
            };

            var userLessonProgress2 = new UserLessonProgress
            {
                UserId = newUser.Id,
                LessonId = 2,
                CompletedCount = 0
            };

            var userLessonProgress3 = new UserLessonProgress
            {
                UserId = newUser.Id,
                LessonId = 3,
                CompletedCount = 0
            };

            var userLessonProgress4 = new UserLessonProgress
            {
                UserId = newUser.Id,
                LessonId = 4,
                CompletedCount = 0
            };

            _context.UserLessonProgresses.Add(userLessonProgress1);
            _context.UserLessonProgresses.Add(userLessonProgress2);
            _context.UserLessonProgresses.Add(userLessonProgress3);
            _context.UserLessonProgresses.Add(userLessonProgress4);
            await _context.SaveChangesAsync();

            var achievements = await _achievementService.GetAllAsync();

            foreach (var achievement in achievements)
            {
                UserAchievement userAchievement = new UserAchievement
                {
                    UserId = newUser.Id,
                    AchievementId = achievement.Id,
                    CurrentLevel = 1,
                    Progress = 0,
                    EarnedAt = DateTime.Now
                };

                await _context.UserAchievements.AddAsync(userAchievement);
            }

            await _context.SaveChangesAsync();

            var dailyQuests = await _questService.GetCurrentDaily();

            foreach (var dailyQuest in dailyQuests)
            {
                UserQuestProgress userDailyQuestProgress = new UserQuestProgress
                {
                    UserId = newUser.Id,
                    QuestId = dailyQuest.Id,
                    Progress = 0,
                    IsCompleted = false,
                };

                await _context.UserQuestProgresses.AddAsync(userDailyQuestProgress);
            }

            var monthlyQuest = await _questService.GetCurrentMonthly();

            UserQuestProgress userMonthlyQuestProgress = new UserQuestProgress
            {
                UserId = newUser.Id,
                QuestId = monthlyQuest.Id,
                Progress = 0,
                IsCompleted = false,
            };

            await _context.UserQuestProgresses.AddAsync(userMonthlyQuestProgress);

            await _context.SaveChangesAsync();

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
        
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            if (dto.Email == null)
            {
                return NotFound();
            }

            var user = _context.Users.FirstOrDefault(u => u.Email == dto.Email);
            if (user == null)
                return NotFound("User not found");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var callback = $"http://localhost:5173/reset-password?token={token}&email={user.Email}";
            await _emailService.SendEmailAsync(user.Email, "Vembo Password reset token", callback);

            return Ok();
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == dto.Email);
            if (user == null)
                return NotFound("User not found");

            user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);

            _context.Update(user);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}

