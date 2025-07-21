using WeatherMicroservice.Models.DTO;
using WeatherMicroservice.Models.Entities;

namespace WeatherMicroservice.Interfaces
{
    public interface IWeatherService
    {
        /// <summary>
        /// Retrieves weather data
        /// </summary>
        /// <param name="city"></param>
        /// <param name="country"></param>
        /// <returns></returns>
        Task<WeatherData> GetCurrentWeatherData(WeatherDataRequest weatherDataRequest);

        /// <summary>
        /// Retrieves weather data of a specified date.
        /// </summary>
        /// <param name="weatherDataRequest"></param>
        /// <returns></returns>
        Task<WeatherData> GetWeatherDataByDate(WeatherDataWithDateRequest weatherDataRequest);
    }
}
