using Forecasting.Goals.DTOs;
using Forecasting.Goals.Entity;
using Forecasting.Products.Entity;
using Forecasting.Sales.Entity;

namespace Forecasting.Utils
{
    public class GoalsMapper
    {
        public static List<GoalResponseDto> MapGoalListToGoalResponseDtoList(List<Goal> goals)
        {
            return [.. goals.Select(MapGoalToGoalResponseDto)];
        }

        public static GoalResponseDto MapGoalToGoalResponseDto(Goal goal) => new()
            {
                Name = goal.Name,
                Category = goal.Category.Name,
                Progress = goal.Progress,
                Status = goal.Status,
                Bonus = goal.Bonus
            };
    }
}
