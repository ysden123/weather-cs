using WeatherCSApp.Data;

namespace WeatherCSTest;

public class TransportServiceTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task GetWeatherTest()
    {
        var city = new City() { Name = "Tambov", Latitude = 52.721169, Longitude = 41.45298 };
        Response? response = await WeatherCSApp.Service.TransportService.GetWeather(city);
        Assert.That(response, Is.Not.Null);
    }
}
