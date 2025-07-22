using Moq;
using WeatherMicroservice.Constants;
using WeatherMicroservice.Controllers;
using WeatherMicroservice.Interfaces;
using WeatherMicroservice.Models.DTO;
using WeatherMicroservice.Models.Entities;

namespace WeatherMicroservice.UnitTest;

public class WeatherControllerTests
{
    private readonly Mock<IWeatherService> _weatherServiceMock;
    private readonly WeatherController _weatherController;

    public WeatherControllerTests()
    {
        _weatherServiceMock = new Mock<IWeatherService>();
        _weatherController = new WeatherController(_weatherServiceMock.Object);
    }

    [Test]
    public async Task GetCurrentWeatherData_Should_Throw_Exception_When_City_Is_Null_Or_Empty()
    {
        //Arrange
        var failureMessage = string.Empty;
        var request = new WeatherDataRequest
        {
            City = string.Empty,
            Country = "TestCountry"
        };

        //Act
        try
        {
            var result = await _weatherController.GetCurrentWeatherData(request);
        }
        catch (ArgumentException ex)
        {
            failureMessage = ex.Message;
        }

        //Assert
        Assert.That(failureMessage, Is.EqualTo(ExceptionMessage.CITY_AND_COUNTRY_FIELD_ARE_REQUIRED));
    }

    [Test]
    public async Task GetCurrentWeatherData_Should_Throw_Exception_When_Country_Is_Null_Or_Empty()
    {
        //Arrange
        var failureMessage = string.Empty;
        var request = new WeatherDataRequest
        {
            City = "TestCity",
            Country = string.Empty,
        };

        //Act
        try
        {
            var result = await _weatherController.GetCurrentWeatherData(request);
        }
        catch (ArgumentException ex)
        {
            failureMessage = ex.Message;
        }

        //Assert
        Assert.That(failureMessage, Is.EqualTo(ExceptionMessage.CITY_AND_COUNTRY_FIELD_ARE_REQUIRED));
    }

    [Test]
    public async Task GetCurrentWeatherData_Should_Return_Weather_Data()
    {
        //Arrange
        var request = new WeatherDataRequest
        {
            City = "TestCity",
            Country = "TestCountry",
        };

        var weatherData = new WeatherData
        {
            City = "TestCity",
            Country = "TestCountry",
        };

        _weatherServiceMock.Setup(s => s.GetCurrentWeatherData(request))
            .ReturnsAsync(weatherData);

        //Act
        var result = await _weatherController.GetCurrentWeatherData(request);

        //Assert
        Assert.IsNotNull(result);
    }

    [Test]
    public async Task GetCurrentWeatherData_Should_Throw_Exception_When_Weather_Data_Result_Is_Null()
    {
        //Arrange
        var failureMessage = string.Empty;
        var request = new WeatherDataRequest
        {
            City = "TestCity",
            Country = "TestCountry",
        };

        _weatherServiceMock.Setup(s => s.GetCurrentWeatherData(request))
            .ReturnsAsync(() => null);

        //Act
        try
        {
            var result = await _weatherController.GetCurrentWeatherData(request);
        }
        catch (Exception ex)
        {
            failureMessage = ex.Message;
        }

        //Assert
        Assert.That(failureMessage, Is.EqualTo(ExceptionMessage.WEATHER_DATA_CANNOT_BE_RETRIEVED_FROM_API));
    }

    [Test]
    public async Task GetWeatherDataByDate_Should_Throw_Exception_When_City_Is_Null_Or_Empty()
    {
        //Arrange
        var failureMessage = string.Empty;
        var request = new WeatherDataWithDateRequest
        {
            City = string.Empty,
            Country = "TestCountry"
        };

        //Act
        try
        {
            var result = await _weatherController.GetWeatherDataByDate(request);
        }
        catch (ArgumentException ex)
        {
            failureMessage = ex.Message;
        }

        //Assert
        Assert.That(failureMessage, Is.EqualTo(ExceptionMessage.CITY_AND_COUNTRY_FIELD_ARE_REQUIRED));
    }

    [Test]
    public async Task GetWeatherDataByDate_Should_Throw_Exception_When_Country_Is_Null_Or_Empty()
    {
        //Arrange
        var failureMessage = string.Empty;
        var request = new WeatherDataWithDateRequest
        {
            City = "TestCity",
            Country = string.Empty,
        };

        //Act
        try
        {
            var result = await _weatherController.GetWeatherDataByDate(request);
        }
        catch (ArgumentException ex)
        {
            failureMessage = ex.Message;
        }

        //Assert
        Assert.That(failureMessage, Is.EqualTo(ExceptionMessage.CITY_AND_COUNTRY_FIELD_ARE_REQUIRED));
    }

    [Test]
    public async Task GetWeatherDataByDate_Should_Throw_Exception_When_Date_Specified_Is_Default_Value()
    {
        //Arrange
        var failureMessage = string.Empty;
        var request = new WeatherDataWithDateRequest
        {
            City = "TestCity",
            Country = "TestCountry",
            Date = default(DateOnly) // Default value
        };

        //Act
        try
        {
            var result = await _weatherController.GetWeatherDataByDate(request);
        }
        catch (ArgumentException ex)
        {
            failureMessage = ex.Message;
        }

        //Assert
        Assert.That(failureMessage, Is.EqualTo(ExceptionMessage.DATE_IS_REQUIRED));
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

        _weatherServiceMock
            .Setup(s => s.GetWeatherDataByDate(request))
            .ReturnsAsync(weatherData);

        // Act
        var result = await _weatherController.GetWeatherDataByDate(request);

        // Assert
        Assert.IsNotNull(result);
    }
}
