using Forecasting.Predictions.entity;

namespace Forecasting.Predictions
{
    public class PredictionClient(HttpClient _httpClient)
    {
        public async Task<ForecastResponse> GetPrediction(string productIdentifier)
        {
            var response = await _httpClient.GetFromJsonAsync<ForecastResponse>($"/forecast/{productIdentifier}");
            return response ?? throw new Exception("Error getting prediction");
        }
    }
}
