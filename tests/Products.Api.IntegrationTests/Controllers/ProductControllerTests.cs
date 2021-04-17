using FluentAssertions;
using Products.Api.Application.Response;
using Products.Api.IntegrationTests.Common;
using Products.Api.IntegrationTests.Theories;
using Products.Api.IntegrationTests.Theories.Data;
using System.Threading.Tasks;
using Xunit;
using static Products.Api.IntegrationTests.Common.Utilities;

namespace Products.Api.IntegrationTests.Controllers
{
    public class ProductControllerTests : IClassFixture<IntegrationTestWebApplicationFactory<Startup>>
    {
        private readonly IntegrationTestWebApplicationFactory<Startup> _factory;

        public ProductControllerTests(IntegrationTestWebApplicationFactory<Startup> factory) => _factory = factory;

        [Theory]
        [ClassData(typeof(CreateProductTheories))]
        public async Task When_Creating_Product(string description, CreateProductTheoryData theoryData)
        {
            // Arrange
            var client = _factory.CreateClient();
            CreateProductResponse actualResponse = null;

            // Act
            var requestContent = GetRequestContent(theoryData.Product);
            var responseMessage = await client.PostAsync("/api/products", requestContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                actualResponse = await GetResponseContent<CreateProductResponse>(responseMessage);
            }

            // Assert
            Assert.Equal(theoryData.ExpectedStatusCode, responseMessage?.StatusCode);
            actualResponse.Should().BeEquivalentTo(theoryData.ExpectedResponse); 
        }
    }
}
