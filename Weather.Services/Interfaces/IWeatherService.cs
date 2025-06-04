using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Domain.Entities;

namespace Weather.Services.Interfaces
{
    public interface IWeatherService
    {
        Task<WeatherData> GetWeatherAsync(string city);
    }
}
