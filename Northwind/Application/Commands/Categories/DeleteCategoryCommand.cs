using MediatR;
using Northwind.Application.Models.Requests;

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