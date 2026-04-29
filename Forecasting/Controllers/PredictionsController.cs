using Forecasting.Predictions;
using Forecasting.Predictions.entity;
using Microsoft.AspNetCore.Mvc;

namespace Forecasting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PredictionsController(PredictionClient _predictionClient) : ControllerBase
    {
        [HttpGet("{product_identifier}")]
        public async Task<ActionResult<ForecastResponse>> GetForecast(string product_identifier)
        {
            ForecastResponse response = await _predictionClient.GetPrediction(product_identifier);
            return Ok(response);
        }
    }
}
