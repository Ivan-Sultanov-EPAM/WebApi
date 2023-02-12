using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northwind.Application.Commands.Products;
using Northwind.Application.Models.Requests;
using Northwind.Application.Models.Responses;
using Northwind.Application.Queries.Products;

namespace Northwind.Controllers
{
    [ApiController]
    [Route("api/products")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None, Duration = 0)]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet(Name = "Products_GetProducts")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetProducts(
            int pageNumber = 0, int pageSize = 10, int? categoryId = null)
        {
            return await _mediator
                .Send(new GetProductsQuery(pageNumber, pageSize, categoryId));
        }
        
        [HttpGet("{productId}", Name = "Products_GetProductById")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductResponseDto>> GetProductById([Required] int productId)
        {
            var product = await _mediator
                .Send(new GetProductByIdQuery(productId));

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }
        
        [HttpPut("{productId}", Name = "Products_EditProduct")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditProduct([Required] int productId,
            [Required][FromBody] EditProductRequestDto productDto)
        {
            try
            {
                await _mediator
                    .Send(new EditProductCommand(productId, productDto));
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
        
        [HttpPost(Name = "Products_AddProduct")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> AddProduct([Required][FromBody] AddProductRequestDto productDto)
        {
            int productId;

            try
            {
                productId = await _mediator
                    .Send(new AddProductCommand(productDto));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(productId);
        }
        
        [HttpDelete("{productId}", Name = "Products_DeleteProduct")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteProduct([Required] int productId)
        {
            try
            {
                await _mediator
                    .Send(new DeleteProductCommand(productId));
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }

            return Ok();
        }
    }
}
