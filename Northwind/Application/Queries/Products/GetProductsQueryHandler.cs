﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Northwind.Application.Models.Responses;
using Northwind.Data;
using Northwind.Extensions;

namespace Northwind.Application.Queries.Products
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<GetProductsResponseDto>>
    {
        private readonly NorthwindContext _dbContext;

        public GetProductsQueryHandler(NorthwindContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<GetProductsResponseDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Products
                .Select(p => p.toGetProductsResponseDto())
                .ToListAsync(cancellationToken);
        }
    }
}