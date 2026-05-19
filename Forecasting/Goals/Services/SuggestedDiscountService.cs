using Forecasting.Goals.DTOs;
using Forecasting.Goals.Entity;
using Forecasting.Repositories;

namespace Forecasting.Goals.Services
{
    public class SuggestedDiscountService(
        SuggestedDiscountRepository _suggestedDiscountRepository,
        ProductsRepository _productRepository,
        GoalRepository _goalRepository,
        IConfiguration _configuration)
    {

        public async Task GenerateByGoalName(string goalName)
        {
            decimal minimumProfit =
                _configuration.GetValue<decimal>("BusinessRules:MinimumProfit");

            Goal? goal = await _goalRepository.GetGoalByName(goalName);

            if (goal is null)
                throw new Exception("No se encontró la meta.");

            var products = await _productRepository.GetProductsByCategoryIdAsync(goal.CategoryId);

            if(products == null)
                throw new Exception("No se encontraron productos para la categoría.");

            var existingDiscounts = await _suggestedDiscountRepository.GetByGoal(goal.GoalId);
            List<SuggestedDiscount> discountsToUpdate = new();
            List<SuggestedDiscount> discountsToAdd = new();

            foreach (var product in products)
            {
                decimal valorTotalMeta = goal.Quantity * product.ProductPrice;
                decimal pesoBono = goal.Bonus / valorTotalMeta;
                decimal descuentoProducto = (decimal)((Math.Log((double)(1 + pesoBono))) / 10.0);
                decimal minimumPrice = product.ProductPrice * (1 - descuentoProducto);
                decimal maximumPrice = product.ProductPrice;

                if(minimumPrice < 0){
                    minimumPrice = 0;
                }

                var existingDiscount = existingDiscounts.FirstOrDefault(d => d.ProductId == product.ProductId);

                if (existingDiscount != null)
                {
                    existingDiscount.MinimumPrice = decimal.Round(minimumPrice, 2);
                    existingDiscount.MaximumPrice = decimal.Round(maximumPrice, 2);
                    discountsToUpdate.Add(existingDiscount);
                }
                else
                {
                    SuggestedDiscount newDiscount = new()
                    {
                        GoalId = goal.GoalId,
                        ProductId = product.ProductId,
                        MinimumPrice = decimal.Round(minimumPrice, 2),
                        MaximumPrice = decimal.Round(maximumPrice, 2)
                    };
                    discountsToAdd.Add(newDiscount);
                }
            }

            if (discountsToUpdate.Any())
                await _suggestedDiscountRepository.UpdateRange(discountsToUpdate);

            if (discountsToAdd.Any())
                await _suggestedDiscountRepository.AddRange(discountsToAdd);
        }

        public async Task<List<SuggestedDiscountDto>> GetByGoalName(string goalName)
        {
            var goal = await _goalRepository.GetGoalByName(goalName);
            if (goal == null) throw new Exception("Goal not found");
            return await GetByGoal(goal.GoalId);
        }

        public async Task<List<SuggestedDiscountDto>> GetByGoal(
            int goalId)
        {
            var discounts = await _suggestedDiscountRepository
                .GetByGoal(goalId);

            return [.. discounts.Select(x => new SuggestedDiscountDto
            {
                SuggestedDiscountId = x.SuggestedDiscountId,
                ProductId = x.ProductId,
                GoalId = x.GoalId,
                ProductName = x.Product?.ProductName ?? "Unknown",
                MinimumPrice = x.MinimumPrice,
                MaximumPrice = x.MaximumPrice
            })];
        }

        public async Task Delete(int suggestedDiscountId)
        {
            await _suggestedDiscountRepository
                .Delete(suggestedDiscountId);
        }
    }
}