using System;
using System.Text.Json.Serialization;

namespace Products.Api.Contracts
{
    public class ProductOptionDto
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Guid? Id { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public Guid ProductId { get; set; }
    }
}
