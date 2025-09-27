using WeatherCSApp.Data;

namespace WeatherCSTest.Data
{
    internal class WeatherConfigTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void DeserializeWeatherConfigTest()
        {
            var json =
                """
                {
                  "cities": [
                    {
                      "name": "TA",
                      "latitude": 123.4,
                      "longitude": 456.7
                    },
                    {
                      "name": "BU",
                      "latitude": 55.4,
                      "longitude": 33.7
                    }
                  ]
                }
                """;
            WeatherConfig? weatherConfig = WeatherConfig.FromJson(json);
            Assert.That(weatherConfig, Is.Not.Null);

            Console.WriteLine(weatherConfig);

            Assert.That(weatherConfig.Cities?.Length, Is.EqualTo(2));

            var city = weatherConfig.Cities?[1];
            Assert.That(city, Is.Not.Null);
            Assert.That(city.Name, Is.EqualTo("BU"));
            Assert.That(city.Latitude, Is.EqualTo(55.4));
            Assert.That(city.Longitude, Is.EqualTo(33.7));
        }


        [Test]
        public void SerializeWeatherConfigTest()
        {
            var json =
               """
                {
                  "cities": [
                    {
                      "name": "TA",
                      "latitude": 123.4,
                      "longitude": 456.7
                    },
                    {
                      "name": "BU",
                      "latitude": 55.4,
                      "longitude": 33.7
                    }
                  ]
                }
                """;
            WeatherConfig? weatherConfig = WeatherConfig.FromJson(json);
            Assert.That(weatherConfig, Is.Not.Null);

            var jsonNew = weatherConfig.ToJson();
            WeatherConfig? weatherConfigNew = WeatherConfig.FromJson(jsonNew);
            Assert.That(weatherConfigNew, Is.Not.Null);
            Assert.That(weatherConfigNew.ToString(), Is.EqualTo(weatherConfig.ToString()));
        }

        [Test]
        public void FromFileTest()
        {
            WeatherConfig? weatherConfig = WeatherConfig.FromFile("TestData/WeatherConfig.json");
            Assert.That(weatherConfig, Is.Not.Null);
            Assert.That(weatherConfig, Is.Not.Null);

            Console.WriteLine(weatherConfig);

            Assert.That(weatherConfig.Cities?.Length, Is.EqualTo(2));

            var city = weatherConfig.Cities?[1];
            Assert.That(city, Is.Not.Null);
            Assert.That(city.Name, Is.EqualTo("BU"));
            Assert.That(city.Latitude, Is.EqualTo(55.4));
            Assert.That(city.Longitude, Is.EqualTo(33.7));
        }
    }
}
