using Forecasting.Goals.Entity;
using Forecasting.Products.Entity;
using Forecasting.Repositories;
using Forecasting.Sales.Entity;
using Forecasting.Utils;


namespace Forecasting.Sales
{
    public class SalesService(
        SalesRepository _salesRepository,
        ProductsRepository _productsRepository,
        CategoryRepository _categoryRepository)
    {
        public async Task SaveSaleList(SalesDto sales)
        {
            List<string> identificators = sales.Rows
                .Select(s => s.Identificator)
                .Distinct()
                .ToList();
            var products = await _productsRepository.GetProductsByIdentificatorsAsync(identificators);
            var productsByIdentificator = products.ToDictionary(p => p.Identificator);
            var salesToInsert = new List<Sale>();
            foreach (var saleDto in sales.Rows)
            {
                if (!productsByIdentificator.TryGetValue(saleDto.Identificator, out var product))
                {
                    throw new KeyNotFoundException(
                        $"Producto con identificador {saleDto.Identificator} no encontrado.");
                }

                salesToInsert.Add(SalesMapper.MapSaleDtoToSale(saleDto, product));
            }
            await _salesRepository.AddRangeAsync(salesToInsert);
        }

        private Product GetSaleProduct(string identificator)
        {
            Product? product = _productsRepository.GetProductByIdentificator(identificator);
            if (product is not null)
                return product;
            else throw new Exception($"Product with identificator {identificator} was not found.");
        }

        //public async Task<ProductSalesResponseDto> GetSalesGroupedAsync(
        //    string? identificator,
        //    DateTime? from,
        //    DateTime? to)
        //{
        //    var products = await _salesRepository
        //    .GetProductsWithSalesAsync(identificator, from, to);
        //    var productDtos = products.Select(p => new ProductSalesDto
        //    {
        //        ProductId     = p.ProductId,
        //        Identificator = p.Identificator,
        //        ProductName   = p.ProductName,
        //        Sales = p.Sales
        //            .OrderBy(s => s.Week)
        //            .Select(s => new SalePointDto
        //            {
        //                Week     = s.Week,
        //                Date     = s.Date,
        //                Quantity = s.Quantity
        //            })
        //            .ToList()
        //    }).ToList();

        //    return new ProductSalesResponseDto { Products = productDtos };
        //}

        public async Task<SalesTableDto> GetSalesByProductInDateRange(string? identificator, DateTime? from, DateTime? to)
        {
            List<Sale> dbSales = await _salesRepository.GetSalesByProductInDateRange(identificator, from, to);
            return SalesMapper.MapSaleListToSalesTableDto(dbSales);
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

        public async Task SaveSaleListWithProductsAsync(SalesDto sales)
        {
            var invalidRows = sales.Rows
                .Where(r => string.IsNullOrWhiteSpace(r.CategoryCode) || !r.Price.HasValue)
                .Select(r => r.Identificator)
                .ToList();

            if (invalidRows.Count > 0)
            {
                throw new ArgumentException(
                    $"Missing CategoryCode or Price for sale rows with identificators: {string.Join(", ", invalidRows)}");
            }

            var requestedCodes = sales.Rows
                .Select(r => r.CategoryCode!)
                .Distinct()
                .ToList();

            Dictionary<string, Category> categories = await _categoryRepository.GetCategoriesByCodes(requestedCodes);

            var unknownCodes = requestedCodes
                .Where(code => !categories.ContainsKey(code))
                .ToList();

            if (unknownCodes.Count > 0)
            {
                throw new KeyNotFoundException(
                    $"Unknown category codes: {string.Join(", ", unknownCodes)}");
            }

            var salesToInsert = new List<Sale>();

            foreach (var row in sales.Rows)
            {
                Product? product = await _productsRepository.GetProductByIdentificatorAsync(row.Identificator);

                if (product is null)
                {
                    Category category = categories[row.CategoryCode!];
                    product = new Product
                    {
                        Identificator = row.Identificator,
                        ProductName = row.ProductName,
                        ProductPrice = row.Price!.Value,
                        CategoryId = category.CategoryId,
                        Category = category,
                        Sales = []
                    };
                    await _productsRepository.AddAsync(product);
                }

                salesToInsert.Add(SalesMapper.MapSaleDtoToSale(row, product));
            }

            await _salesRepository.AddRangeAsync(salesToInsert);
        }
    }
}
