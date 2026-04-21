using Forecasting.Data;
using Forecasting.Sales.Entity;
using Microsoft.EntityFrameworkCore;

namespace Forecasting.Repositories
{
    public class SalesRepository(AppDbContext _context)
    {
        public async Task<List<Sale>> GetSalesAsync(int? product = null)
        {
            var query = _context.Sales.Include(s => s.Product).AsQueryable();
            if (product.HasValue) { 
                query = query.Where(s => s.ProductId == product.Value);
            }
            return await query.ToListAsync();
        }

        public async void AddRange(List<Sale> sales)
        {
            _context.Sales.AddRange(sales);
            _context.SaveChanges();
        }
    }
}
