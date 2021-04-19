using Products.Api.Data;
using Products.Api.IntegrationTests.Common;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Products.Api.IntegrationTests.Controllers
{
    public class When_Deleting_A_Product : IClassFixture<IntegrationTestWebApplicationFactory<Startup>>
    {
        private readonly IntegrationTestWebApplicationFactory<Startup> _factory;

        public When_Deleting_A_Product(IntegrationTestWebApplicationFactory<Startup> factory) => _factory = factory;

        [Fact]
        public async Task And_Product_Does_Not_Exist()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var responseMessage = await client.DeleteAsync($"/api/products/{Guid.NewGuid()}");

            // Assert
            Assert.True(responseMessage.StatusCode == HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task And_Product_Exists()
        {
            // Arrange
            var client = _factory.CreateClient();
            var product = ProductBuilder.BuildProduct();
            
            ProductDbContext context = _factory.GetContext();
            context.Products.Add(product);
            context.SaveChanges();
            
            // Act
            var responseMessage = await client.DeleteAsync($"/api/products/{product.Id}");
            
            responseMessage.EnsureSuccessStatusCode();

            var deletedProduct = context.Products.FirstOrDefault(x => x.Id == product.Id);

            // Assert
            Assert.True(responseMessage.IsSuccessStatusCode);
            Assert.Null(deletedProduct);
        }
    }
}
