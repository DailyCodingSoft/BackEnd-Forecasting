using Forecasting.Data;
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
        public async Task<List<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }
    }
}
