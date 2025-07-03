using WeatherCSLib.Data;

namespace WeatherCSTest.Data
{
    internal class ResponseTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void FromJsonTest()
        {
            Response? response = null;
            using (var reader = new StreamReader("TestData/response1.json"))
            {
                var json = reader.ReadToEnd();
                response = Response.FromJson(json);
            }
            Assert.That(response, Is.Not.Null);
        }

        [Test]
        public void FromJson2Test()
        {
            Response? response = null;
            using (var reader = new StreamReader("TestData/response2.json"))
            {
                var json = reader.ReadToEnd();
                response = Response.FromJson(json);
            }
            Assert.That(response, Is.Not.Null);
        }
    }
}
