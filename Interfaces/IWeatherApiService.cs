using Weather_API_Wrapper_Service.Entities;

namespace Weather_API_Wrapper_Service.Interfaces
{
    public interface IWeatherApiService
    {
        Task<WeatherLog> GetWeatherAsync(string location, DateTime start, DateTime end);
    }
}
