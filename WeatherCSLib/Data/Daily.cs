namespace WeatherCSLib.Data
{
    public record Daily
    {
        public required string Summary { get; set; }
        public required string Icon { get; set; }
        public required List<DataItem> data { get; set; }

        public double FindMaxTemperature()
        {
            var max = Double.MinValue;
            foreach (var dataItem in data)
            {
                if (dataItem != null && dataItem.TemperatureMax > max)
                {
                    max = dataItem.TemperatureMax;
                }
            }
            return max;
        }

        public double FindMinTemperature()
        {
            var min = Double.MaxValue;
            foreach (var dataItem in data)
            {
                if (dataItem != null && dataItem.TemperatureMin < min)
                {
                    min = dataItem.TemperatureMin;
                }
            }
            return min;
        }
    }

}
