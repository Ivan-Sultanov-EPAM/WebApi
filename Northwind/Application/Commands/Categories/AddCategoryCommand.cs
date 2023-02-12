using MediatR;
using Northwind.Application.Models.Requests;

namespace Northwind.Application.Commands.Categories
{
    public class AddCategoryCommand : IRequest<int>
    {
        public string CategoryName { get; }
        public string Description { get; }

        public AddCategoryCommand(AddCategoryRequestDto request)
        {
            CategoryName = request.CategoryName;
            Description = request.Description;
        }
    }
}