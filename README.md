Weather API (.NET 8) with Redis Caching

This project is a simple yet scalable (ASP.NET Core 8 Web API) that retrieves weather data for a given city
using the (Visual Crossing Weather API) and implements (Redis caching) to avoid repeated API calls and improve performance.

---

Project Architecture

The architecture is clean and follows SOLID principles with proper separation of concerns:

Main Components

| Layer | Class | Responsibility |
|-------|-------|----------------|
| **Controller** | `WeatherController` | Exposes a GET endpoint to fetch weather data. Depends on `IWeatherService`. |
| **Service Layer** | `WeatherService` | Coordinates fetching data from cache or API. Depends on `IWeatherCacheRepository` and `IWeatherApiClient`. |
| **External API Client** | `VisualCrossingApiClient` | Responsible for calling the external Visual Crossing API and mapping its response to the domain model. |
| **Cache Layer** | `RedisWeatherCacheRepository` | Implements `IWeatherCacheRepository` using `IDistributedCache` and Redis for caching results. |
| **Models** | `WeatherData`, `WeatherApiResponse` | DTOs to represent the weather response and API structure. |

 
 Workflow

1. Controller receives a city name → calls `WeatherService`.
2. `WeatherService` checks Redis cache via `RedisWeatherCacheRepository`.
3. If **not cached**, it calls `VisualCrossingApiClient` to fetch new data.
4. New data is **stored in Redis** and returned to the client.

---

Technologies Used

- ASP.NET Core 8
- Redis (via `StackExchange.Redis`)
- Visual Crossing API
- Swagger for API documentation
- Docker (optional, for Redis)
- JSON serialization via `System.Text.Json`

Example usage of endpoint 

  **GET /api/weather?city=Cairo**

