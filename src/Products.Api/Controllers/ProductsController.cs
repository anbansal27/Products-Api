using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Products.Api.Application.Response;
using Products.Api.Application.Services;
using Products.Api.Contracts;

namespace Products.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService) => _productService = productService;
        
        [HttpPost]
        [Produces(typeof(CreateProductResponse))]
        [ProducesResponseType(typeof(CreateProductResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] ProductDto productDto)
        {
            CreateProductResponse response = await _productService.CreateProduct(productDto);
            return CreatedAtAction("Create", response);
        }

        [HttpGet]
        [Produces(typeof(List<ProductDto>))]
        [ProducesResponseType(typeof(List<ProductDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProducts([FromQuery] string name = null)
        {
            var products = await _productService.GetProducts(name);
            return Ok(products);
        }

        [HttpGet("{id}")]
        [Produces(typeof(ProductDto))]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProductById([FromRoute] Guid id)
        {
            var products = await _productService.GetProductById(id);
            return Ok(products);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, [FromBody] ProductDto product)
        {
            product.Id = id;
            await _productService.UpdateProduct(product);
            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid id)
        {
            await _productService.DeleteProduct(id);
            return Ok();
        }
    }
}
