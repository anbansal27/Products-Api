﻿using Products.Api.Contracts;
using Products.Api.IntegrationTests.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using static Products.Api.IntegrationTests.Common.Utilities;

namespace Products.Api.IntegrationTests.Controllers
{
    public class When_Retrieving_All_Products : IClassFixture<IntegrationTestWebApplicationFactory<Startup>>
    {
        private readonly IntegrationTestWebApplicationFactory<Startup> _factory;

        public When_Retrieving_All_Products(IntegrationTestWebApplicationFactory<Startup> factory) => _factory = factory;

        [Fact]
        public async Task And_Products_Exist()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var actualResponse = await client.GetAsync($"/api/products");

            List<ProductDto> products = await GetResponseContent<List<ProductDto>>(actualResponse);

            // Assert
            Assert.True(actualResponse.IsSuccessStatusCode);
            Assert.True(products.Count >= 2);
            Assert.True(products.Count(x => x.Id == SharedContext.ProductIdToGet) == 1);
            Assert.True(products.Count(x => x.Id == SharedContext.ProductIdToUpdate) == 1);
        }
    }
}
