using Forecasting.Sales.Entity;

namespace Forecasting.Utils
{
    public class SalesMapper
    {
        public static SalesTableDto MapSaleListToSalesTableDto(List<Sale> sales)
        {
            SalesTableDto salesDto = new()
            {
                Columns = [.. typeof(SaleDto).GetProperties().Select(p => ChangeFirstLetterToLowerCase(p.Name))],
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

        public static Sale MapSaleDtoToSale(SaleDto saleDto, Product saleDtoProduct) => new()
        {
            Product = saleDtoProduct,
            ProductId = saleDtoProduct.ProductId,
            Quantity = saleDto.Quantity,
            Week = saleDto.Week,
            Date = saleDto.Date
        };

        private static string ChangeFirstLetterToLowerCase(string columnName)
        {
            string result = "";
            result += columnName[0].ToString().ToLower();
            result += columnName.Substring(1);
            return result;
        }
    }
}
