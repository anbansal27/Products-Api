using FluentAssertions;
using Products.Api.Application.Dto;
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
            var responseMessage = await client.GetAsync($"/api/products/{Guid.NewGuid()}");

            // Assert
            Assert.True(responseMessage.StatusCode == HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task And_Product_Exists()
        {
            // Arrange
            var client = _factory.CreateClient();
            var product = SharedContext.BuildDefaultProduct();
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
            var responseMessage = await client.GetAsync($"/api/products/{SharedContext.DefaultProductId}");

            responseMessage.EnsureSuccessStatusCode();

            ProductDto actualProduct = await GetResponseContent<ProductDto>(responseMessage);

            // Assert            
            actualProduct.Should().BeEquivalentTo(expectedProduct);
        }
    }
}
