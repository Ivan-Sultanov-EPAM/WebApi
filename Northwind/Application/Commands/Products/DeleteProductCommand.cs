using MediatR;
using Northwind.Application.Models.Responses;

namespace Northwind.Application.Commands.Products
{
    public class DeleteProductCommand : IRequest
    {
        public int ProductId { get; }

        public DeleteProductCommand(int productId)
        {
            ProductId = productId;
        }
    }
}