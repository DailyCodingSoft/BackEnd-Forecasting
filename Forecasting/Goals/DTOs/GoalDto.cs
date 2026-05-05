namespace Forecasting.Goals.DTOs
{
    public class GoalDto
    {
        public string Name { get; set; } = string.Empty;

        public decimal Progress { get; set; }

        public string CategoryCode { get; set; } = string.Empty;

        public decimal Bonus { get; set; }

        public string? Status { get; set; }
    }
}
