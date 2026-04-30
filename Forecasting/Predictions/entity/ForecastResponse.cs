namespace Forecasting.Predictions.entity
{
    public class ForecastResponse
    {
        public required string product_identifier {  get; set; }
        public required string week {  get; set; }
        public required int sales {  get; set; }
    }
}
