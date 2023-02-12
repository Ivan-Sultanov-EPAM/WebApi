using MediatR;

namespace Northwind.Application.Commands.Categories
{
    public class DeleteCategoryCommand : IRequest
    {
        public int CategoryId { get; }

        public DeleteCategoryCommand(int categoryId)
        {
            CategoryId = categoryId;
        }
    }
}