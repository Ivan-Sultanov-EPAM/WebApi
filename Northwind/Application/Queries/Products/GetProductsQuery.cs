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
            PageNumber = pageNumber;
            PageSize = pageSize == 0 ? 10 : pageSize;
            CategoryId = categoryId;
        }
    }
}