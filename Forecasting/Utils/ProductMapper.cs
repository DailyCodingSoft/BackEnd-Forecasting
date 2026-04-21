using Forecasting.Sales.Entity;

namespace Forecasting.Utils
{
    public class ProductMapper
    {
        public static List<ProductFilterDto> MapProductsToDto(List<Product> products)
        {
            return products.Select(p => new ProductFilterDto
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName
            }).ToList();
        }
    }
}
