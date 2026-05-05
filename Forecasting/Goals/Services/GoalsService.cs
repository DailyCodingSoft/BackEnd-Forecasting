using Forecasting.Goals.DTOs;
using Forecasting.Goals.Entity;
using Forecasting.Repositories;

namespace Forecasting.Goals.Services
{
    public class GoalsService(CategoryRepository _categoryRepository)
    {
        public async Task<List<Goal>> Goals(List<GoalDto> goalDtos)
        {
            var goalList = new List<Goal>();

            var categoryCodes = goalDtos
                    .Select(g => g.CategoryCodeGoal)
                    .Where(code => !string.IsNullOrEmpty(code))
                    .Distinct()
                    .ToList();

            var categories = await _categoryRepository.GetCategoriesByCodes(categoryCodes);

            foreach (var goalDto in goalDtos)
            {
                if (!categories.TryGetValue(goalDto.CategoryCodeGoal, out var category))
                    throw new Exception($"Invalid category code: {goalDto.CategoryCodeGoal}");

                var goal = new Goal
                {
                    Name = goalDto.NameGoal,
                    Progress = goalDto.ProgressGoal,
                    CategoryId = category.CategoryId,
                    Bonus = goalDto.BonusGoal,
                    Status = goalDto.StatusGoal ?? "Active"
                };

                goalList.Add(goal);
            }
            return goalList;
        }
    }
}
