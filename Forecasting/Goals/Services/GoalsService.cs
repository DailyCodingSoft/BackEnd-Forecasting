using Forecasting.Goals.DTOs;
using Forecasting.Goals.Entity;
using Forecasting.Repositories;
using System.Net.NetworkInformation;

namespace Forecasting.Goals.Services
{
    public class GoalsService(CategoryRepository _categoryRepository, GoalRepository _goalRepository)
    {
        public async Task<List<Goal>> GetGoalsByStatus(string status)
        {
            return await _goalRepository.GetGoalsByStatusAsync(status);
        }
        public async Task<List<Goal>> AddGoals(List<GoalDto> goalDtos)
        {
            var goalList = new List<Goal>();

            var categoryCodes = goalDtos
                    .Select(g => g.CategoryCode)
                    .Where(code => !string.IsNullOrEmpty(code))
                    .Distinct()
                    .ToList();

            var categories = await _categoryRepository.GetCategoriesByCodes(categoryCodes);

            foreach (var goalDto in goalDtos)
            {
                if (!categories.TryGetValue(goalDto.CategoryCode, out var category))
                    throw new Exception($"Invalid category code: {goalDto.CategoryCode}");

                var goal = new Goal
                {
                    Name = goalDto.Name,
                    Progress = goalDto.Progress,
                    CategoryId = category.CategoryId, // FK
                    Category = category,
                    Bonus = goalDto.Bonus,
                    Status = goalDto.Status ?? "Active"
                };

                goalList.Add(goal);
            }
            _goalRepository.AddRange(goalList);
            return goalList;
        }
    }
}
