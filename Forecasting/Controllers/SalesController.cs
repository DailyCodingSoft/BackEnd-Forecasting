using Forecasting.Repositories;
using Forecasting.Sales.Entity;
using Forecasting.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Forecasting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController(SalesRepository _salesRepository) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<SalesTableDto>> GetSales()
        {
            List<Sale> dbSales = await _salesRepository.GetSalesAsync();
            SalesTableDto response = SalesMapper.MapSaleListToSalesTableDto(dbSales);
            return Ok(response);
        }
    }
}
