﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Northwind.Application.Models.Responses;
using Northwind.Application.Queries.Categories;
using Northwind.Data;
using Northwind.Entities;

namespace Northwind.Controllers
{
    [ApiController]
    [Route("api/categories")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None, Duration = 0)]
    public class CategoriesController : ControllerBase
    {
        private readonly NorthwindContext _dbContext;
        private readonly IMediator _mediator;

        public CategoriesController(NorthwindContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        [HttpGet(Name = "Categories_GetCategories")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<CategoryResponseDto>>> GetCategories()
        {
            return await _mediator.Send(new GetCategoriesQuery());
        }

        [HttpGet("{categoryId}", Name = "Categories_GetCategoryById")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryResponseDto>> GetCategory(int categoryId)
        {
            var category = await _mediator
                .Send(new GetCategoryByIdQuery(categoryId));

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        [HttpPut("{categoryId}", Name = "Categories_EditCategory")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditCategory(int categoryId, Category categories)
        {
            if (categoryId != categories.CategoryId)
            {
                return BadRequest();
            }

            _dbContext.Entry(categories).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriesExists(categoryId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost(Name = "Categories_AddCategory")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Category>> AddCategory(Category category)
        {
            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction("GetCategories", new { id = category.CategoryId }, category);
        }

        [HttpDelete("{categoryId}", Name = "Categories_DeleteCategory")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Category>> DeleteCategories(int categoryId)
        {
            var categories = await _dbContext.Categories.FindAsync(categoryId);
            if (categories == null)
            {
                return NotFound();
            }

            _dbContext.Categories.Remove(categories);
            await _dbContext.SaveChangesAsync();

            return categories;
        }

        private bool CategoriesExists(int categoryId)
        {
            return _dbContext.Categories.Any(e => e.CategoryId == categoryId);
        }
    }
}
