using WeatherCSLib.Data;

namespace WeatherCSLib
{
    public class ForecastService
    {
        private static readonly Dictionary<string, string> icons = new()
        {
            {"clear", "Resources\\clear.png" },
            {"clear-day", "Resources\\clear-day.png" },
            {"clear-night", "Resources\\clear-night.png" },
            {"cloudy", "Resources\\cloudy.png" },
            {"fog", "Resources\\fog.png" },
            {"partly-cloudy-day", "Resources\\partly-cloudy-day.png" },
            {"partly-cloudy-night", "Resources\\partly-cloudy-night.png" },
            {"rain", "Resources\\rain.png" },
            {"sleet", "Resources\\sleet.png" },
            {"snow", "Resources\\snow.png" },
            {"wind", "Resources\\wind.png" },
        };

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
                        Icon = response.Daily.Icon,
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
                        Icon = null,
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
    
        public static string? BuildIconPath(string iconName)
        {
            if (iconName == null)
                return null;

            icons.TryGetValue(iconName, out string? iconPath);
            string? theIconPath;
            if (iconPath != null)
            {
                theIconPath = Path.Combine(Directory.GetCurrentDirectory(), iconPath);
            }
            else
            {
                theIconPath = null;
            }
            return theIconPath;
        }
    }
}
