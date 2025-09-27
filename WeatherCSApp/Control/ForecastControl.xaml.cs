using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using WeatherCSApp.Data;
using WeatherCSApp.Service;

namespace WeatherCSApp.Control
{
    /// <summary>
    /// Interaction logic for ForecastControl.xaml
    /// </summary>
    public partial class ForecastControl : UserControl
    {
        private readonly WeatherConfig? _weatherConfig;

        public ForecastControl()
        {
            InitializeComponent();
            _weatherConfig = WeatherConfig.GetAppWeatherConfig();
            FillData();
        }

        private async void FillData()
        {
            var currentCursor = forecastListView.Cursor;
            forecastListView.Cursor = Cursors.Wait;
            await Task.Run(async () =>
            {
                var forecasts = await ForecastService.GetForecastAll(_weatherConfig!.Cities);
                ObservableCollection<ForecastModel> forecastModels = [];
                foreach (var forecast in forecasts)
                {
                    var iconPath = ForecastService.BuildIconPath(forecast.Icon!);
                    forecastModels.Add(new ForecastModel()
                    {
                        CityName = forecast.CityName,
                        Status = "Done",
                        Summary = forecast.Summary,
                        T = forecast.T,
                        MaxT = forecast.MaxT,
                        MinT = forecast.MinT,
                        IconImagePath = iconPath!
                    });
                }
                Dispatcher.Invoke(new Action(() =>
                {
                    forecastListView.ItemsSource = forecastModels;
                }));
            });

            forecastListView.Cursor = currentCursor;
        }
    }
}
