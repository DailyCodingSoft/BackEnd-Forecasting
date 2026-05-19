using Forecasting.Data;
using Forecasting.Goals.Entity;
using Microsoft.EntityFrameworkCore;

namespace Forecasting.Repositories
{
    public class SuggestedDiscountRepository(
        AppDbContext _context)
    {
        public async Task AddRange(
            List<SuggestedDiscount> discounts)
        {
            await _context.SuggestedDiscounts
                .AddRangeAsync(discounts);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateRange(
            List<SuggestedDiscount> discounts)
        {
            _context.SuggestedDiscounts
                .UpdateRange(discounts);
 
            await _context.SaveChangesAsync();
        }

        public async Task<List<SuggestedDiscount>> GetByGoal(int goalId)
        {
            return await _context.SuggestedDiscounts
                .Include(sd => sd.Product)
                .Where(sd => sd.GoalId == goalId)
                .ToListAsync();
        }

        public async Task Delete(int suggestedDiscountId)
        {
            var entity = await _context.SuggestedDiscounts.FindAsync(suggestedDiscountId);
            if (entity != null)
            {
                _context.SuggestedDiscounts.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteByGoalIdAsync(int goalId)
        {
            var entities = await _context.SuggestedDiscounts
                .Where(sd => sd.GoalId == goalId)
                .ToListAsync();
            
            if (entities.Any())
            {
                _context.SuggestedDiscounts.RemoveRange(entities);
                await _context.SaveChangesAsync();
            }
        }
    }
}