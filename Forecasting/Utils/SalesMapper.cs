using Forecasting.Sales.Entity;

namespace Forecasting.Utils
{
    public class SalesMapper
    {
        public static SalesTableDto MapSaleListToSalesTableDto(List<Sale> sales)
        {
            SalesTableDto salesDto = new()
            {
                Columns = [.. typeof(SaleDto).GetProperties().Select(p => p.Name)],
                Rows = [.. sales.Select(MapSaleToSaleDto)]
            };

            return salesDto;
        }

        private static SaleDto MapSaleToSaleDto(Sale sale) => new()
        {
            ProductName = sale.Product.ProductName,
            Identificator = sale.Product.Identificator,
            Quantity = sale.Quantity,
            Week = sale.Week,
            Date = sale.Date
        };
    }
}
