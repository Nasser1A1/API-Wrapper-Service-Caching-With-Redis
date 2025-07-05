using Weather_API_Wrapper_Service.Entities;

namespace Weather_API_Wrapper_Service.Interfaces
{
    public interface IWeatherDbRepository
    {
        Task<WeatherLog?> GetWeatherAsync(Guid id);
        Task AddWeatherLogAsync(WeatherLog weatherLog);
        Task<IEnumerable<WeatherLog>> GetWeatherLogsAsync();
        Task<bool> WeatherLogExistsAsync(Guid id);
    }
}
