using Forecasting.Predictions.entity;
using System.Globalization;

namespace Forecasting.Utils
{
    public static class PredictionsMapper
    {
        public static List<PredictionWeekGroupDto> MapToWeekGroups(List<Prediction> predictions)
        {
            return predictions
                .GroupBy(p => new {
                    Week = ISOWeek.GetWeekOfYear(p.PredictedWeek),
                    Year = ISOWeek.GetYear(p.PredictedWeek)
                })
                .OrderBy(g => g.Key.Year)
                .ThenBy(g => g.Key.Week)
                .Select(g => new PredictionWeekGroupDto
                {
                    Week = g.Key.Week,
                    Year = g.Key.Year,
                    Predictions = g.Select(p => new PredictionItemDto
                    {
                        ProductName = p.Product.ProductName,
                        Sku = p.Product.Identificator,
                        PredictedSales = p.PredictedSales,
                        CreatedAt = p.CreatedAt,
                        Category = p.Product.Category.Name
                    }).ToList()
                })
                .ToList();
        }
    }
}