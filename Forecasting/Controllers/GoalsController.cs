using Forecasting.Goals.DTOs;
using Forecasting.Goals.Entity;
using Forecasting.Goals.Services;
using Forecasting.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Forecasting.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class GoalsController(CategoryRepository _categoryRepository, GoalRepository _goalRepository) : ControllerBase
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
                CategoryService categoryService = new CategoryService();
                List<Category> categoryList = categoryService.Categories(category);
                _categoryRepository.AddRange(categoryList);
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
                if (goals == null || !goals.Any())
                {
                    return BadRequest("Goals is empty.");
                }
                GoalsService goalsService = new GoalsService(_categoryRepository);
                List<Goal> goalList = await goalsService.AddGoals(goals);
                _goalRepository.AddRange(goalList);
                return Ok("Goals List Saved Correctly!.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
