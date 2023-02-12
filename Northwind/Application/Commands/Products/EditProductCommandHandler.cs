using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Northwind.Data;
using Northwind.Extensions;

namespace Northwind.Application.Commands.Products
{
    public class EditProductCommandHandler : IRequestHandler<EditProductCommand, Unit>
    {
        private readonly NorthwindContext _dbContext;

        public EditProductCommandHandler(NorthwindContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(EditProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _dbContext.Products
                .FirstOrDefaultAsync(p => p.ProductId == request.ProductId, cancellationToken);

            if (product == null)
                throw new KeyNotFoundException($"Product with Id = {request.ProductId} does not exist");

            product.UpdateProduct(request);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}