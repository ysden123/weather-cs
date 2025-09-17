namespace WeatherCSApp
{
    public record ForecastModel
    {
        public required string CityName { get; set; }
        public string Status { get; set; }
        public string IconImagePath { get; set; }
        public string Summary { get; set; }
        public double MaxT { get; set; }
        public double MinT { get; set; }
    }
}
