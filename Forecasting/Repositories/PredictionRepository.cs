using Forecasting.Data;
using Forecasting.Predictions.entity;
using Microsoft.EntityFrameworkCore;

namespace Forecasting.Repositories
{
    public class PredictionRepository(AppDbContext _context)
    {
        public async Task AddPredictionAsync(Prediction prediction)
        {
            _context.Predictions.Add(prediction);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Prediction>> GetAllPredictions()
        {
            return await _context.Predictions
                .Include(p => p.Product)
                    .ThenInclude(p => p.Category)
                .OrderBy(p => p.PredictedWeek)
                .ToListAsync();
        }
    }
}
