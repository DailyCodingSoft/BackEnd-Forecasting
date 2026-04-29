using Forecasting.Predictions;
using Forecasting.Predictions.entity;
using Microsoft.AspNetCore.Mvc;

namespace Forecasting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PredictionsController(PredictionClient _predictionClient) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<ForecastResponse>> GetForecast()
        {
            ForecastResponse response = await _predictionClient.GetPrediction("001");
            return Ok(response);
        }
    }
}
