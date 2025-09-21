namespace WeatherCSLib.Data
{
    public record Forecast
    {
        public required string CityName {  get; set; }
        public  string? Status {  get; set; }
        public string? Icon {  get; set; }
        public string? Summary {  get; set; }
        public double T {  get; set; }
        public double MaxT {  get; set; }
        public double MinT {  get; set; }
    }
}
