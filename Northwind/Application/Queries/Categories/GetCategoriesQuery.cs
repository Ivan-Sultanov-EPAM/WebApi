using System.Collections.Generic;
using MediatR;
using Northwind.Application.Models.Responses;

namespace Northwind.Application.Queries.Categories
{
    public class GetCategoriesQuery : IRequest<List<CategoryResponseDto>>
    {
    }
}