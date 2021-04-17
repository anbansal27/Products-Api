using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Products.Api.Data
{
    public class EfMigrationsDesignTimeDbContextFactory : IDesignTimeDbContextFactory<ProductDbContext>
    {
        public ProductDbContext CreateDbContext(string[] args)
        {
            var connectionString = "Data Source=Product.db;";

            var optionsBuilder = new DbContextOptionsBuilder<ProductDbContext>();
            optionsBuilder.UseSqlite(connectionString);
            return new ProductDbContext(optionsBuilder.Options);
        }
    }
}