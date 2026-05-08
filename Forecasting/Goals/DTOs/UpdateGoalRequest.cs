namespace Forecasting.Goals.DTOs
{
    public class UpdateGoalRequest
    {
        public string Name { get; set; } = string.Empty;

        public string? NewName { get; set; }

        public decimal? Progress { get; set; }

        public int? CategoryId { get; set; }

        public decimal? Bonus { get; set; }

        public int? GoalStatusId { get; set; }
    }
}