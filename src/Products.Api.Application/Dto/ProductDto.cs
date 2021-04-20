using System;
using System.Text.Json.Serialization;

namespace Products.Api.Application.Dto
{
    public class ProductDto
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Guid? Id { get; set; }

        public string Code { get; set; }

        public decimal? DeliveryPrice { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
