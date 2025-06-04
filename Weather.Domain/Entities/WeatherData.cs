using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Domain.Entities
{
    public class WeatherData
    {
        
        public string City { get; set; }
        public double Temperature { get; set; }
        public DateTime RetrievedAt { get; set; }
    }
}
