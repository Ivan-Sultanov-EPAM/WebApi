using MediatR;
using Northwind.Application.Models.Responses;

namespace Northwind.Application.Queries.Products
{
    public class GetProductByIdQuery : IRequest<ProductResponseDto>
    {
        public int ProductId { get; }

        public GetProductByIdQuery(int productId)
        {
            ProductId = productId;
        }
    }
}