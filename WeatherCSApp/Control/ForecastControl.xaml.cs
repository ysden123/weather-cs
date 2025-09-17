using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Controls;

namespace WeatherCSApp.Control
{
    /// <summary>
    /// Interaction logic for ForecastControl.xaml
    /// </summary>
    public partial class ForecastControl : UserControl
    {
        private readonly Dictionary<string, string> icons = new()
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
        public ForecastControl()
        {
            InitializeComponent();
            ObservableCollection<ForecastModel> forecasts = [new ForecastModel() { CityName = "Test", Status = "Wait..." }];
            forecastListView.ItemsSource = forecasts;

            FillData(forecasts);
        }

        private async void FillData(ObservableCollection<ForecastModel> forecasts)
        {
            await Task.Run(() =>
            {
                Thread.Sleep(3000);
                //var iconName = "clearERROR";
                var iconName = "clear-day";
                string? iconPath;
                icons.TryGetValue(iconName, out iconPath);
                if (iconPath == null) { iconPath = ""; }
                Dispatcher.Invoke(new Action(() =>
                {
                    forecasts[0] = new ForecastModel()
                    {
                        CityName = forecasts[0].CityName,
                        Status = "Done",
                        Summary = "Summary text",
                        //IconImagePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Resources\\clear.png")
                        IconImagePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), iconPath)
                    };
                }));

            });
        }
    }
}
