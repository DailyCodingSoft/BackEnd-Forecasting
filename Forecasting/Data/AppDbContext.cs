using Forecasting.Goals.Entity;
using Forecasting.Products.Entity;
using Forecasting.Sales.Entity;
using Microsoft.EntityFrameworkCore;

namespace Forecasting.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<GoalStatus> GoalStatus { get; set; }
        public DbSet<SuggestedDiscount> SuggestedDiscounts { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }
    }
}
