using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WeatherCSLib.Data;

namespace WeatherCSApp.Control
{
    /// <summary>
    /// Interaction logic for ConfigControl.xaml
    /// </summary>
    public partial class ConfigControl : UserControl
    {
        private readonly static string _configFile = @$"{Environment.GetEnvironmentVariable("APPDATA")}\weather-cs\WeatherConfig.json";
        readonly ObservableCollection<City> Cities = [];
        private ICollectionView _cityListView;

        private bool _changed = false;
        public ConfigControl()
        {
            InitializeComponent();
            WeatherConfig? weatherConfig;
            try
            {
                weatherConfig = WeatherConfig.FromFile(_configFile);
            }
            catch (Exception)
            {
                weatherConfig = null;
            }

            if (weatherConfig != null && weatherConfig.Cities != null)
            {
                Cities = new ObservableCollection<City>(weatherConfig.Cities);
            }

            ButtonAdd.IsEnabled = true;
            ButtonEdit.IsEnabled = false;
            ButtonDelete.IsEnabled = false;
            ButtonSave.IsEnabled = false;

            ListView_Cites.ItemsSource = Cities;

            _cityListView = CollectionViewSource.GetDefaultView(Cities);
        }

        private void ListView_Cites_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChangeButtonAvalability();
        }

        private void ButtonAddClick(object sender, RoutedEventArgs e)
        {
            var newCity = new City { Name = "Enter city name", Latitude = 0.0, Longitude = 0.0 };
            var dialog = new CityEditDialog(newCity)
            {
                Owner = Window.GetWindow(this)
            };
            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                Cities.Add(newCity);
                _cityListView.Refresh();
                _changed = true;
            }
        }

        private void ButtonEditClick(object sender, RoutedEventArgs e)
        {
            if (ListView_Cites.SelectedItem != null)
            {
                City? city = ListView_Cites.SelectedItem as City;
                if (city != null)
                {
                    var dialog = new CityEditDialog(city)
                    {
                        Owner = Window.GetWindow(this)
                    };
                    bool? result = dialog.ShowDialog();
                    if (result == true)
                    {
                        _cityListView.Refresh();
                        _changed = true;
                        ChangeButtonAvalability();
                    }
                }
            }
        }

        private void ButtonDeleteClick(object sender, RoutedEventArgs e)
        {
            if (ListView_Cites.SelectedItem != null)
            {
                Cities.RemoveAt(ListView_Cites.SelectedIndex);
                _cityListView.Refresh();
                _changed = true;
                ChangeButtonAvalability();
            }
        }

        private void ButtonSaveClick(object sender, RoutedEventArgs e)
        {
            var weatherConfig = new WeatherConfig([.. Cities]);
            if (weatherConfig.ToFile(_configFile))
                _changed = false;
            ChangeButtonAvalability();
        }

        private void ChangeButtonAvalability()
        {
            ButtonAdd.IsEnabled = true;
            ButtonSave.IsEnabled = _changed;
            if (ListView_Cites.SelectedItem == null)
            {
                ButtonEdit.IsEnabled = false;
                ButtonDelete.IsEnabled = false;
            }
            else
            {
                ButtonEdit.IsEnabled = true;
                ButtonDelete.IsEnabled = true;
            }
        }
    }
}
