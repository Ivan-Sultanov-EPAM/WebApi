using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<ActionResult<IEnumerable<GetProductsResponseDto>>> GetProducts()
        {
            return await _mediator.Send(new GetProductsQuery());
        }
        
        [HttpGet("{productId}", Name = "Products_GetProductById")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> GetProductById(int productId)
        {
            var products = await _dbContext.Products.FindAsync(productId);

            if (products == null)
            {
                return NotFound();
            }

            return products;
        }
        
        [HttpPut("{productId}", Name = "Products_EditProduct")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditProduct(int productId, Product product)
        {
            if (productId != product.ProductId)
            {
                return BadRequest();
            }

            _dbContext.Entry(product).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductsExists(productId))
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
