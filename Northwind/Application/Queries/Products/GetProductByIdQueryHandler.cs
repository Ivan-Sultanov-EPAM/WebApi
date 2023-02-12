using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Northwind.Application.Models.Responses;
using Northwind.Data;
using Northwind.Extensions;

namespace Northwind.Application.Queries.Products
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductResponseDto>
    {
        private readonly NorthwindContext _dbContext;

        public GetProductByIdQueryHandler(NorthwindContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ProductResponseDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _dbContext.Products
                .FirstOrDefaultAsync(p => p.ProductId == request.ProductId, cancellationToken);
            
            return product?.ToProductResponseDto();
        }
    }
}