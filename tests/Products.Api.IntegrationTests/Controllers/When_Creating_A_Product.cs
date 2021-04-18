using FluentAssertions;
using Products.Api.Application.Response;
using Products.Api.IntegrationTests.Common;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using static Products.Api.IntegrationTests.Common.Utilities;

namespace Products.Api.IntegrationTests.Controllers
{
    public class When_Creating_A_Product : IClassFixture<IntegrationTestWebApplicationFactory<Startup>>
    {
        private readonly IntegrationTestWebApplicationFactory<Startup> _factory;

        public When_Creating_A_Product(IntegrationTestWebApplicationFactory<Startup> factory) => _factory = factory;

        [Fact]
        public async Task And_Product_Already_Exist()
        {
            // Arrange
            var client = _factory.CreateClient();
            var product = ProductBuilder.BuildProductDto();
            
            // set Code for duplicate product
            product.Code = SharedContext.Code;

            var requestContent = GetRequestContent(product);

            // Act
            var responseMessage = await client.PostAsync("/api/products", requestContent);

            // Assert
            Assert.True(responseMessage.StatusCode == HttpStatusCode.Conflict);
        }

        [Fact]
        public async Task And_Product_Does_Not_Exist()
        {
            // Arrange
            var client = _factory.CreateClient();
            var product = ProductBuilder.BuildProductDto();
            var requestContent = GetRequestContent(product);

            // Act
            var responseMessage = await client.PostAsync("/api/products", requestContent);
            responseMessage.EnsureSuccessStatusCode();

            CreateProductResponse actualResponse = await GetResponseContent<CreateProductResponse>(responseMessage);

            // Assert
            Assert.True(responseMessage.StatusCode == HttpStatusCode.Created);
            actualResponse.ProductId.Should().NotBe(Guid.Empty);
            actualResponse.ProductId.Should().NotBe(default);
        }
    }
}
