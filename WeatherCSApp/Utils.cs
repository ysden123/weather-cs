using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace WeatherCSApp
{
    public class Utils
    {
        public static readonly JsonSerializerOptions SerializationOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic)
        };

        public static string? ApiKey()
        {
            return Environment.GetEnvironmentVariable("PIRATE_WEATHER_API_KEY");
        }
    }
}
