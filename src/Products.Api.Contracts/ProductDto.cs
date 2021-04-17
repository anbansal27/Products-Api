namespace Products.Api.Contracts
{
    public class ProductDto
    {
        public string Code { get; set; }

        public decimal? DeliveryPrice { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
