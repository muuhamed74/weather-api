using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Weather.Domain.Entities;
using Weather.Domain.Repositiores;

namespace Weather.Repo.Repositiores
{
    public class RedisWeatherCacheRepository: IWeatherCacheRepository 
    {
        private readonly IDistributedCache _cache;

        public RedisWeatherCacheRepository(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<WeatherData?> GetAsync(string city)
        {
            var key = GetCacheKey(city);
            var json = await _cache.GetStringAsync(key);

            if (string.IsNullOrEmpty(json))
                return null;

            return JsonSerializer.Deserialize<WeatherData>(json);
        }



        public async Task StoreAsync(string city, WeatherData data)
        {
            var key = GetCacheKey(city);
            var json = JsonSerializer.Serialize(data);

            Console.WriteLine($"Storing key: {key} with data: {json}");

            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(12)
            };

            await _cache.SetStringAsync(key, json, options);
        }
        private string GetCacheKey(string city) => $"weather:{city.ToLower()}";
    }
}
