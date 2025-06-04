using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Weather.Domain.Entities;
using Weather.Services.Interfaces;
using Weather.Services.Services;

namespace Weather_API_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet]
        public async Task<IActionResult> GetWeather([FromQuery] string city)
        {
            if (string.IsNullOrWhiteSpace(city))
                return BadRequest("City is required.");

            var data = await _weatherService.GetWeatherAsync(city);
            return data == null ? NotFound("No weather data found.") : Ok(data);
        }
    }
}
    

