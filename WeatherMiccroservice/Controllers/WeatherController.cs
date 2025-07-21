using Microsoft.AspNetCore.Mvc;
using WeatherMicroservice.Constants;
using WeatherMicroservice.Extensions;
using WeatherMicroservice.Interfaces;
using WeatherMicroservice.Models.DTO;
using WeatherMicroservice.Models.Entities;

namespace WeatherMicroservice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService.ThrowIfNull(nameof(weatherService));
        }

        [HttpGet]
        [Route("current-weather")]
        public async Task<ActionResult<WeatherData>> GetCurrentWeatherData([FromQuery] WeatherDataRequest weatherDataRequest)
        {
            if (string.IsNullOrWhiteSpace(weatherDataRequest.City) || string.IsNullOrWhiteSpace(weatherDataRequest.Country))
            {
                throw new ArgumentException(ExceptionMessage.CITY_AND_COUNTRY_FIELD_ARE_REQUIRED);
            }

            var weather = await _weatherService.GetCurrentWeatherData(weatherDataRequest);
            if (weather == null)
            {
                throw new Exception(ExceptionMessage.WEATHER_DATA_CANNOT_BE_RETRIEVED_FROM_API);
            }

            return Ok(weather);
        }

        [HttpGet]
        [Route("lookup-weather")]
        public async Task<ActionResult<WeatherData>> GetWeatherDataByDate([FromQuery] WeatherDataWithDateRequest weatherDataWithDateRequest)
        {
            if (string.IsNullOrWhiteSpace(weatherDataWithDateRequest.City) || string.IsNullOrWhiteSpace(weatherDataWithDateRequest.Country))
            {
                throw new ArgumentException(ExceptionMessage.CITY_AND_COUNTRY_FIELD_ARE_REQUIRED);
            }

            if(weatherDataWithDateRequest.Date == default)
            {
                throw new ArgumentException(ExceptionMessage.DATE_IS_REQUIRED);
            }

            var weather = await _weatherService.GetWeatherDataByDate(weatherDataWithDateRequest);
            if(weather == null)
            {
                throw new Exception(ExceptionMessage.WEATHER_DATA_BY_SPECIFIED_DATE_NOT_FOUND_FROM_DATABASE);
            }

            return Ok(weather);
        }
    }
}
