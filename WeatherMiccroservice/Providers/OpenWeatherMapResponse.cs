using Newtonsoft.Json;

namespace WeatherMicroservice.Providers
{
    public class OpenWeatherMapResponse
    {
        [JsonProperty("main")]
        public OpenWeatherMapTemperature Main;

        [JsonProperty("weather")]
        public List<OpenWeatherMapWeather> Weather;

        [JsonProperty("wind")]
        public OpenWeatherMapWind Wind;

        [JsonProperty("sys")]
        public OpenWeatherMapSystem System;

        [JsonProperty("name")]
        public string City;

        [JsonProperty("coord")]
        public OpenWeatherMapCoordinates Coordinates;

        [JsonProperty("dt")]
        public long DateTime;
    }

    public class OpenWeatherMapTemperature
    {
        [JsonProperty("temp")]
        public double Temperature;

        [JsonProperty("temp_min")]
        public double TemperatureMin;

        [JsonProperty("temp_max")]
        public double TemperatureMax;

        [JsonProperty("humidity")]
        public double Humidity;
    }

    public class OpenWeatherMapWind
    {
        [JsonProperty("speed")]
        public double Speed;
    }

    public class OpenWeatherMapSystem
    {
        [JsonProperty("country")]
        public string Country;
    }

    public class OpenWeatherMapWeather
    {
        [JsonProperty("description")]
        public string Description;
    }

    public class OpenWeatherMapCoordinates
    {
        [JsonProperty("lat")]
        public double Latitude;

        [JsonProperty("long")]
        public double Longitude;
    }
}
