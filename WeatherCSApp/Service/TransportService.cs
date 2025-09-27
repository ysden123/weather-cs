using System.Net.Http;
using WeatherCSApp.Data;

namespace WeatherCSApp.Service
{
    public class TransportService
    {
        public static async Task<Response?> GetWeather(City city)
        {
            using var client = new HttpClient();
            try
            {
                var apiKey = Utils.ApiKey();
                var url = @$"https://api.pirateweather.net/forecast/{apiKey}/{city.Latitude},{city.Longitude}?units=ca";
                var response = await Task<Response>.Run(async () =>
                {
                    var responseJson = await client.GetStringAsync(url);
                    return Response.FromJson(responseJson);
                });
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
