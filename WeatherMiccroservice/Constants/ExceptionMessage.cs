namespace WeatherMicroservice.Constants
{
    public class ExceptionMessage
    {
        public const string WEATHER_DATA_NOT_FOUND = "Weather data could not be retrieved. Please check the city and country.";
        public const string WEATHER_DATA_BY_SPECIFIED_DATE_NOT_FOUND_FROM_DATABASE = "No weather record/s has been found from the database for the specified date.";
        public const string CITY_AND_COUNTRY_FIELD_ARE_REQUIRED = "City and Country must be provided.";
        public const string DATE_IS_REQUIRED = "Date must be provided.";
        public const string WEATHER_DATA_CANNOT_BE_RETRIEVED_FROM_API = "Weather data could not be retrieved from the API. Please check the city and country.";
        public const string OPEN_WEATHER_MAP_API_KEY_NOT_DEFINED = "OpenWeatherMap API key is not defined in the configuration.";
        public const string SPECIFICED_CITY_OR_COUNTRY_NOT_FOUND = "Specified City or Country was not found.";
    }
}
