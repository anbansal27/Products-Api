using FluentAssertions;
using Products.Api.Entities;
using Products.Api.IntegrationTests.Common;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using static Products.Api.IntegrationTests.Common.Utilities;

namespace Products.Api.IntegrationTests.Controllers
{
    public class When_Updating_A_Product : IClassFixture<IntegrationTestWebApplicationFactory<Startup>>
    {
        private readonly IntegrationTestWebApplicationFactory<Startup> _factory;

        public When_Updating_A_Product(IntegrationTestWebApplicationFactory<Startup> factory) => _factory = factory;

        [Fact]
        public async Task And_Product_Does_Not_Exist()
        {
            // Arrange
            var client = _factory.CreateClient();
            var product = ProductBuilder.BuildProductDto();
            var requestContent = GetRequestContent(product);

            // Act
            var actualResponse = await client.PutAsync($"/api/products/{Guid.NewGuid()}", requestContent);

            // Assert
            Assert.True(actualResponse?.StatusCode == HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task And_Another_Product_With_Same_Code_Already_Exist()
        {
            // Arrange
            var client = _factory.CreateClient();
            var product = ProductBuilder.BuildProductDto();

            // set Code for duplicate product
            product.Code = SharedContext.Code;

            var requestContent = GetRequestContent(product);

            // Act
            var actualResponse = await client.PutAsync($"/api/products/{SharedContext.ProductIdToUpdate}", requestContent);

            // Assert
            Assert.True(actualResponse.StatusCode == HttpStatusCode.Conflict);
        }

        [Fact]
        public async Task And_Product_Exist()
        {
            // Arrange
            var client = _factory.CreateClient();
            var product = ProductBuilder.BuildProductDto();
            var requestContent = GetRequestContent(product);
            var expectedProduct = new Product
            {
                Id = SharedContext.ProductIdToUpdate,
                Code = product.Code,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                DeliveryPrice = product.DeliveryPrice
            };

            // Act
            var actualResponse = await client.PutAsync($"/api/products/{SharedContext.ProductIdToUpdate}", requestContent);
            actualResponse.EnsureSuccessStatusCode();
            var context = _factory.GetContext();
            Product updatedProduct = context.Products.FirstOrDefault(x => x.Id == SharedContext.ProductIdToUpdate);

            // Assert
            Assert.True(actualResponse.StatusCode == HttpStatusCode.OK);
            updatedProduct.Should().BeEquivalentTo(expectedProduct, options => options.Excluding(x => x.ProductOptions));
        }
    }
}
