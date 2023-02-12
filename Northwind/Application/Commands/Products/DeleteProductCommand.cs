using MediatR;

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