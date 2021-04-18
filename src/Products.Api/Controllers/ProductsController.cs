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

        #region Product

        [HttpPost]
        [Produces(typeof(CreateProductResponse))]
        [ProducesResponseType(typeof(CreateProductResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] ProductDto product)
        {
            CreateProductResponse response = await _productService.CreateProduct(product);
            return CreatedAtAction("Create", response);
        }

        [HttpGet]
        [Produces(typeof(List<ProductDto>))]
        [ProducesResponseType(typeof(List<ProductDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProductById([FromRoute] Guid id)
        {
            var product = await _productService.GetProductById(id);
            return Ok(product);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid id)
        {
            await _productService.DeleteProduct(id);
            return Ok();
        }

        #endregion

        #region Product Option
        
        [HttpPost("{productId}/options")]
        [Produces(typeof(CreateProductOptionResponse))]
        [ProducesResponseType(typeof(CreateProductOptionResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateOption([FromRoute] Guid productId, [FromBody] ProductOptionDto productOption)
        {
            if (productOption.ProductId != productId)
            {
                return BadRequest("Product Id mis-match between request url and body");
            }

            CreateProductOptionResponse response = await _productService.CreateProductOption(productOption);
            return CreatedAtAction("CreateOption", response);
        }

        [HttpGet("{productId}/options/{optionId}")]
        [Produces(typeof(ProductOptionDto))]
        [ProducesResponseType(typeof(ProductOptionDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProductOptionById([FromRoute] Guid productId, [FromRoute] Guid optionId)
        {
            var products = await _productService.GetProductOptionById(productId, optionId);
            return Ok(products);
        }

        [HttpGet("{productId}/options")]
        [Produces(typeof(List<ProductOptionDto>))]
        [ProducesResponseType(typeof(List<ProductOptionDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProductOptions([FromRoute] Guid productId)
        {
            var products = await _productService.GetProductOptions(productId);
            return Ok(products);
        }

        [HttpPut("{productId}/options/{optionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProductOption([FromRoute] Guid productId, [FromRoute] Guid optionId, [FromBody] ProductOptionDto productOption)
        {
            if (productOption.ProductId != productId)
            {
                return BadRequest("Product Id mis-match between request url and body");
            }

            productOption.Id = optionId;
            await _productService.UpdateProductOption(productOption);
            return Ok();
        }

        [HttpDelete("{productId}/options/{optionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteProductOption([FromRoute] Guid productId, [FromRoute] Guid optionId)
        {
            await _productService.DeleteProductOption(productId, optionId);
            return Ok();
        }

        #endregion
    }
}
