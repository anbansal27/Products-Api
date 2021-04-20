using Products.Api.Application.Dto;
using Products.Api.Entities;
using System;

namespace Products.Api.IntegrationTests.Common
{
    public class ProductOptionBuilder
    {
        public static ProductOptionDto BuildProductOptionDto()
            => new ProductOptionDto
            {
                Name = RandomBuilder.NextString(),
                Description = RandomBuilder.NextString(),
                ProductId = Guid.NewGuid()
            };

        public static ProductOption BuildProductOption()
            => new ProductOption
            {
                Id = Guid.NewGuid(),
                Name = RandomBuilder.NextString(),
                Description = RandomBuilder.NextString(),
                ProductId = Guid.NewGuid()
            };
    }
}
