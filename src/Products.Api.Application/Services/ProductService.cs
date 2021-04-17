using Products.Api.Application.Exceptions;
using Products.Api.Application.Response;
using Products.Api.Data.Repository;
using System;
using System.Threading.Tasks;
using Products.Api.Contracts;
using Products.Api.Entities;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace Products.Api.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<CreateProductResponse> CreateProduct(ProductDto productDto)
        {
            Product existingProduct = await _productRepository.FirstOrDefaultAsync(x => x.Code == productDto.Code);

            if (existingProduct != null)
            {
                throw new DuplicateProductException($"Product with Code - {existingProduct.Code} already exists.");
            }

            var product = new Product
            {
                Id = productDto.Id,
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

        public async Task DeleteProduct(Guid id)
        {
            var product = await _productRepository.FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
            {
                throw new ProductNotFoundException($"Product with Id - {id} not found");
            }

            await _productRepository.DeleteAsync(product);
        }

        public async Task<ProductDto> GetProductById(Guid id)
        {
            var product = await _productRepository.FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
            {
                throw new ProductNotFoundException($"Product with Id - {id} not found");
            }

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<List<ProductDto>> GetProducts(string name)
        {
            var products = string.IsNullOrWhiteSpace(name)
                ? await _productRepository.GetProductsAsync(x => true)
                : await _productRepository.GetProductsAsync(x => x.Name == name);

            if (!products.Any())
            {
                throw new ProductNotFoundException("No products found");
            }

            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task UpdateProduct(ProductDto product)
        {
            var existingProduct = await _productRepository.FirstOrDefaultAsync(x => x.Id == product.Id);

            if (existingProduct == null)
            {
                throw new ProductNotFoundException($"Product with Id - {product.Id} not found");
            }

            var existingProductByCode = await _productRepository.FirstOrDefaultAsync(x => x.Code == product.Code);

            if (existingProductByCode != null && existingProduct.Id != existingProductByCode.Id)
            {
                throw new DuplicateProductException($"This Product cannot be updated as a different Product with same Code - {product.Code} already exists.");
            }

            existingProduct.DeliveryPrice = product.DeliveryPrice;
            existingProduct.Description = product.Description;
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;

            await _productRepository.SaveChangesAsync();
        }
    }
}
