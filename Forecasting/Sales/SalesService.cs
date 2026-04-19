using Forecasting.Repositories;
using Forecasting.Sales.Entity;
using Forecasting.Utils;

namespace Forecasting.Sales
{
    public class SalesService(ProductsRepository _productsRepository)
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
    }
}
