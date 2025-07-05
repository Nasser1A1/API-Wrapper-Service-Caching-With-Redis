using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Weather_API_Wrapper_Service.DTOs;
using Weather_API_Wrapper_Service.Entities;
using Weather_API_Wrapper_Service.Interfaces;
using Weather_API_Wrapper_Service.Interfaces.Caching;

namespace Weather_API_Wrapper_Service.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherDbRepository dbRepository;
        private readonly IWeatherApiService weatherApiService;
        private readonly ICacheService _cacheService;
        //  url = $"https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/{location}/{startDate}/{endDate}?unitGroup=metric&include=days&key={apiKey}&contentType=json";


        public WeatherController(IWeatherDbRepository dbRepository, IWeatherApiService weatherApiService, ICacheService cacheService)
        {
            this.dbRepository = dbRepository;
            this.weatherApiService = weatherApiService;
            _cacheService = cacheService;
        }
        [HttpPost("weather")]
        public async Task<IActionResult> GetWeather([FromBody] WeatherRequestDto weatherRequest)
        {
            // check cahce first
            if (weatherRequest == null || string.IsNullOrWhiteSpace(weatherRequest.Location) || weatherRequest.StartDate == default || weatherRequest.EndDate == default)
            {
                return BadRequest("Invalid weather request data.");
            }
            var cacheKey = $"Weather_{weatherRequest.Location}_{weatherRequest.StartDate:yyyy-MM-dd}_{weatherRequest.EndDate:yyyy-MM-dd}";
            var cachedData = _cacheService.GetDate<WeatherLog>(cacheKey);
          
            if (cachedData != null)
            {
                cachedData.ResponseSource = "CACHE";
                return Ok(cachedData);
            }
          

       



            var weatherLog = await weatherApiService.GetWeatherAsync(weatherRequest.Location, weatherRequest.StartDate, weatherRequest.EndDate);
            _cacheService.SetData(cacheKey, weatherLog);
            if (weatherLog == null)
            {
                return NotFound("Weather data not found for the specified location and date range.");
            }
            // log to database
            await dbRepository.AddWeatherLogAsync(weatherLog);
            return Ok(weatherLog);

        }

    }
}
