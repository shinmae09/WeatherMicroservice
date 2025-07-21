using WeatherMicroservice.Models.Entities;

namespace WeatherMicroservice.Providers
{
    public interface IWeatherApiProvider
    {
        Task<WeatherData> GetWeatherDataAsync(string city, string country);
    }
}
