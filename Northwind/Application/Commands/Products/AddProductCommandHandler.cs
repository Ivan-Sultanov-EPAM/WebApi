using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Northwind.Data;
using Northwind.Extensions;

namespace Northwind.Application.Commands.Products
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, int>
    {
        private readonly NorthwindContext _dbContext;

        public AddProductCommandHandler(NorthwindContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var product = request.ToProduct();

            var result = await _dbContext.Products.AddAsync(product, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return result.Entity.ProductId;
        }
    }
}