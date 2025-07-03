﻿namespace WeatherCSLib.Data
{
    public record Currently
    {
        public long Time { get; set; }
        public string Summary { get; set; }
        public string Icon { get; set; }
        public double NearestStormDistance { get; set; }
        public double NearestStormBearing { get; set; }
        public double PrecipIntensity { get; set; }
        public double PrecipProbability { get; set; }
        public double PrecipIntensityError { get; set; }
        public string PrecipType { get; set; }
        public double Temperature { get; set; }
        public double ApparentTemperature { get; set; }
        public double DewPoint { get; set; }
        public double Humidity { get; set; }
        public double Pressure { get; set; }
        public double WindSpeed { get; set; }
        public double WindGust { get; set; }
        public int WindBearing { get; set; }
        public double CloudCover { get; set; }
        public double UvIndex { get; set; }
        public double Visibility { get; set; }
        public double Ozone { get; set; }
    }
}
