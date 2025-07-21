namespace WeatherMicroservice.Models.DTO
{
    public class WeatherDataWithDateRequest : WeatherDataRequest
    {
        public DateOnly Date { get; set; }
    }
}
