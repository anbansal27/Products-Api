﻿using FluentAssertions;
using Products.Api.Contracts;
using Products.Api.IntegrationTests.Common;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using static Products.Api.IntegrationTests.Common.Utilities;

namespace Products.Api.IntegrationTests.Controllers
{
    public class When_Retrieving_A_Product_By_Name : IClassFixture<IntegrationTestWebApplicationFactory<Startup>>
    {
        private readonly IntegrationTestWebApplicationFactory<Startup> _factory;

        public When_Retrieving_A_Product_By_Name(IntegrationTestWebApplicationFactory<Startup> factory) => _factory = factory;

        [Fact]
        public async Task And_Matching_Product_Does_Not_Exist()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var actualResponse = await client.GetAsync($"/api/products?name={RandomBuilder.NextString()}");

            // Assert
            Assert.True(actualResponse?.StatusCode == HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task And_Matching_Product_Exist()
        {
            // Arrange
            var client = _factory.CreateClient();
            var product = SharedContext.ProductToGet();

            var expectedProducts = new List<ProductDto>
            {
                new ProductDto
                {
                    Id = product.Id,
                    Code = product.Code,
                    Name = product.Name,
                    Description = product.Description,
                    DeliveryPrice = product.DeliveryPrice,
                    Price = product.Price
                }
            };

            // Act
            var actualResponse = await client.GetAsync($"/api/products?name={SharedContext.Name}");
            List<ProductDto> actualProducts = await GetResponseContent<List<ProductDto>>(actualResponse);

            // Assert
            Assert.True(actualResponse.IsSuccessStatusCode);
            actualProducts.Should().BeEquivalentTo(expectedProducts);
        }
    }
}
