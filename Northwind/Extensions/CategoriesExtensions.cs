using Northwind.Application.Models.Responses;
using Northwind.Entities;

namespace Northwind.Extensions
{
    public static class CategoriesExtensions
    {
        public static GetCategoriesResponseDto ToGetCategoriesResponseDto(this Category category)
        {
            return new GetCategoriesResponseDto
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Description = category.Description
            };
        }
    }
}