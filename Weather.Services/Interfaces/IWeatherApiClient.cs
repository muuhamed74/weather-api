using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Domain.Entities;

namespace Weather.Services.Interfaces
{
    public interface IWeatherApiClient
    {
        Task<WeatherData> FetchWeatherAsync(string city);

    }
}
