using Microsoft.EntityFrameworkCore;
using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using VemboAPI.Domain;
using VemboAPI.Infrastructure;
using FluentValidation.AspNetCore;
using VemboAPI.Domain.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;
using VemboAPI.Domain.Entities;
using System.Security.Claims;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using System.Text.Json;
using Hangfire;
using Hangfire.SqlServer;
using VemboAPI.Jobs;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Додай це, якщо ти в custom CLI або консольному застосунку:
        builder.Configuration
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);


        var connectionString = builder.Configuration.GetConnectionString("DbContext");
        builder.Services.AddDbContext<VemboDbContext>(options => options.UseSqlServer(connectionString));

        builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

        builder.Services.AddControllers()
            .AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblyContaining<Program>();
                fv.AutomaticValidationEnabled = true;
            });

        builder.Services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = false;
        });

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowLocalhost",
                policy =>
                {
                    policy.WithOrigins("http://localhost:5173")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[]{}
                }
            });
        builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("Email"));
        // Redis (IDistributedCache)
        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = builder.Configuration["Redis:Configuration"];
            options.InstanceName = builder.Configuration["Redis:InstanceName"];
        });

        // JSON options для серіалізації в кеші
        builder.Services.Configure<JsonSerializerOptions>(opts =>
        {
            opts.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            opts.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
        });

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new() { Title = "VemboAPI", Version = "v1" });

            c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                Description = "Enter 'Bearer' [space] and then your token.\n\nExample: Bearer eyJhbGciOiJIUzI1NiIs..."
            });

            c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
            {
                {
                    new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                    {
                        Reference = new Microsoft.OpenApi.Models.OpenApiReference
                        {
                            Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    },
                    new List<string>()
                }
            });
        });

        var jwtSettings = builder.Configuration.GetSection("Jwt");
        var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key),
                RoleClaimType = ClaimTypes.Role 
            };
        });

        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<ITopicService, TopicService>();
        builder.Services.AddScoped<IUnitService, UnitService>();
        builder.Services.AddScoped<IPeriodService, PeriodService>();
        builder.Services.AddScoped<ILevelService, LevelService>();
        builder.Services.AddScoped<ILessonService, LessonService>();
        builder.Services.AddScoped<IExerciseService, ExerciseService>();
        builder.Services.AddScoped<IExerciseTypeService, ExerciseTypeService>();
        builder.Services.AddScoped<IAchievementService, AchievementService>();
        builder.Services.AddScoped<IAchievementLevelService, AchievementLevelService>();
        builder.Services.AddScoped<IAnswerService, AnswerService>();
        builder.Services.AddScoped<IQuestionService, QuestionService>();
        builder.Services.AddScoped<IUserLeaderBoardService, UserLeaderBoardService>();
        builder.Services.AddScoped<IUserAchievementService, UserAchievementService>();
        builder.Services.AddScoped<IUserStatisticService, UserStatisticService>();
        builder.Services.AddScoped<IUserPeriodProgressService, UserPeriodProgressService>();
        builder.Services.AddScoped<IUserTopicProgressService, UserTopicProgressService>();
        builder.Services.AddScoped<IUserUnitProgressService, UserUnitProgressService>();
        builder.Services.AddScoped<IUserLevelProgressService, UserLevelProgressService>();
        builder.Services.AddScoped<IUserLessonProgressService, UserLessonProgressService>();
        builder.Services.AddScoped<IUserExerciseMistakeService, UserExerciseMistakeService>();
        builder.Services.AddScoped<ILevelTypeService, LevelTypeService>();
        builder.Services.AddScoped<IGuideBookService, GuideBookService>();
        builder.Services.AddScoped<IAchievementLevelService, AchievementLevelService>();
        builder.Services.AddScoped<IAchievementService, AchievementService>();
        builder.Services.AddScoped<IBadgeService, BadgeService>();
        builder.Services.AddScoped<IUserStatisticService, UserStatisticService>();
        builder.Services.AddScoped<IUserLeaderBoardService, UserLeaderBoardService>();
        builder.Services.AddScoped<IUserAchievementService, UserAchievementService>();
        builder.Services.AddScoped<IBadgeService, BadgeService>();
        builder.Services.AddScoped<IUserBadgeService, UserBadgeService>();
        builder.Services.AddScoped<IUserStreakService, UserStreakService>();
        builder.Services.AddScoped<IUserStreakDayService, UserStreakDayService>();
        builder.Services.AddScoped<IMedalService, MedalService>();
        builder.Services.AddScoped<IUserMedalService, UserMedalService>();
        builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        builder.Services.AddScoped<IQuestService, QuestService>();
        builder.Services.AddScoped<IDailyQuestService, DailyQuestService>();
        builder.Services.AddScoped<IUserQuestService, UserQuestService>();
        builder.Services.AddScoped<IEmailSender, EmailService>();
        builder.Services.AddScoped<IUserManager, UserManager>();
        builder.Services.AddSingleton<ICacheService, RedisCacheService>();
        builder.Services.AddSingleton<IContentVersionService, ContentVersionService>();

        builder.Services.AddTransient<CacheWarmupJob>(); 

        var sql = builder.Configuration.GetConnectionString("DbContext");

        builder.Services.AddHangfire(cfg => cfg
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(sql, new SqlServerStorageOptions
            {
                PrepareSchemaIfNecessary = true,
                QueuePollInterval = TimeSpan.FromSeconds(15)
            })
        );
        builder.Services.AddHangfireServer();

        builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors("AllowLocalhost");

        app.UseDeveloperExceptionPage();

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseHangfireDashboard("/hangfire"); 

        BackgroundJob.Enqueue<CacheWarmupJob>(j => j.RunAsync());
        RecurringJob.AddOrUpdate<CacheWarmupJob>(
            "warm-content-cache",
            j => j.RunAsync(),
            "0 3 * * *");
        app.MapControllers();
        app.Run();
    }
}
