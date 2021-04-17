using Products.Api.Application.Response;
using Products.Api.Contracts;
using Products.Api.IntegrationTests.Theories.Data;
using System.Net;
using Xunit;

namespace Products.Api.IntegrationTests.Theories
{
    public class CreateProductTheories : TheoryData<string, CreateProductTheoryData>
    {
        public CreateProductTheories()
        {           
            DuplicateProductTheory();
            CreateProductTheory();
        }

        private void DuplicateProductTheory()
        {
            var theoryData = new CreateProductTheoryData
            {
                Product = new ProductDto
                {
                    Code = "code",
                    Name = "name",
                    Description = "description",
                    Price = 100,
                    DeliveryPrice = 0
                },
                ExpectedStatusCode = HttpStatusCode.Conflict,
                ExpectedResponse = null
            };

            Add($"{nameof(DuplicateProductTheory)}", theoryData);
        }

        private void CreateProductTheory()
        {
            var theoryData = new CreateProductTheoryData
            {
                Product = new ProductDto
                {
                    Code = "newCode",
                    Name = "newName",
                    Description = "newDescription",
                    Price = 100,
                    DeliveryPrice = 0
                },
                ExpectedStatusCode = HttpStatusCode.Created
            };

            theoryData.ExpectedResponse = new CreateProductResponse { ProductId = theoryData.Product.Id };

            Add($"{nameof(CreateProductTheory)}", theoryData);
        }
    }
}
