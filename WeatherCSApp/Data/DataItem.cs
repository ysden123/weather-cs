namespace WeatherCSApp.Data
{
    public record DataItem
    {
        public required double TemperatureMin { get; set; }
        public required double TemperatureMax { get; set; }
    }
}
