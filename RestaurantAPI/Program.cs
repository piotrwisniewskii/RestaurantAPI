using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Middleware;
using RestaurantAPI.Services;
using Serilog;

namespace RestaurantAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Logging.ClearProviders();
            builder.Logging.AddConsole().SetMinimumLevel(LogLevel.Debug);

            builder.Services.AddControllers();
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddScoped<IRestaurantService, RestaurantService>();
            builder.Services.AddScoped<ErrorHandlingMiddleware>();
            builder.Services.AddDbContext<RestaurantDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

            builder.Host.UseSerilog((HostBuilderContext, LoggerConfiguration) =>
            {
                LoggerConfiguration.WriteTo.Console();

                LoggerConfiguration.WriteTo.File("C:\\Users\\piotr\\Desktop\\C#\\REST WebAPI\\RestaurantAPI\\info").MinimumLevel.Information();
            });


            var app = builder.Build();

            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}