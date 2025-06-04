using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Domain.Entities;

namespace Weather.Domain.Repositiores
{
    public interface IWeatherCacheRepository
    {
        Task<WeatherData?> GetAsync(string city);
        Task StoreAsync(string city, WeatherData data);
    }
}
