using Products.Api.Data;
using Products.Api.IntegrationTests.Common;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Products.Api.IntegrationTests.Controllers
{
    public class When_Deleting_A_Product_Option : IClassFixture<IntegrationTestWebApplicationFactory<Startup>>
    {
        private readonly IntegrationTestWebApplicationFactory<Startup> _factory;

        public When_Deleting_A_Product_Option(IntegrationTestWebApplicationFactory<Startup> factory) => _factory = factory;

        [Fact]
        public async Task And_Product_Does_Not_Exist()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var responseMessage = await client.DeleteAsync($"/api/products/{Guid.NewGuid()}/options/{Guid.NewGuid()}");

            // Assert
            Assert.True(responseMessage.StatusCode == HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task And_Product_Option_Does_Not_Exist()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var responseMessage = await client.DeleteAsync($"/api/products/{SharedContext.DefaultProductId}/options/{Guid.NewGuid()}");

            // Assert
            Assert.True(responseMessage.StatusCode == HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task And_Product_Option_Exist()
        {
            // Arrange
            var client = _factory.CreateClient();
            var productOption = ProductOptionBuilder.BuildProductOption();
            productOption.ProductId = SharedContext.DefaultProductId;

            ProductDbContext context = _factory.GetContext();
            context.ProductOptions.Add(productOption);
            context.SaveChanges();

            // Act
            var responseMessage = await client.DeleteAsync($"/api/products/{productOption.ProductId}/options/{productOption.Id}");

            responseMessage.EnsureSuccessStatusCode();

            var deletedProductOption = context.ProductOptions.FirstOrDefault(x => x.Id == productOption.Id);

            // Assert
            Assert.Null(deletedProductOption);
        }
    }
}
