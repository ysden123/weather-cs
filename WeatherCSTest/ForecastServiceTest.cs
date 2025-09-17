using WeatherCSLib;
using WeatherCSLib.Data;

namespace WeatherCSTest;

public class ForecastServiceTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task GetForecastTest()
    {
        var city = new City() { Name = "Tambov", Latitude = 52.721169, Longitude = 41.45298 };
        Forecast? forecast = await ForecastService.GetForecast(city);
        Assert.That(forecast, Is.Not.Null);
        Console.WriteLine(forecast);
    }

    [Test]
    public async Task GetForecastAllTest()
    {
        WeatherConfig? weatherConfig = WeatherConfig.FromFile("TestData/WeatherConfig2.json");
        Assert.That(weatherConfig, Is.Not.Null);
        if (weatherConfig != null && weatherConfig.Cities != null)
        {
            List<Forecast> forecasts = await ForecastService.GetForecastAll(weatherConfig.Cities);
            Assert.That(forecasts, Is.Not.Null);
            foreach (var forecast in forecasts)
            {
                Console.WriteLine(forecast);
            }
        }
    }
}
