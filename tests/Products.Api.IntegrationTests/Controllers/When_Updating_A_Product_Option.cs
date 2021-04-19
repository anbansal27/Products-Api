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
    public class When_Updating_A_Product_Option : IClassFixture<IntegrationTestWebApplicationFactory<Startup>>
    {
        private readonly IntegrationTestWebApplicationFactory<Startup> _factory;

        public When_Updating_A_Product_Option(IntegrationTestWebApplicationFactory<Startup> factory) => _factory = factory;

        [Fact]
        public async Task And_Product_Id_Is_Invalid()
        {
            // Arrange
            var client = _factory.CreateClient();
            var productOption = ProductOptionBuilder.BuildProductOptionDto();

            var requestContent = GetRequestContent(productOption);

            // Act
            var responseMessage = await client.PutAsync($"/api/products/{Guid.NewGuid()}/options/{Guid.NewGuid()}", requestContent);

            // Assert
            Assert.True(responseMessage.StatusCode == HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task And_Product_Does_Not_Exist()
        {
            // Arrange
            var client = _factory.CreateClient();
            var productOption = ProductOptionBuilder.BuildProductOptionDto();
            var requestContent = GetRequestContent(productOption);

            // Act
            var responseMessage = await client.PutAsync($"/api/products/{productOption.ProductId}/options/{Guid.NewGuid()}", requestContent);

            // Assert
            Assert.True(responseMessage.StatusCode == HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task And_Product_Option_Does_Not_Exist()
        {
            // Arrange
            var client = _factory.CreateClient();
            var productOption = ProductOptionBuilder.BuildProductOptionDto();
            productOption.ProductId = SharedContext.DefaultProductId;
            var requestContent = GetRequestContent(productOption);

            // Act
            var responseMessage = await client.PutAsync($"/api/products/{productOption.ProductId}/options/{Guid.NewGuid()}", requestContent);

            // Assert
            Assert.True(responseMessage.StatusCode == HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task And_Another_Product_Option_With_Same_Name_Already_Exist()
        {
            // Arrange
            var client = _factory.CreateClient();
            var productOption = ProductOptionBuilder.BuildProductOptionDto();
            productOption.ProductId = SharedContext.DefaultProductId;
            
            // set Name for duplicate product option
            productOption.Name = SharedContext.Name;

            var requestContent = GetRequestContent(productOption);

            // Act
            var responseMessage = await client.PutAsync($"/api/products/{productOption.ProductId}/options/{SharedContext.ProductOptionIdToUpdate}", requestContent);

            // Assert
            Assert.True(responseMessage.StatusCode == HttpStatusCode.Conflict);
        }

        [Fact]
        public async Task And_Product_Option_Exist()
        {
            // Arrange
            var client = _factory.CreateClient();
            var productOption = ProductOptionBuilder.BuildProductOptionDto();
            productOption.ProductId = SharedContext.DefaultProductId;
            
            var requestContent = GetRequestContent(productOption);
            var expectedProductOption = new ProductOption
            {
                Id = SharedContext.ProductOptionIdToUpdate,
                Name = productOption.Name,
                Description = productOption.Description,
                ProductId = productOption.ProductId
            };

            // Act
            var responseMessage = await client.PutAsync($"/api/products/{productOption.ProductId}/options/{SharedContext.ProductOptionIdToUpdate}", requestContent);
            responseMessage.EnsureSuccessStatusCode();
            var context = _factory.GetContext();
            ProductOption updatedProductOption = context.ProductOptions.FirstOrDefault(x => x.Id == SharedContext.ProductOptionIdToUpdate);

            // Assert
            Assert.True(responseMessage.StatusCode == HttpStatusCode.OK);
            updatedProductOption.Should().BeEquivalentTo(expectedProductOption, options => options.Excluding(x => x.Product));
        }
    }
}
