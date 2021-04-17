using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Products.Api.Configuration;
using Products.Api.Data;

namespace Products.Api.Extensions
{
    public static class DbContextServiceCollectionExtensions
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services)
        {
            services.AddDbContext<ProductDbContext>((provider, builder) =>
            {
                builder.UseSqlite(provider.GetRequiredService<IOptions<AppSettings>>().Value.ConnectionStrings.Product);
            });

            return services;
        }
    }
}
