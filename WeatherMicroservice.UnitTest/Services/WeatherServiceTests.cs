using Moq;
using WeatherMicroservice.Interfaces;
using WeatherMicroservice.Models.DTO;
using WeatherMicroservice.Models.Entities;
using WeatherMicroservice.Providers;
using WeatherMicroservice.Services;

namespace WeatherMicroservice.UnitTest.Services
{
    public class WeatherServiceTests
    {
        private readonly Mock<IWeatherApiProvider> _weatherApiProviderMock;
        private readonly Mock<IWeatherRepository> _weatherRepositoryMock;
        private readonly WeatherService _weatherService;

        public WeatherServiceTests()
        {
            _weatherApiProviderMock = new Mock<IWeatherApiProvider>();
            _weatherRepositoryMock = new Mock<IWeatherRepository>();
            _weatherService = new WeatherService(_weatherApiProviderMock.Object, _weatherRepositoryMock.Object);
        }

        [Test]
        public async Task GetCurrentWeatherData_Should_Return_Existing_Weather_Data_When_Weather_Data_Already_Exists()
        {
            // Arrange
            var request = new WeatherDataRequest { City = "TestCity", Country = "TestCountry" };
            var existingWeatherData = new WeatherData
            {
                City = "TestCity",
                Country = "TestCountry",
            };

            _weatherRepositoryMock
                .Setup(r => r.GetByDateAndLocation(It.IsAny<DateOnly>(), request.City, request.Country))
                .ReturnsAsync(existingWeatherData);

            var result = await _weatherService.GetCurrentWeatherData(request);

            // Assert
            Assert.That(result, Is.EqualTo(existingWeatherData));
        }

        [Test]
        public async Task GetCurrentWeatherData_Should_Fetch_And_Save_New_Weather_Data_When_No_Existing_Weather_Data()
        {
            // Arrange
            var request = new WeatherDataRequest { City = "TestCity", Country = "TestCountry" };
            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            _weatherRepositoryMock
                .Setup(r => r.GetByDateAndLocation(today, request.City, request.Country))
                .ReturnsAsync((WeatherData?)null);

            var newWeatherData = new WeatherData
            {
                City = "TestCity",
                Country = "TestCountry",
                Date = today,
            };

            _weatherApiProviderMock
                .Setup(p => p.GetWeatherDataAsync(request.City, request.Country))
                .ReturnsAsync(newWeatherData);

            _weatherRepositoryMock
                .Setup(r => r.UpdateOrAddAsync(newWeatherData))
                .ReturnsAsync(newWeatherData);

            // Act
            var result = await _weatherService.GetCurrentWeatherData(request);

            // Assert
            Assert.That(result, Is.EqualTo(newWeatherData));
        }

        [Test]
        public void GetCurrentWeatherData_Should_Throw_ArgumentNullException_When_WeatherDataRequest_Is_Null()
        {
            // Act & Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _weatherService.GetCurrentWeatherData(null!));
        }

        [Test]
        public async Task GetWeatherDataByDate_Should_Return_Weather_Data_For_Specified_Date_And_Location()
        {
            // Arrange
            var request = new WeatherDataWithDateRequest
            {
                City = "TestCity",
                Country = "TestCountry",
                Date = DateOnly.FromDateTime(DateTime.UtcNow)
            };
            var weatherData = new WeatherData
            {
                City = "TestCity",
                Country = "TestCountry",
                Date = request.Date,
            };
            _weatherRepositoryMock
                .Setup(r => r.GetByDateAndLocation(request.Date, request.City, request.Country))
                .ReturnsAsync(weatherData);

            // Act
            var result = await _weatherService.GetWeatherDataByDate(request);

            // Assert
            Assert.That(result, Is.EqualTo(weatherData));
        }

        [Test]
        public void GetWeatherDataByDate_Should_Throw_ArgumentNullException_When_Weather_Data_Request_Is_Null()
        {
            // Act & Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _weatherService.GetWeatherDataByDate(null!));
        }
    }
}
