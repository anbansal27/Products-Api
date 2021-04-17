using Microsoft.EntityFrameworkCore;
using Products.Api.Data.EntityBuilder;
using Products.Api.Entities;

namespace Products.Api.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductOption> ProductOptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder?.Entity<Product>().Build();

            modelBuilder?.Entity<ProductOption>().Build();
        }
    }
}
