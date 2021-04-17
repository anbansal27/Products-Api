using Products.Api.Application.Exceptions;
using Products.Api.Application.Response;
using Products.Api.Data.Repository;
using System;
using System.Threading.Tasks;
using Products.Api.Contracts;
using Products.Api.Entities;

namespace Products.Api.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository) => _productRepository = productRepository;

        public async Task<CreateProductResponse> CreateProduct(ProductDto productDto)
        {
            Product existingProduct = await _productRepository.FirstOrDefaultAsync(x => x.Code == productDto.Code);

            if (existingProduct != null)
            {
                throw new DuplicateProductException($"Product with Code - {existingProduct.Code} already exists.");
            }

            var product = new Product
            {
                Id = Guid.NewGuid(),
                Code = productDto.Code,
                DeliveryPrice = productDto.DeliveryPrice,
                Description = productDto.Description,
                Name = productDto.Name,
                Price = productDto.Price
            };

            await _productRepository.AddAsync(product);

            return new CreateProductResponse
            {
                ProductId = product.Id
            };
        }
    }
}
