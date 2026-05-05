using Forecasting.Repositories;
using Forecasting.Sales;
using Forecasting.Sales.Entity;
using Forecasting.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Forecasting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController(SalesRepository _salesRepository, SalesService _salesService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<SalesTableDto>> GetSales()
        {
            List<Sale> dbSales = await _salesRepository.GetSalesAsync();
            SalesTableDto response = SalesMapper.MapSaleListToSalesTableDto(dbSales);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> PostSales(SalesDto sales)
        {
            try
            {
                _salesService.SaveSaleList(sales);
                return Ok("Sale List Saved Correctly!.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPost]
        [Route("grouped")]
        public async Task<ActionResult<SalesTableDto>> GetSalesGrouped([FromBody] SalesGroupedRequestDto request)
        {
            var result = await _salesService.GetSalesByProductInDateRange(request.Identificator, request.From, request.To);
            return Ok(result);
        }
    }
}
