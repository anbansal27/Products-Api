using Products.Api.Entities;
using System;

namespace Products.Api.IntegrationTests.Common
{
    public static class SharedContext
    {
        public static Guid DefaultProductId = Guid.NewGuid();

        public static Guid ProductIdToUpdate = Guid.NewGuid();

        public static Guid ProductOptionId = Guid.NewGuid();

        public static Guid ProductOptionIdToUpdate = Guid.NewGuid();

        public static string Code = RandomBuilder.NextString();

        public static string Name = RandomBuilder.NextString();

        public static string Description = RandomBuilder.NextString();

        public static decimal DeliveryPrice = RandomBuilder.NextDecimal();

        public static decimal Price = RandomBuilder.NextDecimal();

        public static Product BuildDefaultProduct()
            => new Product
            {
                Id = DefaultProductId,
                Code = Code,
                Name = Name,
                Description = Description,
                Price = Price,
                DeliveryPrice = DeliveryPrice
            };

        public static Product BuildProductToUpdate()
        {
            var product = ProductBuilder.BuildProduct();
            product.Id = ProductIdToUpdate;
            return product;
        }

        public static ProductOption BuildDefaultProductOption()
            => new ProductOption
            {
                Id = ProductOptionId,
                Name = Name,
                Description = Description,
                ProductId = DefaultProductId
            };

        public static ProductOption BuildProductOptionToUpdate()
        {
            var productOption = ProductOptionBuilder.BuildProductOption();
            productOption.Id = ProductOptionIdToUpdate;
            productOption.ProductId = DefaultProductId;
            return productOption;
        }
    }
}
