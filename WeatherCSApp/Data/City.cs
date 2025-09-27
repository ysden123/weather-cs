namespace WeatherCSApp.Data
{
    public record City
    {
        public required string Name { get; set; }
        public required double Latitude { get; set; }
        public required double Longitude { get; set; }
    }
}
