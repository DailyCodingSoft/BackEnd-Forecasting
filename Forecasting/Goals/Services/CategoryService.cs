using Forecasting.Goals.DTOs;
using Forecasting.Goals.Entity;
using Forecasting.Repositories;

namespace Forecasting.Goals.Services
{
    public class CategoryService(CategoryRepository _categoryRepository)
    {
        public void AddCategories(List<CategoryDto> categories)
        {
            List<Category> categoryList = new List<Category>();
            foreach (var categoryDto in categories)
            {
                Category category = new()
                {
                    Code = categoryDto.Code,
                    Name = categoryDto.Name
                };
                categoryList.Add(category);
            }
            _categoryRepository.AddRange(categoryList);
        }

        public async Task<List<CategoryDto>> GetCategories()
        {
            var categories = await _categoryRepository.GetCategoriesAsync();
            return [.. categories.Select(c => new CategoryDto {
                Code = c.Code,
                Name = c.Name
            })];
        }
    }
}
