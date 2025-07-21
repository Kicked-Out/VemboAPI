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

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        var connectionString = builder.Configuration.GetConnectionString("DbContext");
        builder.Services.AddDbContext<VemboDbContext>(options => options.UseSqlServer(connectionString));
        // builder.Services.AddDbContext<VemboDbContext>(options => options.UseNpgsql(connectionString)); // якщо PostgreSQL

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
        builder.Services.AddSwaggerGen();


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
                IssuerSigningKey = new SymmetricSecurityKey(key)
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
        builder.Services.AddScoped<IAnswerService, AnswerService>();
        builder.Services.AddScoped<IQuestionService, QuestionService>();
        builder.Services.AddScoped<IUserPeriodProgressService, UserPeriodProgressService>();
        builder.Services.AddScoped<IUserTopicProgressService, UserTopicProgressService>();
        builder.Services.AddScoped<IUserUnitProgressService, UserUnitProgressService>();
        builder.Services.AddScoped<IUserLevelProgressService, UserLevelProgressService>();
        builder.Services.AddScoped<IUserLessonProgressService, UserLessonProgressService>();
        builder.Services.AddScoped<IUserExerciseMistakeService, UserExerciseMistakeService>();
        builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
