using WeatherCSApp.Data;

namespace WeatherCSTest.Data;

public class ForecastModelTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Constructor_RequiredProperties_ShouldSetValues()
    {
        // Arrange
        var cityName = "Seattle";
        var status = "Cloudy";

        // Act
        var model = new ForecastModel
        {
            CityName = cityName,
            Status = status,
            T = 15.5,
            MaxT = 20.0,
            MinT = 10.0,
            IconImagePath = "cloudy.png",
            Summary = "Overcast with light rain"
        };

        using (Assert.EnterMultipleScope())
        {
            // Assert
            Assert.That(model.CityName, Is.EqualTo(cityName));
            Assert.That(model.Status, Is.EqualTo(status));
            Assert.That(model.T, Is.EqualTo(15.5));
            Assert.That(model.MaxT, Is.EqualTo(20.0));
            Assert.That(model.MinT, Is.EqualTo(10.0));
            Assert.That(model.IconImagePath, Is.EqualTo("cloudy.png"));
            Assert.That(model.Summary, Is.EqualTo("Overcast with light rain"));
        }
    }

    [Test]
    public void OptionalProperties_Default_ShouldBeNull()
    {
        // Arrange & Act
        var model = new ForecastModel
        {
            CityName = "London",
            Status = "Rainy",
            T = 12.0,
            MaxT = 14.0,
            MinT = 8.0
        };

        using (Assert.EnterMultipleScope())
        {
            // Assert
            Assert.That(model.IconImagePath, Is.Null);
            Assert.That(model.Summary, Is.Null);
        }
    }
}
