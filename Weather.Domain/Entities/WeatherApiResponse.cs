using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Weather.Domain.Entities
{
    public class WeatherApiResponse
    {
        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("days")]
        public List<WeatherDay> Days { get; set; }
    }
}
