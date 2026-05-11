namespace Forecasting.Goals.DTOs
{
    public class UpdateGoalRequest
    {
        public string Name { get; set; } = string.Empty;

        public string? NewName { get; set; }

        public decimal? Progress { get; set; }

        public string? CategoryCode { get; set; }

        public int? Quantity { get; set; }

        public decimal? Bonus { get; set; }

        public string? StatusCode { get; set; }
    }
}