using Forecasting.Data;
using Forecasting.Goals.Entity;
using Microsoft.EntityFrameworkCore;

namespace Forecasting.Repositories
{
    public class GoalStatusRepository(AppDbContext _context)
    {
        public async Task<List<GoalStatus>> GetGoalStatusAsync()
        {
            return await _context.GoalStatus.ToListAsync();
        }

        public async Task<GoalStatus?> GetGoalStatusByCodeAsync(string code)
        {
            return await _context.GoalStatus.FirstOrDefaultAsync(s => s.Code == code);
        }
    }
}