using System.Windows;
using WeatherCSApp.Control;

namespace WeatherCSApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            if (fvi != null && fvi.FileVersion != null)
            {
                string version = fvi.FileVersion;
                Title = $"{Title} {version}";
            }
        }

        private void ConfigurationMenuItemClick(object sender, RoutedEventArgs e)
        {
            CC.Content = new ConfigControl();
        }

        private void ForecastMenuItemClick(object sender, RoutedEventArgs e)
        {
            CC.Content = new ForecastControl();
        }
    }
}