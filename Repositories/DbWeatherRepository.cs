using System.Data.Entity;
using Weather_API_Wrapper_Service.Data;
using Weather_API_Wrapper_Service.Entities;
using Weather_API_Wrapper_Service.Interfaces;

namespace Weather_API_Wrapper_Service.Repositories
{
    public class DbWeatherRepository : IWeatherDbRepository
    {
        private readonly ApplicationDbContext _context;

        public DbWeatherRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task? AddWeatherLogAsync(WeatherLog weatherLog)
        {
            await   _context.WeatherLogs.AddAsync(weatherLog);
            await _context.SaveChangesAsync();
        }

 

        public async Task<WeatherLog?> GetWeatherAsync(Guid id)
        {
            var weatherlog = await _context.WeatherLogs.FindAsync(id);
            return weatherlog;
        }

        public async Task<IEnumerable<WeatherLog>> GetWeatherLogsAsync()
        {
            var weatherlogs = await _context.WeatherLogs.ToListAsync();
            if (weatherlogs == null || !weatherlogs.Any())
            {
                return Enumerable.Empty<WeatherLog>();
            }
            return weatherlogs;
        }

        public async Task<bool> WeatherLogExistsAsync(Guid id)
        {
            var WeatherLogExisist = await _context.WeatherLogs.AnyAsync(x=> x.Id == id);
            return WeatherLogExisist;
        }
    }
}
