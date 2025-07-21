using Newtonsoft.Json;
using WeatherMicroservice.Models.Entities;

namespace WeatherMicroservice.Providers
{
    public class OpenWeatherMapAirQualityResponse
    {
        [JsonProperty("list")]
        public List<OpenWeatherMapAirQuality> List; 
    }

    public class OpenWeatherMapAirQuality
    {
        [JsonProperty("dt")]
        public long DateTime;

        [JsonProperty("main")]
        public OpenWeatherMapAQI Main;
    }

    public class OpenWeatherMapAQI
    {
        [JsonProperty("aqi")]
        public AirQuality AirQuality;
    }
}
