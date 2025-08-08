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
using System.Security.Claims;

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

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("Email"));

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

        // Services
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<ITopicService, TopicService>();
        builder.Services.AddScoped<IUnitService, UnitService>();
        builder.Services.AddScoped<IPeriodService, PeriodService>();
        builder.Services.AddScoped<ILevelService, LevelService>();
        builder.Services.AddScoped<ILessonService, LessonService>();
        builder.Services.AddScoped<IExerciseService, ExerciseService>();
        builder.Services.AddScoped<IExerciseTypeService, ExerciseTypeService>();
        builder.Services.AddScoped<IAnswerService, AnswerService>();
        builder.Services.AddScoped<IQuestionService, QuestionService>();
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
        builder.Services.AddScoped<IUserStatisticService, UserStatisticService>();
        builder.Services.AddScoped<IUserLeaderBoardService, UserLeaderBoardService>();
        builder.Services.AddScoped<IUserAchievementService, UserAchievementService>();
        builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        builder.Services.AddScoped<IEmailService, EmailService>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        //app.UseHttpsRedirection();

        app.UseAuthentication(); // ⬅️ Це має бути до Authorization
        app.UseAuthorization();

        app.MapControllers();
        app.Run();
    }
}
