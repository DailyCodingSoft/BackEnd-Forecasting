using Forecasting.Products.Entity;
using Forecasting.Repositories;
using Forecasting.Sales.Entity;
using Forecasting.Utils;


namespace Forecasting.Sales
{
    public class SalesService(SalesRepository _salesRepository, ProductsRepository _productsRepository)
    {
        public List<Sale> SaveSaleList(SalesDto sales)
        {
            List<Sale> list = [];
            foreach (SaleDto saleDto in sales.Rows)
            {
                Sale saleToSave = SalesMapper.MapSaleDtoToSale(saleDto, GetSaleProduct(saleDto.Identificator));
                list.Add(saleToSave);
            }
            return list;
        }

        private Product GetSaleProduct(string identificator)
        {
            Product? product = _productsRepository.GetProductByIdentificator(identificator);
            if (product is not null)
                return product;
            else throw new Exception($"Product with identificator {identificator} was not found.");
        }

        public async Task<ProductSalesResponseDto> GetSalesGroupedAsync(
            string? identificator,
            DateTime? from,
            DateTime? to)
        {
            var products = await _salesRepository
            .GetProductsWithSalesAsync(identificator, from, to);
            var productDtos = products.Select(p => new ProductSalesDto
            {
                ProductId     = p.ProductId,
                Identificator = p.Identificator,
                ProductName   = p.ProductName,
                Sales = p.Sales
                    .OrderBy(s => s.Week)
                    .Select(s => new SalePointDto
                    {
                        Week     = s.Week,
                        Date     = s.Date,
                        Quantity = s.Quantity
                    })
                    .ToList()
            }).ToList();

            return new ProductSalesResponseDto { Products = productDtos };
        }

        public async Task SaveSaleListAsync(SalesDto sales)
        {
            var list = new List<Sale>();

            foreach (var saleDto in sales.Rows)
            {
                var product = await _productsRepository
                    .GetProductByIdentificatorAsync(saleDto.Identificator)
                    ?? throw new KeyNotFoundException(
                        $"Producto con identificador {saleDto.Identificator} no encontrado.");

                list.Add(SalesMapper.MapSaleDtoToSale(saleDto, product));
            }

            await _salesRepository.AddRangeAsync(list);
        }
    }
}
