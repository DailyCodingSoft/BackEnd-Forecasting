using Forecasting.Sales.Entity;
using Microsoft.EntityFrameworkCore;

namespace Forecasting.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }
    }
}
