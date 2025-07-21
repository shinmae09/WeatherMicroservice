namespace WeatherMicroservice.Models.Entities
{
    public class WeatherData
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public double Temperature { get; set; }
        public double TemperatureMin { get; set; }
        public double TemperatureMax { get; set; }
        public double Humidity { get; set; }
        public double WindSpeed { get; set; }
        public AirQuality? AirQualityIndex { get; set; }
        public string WeatherDescription { get; set; }
        public DateOnly Date { get; set; }
        public DateTime RetrievedAt { get; set; }
    }
}
