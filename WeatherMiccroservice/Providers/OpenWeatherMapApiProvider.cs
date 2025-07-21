using Newtonsoft.Json;
using WeatherMicroservice.Constants;
using WeatherMicroservice.Extensions;
using WeatherMicroservice.Models.Entities;

namespace WeatherMicroservice.Providers
{
    public class OpenWeatherMapApiProvider : IWeatherApiProvider
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public OpenWeatherMapApiProvider(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient.ThrowIfNull(nameof(httpClient));
            _apiKey = configuration["OpenWeatherMap:ApiKey"] ?? throw new ArgumentException("OpenWeatherMap ApiKey is not defined.");
        }

        public async Task<WeatherData> GetWeatherDataAsync(string city, string country)
        {
            var weatherResponse = await GetWeather(city, country);
            if (weatherResponse != null)
            {
                var airQuality = await GetAirQuality(weatherResponse.Coordinates.Latitude, weatherResponse.Coordinates.Longitude);

                return new WeatherData
                {
                    City = weatherResponse.City,
                    Country = weatherResponse.System.Country,
                    Temperature = weatherResponse.Main.Temperature,
                    TemperatureMin = weatherResponse.Main.TemperatureMin,
                    TemperatureMax = weatherResponse.Main.TemperatureMax,
                    Humidity = weatherResponse.Main.Humidity,
                    WindSpeed = weatherResponse.Wind.Speed,
                    AirQualityIndex = airQuality,
                    WeatherDescription = string.Join(", ", weatherResponse.Weather.Select(w => w.Description)),
                    Date = DateOnly.FromDateTime(DateTimeOffset.FromUnixTimeSeconds(weatherResponse.DateTime).UtcDateTime),
                    RetrievedAt = DateTime.UtcNow,
                };
            }

            return null;
        }

        private async Task<OpenWeatherMapResponse> GetWeather(string city, string country)
        {
            var url = $"https://api.openweathermap.org/data/2.5/weather" +
                $"?q={city},{country}" +
                $"&appid={_apiKey}" +
                $"&units=metric";

            var result = await _httpClient.GetAsync(url);
            if (!result.IsSuccessStatusCode)
            {
                throw new Exception(ExceptionMessage.SPECIFICED_CITY_OR_COUNTRY_NOT_FOUND);
            }

            var content = await result.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<OpenWeatherMapResponse>(content);
            return response;
        }

        private async Task<AirQuality?> GetAirQuality(double latitude, double longitude)
        {
            var url = $"https://api.openweathermap.org/data/2.5/air_pollution" +
                $"?lat={latitude}" +
                $"&lon={longitude}" +
                $"&appid={_apiKey}" +
                $"&units=metric";

            var result = await _httpClient.GetAsync(url);
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<OpenWeatherMapAirQualityResponse>(content);
                if (response != null && response.List.Count > 0)
                {
                    var airQualityIndex = response.List[0].Main.AirQuality;
                    return airQualityIndex;
                }
            }

            return null;
        }
    }
}
