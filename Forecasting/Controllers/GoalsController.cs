using Forecasting.Goals.DTOs;
using Forecasting.Goals.Entity;
using Forecasting.Goals.Services;
using Forecasting.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Forecasting.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class GoalsController(GoalsService _goalService,
        CategoryService _categoryService) : ControllerBase
    {
        [HttpPost]
        [Route("addCategoryofGoals")]
        public async Task<IActionResult> AddCategoryofGoals([FromBody] List<CategoryDto> category)
        {
            try
            {
                if (category == null || !category.Any())
                {
                    return BadRequest("Category is empty.");
                }
                _categoryService.AddCategories(category);
                return Ok("Category List Saved Correctly!.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Goals([FromBody] List<GoalDto> goals)
        {
            try
            {
                if (goals == null || goals.Count == 0)
                {
                    return BadRequest("Goals is empty.");
                }
                await _goalService.AddGoals(goals);
                return Ok("Goals List Saved Correctly!.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
