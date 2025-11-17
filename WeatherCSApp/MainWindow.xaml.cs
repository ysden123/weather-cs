using Serilog;
using System.IO;
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
            var folder = YSCommon.Utils.GetAssemblyFolderInLocalData("weather-cs");
#if DEBUG
            string fileName = Path.Combine(folder, "logs", "weather-cs-debug.log");
            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .Enrich.WithThreadId()
               .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {SourceContext} [{ThreadId}] {Message:lj}{NewLine}{Exception}")
               .WriteTo.File(fileName,
               rollingInterval: RollingInterval.Month,
               outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {SourceContext} [{ThreadId}] {Message:lj}{NewLine}{Exception}")
           .CreateLogger();
#else
            string fileName = Path.Combine(folder, "logs", "weather-cs.log");
            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Error()
               .Enrich.WithThreadId()
               .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {SourceContext} [{ThreadId}] {Message:lj}{NewLine}{Exception}")
               .WriteTo.File(fileName,
               rollingInterval: RollingInterval.Month,
               outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {SourceContext} [{ThreadId}] {Message:lj}{NewLine}{Exception}")
           .CreateLogger();
#endif

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