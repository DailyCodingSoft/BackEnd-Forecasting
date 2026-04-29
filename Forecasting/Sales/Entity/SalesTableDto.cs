namespace Forecasting.Sales.Entity
{
    public class SalesTableDto
    {
        public required SaleDto[] Rows { get; set; }
        public required string[] Columns {  get; set; }
    }
}
