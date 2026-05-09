using Forecasting.Goals.DTOs;
using Forecasting.Goals.Entity;
using Forecasting.Goals.Services;
using Forecasting.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Forecasting.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class GoalsController(GoalsService _goalService,
        CategoryService _categoryService) : ControllerBase
    {
        [HttpGet("categories")]
        public async Task<IActionResult> GetGoalCategories()
        {
            List<CategoryDto> categories = await _categoryService.GetCategories();
            return Ok(categories);
        }

        [HttpGet("status")]
        public async Task<IActionResult> GetGoalStatus()
        {
            List<GoalStatusDto> status = await _goalService.GetGoalStatus();
            return Ok(status);
        }

        [HttpGet("{status}")]
        public async Task<IActionResult> GetGoalsByStatus(string status) {
            try
            {
                List<Goal> goals = await _goalService.GetGoalsByStatus(status);
                List<GoalResponseDto> goalsResponse = GoalsMapper.MapGoalListToGoalResponseDtoList(goals);
                return Ok(goalsResponse);
            }
            catch (Exception ex) { 
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("byName/{name}")]
        public async Task<IActionResult> GetGoalByName(string name)
        {
            try
            {
                Goal? goal = await _goalService.GetGoalByName(name);
                if (goal == null)
                {
                    return NotFound($"Goal with name '{name}' not found.");
                }

                GoalResponseDto goalResponse = GoalsMapper.MapGoalToGoalResponseDto(goal);
                return Ok(goalResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

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
        public async Task<IActionResult> Goals([FromBody] List<GoalRequestDto> goals)
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

        [HttpPut("update")]
        public async Task<IActionResult> UpdateGoal([FromBody] UpdateGoalRequest request)
        {
            if (request == null)
                throw new Exception("Request is empty");
            try
            {
                var message = await _goalService.UpdateGoal(request);
                return Ok(message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
