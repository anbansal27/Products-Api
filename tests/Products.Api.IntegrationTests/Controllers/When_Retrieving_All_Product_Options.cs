using FluentAssertions;
using Products.Api.Contracts;
using Products.Api.IntegrationTests.Common;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using static Products.Api.IntegrationTests.Common.Utilities;

namespace Products.Api.IntegrationTests.Controllers
{
    public class When_Retrieving_All_Product_Options : IClassFixture<IntegrationTestWebApplicationFactory<Startup>>
    {
        private readonly IntegrationTestWebApplicationFactory<Startup> _factory;

        public When_Retrieving_All_Product_Options(IntegrationTestWebApplicationFactory<Startup> factory) => _factory = factory;

        [Fact]
        public async Task And_Product_Does_Not_Exist()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var responseMessage = await client.GetAsync($"/api/products/{Guid.NewGuid()}/options");

            // Assert
            Assert.True(responseMessage.StatusCode == HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task And_Product_Options_Does_Not_Exist()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var responseMessage = await client.GetAsync($"/api/products/{SharedContext.ProductIdToUpdate}/options");

            // Assert
            Assert.True(responseMessage.StatusCode == HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task And_Product_Options_Exist()
        {
            // Arrange
            var client = _factory.CreateClient();
            var productOption = SharedContext.BuildDefaultProductOption();

            var expectedOption = new ProductOptionDto
            {
                Id = productOption.Id,
                Name = productOption.Name,
                Description = productOption.Description,
                ProductId = productOption.ProductId
            };

            // Act
            var responseMessage = await client.GetAsync($"/api/products/{productOption.ProductId}/options");

            responseMessage.EnsureSuccessStatusCode();

            var actualProductOptions = await GetResponseContent<List<ProductOptionDto>>(responseMessage);

            // Assert
            actualProductOptions.Should().ContainEquivalentOf(expectedOption);
        }
    }
}
