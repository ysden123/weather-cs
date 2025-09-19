using System.Text.Json;

namespace WeatherCSLib.Data
{
    public record WeatherConfig(City[]? Cities)
    {
        private readonly static string _configFile = @$"{Environment.GetEnvironmentVariable("APPDATA")}\weather-cs\WeatherConfig.json";

        public static WeatherConfig? GetAppWeatherConfig()
        {
            WeatherConfig? weatherConfig;
            try
            {
                weatherConfig = WeatherConfig.FromFile(_configFile);
            }
            catch (Exception)
            {
                weatherConfig = null;
            }
            return weatherConfig;
        }

        public bool ToAppFile()
        {
            return ToFile(_configFile);
        }

        public override string ToString()
        {
            var result = "{ Cities = [";
            if (Cities != null)
            {
                foreach (var city in Cities)
                {
                    result += city.ToString();
                    result += ", ";
                }
                if (Cities.Length > 0)
                    result = result[0..^2];
            }
            result += "]}";
            return result;
        }

        public static WeatherConfig? FromJson(string json)
        {
            return JsonSerializer.Deserialize<WeatherConfig>(json, Utils.SerializationOptions);
        }

        public string ToJson()
        {
            return JsonSerializer.Serialize<WeatherConfig>(this, Utils.SerializationOptions);
        }

        public static WeatherConfig? FromFile(string file) {
            WeatherConfig? weatherConfig;
            try
            {
                using (var reader = new StreamReader(file))
                {
                    var json = reader.ReadToEnd();
                    weatherConfig = FromJson(json);
                }
                return weatherConfig;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public bool ToFile(string file)
        {
            try
            {
                using var writer = new StreamWriter(file);
                writer.Write(ToJson());
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
