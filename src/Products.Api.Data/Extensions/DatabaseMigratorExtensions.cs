using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Products.Api.Data.Extensions
{
    public static class DatabaseMigratorExtensions
    {
        public static void UseMigrateDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<ProductDbContext>();
            context.Database.EnsureCreated();
        }
    }
}