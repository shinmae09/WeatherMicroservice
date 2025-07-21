using Microsoft.EntityFrameworkCore;
using WeatherMicroservice.Database;
using WeatherMicroservice.Extensions;
using WeatherMicroservice.Interfaces;
using WeatherMicroservice.Models.Entities;

namespace WeatherMicroservice.Repositories
{
    public class WeatherRepository : IWeatherRepository
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<WeatherData> _weatherData;

        public WeatherRepository(AppDbContext context)
        {
            _context = context.ThrowIfNull(nameof(context));
            _weatherData = _context.Set<WeatherData>();
        }

        public async Task<WeatherData> AddAsync(WeatherData entity)
        {
            entity.ThrowIfNull(nameof(entity));

            _weatherData.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var weather = await GetByIdAsync(id);
            if (weather == null)
            {
                throw new InvalidOperationException(string.Format("Weather Data Id not found.", id));
            }

            _weatherData.Remove(weather);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<WeatherData>> GetAllAsync()
        {
            return await _weatherData.ToListAsync();
        }

        public async Task<WeatherData> GetByDateAndLocation(DateOnly date, string city, string country)
        {
            return await _weatherData.FirstOrDefaultAsync(i => 
                i.Date == date && 
                i.City == city && 
                i.Country == country);
        }

        public async Task<WeatherData> GetByIdAsync(int id)
        {
            return await _weatherData.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<WeatherData> UpdateOrAddAsync(WeatherData entity)
        {
            entity.ThrowIfNull(nameof(entity));

            var existingWeather = await GetByDateAndLocation(entity.Date, entity.City, entity.Country);
            if (existingWeather != null)
            {
                existingWeather.Temperature = entity.Temperature;
                existingWeather.TemperatureMin = entity.TemperatureMin;
                existingWeather.TemperatureMax = entity.TemperatureMax;
                existingWeather.Humidity = entity.Humidity;
                existingWeather.WindSpeed = entity.WindSpeed;
                existingWeather.AirQualityIndex = entity.AirQualityIndex;
                existingWeather.WeatherDescription = entity.WeatherDescription;
                existingWeather.RetrievedAt = entity.RetrievedAt;

                _weatherData.Update(existingWeather);
                await _context.SaveChangesAsync();

                return existingWeather;
            }
            else
            {
                _weatherData.Add(entity);
                await _context.SaveChangesAsync();

                return entity;
            }
        }
    }
}
