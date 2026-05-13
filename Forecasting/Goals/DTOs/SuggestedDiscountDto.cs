namespace Forecasting.Goals.DTOs
{
    public class SuggestedDiscountDto
    {
        public int SuggestedDiscountId { get; set; }
        public int ProductId { get; set; }
        public int GoalId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal MinimumPrice { get; set; }
        public decimal MaximumPrice { get; set; }
    }
}
