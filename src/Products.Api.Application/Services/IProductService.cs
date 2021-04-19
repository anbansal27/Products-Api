using Products.Api.Application.Contracts;
using Products.Api.Application.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Products.Api.Application.Services
{
    public interface IProductService
    {
        Task<CreateProductResponse> CreateProduct(ProductDto productDto);

        Task<CreateProductOptionResponse> CreateProductOption(ProductOptionDto productOption);

        Task DeleteProduct(Guid id);

        Task DeleteProductOption(Guid productId, Guid optionId);

        Task<ProductDto> GetProductById(Guid id);

        Task<ProductOptionDto> GetProductOptionById(Guid productId, Guid optionId);

        Task<List<ProductOptionDto>> GetProductOptions(Guid productId);

        Task<List<ProductDto>> GetProducts(string name);

        Task UpdateProduct(ProductDto product);

        Task UpdateProductOption(ProductOptionDto productOption);
    }
}
