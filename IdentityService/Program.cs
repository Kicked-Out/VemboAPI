using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using VemboAPI.Domain.Entities;         // User і пов’язані ентіті
using VemboAPI.Infrastructure.Data;     // VemboDbContext

using IdentityService.Interfaces;       // Інтерфейси сервісів з твого IdentityService.*
using IdentityService.Services;         // Реалізації сервісів з твого IdentityService.*

var builder = WebApplication.CreateBuilder(args);

// ---------- Configuration ----------
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

// ---------- Database (обери свій провайдер) ----------
// PostgreSQL:
builder.Services.AddDbContext<VemboDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("VemboDb"),
        b => b.MigrationsAssembly("VemboAPI.Infrastructure")));

// АБО якщо у тебе SQL Server, закоментуй Npgsql вище і розкоментуй це:
// builder.Services.AddDbContext<VemboDbContext>(opt =>
//     opt.UseSqlServer(builder.Configuration.GetConnectionString("VemboDb"),
//         b => b.MigrationsAssembly("VemboAPI.Infrastructure")));

// ---------- ASP.NET Identity ----------
builder.Services
    .AddIdentityCore<User>(o =>
    {
        o.Password.RequireDigit = false;
        o.Password.RequireUppercase = false;
        o.Password.RequireNonAlphanumeric = false;
        o.Password.RequiredLength = 6;
    })
    .AddRoles<IdentityRole>()                // якщо ролі не потрібні — можеш прибрати
    .AddEntityFrameworkStores<VemboDbContext>()
    .AddDefaultTokenProviders();

// ---------- JWT ----------
var jwt = builder.Configuration.GetSection("Jwt");
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwt["Issuer"],
            ValidAudience = jwt["Audience"],
            IssuerSigningKey = key,
            ClockSkew = TimeSpan.FromSeconds(30)
        };
    });

builder.Services.AddAuthorization();

// ---------- Swagger ----------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ---------- DI: підключаємо ВСІ сервіси з твоєї папки IdentityService/Services ----------
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserStatisticService, UserStatisticService>();
builder.Services.AddScoped<IUserPeriodProgressService, UserPeriodProgressService>();
builder.Services.AddScoped<IUserTopicProgressService, UserTopicProgressService>();
builder.Services.AddScoped<IUserUnitProgressService, UserUnitProgressService>();
builder.Services.AddScoped<IUserLevelProgressService, UserLevelProgressService>();
builder.Services.AddScoped<IUserLessonProgressService, UserLessonProgressService>();
builder.Services.AddScoped<IUserExerciseMistakeService, UserExerciseMistakeService>();
builder.Services.AddScoped<IUserLeaderBoardEntryService, UserLeaderBoardEntryService>();
builder.Services.AddScoped<IUserAchievmentService, UserAchievmentService>();

// Якщо є ще інтерфейси/реалізації — додай їх тут у такому ж стилі

var app = builder.Build();

// ---------- Migrate DB on start ----------
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<VemboDbContext>();
    db.Database.Migrate();
}

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
