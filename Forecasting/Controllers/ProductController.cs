using Forecasting.Goals.DTOs;
using Forecasting.Goals.Entity;
using Forecasting.Products.Entity;
using Forecasting.Repositories;
using Forecasting.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Forecasting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(ProductsRepository _productsRepository, CategoryRepository _categoryRepository) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<ProductFilterDto>> GetProducts([FromQuery] string? category)
        {
            int? categoryId = null;
            if (!string.IsNullOrWhiteSpace(category))
            {
                Category? categoryDto = await _categoryRepository.GetCategoryByCode(category);

                if (categoryDto == null)
                {
                    return NotFound("Category not found");
                };

                categoryId = categoryDto.CategoryId;
            }
            List<Product> dbProduct = await _productsRepository.GetProductsAsync(categoryId);
            List<ProductFilterDto> response = ProductMapper.MapProductsToDto(dbProduct);
            return Ok(response);
        }
    }
}