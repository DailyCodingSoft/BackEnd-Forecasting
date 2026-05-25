using Forecasting.Predictions.entity;

namespace Forecasting.Predictions
{
    public class PredictionClient(HttpClient _httpClient)
    {
        public async Task<ForecastResponse?> GetPrediction(
            string productIdentifier
        )
        {
            try
            {
                var response = await _httpClient
                    .GetFromJsonAsync<ForecastResponse>(
                        $"/forecast/{productIdentifier}"
                    );

                return response;
            }
            catch (HttpRequestException ex)
            {
                // Capturar errores 400 del ML Engine
                if (ex.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    return null;
                }
                return response ?? throw new Exception("Error getting prediction");

                throw;
            }
        }
    }
}