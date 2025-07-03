using WeatherCSLib.Data;

namespace WeatherCSTest;

public class TransportTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task GetWeatherTest()
    {
        var city = new City() { Name = "Tambov", Latitude = 52.721169, Longitude = 41.45298 };
        Response? response = await WeatherCSLib.Transport.GetWeather(city);
        Assert.That(response, Is.Not.Null);
    }
}
