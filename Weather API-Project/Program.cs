
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Weather.Domain.Repositiores;
using Weather.Services.Interfaces;
using Weather.Services.Services;
using Weather.Repo.Repositiores;

namespace Weather_API_Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //builder.Services.AddDbContext<DbContext>(options =>
            //options.UseNpgsql(ConnectionStrings));


            //Because it used for the 3party api
            builder.Services.AddScoped<IWeatherService, WeatherService>();
            builder.Services.AddScoped<IWeatherApiClient, VisualCrossingApiClient>();
            builder.Services.AddScoped<IWeatherCacheRepository, RedisWeatherCacheRepository>();
            builder.Services.AddHttpClient<VisualCrossingApiClient>();



            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = builder.Configuration.GetConnectionString("Redis");
                options.InstanceName = "weather-project";
            });


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("https://YOUR_USERNAME.github.io")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });



            var app = builder.Build();


            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Weather API V1");
                options.RoutePrefix = ""; // يخليه على الصفحة الرئيسية: https://localhost:5001/
            });

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
          

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.UseCors("AllowFrontend");

            app.Run();
        }
    }
}
