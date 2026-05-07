using Forecasting.Goals.DTOs;
using Forecasting.Goals.Entity;
using Forecasting.Repositories;
using System.Net.NetworkInformation;

namespace Forecasting.Goals.Services
{
    public class GoalsService(CategoryRepository _categoryRepository, GoalRepository _goalRepository)
    {
        public async Task<List<GoalStatusDto>> GetGoalStatus()
        {
            var status = await _goalRepository.GetGoalStatus();
            return [.. status.Select(s => new GoalStatusDto
            {
                Code = s.Code,
                Name = s.Name
            })];
        }
        public async Task<List<Goal>> GetGoalsByStatus(string status)
        {
            return await _goalRepository.GetGoalsByStatusAsync(status);
        }
        public async Task<List<Goal>> AddGoals(List<GoalRequestDto> goalDtos)
        {
            var goalList = new List<Goal>();

            var categoryCodes = goalDtos
                    .Select(g => g.CategoryCode)
                    .Where(code => !string.IsNullOrEmpty(code))
                    .Distinct()
                    .ToList();

            var categories = await _categoryRepository.GetCategoriesByCodes(categoryCodes);
            var status = await _goalRepository.GetGoalStatus();

            foreach (var goalDto in goalDtos)
            {
                if (!categories.TryGetValue(goalDto.CategoryCode, out var category))
                    throw new Exception($"Invalid category code: {goalDto.CategoryCode}");
                var goalStatus = status.Find(s => s.Code == goalDto.StatusCode);
                if (goalStatus != null)
                {
                    var goal = new Goal
                    {
                        Name = goalDto.Name,
                        Progress = goalDto.Progress,
                        CategoryId = category.CategoryId, // FK
                        Category = category,
                        Bonus = goalDto.Bonus,
                        GoalStatusId = goalStatus.StatusId,
                        GoalStatus = goalStatus,
                    };

                    goalList.Add(goal);
                }else
                {
                    throw new Exception($"Invalid status code: {goalDto.StatusCode}");
                }
            }
            _goalRepository.AddRange(goalList);
            return goalList;
        }
    }
}
