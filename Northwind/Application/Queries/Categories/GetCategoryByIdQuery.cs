using MediatR;
using Northwind.Application.Models.Responses;

namespace Northwind.Application.Queries.Categories
{
    public class GetCategoryByIdQuery : IRequest<CategoryResponseDto>
    {
        public int CategoryId { get; }

        public GetCategoryByIdQuery(int category)
        {
            CategoryId = category;
        }
    }
}