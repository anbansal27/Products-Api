using System;
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
        
        [Produces(typeof(CreateProductResponse))]
        [ProducesResponseType(typeof(CreateProductResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] ProductDto productDto)
        {
            try
            {
                CreateProductResponse response = await _productService.CreateProduct(productDto);
                return CreatedAtAction("Create", response);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
