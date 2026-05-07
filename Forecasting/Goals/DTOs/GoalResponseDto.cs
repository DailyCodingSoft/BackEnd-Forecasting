namespace Forecasting.Goals.DTOs
{
    public class GoalResponseDto
    {
        public required string Name { get; set; }

        public decimal Progress { get; set; }

        public required string Category { get; set; }

        public decimal Bonus { get; set; }

        public required string Status { get; set; }
    }
}
