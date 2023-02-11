using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public CategoriesController(NorthwindContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet(Name = "Categories_GetCategories")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Categories>>> GetCategories()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        [HttpGet("{categoryId}", Name = "Categories_GetCategoryById")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Categories>> GetCategory(int categoryId)
        {
            var categories = await _dbContext.Categories.FindAsync(categoryId);

            if (categories == null)
            {
                return NotFound();
            }

            return categories;
        }

        [HttpPut("{categoryId}", Name = "Categories_EditCategory")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditCategory(int categoryId, Categories categories)
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
        public async Task<ActionResult<Categories>> AddCategory(Categories category)
        {
            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction("GetCategories", new { id = category.CategoryId }, category);
        }

        [HttpDelete("{categoryId}", Name = "Categories_DeleteCategory")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Categories>> DeleteCategories(int categoryId)
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
