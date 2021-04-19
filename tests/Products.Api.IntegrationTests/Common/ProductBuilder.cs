using Products.Api.Application.Contracts;
using Products.Api.Entities;
using System;

namespace Products.Api.IntegrationTests.Common
{
    public static class ProductBuilder
    {
        public static ProductDto BuildProductDto()
            => new ProductDto
            {
                Code = RandomBuilder.NextString(),
                Name = RandomBuilder.NextString(),
                Description = RandomBuilder.NextString(),
                Price = RandomBuilder.NextDecimal(),
                DeliveryPrice = RandomBuilder.NextDecimal()
            };

        public static Product BuildProduct()
            => new Product
            {
                Id = Guid.NewGuid(),
                Code = RandomBuilder.NextString(),
                Name = RandomBuilder.NextString(),
                Description = RandomBuilder.NextString(),
                Price = RandomBuilder.NextDecimal(),
                DeliveryPrice = RandomBuilder.NextDecimal()
            };
    }
}
