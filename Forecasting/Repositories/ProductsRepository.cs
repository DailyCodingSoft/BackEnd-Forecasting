using Forecasting.Data;
using Forecasting.Sales.Entity;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Forecasting.Repositories
{
    public class ProductsRepository(AppDbContext _context)
    {
        public Product? GetProductByIdentificator(string identificator)
        {
            return _context.Products.FirstOrDefault(p => p.Identificator.Equals(identificator));
        }
    }
}
