namespace WeatherCSApp.Data
{
    public record Currently
    {
        public string? Summary { get; set; }
        public string? Icon { get; set; }
        public double Temperature { get; set; }
    }
}
