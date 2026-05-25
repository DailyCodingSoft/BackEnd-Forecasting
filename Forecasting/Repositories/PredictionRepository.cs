using Forecasting.Data;
using Forecasting.Predictions.entity;

namespace Forecasting.Repositories
{
    public class PredictionRepository(AppDbContext _context)
    {
        public async Task AddPredictionAsync(Prediction prediction)
        {
            _context.Predictions.Add(prediction);
            await _context.SaveChangesAsync();
        }
    }
}
