using System.Collections.ObjectModel;
using System.Windows;
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
                try
                {
                    var forecasts = await ForecastService.GetForecastAll(_weatherConfig!.Cities);

                    forecasts.Sort((f1, f2) => string.Compare(f1.CityName, f2.CityName, StringComparison.Ordinal));

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
                }
                catch (Exception)
                {
                    MessageBox.Show("Error fetching forecasts. See logs.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });

            forecastListView.Cursor = currentCursor;
        }
    }
}
