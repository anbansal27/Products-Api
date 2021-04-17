using System;

namespace Products.Api.Contracts
{
    public class ProductDto
    {
        public ProductDto()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public string Code { get; set; }

        public decimal? DeliveryPrice { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
