namespace Forecasting.Sales.Entity
{
    public class SalesGroupedRequestDto
    {
        public string? Identificator { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public int? Week { get; set; }
        public int? Year { get; set; }
    }
}
