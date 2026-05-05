using Forecasting.Goals.DTOs;
using Forecasting.Goals.Entity;

namespace Forecasting.Goals.Services
{
    public class CategoryService
    {
        public List<Category> Categories (List<CategoryDto> categories)
        {
            List<Category> categoryList = new List<Category>();
            foreach (var categoryDto in categories)
            {
                Category category = new Category
                {
                    Code = categoryDto.CategoryCode,
                    Name = categoryDto.CategoryName
                };
                categoryList.Add(category);
            }
            return categoryList;
        }
    }
}
