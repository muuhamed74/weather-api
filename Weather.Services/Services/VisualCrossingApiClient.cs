using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Weather.Domain.Entities;
using Weather.Services.Interfaces;
using System.Text.Json.Serialization;

namespace Weather.Services.Services
{
    public class VisualCrossingApiClient : IWeatherApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public VisualCrossingApiClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }
        public async Task<WeatherData> FetchWeatherAsync(string city)
        {
            var baseUrl = _config["WeatherApi:BaseUrl"];
            var apiKey = _config["WeatherApi:ApiKey"];
            var unitGroup = _config["WeatherApi:UnitGroup"];
            var contentType = _config["WeatherApi:ContentType"];

            var fullUrl = $"{baseUrl}{city}?unitGroup={unitGroup}&key={apiKey}&contentType={contentType}";
            var response = await _httpClient.GetAsync(fullUrl);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<WeatherApiResponse>(content);

            Console.WriteLine("API Raw Response:");
            Console.WriteLine(content);
            if (result != null && result.Days != null && result.Days.Any())
            {
                var firstDay = result.Days.First();

                var weatherData = new WeatherData
                {
                    City = result.Address,
                    Temperature = firstDay.Temp,
                    RetrievedAt = DateTime.UtcNow
                };

                return weatherData;
            }
            else
            {
                // المشكله هنا
                // Error handling هنا
                throw new Exception("Weather data is unavailable or malformed.");
            }

        }
    }
}
