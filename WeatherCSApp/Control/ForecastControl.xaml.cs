using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Controls;
using WeatherCSLib;
using WeatherCSLib.Data;

namespace WeatherCSApp.Control
{
    /// <summary>
    /// Interaction logic for ForecastControl.xaml
    /// </summary>
    public partial class ForecastControl : UserControl
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

        private readonly WeatherConfig? _weatherConfig;

        public ForecastControl()
        {
            InitializeComponent();
            _weatherConfig = WeatherConfig.GetAppWeatherConfig();
            ObservableCollection<ForecastModel> forecasts = FillInitial();
            forecastListView.ItemsSource = forecasts;

            FillData(forecasts);
        }

        private ObservableCollection<ForecastModel> FillInitial()
        {
            ObservableCollection<ForecastModel> forecasts = [];
            if (_weatherConfig != null)
            {
                City[]? cities = _weatherConfig!.Cities;
                foreach (var city in cities!)
                {
                    forecasts.Add(new ForecastModel() { CityName = city.Name, Status = "Wait..." });
                }
            }
            return forecasts;
        }
        private async void FillData(ObservableCollection<ForecastModel> forecasts)
        {
            await Task.Run(async () =>
            {
                City[]? cities = _weatherConfig!.Cities;
                for (var i = 0; i < cities?.Length; i++)
                {
                    var city = cities[i];
                    var forecast = await ForecastService.GetForecast(city);
                    icons.TryGetValue(forecast.Icon, out string? iconPath);
                    string? theIconPath;
                    if (iconPath != null)
                    {
                        theIconPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), iconPath);
                    }
                    else
                    {
                        theIconPath = null;
                    }

                    Dispatcher.Invoke(new Action(() =>
                    {
                        forecasts[i] = new ForecastModel()
                        {
                            CityName = city.Name,
                            Status = "Done",
                            Summary = forecast.Summary,
                            MaxT = forecast.MaxT,
                            MinT = forecast.MinT,
                            IconImagePath = theIconPath!
                        };
                    }));
                }
            });
        }
    }
}
