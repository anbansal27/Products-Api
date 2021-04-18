using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Products.Api.Data;
using System;
using System.Linq;

namespace Products.Api.IntegrationTests.Common
{
    public class IntegrationTestWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder
                .ConfigureServices(services =>
                {
                    // Remove the app's PlanCalculatorDbContext registration.
                    var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ProductDbContext>));

                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }

                    // Create a new service provider.
                    var serviceProvider = new ServiceCollection()
                        .AddEntityFrameworkInMemoryDatabase()
                        .BuildServiceProvider();

                    services.AddDbContext<ProductDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("Product");
                        options.UseInternalServiceProvider(serviceProvider);
                    });

                    var sp = services.BuildServiceProvider();

                    using var scope = sp.CreateScope();
                    var scopedServices = scope.ServiceProvider;

                    var context = scopedServices.GetRequiredService<ProductDbContext>();

                    context.Database.EnsureCreated();

                    context.Products.Add(SharedContext.ProductToGet());
                    context.Products.Add(SharedContext.ProductToUpdate());
                    context.SaveChanges();
                });
        }

        public ProductDbContext GetContext()
        {
            var scope = Services.CreateScope();
            var scopedServices = scope.ServiceProvider;

            var context = scopedServices.GetRequiredService<ProductDbContext>();
            return context;
        }
    }
}
