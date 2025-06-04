using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Weather.Domain.Entities
{
    public class WeatherDay
    {
        [JsonPropertyName("datetime")]
        public string Datetime { get; set; }

        [JsonPropertyName("temp")]
        public double Temp { get; set; }

        [JsonPropertyName("conditions")]
        public string Conditions { get; set; }
    }
}

