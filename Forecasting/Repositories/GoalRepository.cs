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
            return await _context.Goals.Include(g => g.Category).Include(g => g.GoalStatus).Where(g => g.GoalStatus.Name == status.ToLower()).ToListAsync();
        }

        public async Task<List<GoalStatus>> GetGoalStatus()
        {
            return await _context.GoalStatus.ToListAsync();
        }
    }
}
