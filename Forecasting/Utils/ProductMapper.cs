using Forecasting.Products.Entity;

namespace Forecasting.Utils
{
    public class ProductMapper
    {
        public static List<ProductFilterDto> MapProductsToDto(List<Product> products)
        {
            return [.. products.Select(p => new ProductFilterDto
            {
                Identificator = p.Identificator,
                ProductName = p.ProductName
            })];
        }
    }
}
