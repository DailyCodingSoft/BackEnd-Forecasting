using Forecasting.Products.Entity;
using Forecasting.Sales.Entity;

namespace Forecasting.Utils
{
    public class SalesMapper
    {
        public static SalesTableDto MapSaleListToSalesTableDto(List<Sale> sales)
        {
            SalesTableDto salesDto = new()
            {
                Rows = [.. sales.Select(MapSaleToSalesTableRowDto)]
            };

            return salesDto;
        }

        private static SalesTableRowDto MapSaleToSalesTableRowDto(Sale sale) => new()
        {
            ProductName = sale.Product.ProductName,
            Identificator = sale.Product.Identificator,
            Quantity = sale.Quantity,
            Week = sale.Week,
            Date = sale.Date
        };

        public static Sale MapSaleDtoToSale(SaleDto saleDto, Product saleDtoProduct) => new()
        {
            Product = saleDtoProduct,
            ProductId = saleDtoProduct.ProductId,
            Quantity = saleDto.Quantity,
            Week = saleDto.Week,
            Date = saleDto.Date
        };
    }
}
