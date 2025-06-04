using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Domain.Entities;
using Weather.Domain.Repositiores;
using Weather.Services.Interfaces;
using Weather.Repo.Repositiores;


namespace Weather.Services.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IWeatherCacheRepository _cacheRepo;
        private readonly IWeatherApiClient _apiClient;

        public WeatherService(IWeatherCacheRepository cacheRepo, IWeatherApiClient apiClient)
        {
            _cacheRepo = cacheRepo;
            _apiClient = apiClient;
        }
        public async Task<WeatherData> GetWeatherAsync(string city)
        {
            // 1. Check cache
            var cached = await _cacheRepo.GetAsync(city);
            if (cached != null)
            {
                Console.WriteLine("Returned from cache ✅");
                return cached;
            }

            // 2. Call external API
            var fresh = await _apiClient.FetchWeatherAsync(city);

            // 3. Store in cache
            await _cacheRepo.StoreAsync(city, fresh);

            Console.WriteLine("Returned from API and stored in cache ✅");
            return fresh;
        }
    }


}

