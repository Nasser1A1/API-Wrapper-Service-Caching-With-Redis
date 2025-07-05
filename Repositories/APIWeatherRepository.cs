using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Reflection.PortableExecutable;
using System.Security.Claims;
using System.Text.Json;
using Weather_API_Wrapper_Service.Entities;
using Weather_API_Wrapper_Service.Helpers;
using Weather_API_Wrapper_Service.Interfaces;

namespace Weather_API_Wrapper_Service.Repositories
{
    public class APIWeatherRepository : IWeatherApiService
    {
        private readonly ApiClient _apiClient;
        private readonly string _apiKey;
        private readonly IHttpContextAccessor _httpContextAccessor;

        static string baseUrl = $"https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/";

        public APIWeatherRepository(IConfiguration configuration, ApiClient apiClient, IHttpContextAccessor httpContextAccessor)
        {
            _apiClient = apiClient;
            _apiKey = configuration["WeatherApi:Key"];
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<WeatherLog> GetWeatherAsync(string location, DateTime start, DateTime end)
        {
           var requestUrl = $"{baseUrl}{location}/{start:yyyy-MM-dd}/{end:yyyy-MM-dd}?unitGroup=metric&key={_apiKey}&contentType=json";
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("User-Agent", "MyApp/1.0");
            headers.Add("Accept", "application/json");
            headers.Add("X-API-Key", _apiKey);
            var responseData = await _apiClient.GetAsync<Dictionary<string, JsonElement>>(requestUrl,headers);
            var weatherLog = new WeatherLog
            {
              Location = (string)(responseData["location"] ?? "Unknown"),
              Condition = (string)responseData["condition"] ?? "Unknown",
              Temperature = (double)responseData["temperature"],

            };

            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException("User not authenticated.");

            weatherLog.UserId = Guid.Parse(userId);
            Debug.WriteLine($"UserId: {weatherLog.UserId}");
            if (responseData == null)
            {
                throw new Exception("Failed to retrieve weather data.");
            }

            return weatherLog;

        }
    }
}
