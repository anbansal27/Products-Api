using Products.Api.Application.Response;
using System.Threading.Tasks;
using Products.Api.Contracts;
using System.Collections.Generic;
using System;

namespace Products.Api.Application.Services
{
    public interface IProductService
    {
        Task<CreateProductResponse> CreateProduct(ProductDto productDto);

        Task<List<ProductDto>> GetProducts(string name);
        
        Task<ProductDto> GetProductById(Guid id);
        
        Task UpdateProduct(ProductDto product);

        Task DeleteProduct(Guid id);
    }
}
