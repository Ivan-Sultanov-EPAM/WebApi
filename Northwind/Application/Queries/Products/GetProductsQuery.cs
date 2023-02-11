using System.Collections.Generic;
using MediatR;
using Northwind.Application.Models.Responses;

namespace Northwind.Application.Queries.Products
{
    public class GetProductsQuery : IRequest<List<GetProductsResponseDto>>
    {
    }
}