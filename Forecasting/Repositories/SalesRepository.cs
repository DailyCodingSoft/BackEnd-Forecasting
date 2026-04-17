using Forecasting.Data;
using Forecasting.Sales.Entity;
using Microsoft.EntityFrameworkCore;

namespace Forecasting.Repositories
{
    public class SalesRepository(AppDbContext _context)
    {
        public async Task<List<Sale>> GetSalesAsync()
        {
            return await _context.Sales.Include(s => s.Product).ToListAsync();
        }
    }
}
