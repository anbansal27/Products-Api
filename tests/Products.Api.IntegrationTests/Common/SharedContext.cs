using Products.Api.Entities;
using System;

namespace Products.Api.IntegrationTests.Common
{
    public static class SharedContext
    {
        public static Guid ProductIdToGet = Guid.NewGuid();

        public static Guid ProductIdToUpdate = Guid.NewGuid();

        public static string Code = RandomBuilder.NextString();

        public static string Name = RandomBuilder.NextString();

        public static string Description = RandomBuilder.NextString();

        public static decimal DeliveryPrice = RandomBuilder.NextDecimal();

        public static decimal Price = RandomBuilder.NextDecimal();

        public static Product ProductToGet()
            => new Product
            {
                Id = ProductIdToGet,
                Code = Code,
                Name = Name,
                Description = Description,
                Price = Price,
                DeliveryPrice = DeliveryPrice
            };

        public static Product ProductToUpdate()
            => new Product
            {
                Id = ProductIdToUpdate,
                Code = RandomBuilder.NextString(),
                Name = RandomBuilder.NextString(),
                Description = RandomBuilder.NextString(),
                Price = RandomBuilder.NextDecimal(),
                DeliveryPrice = RandomBuilder.NextDecimal()
            };
    }
}
