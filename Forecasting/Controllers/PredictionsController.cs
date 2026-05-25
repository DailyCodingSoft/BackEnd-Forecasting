using Forecasting.Predictions;
using Forecasting.Predictions.entity;
using Forecasting.Predictions.Services;
using Microsoft.AspNetCore.Mvc;

namespace Forecasting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PredictionsController(
        PredictionClient _predictionClient,
        IServiceScopeFactory _scopeFactory,
        ILogger<PredictionsController> _logger) : ControllerBase
    {
        [HttpGet("{product_identifier}")]
        public async Task<ActionResult<ForecastResponse>> GetForecast(string product_identifier)
        {
            
            ForecastResponse? response = await _predictionClient.GetPrediction(product_identifier);

            if (response == null)
                return NoContent();

            Response.OnCompleted(async () =>
            {
                using var scope = _scopeFactory.CreateScope();
                var predictionService = scope.ServiceProvider.GetRequiredService<PredictionService>();
                try
                {
                    await predictionService.AddPredictionAsync(response);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to persist prediction for product {ProductIdentifier} after response was sent", product_identifier);
                }
            });

            return Ok(response);
        }
    }
}
