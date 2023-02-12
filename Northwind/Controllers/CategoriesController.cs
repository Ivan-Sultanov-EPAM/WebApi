using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northwind.Application.Commands.Categories;
using Northwind.Application.Models.Requests;
using Northwind.Application.Models.Responses;
using Northwind.Application.Queries.Categories;

namespace Northwind.Controllers
{
    [ApiController]
    [Route("api/categories")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None, Duration = 0)]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
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
        public async Task<ActionResult<CategoryResponseDto>> GetCategory([Required] int categoryId)
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
        public async Task<IActionResult> EditCategory([Required] int categoryId,
            [Required][FromBody] EditCategoryRequestDto categoryDto)
        {
            if (categoryDto.CategoryName.Length > 15)
            {
                return BadRequest("Category name should not be longer than 15 symbols.");
            }

            try
            {
                await _mediator
                    .Send(new EditCategoryCommand(categoryId, categoryDto));
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        [HttpPost(Name = "Categories_AddCategory")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> AddCategory([Required][FromBody] AddCategoryRequestDto categoryDto)
        {
            if (categoryDto.CategoryName.Length > 15)
            {
                return BadRequest("Category name should not be longer than 15 symbols.");
            }

            int categoryId;

            try
            {
                categoryId = await _mediator
                    .Send(new AddCategoryCommand(categoryDto));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(categoryId);
        }

        [HttpDelete("{categoryId}", Name = "Categories_DeleteCategory")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCategories([Required] int categoryId)
        {
            try
            {
                await _mediator
                    .Send(new DeleteCategoryCommand(categoryId));
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }

            return Ok();
        }
    }
}
