using System.Text.Json;

namespace WeatherCSApp.Data
{
    public record Response
    {
        public required Currently Currently { get; set; }
        public required Daily Daily{ get; set; }

        public static Response? FromJson(string json)
        {
            return JsonSerializer.Deserialize<Response>(json, Utils.SerializationOptions);
        }
    }
}
