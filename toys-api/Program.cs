using Microsoft.EntityFrameworkCore;
using Serilog.Events;
using Serilog;
using Data.Models;

namespace api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // LOGS
            string logsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
            Directory.CreateDirectory(logsFolder);
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.File(Path.Combine(logsFolder, "logs.txt"), rollingInterval: RollingInterval.Day)
                .CreateLogger();

            builder.Services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddSerilog();
            });

            // DB
            builder.Services.AddDbContext<toystoreContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            CompositeRoot.DependencyInjection(builder);

            // DEPLOY
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyCors",
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            builder.Services.AddLogging(loggingBuilder => { loggingBuilder.ClearProviders(); loggingBuilder.AddSerilog(); });
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors("MyCors");

            app.MapControllers();

            app.Run();
        }
    }
}