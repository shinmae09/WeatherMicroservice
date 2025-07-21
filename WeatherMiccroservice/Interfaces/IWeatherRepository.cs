using WeatherMicroservice.Models.Entities;

namespace WeatherMicroservice.Interfaces
{
    public interface IWeatherRepository : IRepository<WeatherData>
    {
        Task<WeatherData> GetByDateAndLocation(DateOnly date, string city, string country);
    }
}
