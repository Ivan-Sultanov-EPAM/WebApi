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
    }
}