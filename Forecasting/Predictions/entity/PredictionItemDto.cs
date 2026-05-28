public class PredictionItemDto
{
    public string ProductName { get; set; } = "";
    public string Sku { get; set; } = "";
    public int PredictedSales { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Category { get; set; } = "";
}