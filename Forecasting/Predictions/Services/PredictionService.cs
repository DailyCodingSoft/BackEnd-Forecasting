using Forecasting.Predictions.entity;
using Forecasting.Repositories;

namespace Forecasting.Predictions.Services
{
    public class PredictionService(PredictionRepository _predictionRepository, ProductsRepository _productsRepository)
    {
        public async Task AddPredictionAsync(ForecastResponse forecastResponse)
        {   
            var product = await _productsRepository.GetProductByIdentificatorAsync(forecastResponse.product_identifier);
            if (product == null) {
                throw new Exception("Product not found");
            }
            var prediction = new Prediction
            {
                ProductId = product.ProductId,
                PredictedWeek = DateTime.SpecifyKind(DateTime.Parse(forecastResponse.week), DateTimeKind.Utc),
                PredictedSales = forecastResponse.sales,
                Product = product
            };

            await _predictionRepository.AddPredictionAsync(prediction);
        }
    }
}
