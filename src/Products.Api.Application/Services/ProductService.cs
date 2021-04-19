using AutoMapper;
using Products.Api.Application.Contracts;
using Products.Api.Application.Exceptions;
using Products.Api.Application.Response;
using Products.Api.Data.Repository;
using Products.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            var product = _mapper.Map<Product>(productDto);
            product.Id = Guid.NewGuid();

            await _productRepository.AddAsync(product);

            return new CreateProductResponse
            {
                ProductId = product.Id
            };
        }

        public async Task<CreateProductOptionResponse> CreateProductOption(ProductOptionDto productOption)
        {
            var product = await GetProduct(productOption.ProductId);

            if (product.ProductOptions.Any(x => x.Name == productOption.Name))
            {
                throw new DuplicateProductOptionException($"ProductOption cannot be added as an option with the same name already exists for Product - {productOption.ProductId}");
            }

            var option = _mapper.Map<ProductOption>(productOption);
            option.Id = Guid.NewGuid();

            await _productRepository.AddOptionAsync(option);

            return new CreateProductOptionResponse
            {
                ProductOptionId = option.Id
            };
        }

        public async Task DeleteProduct(Guid id)
        {
            var product = await GetProduct(id);

            await _productRepository.DeleteAsync(product);
        }

        public async Task DeleteProductOption(Guid productId, Guid optionId)
        {
            Product product = await GetProduct(productId);

            ProductOption productOption = GetProductOption(product, optionId);

            await _productRepository.DeleteOptionAsync(productOption);
        }

        public async Task<ProductDto> GetProductById(Guid id)
        {
            var product = await GetProduct(id);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductOptionDto> GetProductOptionById(Guid productId, Guid optionId)
        {
            Product product = await GetProduct(productId);

            ProductOption productOption = GetProductOption(product, optionId);

            return _mapper.Map<ProductOptionDto>(productOption);
        }

        public async Task<List<ProductOptionDto>> GetProductOptions(Guid productId)
        {
            Product product = await GetProduct(productId);

            if (!product.ProductOptions.Any())
            {
                throw new ProductOptionNotFoundException($"No options found for Product - {productId}");
            }

            return _mapper.Map<List<ProductOptionDto>>(product.ProductOptions);
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
            var existingProduct = await GetProduct(product.Id.Value);

            var existingProductByCode = await _productRepository.FirstOrDefaultAsync(x => x.Code == product.Code);

            if (existingProductByCode != null && existingProduct.Id != existingProductByCode.Id)
            {
                throw new DuplicateProductException($"This Product cannot be updated as a different Product with same Code - {product.Code} already exists.");
            }

            existingProduct.Code = product.Code;
            existingProduct.DeliveryPrice = product.DeliveryPrice;
            existingProduct.Description = product.Description;
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;

            await _productRepository.SaveChangesAsync();
        }

        public async Task UpdateProductOption(ProductOptionDto productOption)
        {
            Product product = await GetProduct(productOption.ProductId);

            ProductOption existingOption = GetProductOption(product, productOption.Id.Value);

            if (product.ProductOptions.Any(x => x.Id != existingOption.Id && x.Name == productOption.Name))
            {
                throw new DuplicateProductOptionException($"ProductOption cannot be updated as an Option with the same name already exists for Product - {productOption.ProductId}");
            }

            existingOption.Name = productOption.Name;
            existingOption.Description = productOption.Description;

            await _productRepository.SaveChangesAsync();
        }

        private async Task<Product> GetProduct(Guid id)
        {
            var product = await _productRepository.FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
            {
                throw new ProductNotFoundException($"Product with Id - {id} not found");
            }

            return product;
        }

        private ProductOption GetProductOption(Product product, Guid optionId)
        {
            ProductOption productOption = product.ProductOptions.FirstOrDefault(x => x.Id == optionId);

            if (productOption == null)
            {
                throw new ProductOptionNotFoundException($"ProductOption with Id - {optionId} not found for Product - {product.Id}");
            }

            return productOption;
        }
    }
}
