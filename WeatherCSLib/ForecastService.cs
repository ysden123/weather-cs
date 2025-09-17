using WeatherCSLib.Data;

namespace WeatherCSLib
{
    public class ForecastService
    {
        public static async Task<Forecast> GetForecast(City city)
        {
            try
            {
                Forecast forecast;
                Response? response = await Transport.GetWeather(city);
                if (response != null)
                {
                    forecast = new Forecast()
                    {
                        CityName = city.Name,
                        Icon = "",
                        Status = "Done",
                        MaxT = response.Daily.FindMaxTemperature(),
                        MinT = response.Daily.FindMinTemperature(),
                        Summary = response.Daily.Summary
                    };
                }
                else
                {
                    forecast = new Forecast()
                    {
                        CityName = city.Name,
                        Icon = "",
                        Status = "Error",
                        MaxT = 0.0,
                        MinT = 0.0,
                        Summary = ""
                    };
                }
                return forecast;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public static async Task<List<Forecast>> GetForecastAll(City[]? cities)
        {
            var forecastTasks = new List<Task<Forecast>>();
            foreach (var city in cities!)
            {
                forecastTasks.Add(GetForecast(city));
            }
            await Task.WhenAll(forecastTasks);
            var forecasts = new List<Forecast>();
            foreach (var forecastTask in forecastTasks)
            {
                forecasts.Add(forecastTask.Result);
            }
            return forecasts;
        }
    }
}
