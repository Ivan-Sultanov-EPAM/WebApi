using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Northwind.Data;
using Northwind.Extensions;

namespace Northwind.Application.Commands.Categories
{
    public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, int>
    {
        private readonly NorthwindContext _dbContext;

        public AddCategoryCommandHandler(NorthwindContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = request.ToCategory();

            var result = await _dbContext.Categories.AddAsync(category, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return result.Entity.CategoryId;
        }
    }
}