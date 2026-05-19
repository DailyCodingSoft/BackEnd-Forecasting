namespace Forecasting.Goals.DTOs
{
    public class SuggestedDiscountDto
    {
        public int SuggestedDiscountId { get; set; }
        public int ProductId { get; set; }
        public int GoalId { get; set; }
        public required string ProductName { get; set; }
        public decimal MinimumPrice { get; set; }
        public decimal MaximumPrice { get; set; }
    }
}
