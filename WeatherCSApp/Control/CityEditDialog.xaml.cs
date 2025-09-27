using System.Text.RegularExpressions;
using System.Windows;
using WeatherCSApp.Data;

namespace WeatherCSApp.Control
{
    public static partial class RegexUtilities
    {
        [GeneratedRegex(@"^[+-]?\d*\.?\d*$")]
        public static partial Regex ValidateNumberRegex();
    }

    /// <summary>
    /// Interaction logic for CityEditDialog.xaml
    /// </summary>
    public partial class CityEditDialog : Window
    {
        City _city;
        public CityEditDialog(City city)
        {
            InitializeComponent();
            _city = city;
            DataContext = city;
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            _city.Name = NameText.Text;
            _city.Latitude = Convert.ToDouble(LatitudeText.Text);
            _city.Longitude = Convert.ToDouble(LongitudeText.Text);
            DialogResult = true;
            Close();
        }

        private void NumberOnly(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !RegexUtilities.ValidateNumberRegex().IsMatch(e.Text);
        }
    }
}
