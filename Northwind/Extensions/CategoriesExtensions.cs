using Northwind.Application.Commands.Categories;
using Northwind.Application.Models.Responses;
using Northwind.Entities;

namespace Northwind.Extensions
{
    public static class CategoriesExtensions
    {
        public static CategoryResponseDto ToCategoryResponseDto(this Category category)
        {
            return new CategoryResponseDto
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Description = category.Description
            };
        }

        public static void UpdateCategory(this Category category, EditCategoryCommand request)
        {
            category.CategoryName = request.CategoryName;
            category.Description = request.Description;
        }

        public static Category ToCategory(this AddCategoryCommand category)
        {
            return new Category
            {
                CategoryName = category.CategoryName,
                Description = category.Description
            };
        }
    }
}