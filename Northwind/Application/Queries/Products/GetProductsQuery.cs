using System.Collections.Generic;
using MediatR;
using Northwind.Application.Models.Responses;

namespace Northwind.Application.Queries.Products
{
    public class GetProductsQuery : IRequest<List<ProductResponseDto>>
    {
        public int PageNumber { get; }
        public int PageSize { get; }
        public int? CategoryId { get; }

        public GetProductsQuery(int pageNumber, int pageSize, int? categoryId)
        {
            PageNumber = pageNumber < 0 ? 0 : pageNumber;
            PageSize = pageSize < 1 ? 1 : pageSize;
            CategoryId = categoryId;
        }
    }
}