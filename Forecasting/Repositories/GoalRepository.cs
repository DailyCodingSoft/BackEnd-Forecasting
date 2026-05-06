using Forecasting.Data;
using Forecasting.Goals.Entity;
using Microsoft.EntityFrameworkCore;

namespace Forecasting.Repositories
{
    public class GoalRepository(AppDbContext _context)
    {
        public async void AddRange(List<Goal> goals)
        {
            _context.Goals.AddRange(goals);
            _context.SaveChanges();
        }

        public async Task<List<Goal>> GetGoalsByStatusAsync(string status)
        {
            return await _context.Goals.Where(g => g.Status == status).Include(g => g.Category).ToListAsync();
        }
    }
}
