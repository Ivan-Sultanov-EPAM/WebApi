﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Northwind.Application.Models.Responses;
using Northwind.Data;
using Northwind.Extensions;

namespace Northwind.Application.Queries.Categories
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, List<GetCategoriesResponseDto>>
    {
        private readonly NorthwindContext _dbContext;

        public GetCategoriesQueryHandler(NorthwindContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<GetCategoriesResponseDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Categories
                .Select(p => p.ToGetCategoriesResponseDto())
                .ToListAsync(cancellationToken);
        }
    }
}