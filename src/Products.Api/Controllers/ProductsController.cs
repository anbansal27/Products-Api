using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Products.Api.Application.Contracts;
using Products.Api.Application.Response;
using Products.Api.Application.Services;

namespace Products.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService) => _productService = productService;

        #region Product

        /// <summary>
        /// Create a new Product.
        /// </summary>
        /// <param name="product"></param>        
        /// <returns>Response with created Product Id</returns>
        /// <response code="201">Successful Creation of Product.</response>
        /// <response code="400">A validation error inidicating that something was wrong with the request.</response>        
        /// <response code="409">A conflict error inidicating that a product with same code already exist.</response>        
        /// <response code="500">An unexpected error</response>
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

        /// <summary>
        /// Get All Products or Get Products by Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>List of Products</returns>
        /// <response code="200">Successful Retrieval of Products</response>
        /// <response code="400">Error when no products are found for the provided parameters.</response>        
        /// <response code="500">An unexpected error</response>
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

        /// <summary>
        /// Get Product By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Product</returns>
        /// <response code="200">Successful Retrieval of Product</response>
        /// <response code="400">Error when no products are found for the provided Product Id.</response>        
        /// <response code="500">An unexpected error</response>
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

        /// <summary>
        /// Update Product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <response code="200">Successful Update</response>
        /// <response code="400">Error when no products are found for the passed Id.</response>
        /// <response code="409">When the product cannot be updated as a different Product with the same code already exists.</response>
        /// <response code="500">An unexpected error</response>
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

        /// <summary>
        /// Delete Product
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Successful Delete</response>
        /// <response code="400">Error when no products are found for the provided Product Id.</response>
        /// <response code="500">An unexpected error</response>
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

        /// <summary>
        /// Create a new Product Option.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="productOption"></param>
        /// <returns>Response with created Product Option Id</returns>
        /// <response code="201">Successful Creation of Product Option.</response>
        /// <response code="400">A validation error inidicating that something was wrong with the request.</response>        
        /// <response code="409">A conflict error inidicating that a Product Option with same name already exist.</response>        
        /// <response code="500">An unexpected error</response>
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
                return BadRequest("Product Id is not valid");
            }

            CreateProductOptionResponse response = await _productService.CreateProductOption(productOption);
            return CreatedAtAction("CreateOption", response);
        }

        /// <summary>
        /// Get Product Option By Id
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="optionId"></param>
        /// <returns>ProductOption</returns>
        /// <response code="200">Successful Retrieval of Product Option</response>
        /// <response code="400">Error when no product or product option is found for the provided Id.</response>        
        /// <response code="500">An unexpected error</response>
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

        /// <summary>
        /// Get All Product Options for a Product
        /// </summary>
        /// <param name="productId"></param>
        /// <returns>List of Product Options</returns>
        /// <response code="200">Successful Retrieval of Product Options</response>
        /// <response code="400">Error when no product or product option is found for the provided ProductId.</response>        
        /// <response code="500">An unexpected error</response>

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

        /// <summary>
        /// Update Product Option
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="optionId"></param>
        /// <param name="productOption"></param>
        /// <response code="200">Successful Update</response>
        /// <response code="400">Error when product Id is invalid or when no product/product option are found for the provided Id.</response>
        /// <response code="409">When the product option cannot be updated as a different Product Option with the same name already exists.</response>
        /// <response code="500">An unexpected error</response>

        [HttpPut("{productId}/options/{optionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProductOption([FromRoute] Guid productId, [FromRoute] Guid optionId, [FromBody] ProductOptionDto productOption)
        {
            if (productOption.ProductId != productId)
            {
                return BadRequest("Product Id is not valid");
            }

            productOption.Id = optionId;
            await _productService.UpdateProductOption(productOption);
            return Ok();
        }

        /// <summary>
        /// Delete Product Option
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="optionId"></param>
        /// <response code="200">Successful Delete</response>
        /// <response code="400">Error when no product/product option are found for the provided Id.</response>
        /// <response code="500">An unexpected error</response>
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
