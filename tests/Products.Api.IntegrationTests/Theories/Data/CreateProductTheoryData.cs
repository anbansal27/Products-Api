using Products.Api.Contracts;
using System.Net;

namespace Products.Api.IntegrationTests.Theories.Data
{
    public class CreateProductTheoryData
    {
        public ProductDto Product { get; set; }

        public HttpStatusCode ExpectedStatusCode { get; set; }        
    }
}
