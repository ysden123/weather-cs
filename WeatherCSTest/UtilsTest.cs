using WeatherCSLib;

namespace WeatherCSTest
{
    internal class UtilsTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ApiKeyTest()
        {
            string? apiKey = Utils.ApiKey();
            Assert.That(apiKey, Is.Not.Null);
        }
    }
}
