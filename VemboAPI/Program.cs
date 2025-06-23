using Microsoft.EntityFrameworkCore;
using VemboAPI.Domain.Data;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Infrastructure.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // builder.Services.AddDbContext<VemboDbContext>(options =>
        //     options.UseNpgsql(builder.Configuration.GetConnectionString("DbContext")));

        builder.Services.AddDbContext<VemboDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DbContext"),
        x => x.MigrationsAssembly("VemboAPI.Domain") 
    ));


        // Add services to the container.
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<ITopicService, TopicService>();
        builder.Services.AddScoped<IUnitService, UnitService>();
        builder.Services.AddScoped<IPeriodService, PeriodService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
