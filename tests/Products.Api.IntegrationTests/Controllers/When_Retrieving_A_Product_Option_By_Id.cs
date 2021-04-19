using FluentAssertions;
using Products.Api.Application.Contracts;
using Products.Api.IntegrationTests.Common;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using static Products.Api.IntegrationTests.Common.Utilities;

namespace Products.Api.IntegrationTests.Controllers
{
    public class When_Retrieving_A_Product_Option_By_Id : IClassFixture<IntegrationTestWebApplicationFactory<Startup>>
    {
        private readonly IntegrationTestWebApplicationFactory<Startup> _factory;

        public When_Retrieving_A_Product_Option_By_Id(IntegrationTestWebApplicationFactory<Startup> factory) => _factory = factory;

        [Fact]
        public async Task And_Product_Does_Not_Exist()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var responseMessage = await client.GetAsync($"/api/products/{Guid.NewGuid()}/options/{Guid.NewGuid()}");

            // Assert
            Assert.True(responseMessage.StatusCode == HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task And_Product_Option_Does_Not_Exist()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var responseMessage = await client.GetAsync($"/api/products/{SharedContext.DefaultProductId}/options/{Guid.NewGuid()}");

            // Assert
            Assert.True(responseMessage.StatusCode == HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task And_Product_Option_Exists()
        {
            // Arrange
            var client = _factory.CreateClient();
            var productOption = SharedContext.BuildDefaultProductOption();
            var expectedProductOption = new ProductOptionDto
            {
                Id = productOption.Id,
                Name = productOption.Name,
                Description = productOption.Description,
                ProductId = productOption.ProductId
            };

            // Act
            var responseMessage = await client.GetAsync($"/api/products/{productOption.ProductId}/options/{productOption.Id}");

            responseMessage.EnsureSuccessStatusCode();

            ProductOptionDto actualProductOption = await GetResponseContent<ProductOptionDto>(responseMessage);

            // Assert            
            actualProductOption.Should().BeEquivalentTo(expectedProductOption);
        }
    }
}
