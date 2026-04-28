using Forecasting.Repositories;
using Forecasting.Sales.Entity;
using Forecasting.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Forecasting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(ProductsRepository _productsRepository) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<ProductFilterDto>> GetProducts()
        {
            List<Product> dbProduct = await _productsRepository.GetProductsAsync();
            List<ProductFilterDto> response = ProductMapper.MapProductsToDto(dbProduct);
            return Ok(response);

        }


    }
}