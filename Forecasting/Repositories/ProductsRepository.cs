using Forecasting.Data;
using Forecasting.Goals.Entity;
using Forecasting.Products.Entity;
using Microsoft.EntityFrameworkCore;

namespace Forecasting.Repositories
{
    public class ProductsRepository(AppDbContext _context)
    {
        public Product? GetProductByIdentificator(string identificator)
        {
            return _context.Products.FirstOrDefault(p => p.Identificator.Equals(identificator));
        }
        public async Task<Product?> GetProductByIdentificatorAsync(string identificator)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Identificator.Equals(identificator));
        }
        public async Task<List<Product>> GetProductsAsync(int? categoryId = null)
        {
            IQueryable<Product> query = _context.Products;

            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }

            return await query.ToListAsync();
        }
    }
}
