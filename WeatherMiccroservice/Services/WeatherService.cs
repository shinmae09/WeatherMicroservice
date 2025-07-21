using WeatherMicroservice.Extensions;
using WeatherMicroservice.Interfaces;
using WeatherMicroservice.Models.DTO;
using WeatherMicroservice.Models.Entities;
using WeatherMicroservice.Providers;

namespace WeatherMicroservice.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IWeatherApiProvider _weatherApiProvider;
        private readonly IWeatherRepository _weatherRepository;
        public WeatherService(
            IWeatherApiProvider weatherApiProvider, 
            IWeatherRepository weatherRepository)
        {
            _weatherApiProvider = weatherApiProvider.ThrowIfNull(nameof(weatherApiProvider));
            _weatherRepository = weatherRepository.ThrowIfNull(nameof(weatherRepository));
        }

        public async Task<WeatherData> GetCurrentWeatherData(WeatherDataRequest weatherDataRequest)
        {
            weatherDataRequest.ThrowIfNull(nameof(weatherDataRequest));

            var existingWeatherData = await _weatherRepository.GetByDateAndLocation(
                DateOnly.FromDateTime(DateTime.UtcNow),
                weatherDataRequest.City,
                weatherDataRequest.Country);
            
            // If weather data for today already exists, return it
            if (existingWeatherData != null)
            {
                return existingWeatherData;
            }

            // Else get new weather data from the API and save it
            var newWeatherData = await _weatherApiProvider.GetWeatherDataAsync(weatherDataRequest.City, weatherDataRequest.Country);
            return await _weatherRepository.UpdateOrAddAsync(newWeatherData);
        }

        public async Task<WeatherData> GetWeatherDataByDate(WeatherDataWithDateRequest weatherDataWithDateRequest)
        {
            weatherDataWithDateRequest.ThrowIfNull(nameof(weatherDataWithDateRequest));

            // Get weather data for the specified date and location exists
           return await _weatherRepository.GetByDateAndLocation(
                weatherDataWithDateRequest.Date,
                weatherDataWithDateRequest.City,
                weatherDataWithDateRequest.Country);
        }
    }
}
