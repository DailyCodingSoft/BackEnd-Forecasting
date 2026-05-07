namespace Forecasting.Goals.DTOs
{
    public class GoalRequestDto
    {
        public required string Name { get; set; }

        public decimal Progress { get; set; }

        public required string CategoryCode { get; set; }

        public decimal Bonus { get; set; }

        public required string StatusCode { get; set; }
    }
}
