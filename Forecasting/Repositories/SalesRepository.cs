using Forecasting.Data;
using Forecasting.Products.Entity;
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

        public async void AddRange(List<Sale> sales)
        {
            _context.Sales.AddRange(sales);
            _context.SaveChanges();
        }

        public async Task AddRangeAsync(List<Sale> sales)
        {
            await _context.Sales.AddRangeAsync(sales);
            await _context.SaveChangesAsync();
        }

        //public async Task<List<Product>> GetProductsWithSalesAsync(
        //    string? identificator,
        //    DateTime? from,
        //    DateTime? to)
        //{
        //    DateTime? fromUtc = from.HasValue ? DateTime.SpecifyKind(from.Value, DateTimeKind.Utc) : null;
        //    DateTime? toUtc = to.HasValue ? DateTime.SpecifyKind(to.Value.Date.AddDays(1).AddTicks(-1), DateTimeKind.Utc) : null;
        //    var query = _context.Products
        //        .Include(p => p.Sales)
        //        .AsQueryable();

        //    if (!string.IsNullOrEmpty(identificator))
        //        query = query.Where(p => p.Identificator == identificator);

        //    if (fromUtc.HasValue)
        //        query = query.Where(p => p.Sales.Any(s => s.Date >= fromUtc));

        //    if (toUtc.HasValue)
        //        query = query.Where(p => p.Sales.Any(s => s.Date <= toUtc));
            
        //    query = query.Include(p => p.Sales.Where(s =>
        //        (fromUtc == null || s.Date >= fromUtc) &&
        //        (toUtc == null || s.Date <= toUtc)
        //    ));
        //    var result = await query.ToListAsync();
        //    return result;
        //}

        public async Task<List<Sale>> GetSalesByProductInDateRange(string? identificator, DateTime? from, DateTime? to, int? week, int? year){
            var query = _context.Sales.Include(s => s.Product).AsQueryable();
            if (week.HasValue)
            {
                int targetYear = year ?? DateTime.UtcNow.Year;
                query = query.Where(s => s.Week == week.Value && s.Date.Year == targetYear);
            }
            else if (from.HasValue && to.HasValue)
            {
                DateTime fromUtc = DateTime.SpecifyKind(from.Value, DateTimeKind.Utc);
                DateTime toUtc = DateTime.SpecifyKind(to.Value.Date.AddDays(1).AddTicks(-1), DateTimeKind.Utc);
                query = query.Where(s => s.Date >= fromUtc && s.Date <= toUtc);
            }
            else
            {
                return [];
            }

            if (!string.IsNullOrEmpty(identificator))
                query = query.Where(s => s.Product.Identificator == identificator);

            return await query.ToListAsync();
        }
    }
}
