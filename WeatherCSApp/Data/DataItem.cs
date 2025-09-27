namespace WeatherCSApp.Data
{
    public record DataItem
    {
        public required long Time { get; set; }
        public required string Icon { get; set; }
        public required string Summary { get; set; }
        public required long SunriseTime { get; set; }
        public required long SunsetTime { get; set; }
        public required double MoonPhase { get; set; }
        public required double PrecipIntensity { get; set; }
        public required double PrecipIntensityMax { get; set; }
        public required double PrecipIntensityMaxTime { get; set; }
        public required double PrecipProbability { get; set; }
        public required double PrecipAccumulation { get; set; }
        public required string PrecipType { get; set; }
        public required double TemperatureHigh { get; set; }
        public required long TemperatureHighTime { get; set; }
        public required double TemperatureLow { get; set; }
        public required long TemperatureLowTime { get; set; }
        public required double ApparentTemperatureHigh { get; set; }
        public required long ApparentTemperatureHighTime { get; set; }
        public required double ApparentTemperatureLow { get; set; }
        public required long ApparentTemperatureLowTime { get; set; }
        public required double DewPoint { get; set; }
        public required double Humidity { get; set; }
        public required double Pressure { get; set; }
        public required double WindSpeed { get; set; }
        public required double WindGust { get; set; }
        public required long WindGustTime { get; set; }
        public required int WindBearing { get; set; }
        public required double CloudCover { get; set; }
        public required double UvIndex { get; set; }
        public required long UvIndexTime { get; set; }
        public required double Visibility { get; set; }
        public required double TemperatureMin { get; set; }
        public required long TemperatureMinTime { get; set; }
        public required double TemperatureMax { get; set; }
        public required long TemperatureMaxTime { get; set; }
        public required double ApparentTemperatureMin { get; set; }
        public required long ApparentTemperatureMinTime { get; set; }
        public required double ApparentTemperatureMax { get; set; }
        public required long ApparentTemperatureMaxTime { get; set; }
    }
}
