using Serilog;
using System.Net.Http;
using WeatherCSApp.Data;

namespace WeatherCSApp.Service
{
    public class TransportService
    {
        private readonly static ILogger _logger = Log.ForContext<TransportService>();
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
                    //_logger.Debug("Received response: {ResponseJson}", responseJson);
                    return Response.FromJson(responseJson);
                });
                return response;
            }
            catch (Exception ex)
            {
                _logger.Error("Error fetching weather data: {Message}", ex.Message);
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
