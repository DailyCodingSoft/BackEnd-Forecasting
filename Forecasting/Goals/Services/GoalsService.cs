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
                        Quantity = goalDto.Quantity,
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
        
        public async Task<string> UpdateGoal(UpdateGoalRequest request)
        {
            var goal = await _goalRepository.GetGoalByName(request.Name);

            if (goal == null)
                throw new Exception("Meta no encontrada");

            bool changed = false;

            if (!string.IsNullOrWhiteSpace(request.NewName) &&
                request.NewName != goal.Name)
            {
                goal.Name = request.NewName;
                changed = true;
            }

            if (request.Progress.HasValue &&
                request.Progress.Value != goal.Progress)
            {
                goal.Progress = request.Progress.Value;
                changed = true;
            }

            if (request.CategoryId.HasValue &&
                request.CategoryId.Value != goal.CategoryId)
            {
                goal.CategoryId = request.CategoryId.Value;
                changed = true;
            }

            if (request.Bonus.HasValue &&
                request.Bonus.Value != goal.Bonus)
            {
                goal.Bonus = request.Bonus.Value;
                changed = true;
            }

            if (request.GoalStatusId.HasValue &&
                request.GoalStatusId.Value != goal.GoalStatusId)
            {
                goal.GoalStatusId = request.GoalStatusId.Value;
                changed = true;
            }

            if (!changed)
                throw new Exception("No hubo cambios");

            await _goalRepository.SaveChangesAsync();

            return "Meta actualizada correctamente";
        }

    }
}
