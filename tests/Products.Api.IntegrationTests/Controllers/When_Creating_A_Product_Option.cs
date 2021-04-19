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
    public class When_Creating_A_Product_Option : IClassFixture<IntegrationTestWebApplicationFactory<Startup>>
    {
        private readonly IntegrationTestWebApplicationFactory<Startup> _factory;

        public When_Creating_A_Product_Option(IntegrationTestWebApplicationFactory<Startup> factory) => _factory = factory;

        [Fact]
        public async Task And_Product_Id_Is_Invalid()
        {
            // Arrange
            var client = _factory.CreateClient();
            var productOption = ProductOptionBuilder.BuildProductOptionDto();
            
            var requestContent = GetRequestContent(productOption);

            // Act
            var responseMessage = await client.PostAsync($"/api/products/{Guid.NewGuid()}/options", requestContent);

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
            var responseMessage = await client.PostAsync($"/api/products/{productOption.ProductId}/options", requestContent);                        

            // Assert
            Assert.True(responseMessage.StatusCode == HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task And_A_Product_Option_With_Same_Name_Already_Exist()
        {
            // Arrange
            var client = _factory.CreateClient();
            var productOption = ProductOptionBuilder.BuildProductOptionDto();
            productOption.ProductId = SharedContext.DefaultProductId;

            // set name for duplicate option
            productOption.Name = SharedContext.Name;
            var requestContent = GetRequestContent(productOption);

            // Act
            var responseMessage = await client.PostAsync($"/api/products/{productOption.ProductId}/options", requestContent);

            // Assert
            Assert.True(responseMessage.StatusCode == HttpStatusCode.Conflict);
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
            var responseMessage = await client.PostAsync($"/api/products/{productOption.ProductId}/options", requestContent);
            responseMessage.EnsureSuccessStatusCode();

            CreateProductOptionResponse actualResponse = await GetResponseContent<CreateProductOptionResponse>(responseMessage);

            // Assert
            Assert.True(responseMessage.StatusCode == HttpStatusCode.Created);
            actualResponse.ProductOptionId.Should().NotBe(Guid.Empty);
            actualResponse.ProductOptionId.Should().NotBe(default);
        }
    }
}
