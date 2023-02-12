using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Northwind.Application.Commands.Products;
using Northwind.Application.Models.Requests;
using Northwind.Application.Models.Responses;
using Northwind.Application.Queries.Products;
using Northwind.Data;
using Northwind.Entities;

namespace Northwind.Controllers
{
    [ApiController]
    [Route("api/products")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None, Duration = 0)]
    public class ProductsController : ControllerBase
    {
        private readonly NorthwindContext _dbContext;
        private readonly IMediator _mediator;

        public ProductsController(NorthwindContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }
        
        [HttpGet(Name = "Products_GetProducts")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetProducts()
        {
            return await _mediator.Send(new GetProductsQuery());
        }
        
        [HttpGet("{productId}", Name = "Products_GetProductById")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductResponseDto>> GetProductById(int productId)
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
        public async Task<IActionResult> EditProduct(int productId, EditProductRequestDto productDto)
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
        public async Task<ActionResult<Product>> AddProduct(Product product)
        {
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction("GetProducts", new { id = product.ProductId }, product);
        }
        
        [HttpDelete("{productId}", Name = "Products_DeleteProduct")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Product>> DeleteProduct(int productId)
        {
            var products = await _dbContext.Products.FindAsync(productId);
            if (products == null)
            {
                return NotFound();
            }

            _dbContext.Products.Remove(products);
            await _dbContext.SaveChangesAsync();

            return products;
        }

        private bool ProductsExists(int productId)
        {
            return _dbContext.Products.Any(e => e.ProductId == productId);
        }
    }
}
