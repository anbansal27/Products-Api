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
            var actualResponse = await client.DeleteAsync($"/api/products/{Guid.NewGuid()}");

            // Assert
            Assert.True(actualResponse?.StatusCode == HttpStatusCode.BadRequest);
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
            var actualResponse = await client.DeleteAsync($"/api/products/{product.Id}");
            
            actualResponse.EnsureSuccessStatusCode();

            var deletedProduct = context.Products.FirstOrDefault(x => x.Id == product.Id);

            // Assert
            Assert.True(actualResponse.IsSuccessStatusCode);
            Assert.Null(deletedProduct);
        }
    }
}
