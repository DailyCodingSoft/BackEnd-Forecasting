using Forecasting.Data;
using Forecasting.Goals.DTOs;
using Forecasting.Goals.Entity;
using Microsoft.EntityFrameworkCore;

namespace Forecasting.Repositories
{
    public class CategoryRepository(AppDbContext _context)
    {
        public async void AddRange(List<Category> categories)
        {
            _context.Categories.AddRange(categories);
            _context.SaveChanges();
        }
        public async Task<Dictionary<string, Category>> GetCategoriesByCodes(List<string> codes)
        {
            return await _context.Categories
                .Where(c => codes.Contains(c.Code))
                .ToDictionaryAsync(c => c.Code);
        }
    }
}
