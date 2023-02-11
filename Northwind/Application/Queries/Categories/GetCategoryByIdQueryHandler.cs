using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Northwind.Application.Models.Responses;
using Northwind.Data;
using Northwind.Extensions;

namespace Northwind.Application.Queries.Categories
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryResponseDto>
    {
        private readonly NorthwindContext _dbContext;

        public GetCategoryByIdQueryHandler(NorthwindContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CategoryResponseDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _dbContext.Categories
                .FirstOrDefaultAsync(c => c.CategoryId == request.CategoryId, cancellationToken);
            
            return category?.ToCategoryResponseDto();
        }
    }
}