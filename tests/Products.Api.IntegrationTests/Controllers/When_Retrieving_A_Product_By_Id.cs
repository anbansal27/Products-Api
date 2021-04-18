using FluentAssertions;
using Products.Api.Contracts;
using Products.Api.IntegrationTests.Common;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using static Products.Api.IntegrationTests.Common.Utilities;

namespace Products.Api.IntegrationTests.Controllers
{
    public class When_Retrieving_A_Product_By_Id : IClassFixture<IntegrationTestWebApplicationFactory<Startup>>
    {
        private readonly IntegrationTestWebApplicationFactory<Startup> _factory;

        public When_Retrieving_A_Product_By_Id(IntegrationTestWebApplicationFactory<Startup> factory) => _factory = factory;

        [Fact]
        public async Task And_Product_Does_Not_Exist()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var actualResponse = await client.GetAsync($"/api/products/{Guid.NewGuid()}");

            // Assert
            Assert.True(actualResponse?.StatusCode == HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task And_Product_Exists()
        {
            // Arrange
            var client = _factory.CreateClient();
            var product = SharedContext.ProductToGet();
            var expectedProduct = new ProductDto
            {
                Id = product.Id,
                Code = product.Code,
                Name = product.Name,
                Description = product.Description,
                DeliveryPrice = product.DeliveryPrice,
                Price = product.Price
            };

            // Act
            var actualResponse = await client.GetAsync($"/api/products/{SharedContext.ProductIdToGet}");
            ProductDto actualProduct = await GetResponseContent<ProductDto>(actualResponse);

            // Assert
            Assert.True(actualResponse.IsSuccessStatusCode);
            actualProduct.Should().BeEquivalentTo(expectedProduct);
        }
    }
}
