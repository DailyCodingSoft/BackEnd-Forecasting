using Forecasting.Predictions.entity;
using Forecasting.Predictions.Services;

namespace Forecasting.Predictions
{
    public class PredictionClient(HttpClient _httpClient, PredictionService _predictionService)
    {
        public async Task<ForecastResponse> GetPrediction(string productIdentifier)
        {
            var response = await _httpClient.GetFromJsonAsync<ForecastResponse>($"/forecast/{productIdentifier}");
            if (response != null){
                await _predictionService.AddPredictionAsync(response);
                return response;
            }
            else
                throw new Exception("Error getting prediction");
        }
    }
}
