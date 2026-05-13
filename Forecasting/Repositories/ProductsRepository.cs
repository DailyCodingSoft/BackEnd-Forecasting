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
        public async Task<List<Product>> GetProductsByCategoryIdAsync(int categoryId)
        {
            return await _context.Products.Where(p => p.CategoryId == categoryId).ToListAsync();
        }
        public async Task<List<int>> GetExistingIds(List<int> productIds)
        {
            return await _context.Products
                .Where(p => productIds.Contains(p.ProductId))
                .Select(p => p.ProductId)
                .ToListAsync();
        }
    }
}
